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
        private readonly IService<Book> _booksService;
        private readonly IService<Author> _authorsService;
        private IEnumerable<SelectListItem> _authors;
        private IEnumerable<SelectListItem> _genres;

        public BooksController(IService<Book> booksService, IService<Author> authorsService)
        {
            _booksService = booksService;
            _authorsService = authorsService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _booksService.GetAllAsync();

            return View(books);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _booksService.GetByIdAsync(id);
            
            var vm = new AddEditBookViewModel();
            vm.Book = book;
            vm.Authors = await GetAuthors();
            vm.Genres = GetGenres();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!await _booksService.UpdateAsync(book))
            {
                AddEditBookViewModel vm = new();
                vm.Book = book;             
                vm.Authors = await GetAuthors();
                vm.Genres = GetGenres();

                return View(vm);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddBook()
        {
            var vm = new AddEditBookViewModel();
            vm.Book = new Book();
            vm.Authors = await GetAuthors();
            vm.Genres = GetGenres();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (!await _booksService.AddAsync(book))
            {
                AddEditBookViewModel vm = new();
                vm.Book = book;
                vm.Authors = await GetAuthors();
                vm.Genres = GetGenres();

                return View(vm);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _booksService.DeleteByIdAsync(id);

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetAuthors()
        {
            if (_authors == null)
            {
                var authors = await _authorsService.GetAllAsync();
                _authors = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstName + " " + a.LastName });
            }
            return _authors;
        }

        private IEnumerable<SelectListItem> GetGenres()
        {
            if (_genres == null)
            {
                var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
                _genres = genres.Select(g => new SelectListItem { Value = ((int)g).ToString(), Text = g.ToString() });
            }
            return _genres;
        }
    }
}
