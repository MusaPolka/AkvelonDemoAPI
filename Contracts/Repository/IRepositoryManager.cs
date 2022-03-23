﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repository
{
    public interface IRepositoryManager
    {
        IProjectRepository ProjectRepository { get; }
        ITaskRepository TaskRepository { get; }
        Task SaveAsync();
    }
}
