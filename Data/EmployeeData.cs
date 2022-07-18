using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOET
{
    public class EmployeeData
    {
        internal List<Employee> GetEmployeeData(string file)
        {

            List<Employee> employeeList = new();

            var targetLines = File.ReadAllLines(file)
                  .Select((x, i) => new { Line = x, LineNumber = i }).ToList();

            foreach (var line in targetLines)
            {

                List<WorkingDay> workingDays = new();
                var employee = line.Line.Split("=");

                foreach (string workingDay in employee[1].Split(",").ToList())
                {
                    
                    var day = workingDay.Trim().Substring(0, 2);
                    var hr = workingDay.Trim().Remove(0, 2).Split("-").ToList();

                    var error = ErrorHandler.ValidateEmployeeData(workingDay, employee[0]);
                    if (error.IsError)
                    {
                        Console.WriteLine("Error at {0} : {1}", line.LineNumber, line.Line);
                        throw new Exception(error.ErrorMessage);
                    }
                    
                    workingDays.Add(new ()
                    {
                        Day = day,
                        StartTime = TimeOnly.Parse(hr[0]),
                        EndTime = TimeOnly.Parse(hr[1])
                    });
                }

                employeeList.Add(new ()
                {
                    Name = employee[0],
                    WorkingDays = workingDays
                });

                Console.WriteLine("{0} : {1}", line.LineNumber, line.Line);
            }
            return employeeList;

        }
    }
}
