using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok2.Areas.Admin.Controllers;
using Pustok2.Contexts;
using Pustok2.ModelClass;
using Pustok2.Models;
using System.Diagnostics;

namespace Pustok2.Controllers
{
    public class HomeController : Controller
    {
        PustokDbContent _context { get; }

        public HomeController(PustokDbContent context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.ToListAsync();
            var products = await _context.Products.ToListAsync();
            ProductSliderVM md = new ProductSliderVM();
            md.Sliders = sliders;
            md.Products = products;
            return View(md);
        }

    }
}