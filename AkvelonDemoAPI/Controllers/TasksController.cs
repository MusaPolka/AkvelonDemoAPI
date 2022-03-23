using BusinessLogicLayer.Base;
using Contracts;
using Contracts.Repository;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonDemoAPI.Controllers
{
    [Route("api/projects/{projectId}/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILoggerManager _loggerService;
        private readonly IProjectService _projectService;
        private readonly IRepositoryManager _repository;

        public TasksController(ITaskService taskService, ILoggerManager loggerService, 
            IProjectService projectService, IRepositoryManager repository)
        {
            _taskService = taskService;
            _loggerService = loggerService;
            _projectService = projectService;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksForProject(int projectId)
        {
            try
            {
                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                var tasks = await _taskService.FetchTasksForProjectAsync(projectId, trackChanges: false);

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

        [HttpGet("{id}", Name = "TaskById")]
        public async Task<IActionResult> GetTaskForProjectAsync(int projectId, int id)
        {
            try
            {
                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                var task = await _taskService.FetchTaskForProjectAsync(projectId, id, trackChanges: false);

                if (task == null)
                {
                    _loggerService.LogInfo($"Task with {id} does not exist");
                    return NotFound();
                }

                var taskDto = new TaskModelDto
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    Priority = task.Priority
                };

                return Ok(taskDto);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(GetTaskForProjectAsync)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody]CreateTaskDto createTaskDto)
        {
            try
            {
                if (createTaskDto == null)
                {
                    _loggerService.LogInfo("createTaskDto is null");
                    return BadRequest("Object is null");
                }

                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                var task = new TaskModel
                {
                    Name = createTaskDto.Name,
                    Description = createTaskDto.Description,
                    Priority = createTaskDto.Priority
                };

                _taskService.Create(projectId, task);

                var returnTask = new TaskModelDto
                {
                    Name = task.Name,
                    Description = task.Description,
                    Priority = task.Priority
                };

                return CreatedAtRoute("TaskById", new {projectId, id = returnTask.Id }, returnTask);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(CreateTask)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskForProject(int projectId, int id)
        {
            try
            {
                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                var task = await _taskService.FetchTaskForProjectAsync(projectId, id, trackChanges: false);


                if (task == null)
                {
                    _loggerService.LogInfo($"Task with {id} does not exist");
                    return NotFound();
                }

                _taskService.Delete(task);

                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(DeleteTaskForProject)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int projectId, int id, [FromBody]UpdateTaskDto updateTaskDto)
        {
            try
            {
                if (updateTaskDto == null)
                {
                    _loggerService.LogInfo("UpdateTaskDto is null");
                    return BadRequest("Object is null");
                }

                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                var task = await _taskService.FetchTaskForProjectAsync(projectId, id, trackChanges: true);

                if (task == null)
                {
                    _loggerService.LogInfo($"Task with {id} does not exist");
                    return NotFound();
                }

                task.Name = updateTaskDto.Name;
                task.Description = updateTaskDto.Description;
                task.Priority = updateTaskDto.Priority;

                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(UpdateTask)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
