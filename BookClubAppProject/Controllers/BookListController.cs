using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookClubAppProject.Abstract;
using BookClubAppProject.DAL;
using BookClubAppProject.Models;

namespace BookClubAppProject.Controllers
{
        [Authorize]
        public class BookListController : Controller
        {
            private IBookClubAppContext db = new BookClubAppContext();

            public BookListController() { }
            public BookListController(IBookClubAppContext context)
            {
                db = context;
            }

            // GET: BookList
            public ActionResult Index()
            {
                return View(db.BookLists.ToList());
            }

            // GET: BookList/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BookList bookList = db.BookLists.Find(id);
                if (bookList == null)
                {
                    return HttpNotFound();
                }
                return View(bookList);
            }

            // GET: BookList/Create
            public ActionResult Create(int? id) //bookclubid
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BookClub bookClub = db.BookClubs.Find(id);
                if (bookClub == null)
                {
                    return HttpNotFound();
                }
                ViewBag.BookClubID = bookClub.BookClubID;
                return View();
            }

            // POST: BookList/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "BookListID,BookListName,BookListType,BookListContent,BookClubID")] BookList bookList)
            {
                if (ModelState.IsValid)
                {
                    db.BookLists.Add(bookList);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = bookList.BookListID });
                }
                ViewBag.BookClubID = bookList.BookClubID;

                return View(bookList);
            }

            public ActionResult BookToList(int? id, int? id2)
            {
                if (id == null || id2 == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Book book = db.Books.Find(id);
                BookList bookList = db.BookLists.Find(id2);
                if (book == null || bookList == null)
                {
                    return HttpNotFound();
                }

                bookList.Books.Add(book);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = id2 });
            }


            // GET: BookList/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BookList bookList = db.BookLists.Find(id);
                if (bookList == null)
                {
                    return HttpNotFound();
                }
                return View(bookList);
            }

            // POST: BookList/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "BookListID,BookListName,BookListType,BookListContent,BookClubID")] BookList bookList)
            {
                if (ModelState.IsValid)
                {
                    db.MarkAsModified(bookList);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(bookList);
            }

            // GET: BookList/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BookList bookList = db.BookLists.Find(id);
                if (bookList == null)
                {
                    return HttpNotFound();
                }
                return View(bookList);
            }

            // POST: BookList/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                BookList bookList = db.BookLists.Find(id);
                db.BookLists.Remove(bookList);
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
