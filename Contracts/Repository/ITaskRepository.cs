using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> FetchTasksAsync(int projectId, bool trackChanges);

        Task<TaskModel> FetchTaskAsync(int projectId, int id, bool trackChanges);

        void CreateTask(int projectId, TaskModel taskModel);
    }
}
