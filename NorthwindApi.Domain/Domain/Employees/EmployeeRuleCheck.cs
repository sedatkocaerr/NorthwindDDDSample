using NorthwindApi.Domain.Domain.Employees.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Domain.Employees
{
    public class EmployeeRuleCheck
    {
        public static void CheckEmployeeFirstName (string firstName)
        {
            if(firstName.Length <2)
            {
                throw new FirstNameShortException();
            }
        }

        public static void CheckEmployeeLastName(string lastName)
        {
            if (lastName.Length < 2)
            {
                throw new LastNameShortException();
            }
        }

        public static void CheckEmployeeTitle(string title)
        {
            if (title.Length < 3)
            {
                throw new TitleShortException();
            }
        }

        public static void CheckEmployeeBirthDate(DateTime birthDate)
        {
            if (birthDate.ToString().Equals("0.00.0000 00:00:00"))
            {
                throw new InvalidBirthDateException();
            }
        }

        public static void CheckEmployeeHireDate(DateTime hireDate)
        {
            if (hireDate.ToString().Equals("0.00.0000 00:00:00"))
            {
                throw new InvalidHireDateException();
            }
        }

    }
}
