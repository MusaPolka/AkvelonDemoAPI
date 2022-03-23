using AutoMapper;
using BusinessLogicLayer.Base;
using Contracts;
using Contracts.Repository;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkvelonDemoAPI.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ILoggerManager _loggerService;
        private readonly IRepositoryManager _repository;

        public ProjectsController(IProjectService projectService, ILoggerManager loggerService,
            IRepositoryManager repository)
        {
            _projectService = projectService;
            _loggerService = loggerService;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                var projects = await _projectService.FetchAllAsync(trackChanges: false);

                var projectsDto = projects.Select(c => new ProjectDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    StartedAt = c.StartedAt,
                    CompletedAt = c.CompletedAt,
                    Status = c.Status
                });

                return Ok(projectsDto);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(GetProjects)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name ="ProjectById")]
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                var project = await _projectService.FetchAsync(id, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {id} does not exist");
                    return NotFound();
                }

                var projectDto = new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    StartedAt = project.StartedAt,
                    CompletedAt = project.CompletedAt,
                    Status = project.Status
                };

                return Ok(projectDto);

            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(GetProjects)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]CreateProjectDto createProjectDto)
        {
            try
            {
                if (createProjectDto == null)
                {
                    _loggerService.LogInfo("CreateProjectDto is null");
                    return BadRequest("Object is null");
                }

                var project = new Project
                {
                    Name = createProjectDto.Name,
                    StartedAt = createProjectDto.StartedAt,
                    CompletedAt = createProjectDto.CompletedAt,
                    Status = createProjectDto.Status,
                };

                _projectService.Create(project);

                var returnProject = new ProjectDto
                {
                    Name = project.Name,
                    StartedAt = project.StartedAt,
                    CompletedAt = project.CompletedAt,
                    Status = project.Status,
                };

                return CreatedAtRoute("ProjectById", new { id = returnProject.Id }, returnProject);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(GetProject)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var project = await _projectService.FetchAsync(id, trackChanges: false);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {id} does not exist");
                    return NotFound();
                }

                _projectService.Delete(project);

                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(DeleteProject)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody]UpdateProjectDto updateProjectDto)
        {
            try
            {
                if (updateProjectDto == null)
                {
                    _loggerService.LogInfo("UpdateProjectDto is null");
                    return BadRequest("Object is null");
                }

                var project = await _projectService.FetchAsync(id, trackChanges: true);

                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {id} does not exist");
                    return NotFound();
                }

                project.Name = updateProjectDto.Name;
                project.StartedAt = updateProjectDto.StartedAt;
                project.CompletedAt = updateProjectDto.CompletedAt;
                project.Status = updateProjectDto.Status;

                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(UpdateProject)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
