using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    internal class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper _mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            this._orderRepository = orderRepository;
            this._mapper = _mapper;
            this._emailService = emailService;
            this._logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = this._mapper.Map<Order>(request);

            var newOrder = await this._orderRepository.AddAsync(orderEntity);

            this._logger.LogInformation($"Order {newOrder.Id} is successfully created.");

            await SendMail(newOrder);

            return newOrder.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new Email { To = "test12@test12.com", Body = "Order created", Subject = "Order created" };

            try
            {
                await this._emailService.SendEmail(email);
            }
            catch (Exception)
            {
                this._logger.LogError($"Order {order.Id} failed due to an email error.");
            }
        }
    }
}
