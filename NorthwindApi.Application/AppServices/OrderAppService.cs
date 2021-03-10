using AutoMapper;
using FluentValidation.Results;
using NorthwindApi.Application.ElasticSearhServices.Interfaces;
using NorthwindApi.Application.Interfaces;
using NorthwindApi.Application.ViewModels;
using NorthwindApi.Data.Mediator;
using NorthwindApi.Domain.Commands.Orders;
using NorthwindApi.Domain.Core.Command;
using NorthwindApi.Domain.Domain.OrderDetails;
using NorthwindApi.Domain.Domain.Orders;
using NorthwindApi.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindApi.Application.AppServices
{
    public class OrderAppService : IOrderAppService
    {
        private IElasticSearchService _elasticSearchService;
        private IMediatorHandler _mediatorHandler;
        private IMapper _mapper;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;

        public OrderAppService(IElasticSearchService elasticSearchService, IMediatorHandler mediatorHandler,
            IMapper mapper, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _elasticSearchService = elasticSearchService;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<CommandResponse> AddOrder(OrderViewModel orderViewModel)
        {
           var addCommand = _mapper.Map<OrderAddCommand>(orderViewModel);
           return await _mediatorHandler.SendCommand(addCommand);
        }

        public async Task<ValidationResult> AddOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            var validationResult = new ValidationResult();

          var orderCheck = await _orderRepository.FindById(orderDetailViewModel.OrderID);
          if (orderCheck==null)
          {
                validationResult.Errors.Add(new ValidationFailure("","Invalidad Order Id"));
          }
          var productCheck = await _productRepository.FindById(orderDetailViewModel.ProductId);

          if (productCheck==null)
          {
                validationResult.Errors.Add(new ValidationFailure("", "Invalidad Product Id"));
          }
            var data = new Guid();
            var orderDetail = new OrderDetail(Guid.NewGuid(), productCheck, orderCheck, orderDetailViewModel.UnitPrice,
                orderDetailViewModel.Quantity, orderDetailViewModel.DisCount);
          orderCheck.AddOrderDetail(orderDetail);
           _orderRepository.Update(orderCheck);
           await _orderRepository.UnitOfWork.Commit();
           return validationResult;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAll()
        {
           var list = await _elasticSearchService.SimpleSearchAsync<OrderViewModel>("orderevent",new 
                Nest.SearchDescriptor<OrderViewModel>().Query(x => x.MatchAll()).From(0)
                .Size(2000));
            return list.Documents.ToList();
        }

        public async Task<OrderViewModel> GetById(Guid id)
        {
           var customer =  await _elasticSearchService.GetId<OrderViewModel>("orderevent", id);
            return customer;
        }

        public async Task<CommandResponse> Remove(Guid id)
        {
            var removeCommand = new OrderRemoveCommand(id);
            return  await _mediatorHandler.SendCommand(removeCommand);
        }

        public async Task<CommandResponse> UpdateOrder(OrderViewModel orderViewModel)
        {
           var updateCommand =  _mapper.Map<OrderUpdateCommand>(orderViewModel);
            return await _mediatorHandler.SendCommand(updateCommand);
        }
    }
}
