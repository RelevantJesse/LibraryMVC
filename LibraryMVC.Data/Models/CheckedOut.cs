using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMVC.Data.Models
{
    public class CheckedOut
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Member Member { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
