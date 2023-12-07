using Microsoft.AspNetCore.Mvc;
using Pustok2.Contexts;
using Microsoft.EntityFrameworkCore;
using Pustok2.ViewModel.BlogVM;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Pustok2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        PustokDbContent _db { get; }
        public BlogController(PustokDbContent db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Blogs.Select(c => new BlogListVM { 
                Id = c.Id,
                Title = c.Title,
                Description =c.Description,
                CreatedAt =c.CreatedAt,
                Author=c.Author,
                UptadedAt =c.UptadedAt,
            }).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(BlogListVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _db.Blogs.AnyAsync(x => x.Title== vm.Title))
            {
                ModelState.AddModelError("Title", vm.Title + " already exist");
                return View(vm);
            }
            await _db.Blogs.AddAsync(new Models.Blog { 
                Title = vm.Title,
                Description = vm.Description,
                CreatedAt = vm.CreatedAt,
                UptadedAt=vm.UptadedAt,
                AuthorId=vm.AuthorId,
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
