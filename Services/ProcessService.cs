using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOET
{
    public class ProcessService
    {
        public bool ProcessData(string file) {
            
            try
            {
                EmployeeData employeeData = new();

                var employees = employeeData.GetEmployeeData(file).OrderBy(o => o.Name);

                List<string> result = new();

                Console.WriteLine();
                Console.WriteLine("OUTPUT:");
                foreach (var employee in employees)
                {
                    employee.IsDeleted = true;

                    var emplyeesToCompare = from newEmplyees in employees
                        where !newEmplyees.IsDeleted
                        select newEmplyees;

                    foreach (var emp in emplyeesToCompare) {
                        if (employee.Name == emp.Name)
                            throw new Exception($"{emp.Name} was define multiple times.");

                        result.Add(GetCoincidence(employee, emp));
                    }
                }

                SaveFile.WriteLine(result);
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
                
        private string GetCoincidence(Employee mainEmployee, Employee toCompare)
        {
            
            int num = 0;
            foreach (var WorkingDay in mainEmployee.WorkingDays) {
                var employe = from employee in toCompare.WorkingDays
                where employee.Day == WorkingDay.Day
                    && (employee.StartTime >= WorkingDay.StartTime && employee.StartTime < WorkingDay.EndTime)
                    || (WorkingDay.StartTime >= employee.StartTime && WorkingDay.StartTime < employee.EndTime)
                select employee;

                if (employe.Any())
                    num++;
            }

            if (num == 0)
                return string.Empty;

            var info = $"{mainEmployee.Name}-{toCompare.Name}: {num}";
            Console.WriteLine(info);

            return info;
        }
    }
}
