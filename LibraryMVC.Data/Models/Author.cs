using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }  
    }
}