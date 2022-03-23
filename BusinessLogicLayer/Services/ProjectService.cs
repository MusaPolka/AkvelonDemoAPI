
using BusinessLogicLayer.Base;
using Contracts.Repository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// We could have call our Repository classes inside of our controllers.
    /// But in order to free our controllers from repository logic we created additional layer
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IRepositoryManager _repository;

        public ProjectService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Project>> FetchAllAsync(bool trackChanges)
        {
            return await _repository.ProjectRepository.FetchAllProjectsAsync(trackChanges);
        }

        public async Task<Project> FetchAsync(int Id, bool trackChanges)
        {
            return await _repository.ProjectRepository.FetchProjectAsync(Id, trackChanges);
        }

        public void Create(Project project)
        {
            _repository.ProjectRepository.CreateProject(project);
            _repository.SaveAsync();
        }

        public void Delete(Project project)
        {
            _repository.ProjectRepository.DeleteProject(project);
            _repository.SaveAsync();
        }
    }
}
