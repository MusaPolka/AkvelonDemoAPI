﻿using BusinessLogicLayer.Base;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AkvelonDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ILoggerManager _loggerService;

        public ProjectsController(IProjectService projectService, ILoggerManager loggerService)
        {
            _projectService = projectService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                var project = await _projectService.FetchAllAsync(trackChanges: false);

                return Ok(project);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(GetProjects)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
