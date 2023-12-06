using Pustok2.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok2.ViewModel.CategoryVM;

namespace Pustok2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        PustokDbContent _db { get; }

        public CategoryController(PustokDbContent db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Categories.Select(c => new CategoryListItemVM {Id = c.Id, Name = c.Name}).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid) 
            { 
                return View(vm);
            }
            if (await _db.Categories.AnyAsync(x=>x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", vm.Name + " already exist");
                return View(vm);
            }
            await _db.Categories.AddAsync(new Models.Category { Name = vm.Name });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
