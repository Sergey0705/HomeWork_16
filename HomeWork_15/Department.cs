using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_15
{
    public class Department
    {
        /// <summary>
        /// Наименование отдела
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Id отдела
        /// </summary>
        public int DepartmentId { get; set; }

        public Department(string Name, int Id)
        {
            DepartmentName = Name;
            DepartmentId = Id;
        }
    }
}
