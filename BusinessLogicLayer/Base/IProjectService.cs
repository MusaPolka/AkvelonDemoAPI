using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Base
{
    public interface IProjectService
    {
        public Task<IEnumerable<Project>> FetchAllAsync(bool trackChanges);
    }
}
