using Discount.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _client;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
        {
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName};

            return await this._client.GetDiscountAsync(discountRequest);
        }
    }
}
