using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubAppProject.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public string UserID { get; set; }

        [Required(ErrorMessage = "Please enter the Book Title for the Book you wish to Add or View review.")]
        [Display(Name = "Book Title")]
        public int BookISBN { get; set; }

        //navigation key
        public virtual Book Book { get; set; }

        [Required(ErrorMessage = "Please enter a Rating")]
        [Range(1, 10)]
        public int Rating { get; set; }

        [StringLength(200, ErrorMessage = "Comment can only be 200 characters.")]
        public string Comment { get; set; }
    }
}