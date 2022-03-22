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
        public Task<IEnumerable<TaskModel>> FetchAllAsync(bool trackChanges);
    }
}
