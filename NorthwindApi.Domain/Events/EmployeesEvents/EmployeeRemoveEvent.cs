using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindApi.Domain.Events.EmployeesEvents
{
    public class EmployeeRemoveEvent:Event
    {
        public EmployeeRemoveEvent(Guid Id)
        {
            this.Id = Id;
        }

        public Guid Id { get; private set; }
    }
}
