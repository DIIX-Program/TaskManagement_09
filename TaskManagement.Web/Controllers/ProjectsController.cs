using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectApiService _projectService;
        private readonly UserApiService _userService;

        public ProjectsController(ProjectApiService projectService, UserApiService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "All Projects";
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var role = HttpContext.Session.GetString("Role");
            
            var response = await _projectService.GetAllAsync(userId, role);
            return View(response.Data ?? new List<ProjectViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJson()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var role = HttpContext.Session.GetString("Role");
            var response = await _projectService.GetAllAsync(userId, role);
            return Json(response.Data ?? new List<ProjectViewModel>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _projectService.GetByIdAsync(id);
            if (response.Success && response.Data != null)
            {
                var project = response.Data;
                var membersResponse = await _projectService.GetMembersAsync(id);
                project.Members = membersResponse.Data;
                
                // Fetch all users for the Add Member modal (Legacy, now we use Email too)
                var usersResponse = await _userService.GetAllAsync();
                ViewBag.AllUsers = usersResponse.Data ?? new List<UserViewModel>();

                ViewData["Title"] = project.Name;
                return View(project);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Employee") // Enforce no project creation for employees
                return RedirectToAction("Index");

            ViewData["Title"] = "New Project";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel model)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Employee")
                return RedirectToAction("Index");

            model.CreatedBy = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var response = await _projectService.CreateAsync(model, role);
            if (response.Success)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMemberByEmail(int projectId, string email)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var role = HttpContext.Session.GetString("Role");
            var response = await _projectService.AddMemberByEmailAsync(projectId, email, userId, role);
            if (response.Success) return Ok();
            return BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var role = HttpContext.Session.GetString("Role");

            var response = await _projectService.GetByIdAsync(id);
            if (!response.Success) return RedirectToAction("Index");

            // Managers can only edit their own projects
            if (role == "Manager" && response.Data.CreatedBy != userId)
                return RedirectToAction("Index");
            
            if (role == "Employee")
                return RedirectToAction("Index");

            ViewData["Title"] = "Edit Project";
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProjectViewModel model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var role = HttpContext.Session.GetString("Role");
            var response = await _projectService.UpdateAsync(id, model, userId, role);
            if (response.Success) return RedirectToAction("Details", new { id });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(int projectId, int userId)
        {
            var requestorId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var role = HttpContext.Session.GetString("Role");
            var response = await _projectService.AddMemberAsync(projectId, userId, requestorId, role);
            if (response.Success) return Ok();
            return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMember(int projectId, int userId)
        {
            var response = await _projectService.RemoveMemberAsync(projectId, userId);
            if (response.Success) return Ok();
            return BadRequest(response.Message);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _projectService.DeleteAsync(id);
            if (response.Success) return Ok();
            return BadRequest(response.Message);
        }
    }
}
