using LibraryMVC.BL;
using LibraryMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.UI.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _booksService.GetBooksAsync();
           
            return View(books);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            return View(book);
        }
    }
}
