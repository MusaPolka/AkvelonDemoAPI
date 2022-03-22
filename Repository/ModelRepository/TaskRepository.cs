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

        public async Task<IEnumerable<TaskModel>> FetchTaskAsync(int Id, bool trackChanges)
        {
            return await FetchByCondition(e => e.ProjectId == Id, trackChanges).OrderBy(e => e.Id).ToListAsync();
        }
    }
}
