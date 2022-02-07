using LibraryMVC.BL;
using LibraryMVC.Data;
using LibraryMVC.Data.Models;
using LibraryMVC.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.UI.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksService _booksService;
        private readonly AuthorsService _authorsService;

        public BooksController(BooksService booksService, AuthorsService authorsService)
        {
            _booksService = booksService;
            _authorsService = authorsService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _booksService.GetBooksAsync();
           
            return View(books);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            var authors = await _authorsService.GetAuthorsAsync();
            var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
            var vm = new EditBookViewModel();
            vm.Book = book;
            vm.Authors = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstName + " " + a.LastName });
            vm.Genres = genres.Select(g => new SelectListItem { Value = ((int)g).ToString(), Text = g.ToString() });

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!await _booksService.UpdateBookAsync(book))
            {
                EditBookViewModel vm = new();
                vm.Book = book;
                var authors = await _authorsService.GetAuthorsAsync();
                var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
                vm.Authors = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstName + " " + a.LastName });
                vm.Genres = genres.Select(g => new SelectListItem { Value = ((int)g).ToString(), Text = g.ToString() });

                return View(vm);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _booksService.DeleteBookById(id);

            return RedirectToAction("Index");
        }
    }
}
