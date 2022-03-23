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

        //Get all tasks that has given Project
        [HttpGet]
        public async Task<IActionResult> GetTasksForProject(int projectId)
        {
            try
            {
                //get project we want to fetch all tasks
                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                //check for null
                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                //get all tasks from given project 
                var tasks = await _taskService.FetchTasksForProjectAsync(projectId, trackChanges: false);


                //Mapping it to DTO
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


        //Get only one task from given Project
        [HttpGet("{id}", Name = "TaskById")]
        public async Task<IActionResult> GetTaskForProjectAsync(int projectId, int id)
        {
            try
            {
                //get project we want to fetch task
                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                //Check for null
                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                //get task 
                var task = await _taskService.FetchTaskForProjectAsync(projectId, id, trackChanges: false);

                //check task for null
                if (task == null)
                {
                    _loggerService.LogInfo($"Task with {id} does not exist");
                    return NotFound();
                }

                //mapping it to DTO
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

        //Create Task 
        [HttpPost]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody]CreateTaskDto createTaskDto)
        {
            try
            {
                //check if request is null
                if (createTaskDto == null)
                {
                    _loggerService.LogInfo("createTaskDto is null");
                    return BadRequest("Object is null");
                }

                //get project to add new task to it
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

                //Create task for Project
                _taskService.Create(projectId, task);

                var returnTask = new TaskModelDto
                {
                    Name = task.Name,
                    Description = task.Description,
                    Priority = task.Priority
                };

                //Return status code of 201 which is provided by CreateAtRoute method
                return CreatedAtRoute("TaskById", new {projectId, id = returnTask.Id }, returnTask);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(CreateTask)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Delete task for project
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskForProject(int projectId, int id)
        {
            try
            {
                //get project
                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                //check for null
                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                //get task if project is not null
                var task = await _taskService.FetchTaskForProjectAsync(projectId, id, trackChanges: false);


                if (task == null)
                {
                    _loggerService.LogInfo($"Task with {id} does not exist");
                    return NotFound();
                }

                //Delete task
                _taskService.Delete(task);

                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(DeleteTaskForProject)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Update task for project
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int projectId, int id, [FromBody]UpdateTaskDto updateTaskDto)
        {
            try
            {
                //check if request is null
                if (updateTaskDto == null)
                {
                    _loggerService.LogInfo("UpdateTaskDto is null");
                    return BadRequest("Object is null");
                }

                //Get project 
                var project = await _projectService.FetchAsync(projectId, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {projectId} does not exist");
                    return NotFound();
                }

                //get task if projecct is not null
                //Notice that we put true for trackChanges param.
                //It modifies our ProjectModel every time we make changes to it
                var task = await _taskService.FetchTaskForProjectAsync(projectId, id, trackChanges: true);

                if (task == null)
                {
                    _loggerService.LogInfo($"Task with {id} does not exist");
                    return NotFound();
                }

                //Making changes to our Task Model
                task.Name = updateTaskDto.Name;
                task.Description = updateTaskDto.Description;
                task.Priority = updateTaskDto.Priority;

                //Just save without Update method
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
