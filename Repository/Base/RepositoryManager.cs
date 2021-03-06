using Contracts.Repository;
using DataAccessLayer.Contexts;
using Repository.ModelRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    /// <summary>
    /// Wrapper class that can be injected as a service. 
    /// Provides ability to call Project or Task repository.
    /// </summary>
    public class RepositoryManager : IRepositoryManager
    {
        private AkvelonDemoAPIContext _context;
        private IProjectRepository _projectRepository;
        private ITaskRepository _taskRepository;

        public RepositoryManager(AkvelonDemoAPIContext context)
        {
            _context = context;
        }

        public IProjectRepository ProjectRepository
        {
            get
            {
                if(_projectRepository == null)
                {
                    _projectRepository = new ProjectRepository(_context);
                }
                return _projectRepository;
            }
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                if (_taskRepository == null)
                {
                    _taskRepository = new TaskRepository(_context);
                }
                return _taskRepository;
            }
        }

        //
        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
