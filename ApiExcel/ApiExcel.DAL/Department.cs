using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiExcel.DAL
{
    public class Department
    {
        public int Id { get; set; }
        public List<User> Users { get; set; }

        public string Name { get; set; }

        public string? ParentDepartmentName { get; set; }
    }
}
