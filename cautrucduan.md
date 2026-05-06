TaskManagementSolution
│
├── TaskManagement.API
│
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   ├── UsersController.cs
│   │   ├── ProjectsController.cs
│   │   ├── ProjectMembersController.cs
│   │   ├── TasksController.cs
│   │   ├── NotificationsController.cs
│   │   └── ReportsController.cs
│   │
│   ├── Services
│   │   ├── Interfaces
│   │   ├── AuthService.cs
│   │   ├── UserService.cs
│   │   ├── ProjectService.cs
│   │   ├── TaskService.cs
│   │   ├── NotificationService.cs
│   │   └── ReportService.cs
│   │
│   ├── Repositories
│   │   ├── Interfaces
│   │   ├── UserRepository.cs
│   │   ├── ProjectRepository.cs
│   │   ├── TaskRepository.cs
│   │   └── NotificationRepository.cs
│   │
│   ├── DTOs
│   │   ├── Auth
│   │   ├── Project
│   │   ├── Task
│   │   ├── Notification
│   │   └── Report
│   │
│   ├── Models
│   │   └── Entities
│   │
│   ├── Data
│   │   ├── ApplicationDbContext.cs
│   │   └── SeedData.cs
│   │
│   ├── Helpers
│   │   ├── PasswordHasher.cs
│   │   └── ApiResponse.cs
│   │
│   ├── Middleware
│   │   └── ExceptionMiddleware.cs
│   │
│   ├── Mappings
│   │
│   ├── Migrations
│   │
│   ├── appsettings.json
│   └── Program.cs
│
│
├── TaskManagement.Web
│
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   ├── DashboardController.cs
│   │   ├── ProjectsController.cs
│   │   ├── TasksController.cs
│   │   ├── TeamController.cs
│   │   └── NotificationsController.cs
│   │
│   ├── Services
│   │   ├── ApiService.cs
│   │   ├── AuthApiService.cs
│   │   ├── ProjectApiService.cs
│   │   ├── TaskApiService.cs
│   │   └── NotificationApiService.cs
│   │
│   ├── Models
│   │   └── ViewModels
│   │
│   ├── Helpers
│   │   ├── SessionHelper.cs
│   │   └── HttpClientHelper.cs
│   │
│   ├── Views
│   │
│   ├── wwwroot
│   │
│   ├── appsettings.json
│   └── Program.cs