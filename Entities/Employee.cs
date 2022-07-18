using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOET
{
    internal class Employee
    {
        public string? Name { get; set; }
        public List<WorkingDay>? WorkingDays { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    internal class WorkingDay
    {
        public string? Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
