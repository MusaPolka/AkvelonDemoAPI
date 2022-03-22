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
    public class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;

        public TaskService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskModel>> FetchAllAsync(bool trackChanges)
        {
            return await _repository.TaskRepository.FetchAllTasksAsync(trackChanges);
        }
    }
}
