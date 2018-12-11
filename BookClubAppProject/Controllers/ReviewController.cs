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
using Microsoft.AspNet.Identity;

namespace BookClubAppProject.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private IBookClubAppContext db = new BookClubAppContext();

        public ReviewController() { }
        public ReviewController(IBookClubAppContext context)
        {
            db = context;
        }

        // GET: Review
        public ActionResult Index()
        {
            IEnumerable<Review> reviewsToShow;
            if (User.IsInRole("BCServiceAdmin"))
            {
                reviewsToShow = db.Reviews.Include(r => r.Book);
            }
            else
            {
                string loggedUser = User.Identity.GetUserId();
                reviewsToShow = db.Reviews.Include(r => r.Book).Where(r => r.UserID == loggedUser);
            }


            return View(reviewsToShow.ToList());
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Review/Create
        public ActionResult Create(int? id) //bookISBN
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
            ViewBag.BookTitle = book.BookTitle;
            ViewBag.BookISBN = book.BookISBN;
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookISBN,Rating,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.UserID = User.Identity.GetUserId();
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Details", "Book", new { id = review.BookISBN });
            }

            //  ViewBag.BookTitle = new SelectList(db.Books, "BookISBN", "AuthorName", review.BookISBN);
            return View(review);
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookReview = new SelectList(db.Books, "BookTitle", "AuthorName", review.BookISBN);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookTitle,ReviewID,UserID,BookISBN,Rating,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.MarkAsModified(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookTitle = new SelectList(db.Books, "BookTitle", "AuthorName", review.BookISBN);
            return View(review);
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            if (!User.IsInRole("BCServiceAdmin") && review.UserID != User.Identity.GetUserId())
            {
                TempData["message"] = string.Format("You do not have authority to delete this review");
                return RedirectToAction("Index");
            }

            db.Reviews.Remove(review);
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
