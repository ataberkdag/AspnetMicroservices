using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            this._orderRepository = orderRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await this._orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            await this._orderRepository.DeleteAsync(orderToDelete);

            this._logger.LogInformation($"Order {orderToDelete.Id} is deleted successfully.");

            return Unit.Value;
        }
    }
}
