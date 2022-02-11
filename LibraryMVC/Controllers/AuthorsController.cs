using LibraryMVC.BL;
using LibraryMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.UI.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IService<Author> _authorsService;

        public AuthorsController(IService<Author> authorsService)
        {
            _authorsService = authorsService;
        }

        public async Task<IActionResult> Index()
        {     
            var authors = await _authorsService.GetAllAsync();

            return View(authors);
        }

        public IActionResult AddAuthor()
        {
            var author = new Author();
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (!await _authorsService.AddAsync(author))
            {
                return View(author);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorsService.GetByIdAsync(id);
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            if (!await _authorsService.UpdateAsync(author))
            {
                return View(author);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _authorsService.DeleteByIdAsync(id);

            return RedirectToAction("Index");
        }
    }
}
