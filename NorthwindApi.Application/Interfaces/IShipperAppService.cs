using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.Interfaces
{
    public interface IShipperAppService
    {
        Task<IEnumerable<ShipperViewModel>> GetAll();
        Task<ShipperViewModel> GetById(Guid id);

        Task<ValidationResult> AddShipper(ShipperViewModel shipperViewModel);
        Task<ValidationResult> UpdateShipper(ShipperViewModel shipperViewModel);
        Task<ValidationResult> Remove(Guid id);
    }
}
