using LibraryMVC.Data.Models;

namespace LibraryMVC.UI.ViewModels
{
    public class CheckedOutSearchViewModel
    {
        public string BookTitle { get; set; }
        public string MemberName { get; set; }
        public bool IsCheckedOut { get; set; }

        public IEnumerable<CheckedOut> CheckedOuts { get; set; }
    }
}
