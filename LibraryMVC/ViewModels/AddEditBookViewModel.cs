using LibraryMVC.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryMVC.UI.ViewModels
{
    public class AddEditBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
    }
}
