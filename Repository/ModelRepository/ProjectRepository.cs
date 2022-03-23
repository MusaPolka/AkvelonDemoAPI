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
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(AkvelonDemoAPIContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> FetchAllProjectsAsync(bool trackChanges)
        {
            return await FetchAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Project> FetchProjectAsync(int Id, bool trackChanges)
        {
            return await FetchByCondition(c => c.Id == Id, trackChanges).SingleOrDefaultAsync();
        }

        public void CreateProject(Project project)
        {
            Create(project);
        }

        public void DeleteProject(Project project)
        {
            Delete(project);
        }
    }
}
