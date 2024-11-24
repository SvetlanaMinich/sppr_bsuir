using System.Text.Json;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.Domain.Entities;

namespace WEB_253503_MINICH.UI.Sessions
{
    public class SessionCart : Cart
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public SessionCart(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            LoadCartFromSession();
        }

        private void LoadCartFromSession()
        {
            var session = _contextAccessor.HttpContext.Session;
            var cartJson = session.GetString("CartSession");
            if (cartJson != null)
            {
                var items = JsonSerializer.Deserialize<Dictionary<int, CartItem>>(cartJson);
                if (items != null)
                {
                    CartItems = items;
                }
            }
        }

        private void SaveCartToSession()
        {
            var session = _contextAccessor.HttpContext.Session;
            var cartJson = JsonSerializer.Serialize(CartItems);
            session.SetString("CartSession", cartJson);
        }

        public override void AddToCart(Cup product)
        {
            base.AddToCart(product);
            SaveCartToSession();
        }

        public override void RemoveItem(int id)
        {
            base.RemoveItem(id);
            SaveCartToSession();
        }

        public override void ClearAll()
        {
            base.ClearAll();
            SaveCartToSession();
        }
    }
}