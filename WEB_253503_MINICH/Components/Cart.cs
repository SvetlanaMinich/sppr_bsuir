﻿using System;
using Microsoft.AspNetCore.Mvc;
using WEB_253503_MINICH.UI.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;


namespace WEB_253503_MINICH.UI.Components
{
    public class CartViewComponent : ViewComponent
    {
        CartModel cart = new();
        
        public IViewComponentResult Invoke()
        {
            return View(cart);            
        }
    }
}
