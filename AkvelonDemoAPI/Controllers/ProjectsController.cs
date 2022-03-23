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
    /// <summary>
    /// Im copying a lot of code here meaning not following DRY, so
    /// 
    /// TODO Create Exception handling middleware to not repeat myself everytime using Try-Catch
    /// 
    /// TODO implement Mapping service 
    /// </summary>

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

        //Get all projects from DB
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                //Get all projects 
                var projects = await _projectService.FetchAllAsync(trackChanges: false);

                //Using DTO pattern and mapping it to our DTO
                var projectsDto = projects.Select(c => new ProjectDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    StartedAt = c.StartedAt,
                    CompletedAt = c.CompletedAt,
                    Status = c.Status
                });

                //returning all projects
                return Ok(projectsDto);
            }
            catch (Exception ex)
            {
                //Log it If error was accured
                _loggerService.LogError($"Error accured in the {nameof(GetProjects)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }


        //Get single project from DB
        [HttpGet("{id}", Name ="ProjectById")]
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                //get project by id
                var project = await _projectService.FetchAsync(id, trackChanges: false);

                //check for null
                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {id} does not exist");
                    return NotFound();
                }

                //Mapping it to DTO
                var projectDto = new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    StartedAt = project.StartedAt,
                    CompletedAt = project.CompletedAt,
                    Status = project.Status
                };

                //return if it is not null
                return Ok(projectDto);

            }
            catch (Exception ex)
            {
                //Log it If error was accured
                _loggerService.LogError($"Error accured in the {nameof(GetProjects)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Create Project 
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]CreateProjectDto createProjectDto)
        {
            try
            {
                //check for null request
                if (createProjectDto == null)
                {
                    _loggerService.LogInfo("CreateProjectDto is null");
                    return BadRequest("Object is null");
                }

                //create new Project and map given properties
                var project = new Project
                {
                    Name = createProjectDto.Name,
                    StartedAt = createProjectDto.StartedAt,
                    CompletedAt = createProjectDto.CompletedAt,
                    Status = createProjectDto.Status,
                };

                //Create And Save inside DB
                _projectService.Create(project);

                //Map it to DTO
                var returnProject = new ProjectDto
                {
                    Name = project.Name,
                    StartedAt = project.StartedAt,
                    CompletedAt = project.CompletedAt,
                    Status = project.Status,
                };

                //Return status code of 201 which is provided by CreateAtRoute method
                return CreatedAtRoute("ProjectById", new { id = returnProject.Id }, returnProject);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(GetProject)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }


        //Delete Project from DB
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                //Get project that we want to delete
                var project = await _projectService.FetchAsync(id, trackChanges: false);

                //check if it is null
                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {id} does not exist");
                    return NotFound();
                }

                //if it is not null, Delete it 
                _projectService.Delete(project);

                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error accured in the {nameof(DeleteProject)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Update Project 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody]UpdateProjectDto updateProjectDto)
        {
            try
            {
                //check if request is null
                if (updateProjectDto == null)
                {
                    _loggerService.LogInfo("UpdateProjectDto is null");
                    return BadRequest("Object is null");
                }

                //Get project that we want to update
                //Notice that we put true for trackChanges param.
                //It modifies our ProjectModel every time we make changes to it
                var project = await _projectService.FetchAsync(id, trackChanges: true);

                //check if it is null
                if (project == null)
                {
                    _loggerService.LogInfo($"Project with {id} does not exist");
                    return NotFound();
                }

                //Making changes to our ProjectModel
                project.Name = updateProjectDto.Name;
                project.StartedAt = updateProjectDto.StartedAt;
                project.CompletedAt = updateProjectDto.CompletedAt;
                project.Status = updateProjectDto.Status;

                //And without using Update method we just SaveChanges
                //Cus After making changes, EF going to Modify our model
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
