using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pustok2.Contexts;
using Pustok2.ViewModel.AuthorVM;

namespace Pustok2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        PustokDbContent _db { get; }

            public AuthorController(PustokDbContent db)
            {
                _db = db;
            }

            public async Task<IActionResult> Index()
            {
                return View(await _db.Author.Include(a=>a.Blogs).Select(c => new AuthorListVm
                {
                    Id = c.Id,
                    Name = c.Name,
                    Surname = c.Surname,
                    Blogs = c.Blogs.ToList(),
                    IsDeleted = c.IsDeleted,
                }).ToListAsync());
            }
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(AuthorCreateVm vm)
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                if (await _db.Author.AnyAsync(x => x.Name == vm.Name))
                {
                    ModelState.AddModelError("Name", vm.Name + " already exist");
                    return View(vm);
                }
                await _db.Author.AddAsync(new Models.Author {
                    Name = vm.Name,
                    Surname = vm.Surname,
                });
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        public async Task<IActionResult> Selection()
        {
            var authors = await _db.Author.ToListAsync();
            ViewBag.AuthorsList = new SelectList(authors, "Name", "Surname");
            return View();
        }
    }
}
