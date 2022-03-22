using BusinessLogicLayer.Base;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AkvelonDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILoggerManager _loggerService;

        public TasksController(ITaskService taskService, ILoggerManager loggerService)
        {
            _taskService = taskService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var project = await _taskService.FetchAllAsync(trackChanges: false);

                return Ok(project);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(GetTasks)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
