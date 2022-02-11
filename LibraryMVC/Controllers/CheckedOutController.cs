using LibraryMVC.BL;
using LibraryMVC.Data.Models;
using LibraryMVC.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryMVC.UI.Controllers
{
    public class CheckedOutController : Controller
    {
        private readonly CheckOutService _checkOutService;
        private readonly IService<Member> _membersService;
        private readonly IService<Book> _booksService;

        public CheckedOutController(CheckOutService checkOutService, IService<Member> membersService, IService<Book> booksService)
        {
            _checkOutService = checkOutService;
            _membersService = membersService;
            _booksService = booksService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new CheckedOutSearchViewModel();
            vm.IsCheckedOut = false;
            vm.CheckedOuts = await _checkOutService.GetAllAsync();
            return View(vm);
        }

        public async Task<IActionResult> CheckOut()
        {
            var vm = new CheckOutViewModel();
            vm.CheckedOut = new CheckedOut();

            var members = await _membersService.GetAllAsync();
            var books = (await _booksService.GetAllAsync()).Where(b => b.CheckedOutTo == null);

            vm.Members = members.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.FirstName + " " + m.LastName });
            vm.Books = books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckedOut checkedOut)
        {
            if (checkedOut == null || checkedOut.Book == null || checkedOut.Member == null)
            {
                var vm = new CheckOutViewModel();
                vm.CheckedOut = checkedOut;

                var members = await _membersService.GetAllAsync();
                var books = await _booksService.GetAllAsync();

                vm.Members = members.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.FirstName + " " + m.LastName });
                vm.Books = books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
                return View(vm);
            }

            if (!await _checkOutService.CheckOutBook(checkedOut.Member.Id, checkedOut.Book.Id))
            {
                var vm = new CheckOutViewModel();
                vm.CheckedOut = checkedOut;

                var members = await _membersService.GetAllAsync();
                var books = await _booksService.GetAllAsync();

                vm.Members = members.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.FirstName + " " + m.LastName });
                vm.Books = books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CheckIn(int bookId, int memberId)
        {
            await _checkOutService.CheckInBook(bookId, memberId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SearchCheckOuts(CheckedOutSearchViewModel checkedOutSearchViewModel)
        {
            var vm = new CheckedOutSearchViewModel();
            vm.IsCheckedOut = false;
            vm.CheckedOuts = (await _checkOutService.GetAllAsync())
                .Where(c => (string.IsNullOrWhiteSpace(checkedOutSearchViewModel.MemberName)
                    || c.Member.FirstName.ToLower().Contains(checkedOutSearchViewModel.MemberName.ToLower())
                    || c.Member.LastName.ToLower().Contains(checkedOutSearchViewModel.MemberName.ToLower()))
                    && (string.IsNullOrWhiteSpace(checkedOutSearchViewModel.BookTitle)
                    || c.Book.Title.ToLower().Contains(checkedOutSearchViewModel.BookTitle.ToLower()))
                    && ((checkedOutSearchViewModel.IsCheckedOut && c.ReturnedDate == null)
                    || (!checkedOutSearchViewModel.IsCheckedOut && c.ReturnedDate != null)));
            return View("Index", vm);
        }
    }
}
