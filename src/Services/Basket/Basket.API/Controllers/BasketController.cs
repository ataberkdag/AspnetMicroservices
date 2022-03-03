using Basket.API.Entites;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        public BasketController(IBasketRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var result = await this._repository.GetBasket(userName);
            
            return Ok(result ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return Ok(await this._repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeletBasket")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(void))]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await this._repository.DeleteBasket(userName);
            return Ok();
        }
    }
}
