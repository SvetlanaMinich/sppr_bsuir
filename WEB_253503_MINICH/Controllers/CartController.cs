using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.UI.Extensions;
using WEB_253503_MINICH.UI.Services.ApiCupService;

namespace WEB_253503_MINICH.UI.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ICupService _cupService;
        private readonly Cart _cart;

        public CartController(ICupService cupService, Cart cart)
        {
            _cupService = cupService;
            _cart = cart;
        }

        public IActionResult Index()
        {
            return View(_cart);
        }

        [Authorize]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> Add(int id, string returnUrl)
        {
            var data = await _cupService.GetCupByIdAsync(id);
            if (data.Successfull)
            {
                _cart.AddToCart(data.Data);
            }
            return Redirect(returnUrl);
        }
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> ClearCart(string returnUrl)
        {
            _cart.ClearAll();
            return Redirect(returnUrl);
        }

        [Authorize]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> RemoveFromCart(int id, string returnUrl)
        {
            _cart.RemoveItem(id);
            return Redirect(returnUrl);
        }
    }
}
