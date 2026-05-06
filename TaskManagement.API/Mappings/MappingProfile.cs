using AutoMapper;
using TaskManagement.API.DTOs.Auth;
using TaskManagement.API.DTOs.Notification;
using TaskManagement.API.DTOs.Project;
using TaskManagement.API.DTOs.Task;
using TaskManagement.API.Models.Entities;

namespace TaskManagement.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Project
            CreateMap<Project, ProjectResponseDto>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator.FullName))
                .ForMember(dest => dest.MemberCount, opt => opt.MapFrom(src => src.ProjectMembers.Count))
                .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => 
                    src.Tasks.Any() ? (src.Tasks.Count(t => t.StatusId == 3) * 100 / src.Tasks.Count) : 0));
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();

            // Task
            CreateMap<Models.Entities.Task, TaskResponseDto>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.AssigneeName, opt => opt.MapFrom(src => src.Assignee != null ? src.Assignee.FullName : null))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.StatusName));
            CreateMap<CreateTaskDto, Models.Entities.Task>();
            CreateMap<UpdateTaskDto, Models.Entities.Task>();

            // Notification
            CreateMap<Notification, NotificationDto>();
        }
    }
}
