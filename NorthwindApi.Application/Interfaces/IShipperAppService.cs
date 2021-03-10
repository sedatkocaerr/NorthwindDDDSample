using FluentValidation.Results;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Domain.Core.Command;
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

        Task<CommandResponse> AddShipper(ShipperViewModel shipperViewModel);
        Task<CommandResponse> UpdateShipper(ShipperViewModel shipperViewModel);
        Task<CommandResponse> Remove(Guid id);
    }
}
