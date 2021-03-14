using MediatR;
using NorthwindApi.Domain.Command;
using NorthwindApi.Domain.Domain.Accounts;
using System;


namespace NorthwindApi.Application.Commands.AccountCommands
{
    public class AccountCommand: Command, IRequest<Account>
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string SurName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
    }
}
