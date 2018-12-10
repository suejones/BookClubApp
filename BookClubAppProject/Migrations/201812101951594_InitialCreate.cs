namespace BookClubAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookClub",
                c => new
                    {
                        BookClubID = c.Int(nullable: false, identity: true),
                        BookClubName = c.String(nullable: false, maxLength: 55),
                        AdminEmail = c.String(nullable: false, maxLength: 55),
                        Profile = c.String(nullable: false, maxLength: 250),
                        Status = c.Int(nullable: false),
                        Province = c.Int(nullable: false),
                        County = c.String(nullable: false),
                        Area = c.String(nullable: false),
                        LibraryID = c.Int(nullable: false),
                        NextMeeting = c.String(nullable: false),
                        CurrentRead = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BookClubID)
                .ForeignKey("dbo.Library", t => t.LibraryID, cascadeDelete: true)
                .Index(t => t.LibraryID);
            
            CreateTable(
                "dbo.BookList",
                c => new
                    {
                        BookListID = c.Int(nullable: false, identity: true),
                        BookListName = c.String(nullable: false, maxLength: 55),
                        BookListType = c.String(nullable: false),
                        BookClubID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookListID)
                .ForeignKey("dbo.BookClub", t => t.BookClubID, cascadeDelete: true)
                .Index(t => t.BookClubID);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookISBN = c.Int(nullable: false, identity: true),
                        BookTitle = c.String(nullable: false, maxLength: 55),
                        AuthorName = c.String(nullable: false, maxLength: 55),
                        Genre = c.Int(nullable: false),
                        GenreType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookISBN);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        BookISBN = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Book", t => t.BookISBN, cascadeDelete: true)
                .Index(t => t.BookISBN);
            
            CreateTable(
                "dbo.Library",
                c => new
                    {
                        LibraryID = c.Int(nullable: false, identity: true),
                        LibraryName = c.String(nullable: false, maxLength: 55),
                        LibraryEmail = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        County = c.String(nullable: false),
                        Area = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LibraryID);
            
            CreateTable(
                "dbo.BooksInBookList",
                c => new
                    {
                        BookISBN = c.Int(nullable: false),
                        BookListID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookISBN, t.BookListID })
                .ForeignKey("dbo.Book", t => t.BookISBN, cascadeDelete: true)
                .ForeignKey("dbo.BookList", t => t.BookListID, cascadeDelete: true)
                .Index(t => t.BookISBN)
                .Index(t => t.BookListID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookClub", "LibraryID", "dbo.Library");
            DropForeignKey("dbo.Review", "BookISBN", "dbo.Book");
            DropForeignKey("dbo.BooksInBookList", "BookListID", "dbo.BookList");
            DropForeignKey("dbo.BooksInBookList", "BookISBN", "dbo.Book");
            DropForeignKey("dbo.BookList", "BookClubID", "dbo.BookClub");
            DropIndex("dbo.BooksInBookList", new[] { "BookListID" });
            DropIndex("dbo.BooksInBookList", new[] { "BookISBN" });
            DropIndex("dbo.Review", new[] { "BookISBN" });
            DropIndex("dbo.BookList", new[] { "BookClubID" });
            DropIndex("dbo.BookClub", new[] { "LibraryID" });
            DropTable("dbo.BooksInBookList");
            DropTable("dbo.Library");
            DropTable("dbo.Review");
            DropTable("dbo.Book");
            DropTable("dbo.BookList");
            DropTable("dbo.BookClub");
        }
    }
}
