using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok2.Contexts;
using Pustok2.ViewModel.ProductImagesVm;

namespace Pustok2.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductImagesController : Controller
    {
        PustokDbContent _db { get; }
        public ProductImagesController(PustokDbContent db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.ProductImages.Select(c => new ProductImagesListVm
            {
                Id = c.Id,
                ImagePath = c.ImagePath,
                IsActive = c.IsActive,
                Product = c.Product
            }).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
