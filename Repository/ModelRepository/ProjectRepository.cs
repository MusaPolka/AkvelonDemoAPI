﻿using Contracts.Repository;
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
    }
}
