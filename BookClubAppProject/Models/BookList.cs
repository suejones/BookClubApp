using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubAppProject.Models
{
    public class BookList
    {
        public BookList()
        {
            Books = new List<Book>();
        }
        [Key]
        public int BookListID { get; set; }

        [Required(ErrorMessage = "Indicate Book List Name.")]
        [StringLength(55, ErrorMessage = "Book Club List cannot be longer than 25 characters.")]
        [Display(Name = "Book List Name")]
        public string BookListName { get; set; }

        [Required]
        [Display(Name = "Book List Type")] /* enum?*/
        public string BookListType { get; set; }

        //[Required(ErrorMessage = "This BookList contains....")]
        //[Display(Name = "BookClub Content")]
        //public string BookListContent { get; set; }

        //not linking at this stage

        //navigation key
        public int BookClubID { get; set; }
        public virtual BookClub BookClub { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}
   