using LibraryMVC.BL;
using LibraryMVC.Data;
using LibraryMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.UI.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorsService _authorsService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        public async Task<IActionResult> Index()
        {     
            var authors = await _authorsService.GetAuthorsAsync();

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
            if (!await _authorsService.AddAuthorAsync(author))
            {
                return View(author);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorsService.GetAuthorByIdAsync(id);
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            if (!await _authorsService.UpdateAuthorAsync(author))
            {
                return View(author);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _authorsService.DeleteAuthorAsync(id);

            return RedirectToAction("Index");
        }
    }
}
