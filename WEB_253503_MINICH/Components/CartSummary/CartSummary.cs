using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WEB_253503_MINICH.UI.Components.Cart
{
    public class CartSummary : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
