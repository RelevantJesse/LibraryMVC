using LibraryMVC.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryMVC.UI.ViewModels
{
    public class CheckOutViewModel
    {
        public CheckedOut CheckedOut { get; set; }
        public IEnumerable<SelectListItem> Books { get; set; }
        public IEnumerable<SelectListItem> Members { get; set; }

    }
}
