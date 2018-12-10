using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClubAppProject.Models;

namespace BookClubAppProject.Abstract
{
    public interface IBookClubAppContext : IDisposable
    {
        DbSet<Book> Books { get; }
        DbSet<Review> Reviews { get; }
        DbSet<BookList> BookLists { get; }
        DbSet<Library> Libraries { get; }
        DbSet<BookClub> BookClubs { get; }
        //DbSet<BookClubMember> BookClubMembers { get; }

        int SaveChanges();
        void MarkAsModified(Object item);
    }
}
