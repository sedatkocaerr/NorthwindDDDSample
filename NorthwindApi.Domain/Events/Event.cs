using MediatR;
using System;

namespace NorthwindApi.Domain.Events
{
    public abstract class Event:INotification
    {
        public DateTime EventDate { get; set; }
    }
}
