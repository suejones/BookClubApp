using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookClubAppProject.Abstract;
using BookClubAppProject.DAL;
using BookClubAppProject.Models;

namespace BookClubAppProject.Controllers
{
[Authorize]
    public class BookController : Controller
    {
        //from Microsoft Docs - Mocking EF when Unit Testing Web API 2
        
        private IBookClubAppContext db = new BookClubAppContext();

        public BookController() { }
        public BookController(IBookClubAppContext context)
        {
            db = context;
        }
        
        // GET: Book
        public ActionResult Index(string bookTitleSearch, string authorNameSearch, int? bookListid)
        {

            var books = db.Books.AsQueryable();
            if(!String.IsNullOrEmpty(bookTitleSearch))
            {
                books = books.Where(t => t.BookTitle.Contains(bookTitleSearch));
                if (books.Count() == 0)
                {
                    TempData["message"] = string.Format("This book does not exist - continue to add to Database");
                    return RedirectToAction("Create");

                }
            }
           
            if (!String.IsNullOrEmpty(authorNameSearch))
            {
                books = books.Where(t => t.AuthorName.Contains(authorNameSearch));
                if (books.Count() == 0)
                {
                    TempData["message"] = string.Format("This Author does not exist - continue to add to Database");
                    return RedirectToAction("Create");

                }
            }

            ViewBag.BookListID = bookListid;
            return View(books.ToList());
        }


        ////Search
        //// GET: Book/Search
        //[AllowAnonymous]
        //public ActionResult Search(BookSearch searchBook)
        //{
        //    var result = db.Books.AsQueryable();
        //    if (searchBook != null)
        //    {
        //        if (!string.IsNullOrEmpty(searchBook.BookTitle))
        //        {
        //            result = result.Where(t => t.BookTitle.Contains(searchBook.BookTitle));
        //        }

        //        if (!string.IsNullOrEmpty(searchBook.AuthorName))
        //        {
        //            result = result.Where(t => t.AuthorName == searchBook.AuthorName);
        //        }

        //        /*if (searchBook.Genre.)
        //           {
        //             result = result.Where(t => t.Genre == searchBook.Genre);
        //            }
                
        //        if (searchBook.GenreType)
        //            {
        //               result = result.Where(t => t.GenreType == searchBook.GenreType);
        //            }
        //        */

        //            }
        //            return View("Index", result.OrderByDescending(t => t.AuthorName));



/* var books = from b in db.Books
         select b;
if(!String.IsNullOrEmpty(BookSearchModel))
{
 books = books.Where(k => k.BookTitle.Contains(BookSearchModel));
}
if (!String.IsNullOrEmpty(authorSearch))
{
 books = books.Where(k => k.AuthorName.Contains(authorSearch));
}
if(books.ToList().Count == 0)
{
 //google api search
 // or redirect to add..do you want to add book


 return RedirectToAction("Create");
 */
//}
    


     
        // GET: Book
       public ActionResult ViewAllBooks()
        {
            var books = db.Books.Include(b => b.BookTitle);
            return View(books.ToList());
        }
      

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            //ViewBag.BookTitle = new ChoiceList(db.Books, "BookISBN", "BookTitle", "AuthorName");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookISBN,BookTitle,AuthorName,Genre,GenreType")] Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(db.Books.Any(b=>b.BookTitle==book.BookTitle))
                    {
                        ModelState.AddModelError("", "Unable to create this Book. These Book Title already exists");
                    }
                    else
                    {
                        db.Books.Add(book);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
            }
            catch(DuplicateNameException)
            {
                ModelState.AddModelError("", "Unable to save changes. These Book Details already exist");
            }
            //ViewBag.BookName = new ChoiceList(db.Books, "BookISBN", "BookTitle", "AuthorName");


            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookISBN,BookTitle,AuthorName,Genre,GenreType")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.MarkAsModified(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}