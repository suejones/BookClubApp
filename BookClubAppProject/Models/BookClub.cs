using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookClubAppProject.Models
{
    public enum province { Leinster, Munster, Connaught, Ulster }
    public enum status { Open, Closed }

    public class BookClub
    {
        [Key]
        public int BookClubID { get; set; }

        [Required(ErrorMessage = "Indicate Book Club Name.")]
        [StringLength(55, ErrorMessage = "Book Club Name cannot be longer than 25 characters.")]
        [Display(Name = "Book Club Name")]
        public string BookClubName { get; set; }

        [Required]
        [Display(Name = "BookClub Admin email address")]
        [StringLength(55, ErrorMessage = "Book Club email address cannot be longer than 25 characters.")]
        public string AdminEmail { get; set; }

        [Required(ErrorMessage = "Tell us about your Book Club....")]
        [StringLength(250, ErrorMessage = "Book Club Profile can be as long as 250 characters.")]
        [Display(Name = "BookClub Profile")]
        public string Profile { get; set; }

        [Required(ErrorMessage = "Is your BookClub Open or Closed to new members")]
        [Display(Name = "Status")]
        public status Status { get; set; }

        [Required(ErrorMessage = "What province is your Book Club in?")]
        [Display(Name = "Province")]
        public province Province { get; set; }

        [Required(ErrorMessage = "What County is your Book Club in?")]
        public string County { get; set; }


        [Required(ErrorMessage = "What Area within your County is your Book Club in?")]
        public string Area { get; set; }

        [Required(ErrorMessage = "Select the local Library linked with your Book Club")]
        [Display(Name = "Library")]
        public int LibraryID { get; set; }

        [Required(ErrorMessage = "Set the Next Meeting Details for your Book Club")]
        [Display(Name = "Next Meeting")]
        public string NextMeeting { get; set; }

        [Required(ErrorMessage = "Set the Current Read for your Book Club")]
        [Display(Name = "Current Read")]
        public string CurrentRead { get; set; }

        //[Required(ErrorMessage = "List last 10 books readby your Book Club")]
        //public IList ReadList { get; set; }

        //public virtual ICollection<BookClubMember> BookClubMembers { get; set; }
        public virtual Library Library { get; set; }
        public virtual ICollection<BookList> BookLists { get; set; }
    }
}