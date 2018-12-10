using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using BookClubAppProject.Abstract;
using BookClubAppProject.Models;

namespace BookClubAppProject.DAL
{
        public class BookClubAppContext : DbContext , IBookClubAppContext
        {
            public BookClubAppContext() : base("BookClubAppContext")
            {
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContext, EF6Console.Migrations.Configuration>());

            }

            public DbSet<Book> Books { get; set; }
            public DbSet<Review> Reviews { get; set; }
            public DbSet<BookList> BookLists { get; set; }
            public DbSet<Library> Libraries { get; set; }
            public DbSet<BookClub> BookClubs { get; set; }
            //public DbSet<BookClubMember> BookClubMembers { get; set; }
            //public DbSet<Role> Roles { get; set; }
           // public static IEnumerable<object> Roles { get; internal set; }

            //to prevent EF to pluralize table names
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

                modelBuilder.Entity<Book>()
                     .HasMany(c => c.BookLists).WithMany(i => i.Books)
                     .Map(t => t.MapLeftKey("BookISBN")
                         .MapRightKey("BookListID")
                         .ToTable("BooksInBookList"));
                /*modelBuilder.Entity<BookList>()
                       .HasOptional(p => p.BookClub).WithRequired(p => p.BookList);       
                */
            }


            public void MarkAsModified(Object item)
            {
                Entry(item).State = EntityState.Modified;
            }


        }
    }
