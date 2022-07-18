# Description
This excercise was created as part of the recruitment process for the position of .NET core Software Developer at [IOET](https://www.ioet.com/) company.

## Exercise

The company ACME offers their employees the flexibility to work the hours they want. But due to some external circumstances they need to know what employees have been at the office within the same time frame

The goal of this exercise is to output a table containing pairs of employees and how often they have coincided in the office.

Input: the name of an employee and the schedule they worked, indicating the time and hours. This should be a .txt file with at least five sets of data. You can include the data from our examples below:

Example 1:

INPUT\
RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00\
ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00\
ANDRES=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00

OUTPUT:\
ASTRID-RENE: 2\
ASTRID-ANDRES: 3\
RENE-ANDRES: 2

##Solution
As part of this solution a console application was created with three main classes:

## First
```bash
EmployeeData
```
In this class I´ve created a method which is responsable for process the .txt file and return the information in a readable format, for the porpuese of this excersi I am returning a object List

```c#
using System.Collections.Generic;

# returns 'List<Employee>'
internal List<Employee> GetEmployeeData(string file)
```
The Employee Object contains Name and Days the employees were working during the week:

```c#
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
```

## Second
```bash
ErrorHandler
```
This object allow me to get all errores I found in the txt file, those errors are returned to the user

## Third
```bash
ProcessService
```
In this class I am processing all the information that EmployeeData object is returning, two methods were created for this action.
I´ve used Linq for filtering and ordering the data by name, that give me option to print the data in an organized way.
I've created a loop for all employees I am receiving from EmployeeData, every time I read en employee I am asking to a new method to return the coincidence for day and hour for that employee in particular + the employee I am comparing the match, that way a can iterate day by day get the match for a particular period of time.

```c#
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
```

```c#
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
```

At the end of the process if everything is OK we write a new output txt file with the result, an object was created for this action.\
I´ve apply SOLID principles, keeping my code small and with a unique purpose, that way my is more elegant, clean and readable.

## How to testing
You can create a test project.
After adding the project reference and debigging the application.
You can add into the bin folder the input.txt file with the employee information.
For the porpuse of this project, we add some txt files with differents variations, for example:
- Non existing name
- Reated name
- Missing day
- Mising time

```c#
namespace IOETTests
{
    [TestClass]
    public class ProcessTests
    {
        [TestMethod]
        public void Test_ProcessData()
        {
            string file1 = @"input.txt";
            ProcessService service = new ProcessService();
            var response = service.ProcessData(file1);
            Assert.AreEqual(response, true);
        }

        [TestMethod]
        public void Test_ProcessDataOneRecord()
        {
            string file1 = @"input3.txt";
            ProcessService service = new ProcessService();
            var response = service.ProcessData(file1);
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void Test_ProcessDataNoFile()
        {
            string file1 = "";
            ProcessService service = new ProcessService();
            var response = service.ProcessData(file1);
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void Test_ProcessDataRepeatedName()
        {
            string file1 = @"input1.txt";
            ProcessService service = new ProcessService();
            var response = service.ProcessData(file1);
            Assert.IsTrue(response);
        }
    }
}
```
file1 represent the file name you added into the bin folder.

## Important notes
- I am not working with full path because it could be confusing change the path in every part of the project, using the name only I avoid any type of issues.
- If you run the .exe without the the test project, the app will ask you for a full path of the file.
- Output file is created as well into the bin forlder by default, if you run the test application you need to look the file into this project, if you run the .exe directly you need to look the bin forlder for result.
- If you create the .exe, you can add the input.txt files in the same root.
- For information only, the console is printing the same information from both txt files (input and output) only for quick review of the result.

## License
[@roger1378]
