using Microsoft.AspNetCore.Mvc;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskApiService _taskService;
        private readonly ProjectApiService _projectService;
        private readonly UserApiService _userService;

        public TasksController(TaskApiService taskService, ProjectApiService projectService, UserApiService userService)
        {
            _taskService = taskService;
            _projectService = projectService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int? projectId)
        {
            ViewData["Title"] = "Task List";
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var role = HttpContext.Session.GetString("Role");
            
            var response = await _taskService.GetAllAsync(projectId, userId, role);
            return View(response.Data ?? new List<TaskResponseViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Employee") return RedirectToAction("Index");

            ViewData["Title"] = "Create Task";
            var projects = await _projectService.GetAllAsync();
            var users = await _userService.GetAllAsync();
            ViewBag.Projects = projects.Data ?? new List<ProjectViewModel>();
            ViewBag.Users = users.Data ?? new List<UserViewModel>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Employee") return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                var projects = await _projectService.GetAllAsync();
                var users = await _userService.GetAllAsync();
                ViewBag.Projects = projects.Data ?? new List<ProjectViewModel>();
                ViewBag.Users = users.Data ?? new List<UserViewModel>();
                return View(model);
            }

            var response = await _taskService.CreateAsync(model);
            if (response.Success)
                return RedirectToAction("Index");

            ModelState.AddModelError("", response.Message);
            var projectsRes = await _projectService.GetAllAsync();
            var usersRes = await _userService.GetAllAsync();
            ViewBag.Projects = projectsRes.Data ?? new List<ProjectViewModel>();
            ViewBag.Users = usersRes.Data ?? new List<UserViewModel>();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Employee") return RedirectToAction("Index");

            ViewData["Title"] = "Edit Task";
            var response = await _taskService.GetByIdAsync(id);
            if (!response.Success) return RedirectToAction("Index");
            // ... (rest of edit logic)

            var task = response.Data;
            var model = new CreateTaskViewModel
            {
                Title = task.Title,
                Description = task.Description,
                ProjectId = task.ProjectId,
                AssignedTo = task.AssignedTo,
                Priority = task.Priority,
                Deadline = task.Deadline
            };

            var projects = await _projectService.GetAllAsync();
            var users = await _userService.GetAllAsync();
            ViewBag.Projects = projects.Data ?? new List<ProjectViewModel>();
            ViewBag.Users = users.Data ?? new List<UserViewModel>();
            ViewBag.TaskId = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateTaskViewModel model)
        {
            var response = await _taskService.UpdateAsync(id, model);
            if (response.Success) return RedirectToAction("Index");

            var projects = await _projectService.GetAllAsync();
            var users = await _userService.GetAllAsync();
            ViewBag.Projects = projects.Data ?? new List<ProjectViewModel>();
            ViewBag.Users = users.Data ?? new List<UserViewModel>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, int statusId)
        {
            await _taskService.UpdateStatusAsync(id, statusId);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _taskService.DeleteAsync(id);
            if (response.Success) return Ok();
            return BadRequest(response.Message);
        }
    }
}
