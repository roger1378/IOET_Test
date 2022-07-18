using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOET
{
    static class ErrorHandler
    {
        public static ErrorResponse ValidateEmployeeData(string workingDay, string employeeName)
        {
            var day = workingDay.Trim().Substring(0, 2);
            var hr = workingDay.Trim().Remove(0, 2).Split("-").ToList();

            if (employeeName == String.Empty) {
                return new ErrorResponse
                {
                    IsError = true,
                    ErrorMessage = "Employee name is empty."
                };
            }

            if (TimeOnly.Parse(hr[0]) > TimeOnly.Parse(hr[1])) {
                return new ErrorResponse
                {
                    IsError = true,
                    ErrorMessage = $"For {employeeName} the start time ({hr[0]}) can not be bigger than the end time ({hr[1]})."
                };
            }

            if ((hr[0] == String.Empty) || (hr[1] == String.Empty)) {
                return new ErrorResponse
                {
                    IsError = true,
                    ErrorMessage = $"Start time or End time are empty."
                };
            }

            //if ((day != "MO") && (day != "TU") && (day != "WE") && (day != "TH") && (day != "FR") && (day != "SA") && (day != "SU")) {
            //    return new ErrorResponse
            //    {
            //        IsError = true,
            //        ErrorMessage = $"Day {day} is not valid."
            //    };
            //}

            if ((TimeOnly.Parse(hr[0]) > TimeOnly.Parse("23:59")) || (TimeOnly.Parse(hr[1]) > TimeOnly.Parse("23:59"))) {
                return new ErrorResponse
                {
                    IsError = true,
                    ErrorMessage = $"Start time or End time can not be bigger the 24:00."
                };
            }

            return new ErrorResponse
            {
                ErrorMessage = "OK"
            };
        }
    }
}
