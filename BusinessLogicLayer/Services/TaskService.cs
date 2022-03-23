using BusinessLogicLayer.Base;
using Contracts.Repository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;

        public TaskService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskModel>> FetchTasksForProjectAsync(int Id, bool trackChanges)
        {
            return await _repository.TaskRepository.FetchTasksAsync(Id, trackChanges);
        }

        public async Task<TaskModel> FetchTaskForProjectAsync(int projectId, int id, bool trackChanges)
        {
            return await _repository.TaskRepository.FetchTaskAsync(projectId, id, trackChanges);
        }

        public void Create(int projectId, TaskModel taskModel)
        {
            _repository.TaskRepository.CreateTask(projectId, taskModel);
            _repository.SaveAsync();
        }

        public void Delete(TaskModel taskModel)
        {
            _repository.TaskRepository.DeleteTask(taskModel);
            _repository.SaveAsync();
        }

    }
}
