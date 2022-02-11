using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMVC.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        #nullable enable
        public Member? CheckedOutTo { get; set; }
        #nullable disable
    }
}
