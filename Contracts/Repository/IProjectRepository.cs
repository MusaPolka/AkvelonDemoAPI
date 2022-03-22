using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> FetchAllProjectsAsync(bool trackChanges);

        Task<Project> FetchProjectAsync(int Id, bool trackChanges);
    }
}
