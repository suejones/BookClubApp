using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubAppProject.Models
{
    public class Library
    {
        [Key]
        public int LibraryID { get; set; }

        [Required(ErrorMessage = "Indicate Library Name.")]
        [StringLength(55, ErrorMessage = "Library Name cannot be longer than 25 characters.")]
        [Display(Name = "Library Name")]
        public string LibraryName { get; set; }

        [Required]
        [Display(Name = "Library email address")]
        public string LibraryEmail { get; set; }

        [Required(ErrorMessage = "What province is your Library in?")]
        public string Province { get; set; }

        [Required(ErrorMessage = "What County is your Library in?")]
        public string County { get; set; }


        [Required(ErrorMessage = "What Area within your County is your Library in?")]
        public string Area { get; set; }

        //navigation key
        public virtual IEnumerable<BookClub> BookClubs { get; set; }

    }
}
