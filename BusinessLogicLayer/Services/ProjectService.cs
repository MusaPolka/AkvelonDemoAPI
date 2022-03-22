﻿
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
    }
}
