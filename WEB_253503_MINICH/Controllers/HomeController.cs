using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253503_MINICH.UI.Models;

namespace WEB_253503_MINICH.UI.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Лабораторная работа №5";

        var listItems = new List<ListDemo>
        {
            new() { Id = 1, Name = "Элемент 1" },
            new () { Id = 2, Name = "Элемент 2" },
            new () { Id = 3, Name = "Элемент 3" }
        };

        // Передаём список через SelectList
        ViewBag.ListItems = new SelectList(listItems, "Id", "Name");

        return View();
    }
    public IActionResult Logout()
    {
        return View();
    }
}

public class ListDemo
{
    public int Id { get; set; }
    public string Name { get; set; }
}
