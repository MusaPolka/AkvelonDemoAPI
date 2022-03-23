using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CreateTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
    }
}
