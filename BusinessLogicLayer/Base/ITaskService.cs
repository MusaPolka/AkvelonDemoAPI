using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Base
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> FetchTasksForProjectAsync(int Id, bool trackChanges);
        Task<TaskModel> FetchTaskForProjectAsync(int projectId, int id, bool trackChanges);
        void Create(int projectId, TaskModel taskModel);
    }
}
