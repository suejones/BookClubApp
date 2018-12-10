using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubAppProject.Models
{
    public enum GenreType { Fiction, NonFiction }
    public enum Genre { Thriller, Romance, Action, Sport, AutoBiography, History, Cookery, Health }


    public class Book
    {
        [Key]
        public int BookISBN { get; set; }

        [Required(ErrorMessage = "Book Title")]
        [StringLength(55, ErrorMessage = "Book Title cannot be longer than 55 characters.")]
        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }

        [Required(ErrorMessage = "Author Name")]
        [StringLength(55, ErrorMessage = "Author's Name cannot be longer than 55 characters.")]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }


        [Required(ErrorMessage = "Please select a Genre")]
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Please select the Genre type")]
        [Display(Name = "Genre Type")]
        public GenreType GenreType { get; set; }


        //public image BookCover {get;set;}


        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<BookList> BookLists { get; set; }
    }
}