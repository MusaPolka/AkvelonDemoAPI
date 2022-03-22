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
        Task<IEnumerable<TaskModel>> FetchTaskForProjectAsync(int Id, bool trackChanges);
    }
}
