using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Models.Entities;
using BC = BCrypt.Net.BCrypt;

namespace TaskManagement.API.Data
{
    public static class SeedData
    {
        public static async System.Threading.Tasks.Task InitializeAsync(ApplicationDbContext context)
        {
            // Seed Roles
            if (!await context.Roles.AnyAsync())
            {
                context.Roles.AddRange(
                    new Role { RoleName = "Admin" },
                    new Role { RoleName = "Manager" },
                    new Role { RoleName = "Employee" }
                );
                await context.SaveChangesAsync();
            }

            // Seed Task Statuses
            if (!await context.TaskStatuses.AnyAsync())
            {
                context.TaskStatuses.AddRange(
                    new Models.Entities.TaskStatus { StatusName = "To Do" },
                    new Models.Entities.TaskStatus { StatusName = "In Progress" },
                    new Models.Entities.TaskStatus { StatusName = "Completed" },
                    new Models.Entities.TaskStatus { StatusName = "On Hold" }
                );
                await context.SaveChangesAsync();
            }

            // Seed Users
            if (!await context.Users.AnyAsync())
            {
                var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin");
                var managerRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Manager");
                var employeeRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Employee");

                if (adminRole == null || managerRole == null || employeeRole == null)
                {
                    throw new Exception("Roles not found in database. Seed Roles first.");
                }

                var fixedDate = new DateTime(2026, 1, 1);
                var users = new List<User>();

                // Admins (2)
                users.Add(new User { FullName = "Admin One", Email = "admin1@task.com", PasswordHash = BC.HashPassword("Admin@123"), RoleId = adminRole.RoleId, CreatedAt = fixedDate });
                users.Add(new User { FullName = "Admin Two", Email = "admin2@task.com", PasswordHash = BC.HashPassword("Admin@123"), RoleId = adminRole.RoleId, CreatedAt = fixedDate });

                // Managers (12)
                for (int i = 1; i <= 12; i++)
                {
                    users.Add(new User { FullName = $"Manager {i}", Email = $"manager{i}@task.com", PasswordHash = BC.HashPassword("Manager@123"), RoleId = managerRole.RoleId, CreatedAt = fixedDate });
                }

                // Employees (30)
                for (int i = 1; i <= 30; i++)
                {
                    users.Add(new User { FullName = $"Employee {i}", Email = $"employee{i}@task.com", PasswordHash = BC.HashPassword("Employee@123"), RoleId = employeeRole.RoleId, CreatedAt = fixedDate });
                }

                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }

            // Seed Projects
            if (!await context.Projects.AnyAsync())
            {
                var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin");
                var managerRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Manager");
                var employeeRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Employee");

                var admin = await context.Users.FirstAsync(u => u.RoleId == adminRole!.RoleId);
                var manager = await context.Users.FirstAsync(u => u.RoleId == managerRole!.RoleId);
                
                var projects = new List<Project>
                {
                    new Project 
                    { 
                        Name = "Summer Beverage Launch", 
                        Description = "Launching new seasonal drinks including Mango Dragonfruit and Matcha Frappuccino.",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(3),
                        CreatedBy = manager.UserId
                    },
                    new Project
                    {
                        Name = "Store Expansion 2026",
                        Description = "Opening 50 new sustainable locations across Southeast Asia.",
                        StartDate = DateTime.Now.AddMonths(-1),
                        EndDate = DateTime.Now.AddYears(1),
                        CreatedBy = admin.UserId
                    }
                };

                context.Projects.AddRange(projects);
                await context.SaveChangesAsync();

                // Seed Project Members
                var employees = await context.Users.Where(u => u.RoleId == employeeRole!.RoleId).Take(10).ToListAsync();
                foreach (var project in projects)
                {
                    // Add creator as member
                    context.ProjectMembers.Add(new ProjectMember { ProjectId = project.ProjectId, UserId = project.CreatedBy });
                    
                    // Add some employees
                    foreach (var emp in employees.Take(5))
                    {
                        context.ProjectMembers.Add(new ProjectMember { ProjectId = project.ProjectId, UserId = emp.UserId });
                    }
                }
                await context.SaveChangesAsync();

                // Seed Tasks
                foreach (var project in projects)
                {
                    var projectMembers = await context.ProjectMembers.Where(pm => pm.ProjectId == project.ProjectId).ToListAsync();
                    
                    context.Tasks.AddRange(
                        new Models.Entities.Task 
                        { 
                            Title = "Finalize Recipe", 
                            Description = "Ensure ingredient ratios are perfect for production.",
                            ProjectId = project.ProjectId,
                            AssignedTo = projectMembers.Skip(1).First().UserId,
                            StatusId = 3, // Completed
                            Priority = 3,
                            Deadline = DateTime.Now.AddDays(-5)
                        },
                        new Models.Entities.Task
                        {
                            Title = "Design Marketing Banners",
                            Description = "Create high-res visuals for social media and in-store displays.",
                            ProjectId = project.ProjectId,
                            AssignedTo = projectMembers.Skip(2).First().UserId,
                            StatusId = 2, // In Progress
                            Priority = 2,
                            Deadline = DateTime.Now.AddDays(10)
                        },
                        new Models.Entities.Task
                        {
                            Title = "Staff Training",
                            Description = "Train baristas on the new brewing process.",
                            ProjectId = project.ProjectId,
                            AssignedTo = projectMembers.Skip(3).First().UserId,
                            StatusId = 1, // To Do
                            Priority = 1,
                            Deadline = DateTime.Now.AddDays(15)
                        }
                    );
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
