using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<AccountViewModel> GetById(Guid id);
        Task<ValidationResult> AddAccount(AccountViewModel AccountViewModel);
        Task<ValidationResult> UpdateAccount(AccountViewModel AccountViewModel);
    }
}
