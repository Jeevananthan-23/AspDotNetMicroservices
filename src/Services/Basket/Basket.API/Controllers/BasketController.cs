using Basket.API.Entities;
using Basket.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : Controller
    {
        private readonly IBasketRepo _repo;

        public BasketController(IBasketRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
           var basket= await _repo.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart basket)
        {
            return Ok(await _repo.UpdateBasket(basket));
        }

        [HttpDelete]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket(string userName)
        {
            await _repo.DeleteBasket(userName);
            return Ok();
        }
    }
}
