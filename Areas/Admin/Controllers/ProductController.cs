using Pustok2.Contexts;
using Pustok2.Models;
using Pustok2.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pustok2.ViewModel.ProductVM
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        PustokDbContent _db { get; }

        public ProductController(PustokDbContent db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            return View(_db.Products.Select(p => new ProductListVM
            {
                Id = p.Id,
                Name = p.Name,
                CostPrice = p.CostPrice,
                Discount = p.Discount,
                About = p.About,
                IsDeleted = p.IsDeleted,
                Quantity = p.Quantity,
                SellPrice = p.SellPrice,
                ProductCode =p.ProductCode,
                CategoryId = p.CategoryId,
                Description =p.Description
            }));
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (vm.CostPrice > vm.SellPrice)
            {
                ModelState.AddModelError("CostPrice", "Sell price must be bigger than cost price");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories;
                return View(vm);
            }
            if (!await _db.Categories.AnyAsync(c => c.Id == vm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category doesnt exist");
                ViewBag.Categories = _db.Categories;
                return View(vm);
            }
            Product prod = new Product
            {
                Name = vm.Name,
                CostPrice = vm.CostPrice,
                Discount = vm.Discount,
                About = vm.About,
                Quantity = vm.Quantity,
                SellPrice = vm.SellPrice,
                ProductCode = vm.ProductCode,
                CategoryId = vm.CategoryId,
                Description = vm.Description
            };

            await _db.Products.AddAsync(prod);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
