using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            this._logger = logger;
            this._orderRepository = orderRepository;
            this._mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await this._orderRepository.GetByIdAsync(request.Id);
            if (orderToUpdate == null)
            {
                this._logger.LogError("Order not exists on database");
                throw new NotFoundException(nameof(Order), request.Id);
            }

            this._mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

            await this._orderRepository.UpdateAsync(orderToUpdate);

            this._logger.LogInformation($"Order {orderToUpdate.Id} is updated successfully.");

            return Unit.Value;
        }
    }
}
