using FluentValidation.Results;
using System;

namespace NorthwindApi.Domain.Core.Command
{
    public class CommandResponse
    {
        public ValidationResult ValidationResult { get; set; }
        public Guid Id { get; set; }
    }
}
