using BusinessLogicLayer.Base;
using Contracts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonDemoAPI.Controllers
{
    [Route("api/projects/{id}/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILoggerManager _loggerService;
        private readonly IProjectService _projectService;

        public TasksController(ITaskService taskService, ILoggerManager loggerService, IProjectService projectService)
        {
            _taskService = taskService;
            _loggerService = loggerService;
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksForProject(int Id)
        {
            try
            {
                var project = await _projectService.FetchAsync(Id, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {Id} does not exist");
                    return NotFound();
                }

                var tasks = await _taskService.FetchTaskForProjectAsync(Id, trackChanges: false);

                var tasksDto = tasks.Select(c => new TaskModelDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Priority = c.Priority
                });

                return Ok(tasksDto);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(GetTasksForProject)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
