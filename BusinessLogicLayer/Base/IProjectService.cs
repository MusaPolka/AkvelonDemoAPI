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
        Task<IEnumerable<Project>> FetchAllAsync(bool trackChanges);
        Task<Project> FetchAsync(int Id, bool trackChanges);
        void Create(Project project);
    }
}
