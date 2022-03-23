using Contracts.Repository;
using DataAccessLayer.Contexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ModelRepository
{
    public class TaskRepository : RepositoryBase<TaskModel>, ITaskRepository
    {
        public TaskRepository(AkvelonDemoAPIContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskModel>> FetchTasksAsync(int projectId, bool trackChanges)
        {
            return await FetchByCondition(e => e.ProjectId == projectId, trackChanges).OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<TaskModel> FetchTaskAsync(int projectId, int id, bool trackChanges)
        {
            return await FetchByCondition(e => e.ProjectId == projectId && e.Id == id, trackChanges).SingleOrDefaultAsync();
        }

        public void CreateTask(int projectId, TaskModel taskModel)
        {
            taskModel.ProjectId = projectId;
            Create(taskModel);
        }

        public void DeleteTask(TaskModel taskModel)
        {
            Delete(taskModel);
        }
    }
}
