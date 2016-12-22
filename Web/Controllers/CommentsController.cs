using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View(GetMyComments());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Author,Content")] Comment comment, int? id)
        {
            if(id == null)
            {
                RedirectToAction("Index", "Announcements");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string currentUserId = User.Identity.GetUserId();
                    ApplicationUser currentUser = db.Users.FirstOrDefault(
                        x => x.Id == currentUserId);

                    comment.User = currentUser;
                    comment.DateTime = DateTime.Now;
                    comment.Author = currentUser.Name + " " + currentUser.Surname;
                    comment.AnnouncementId = (int)id;

                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Details/" + (id.ToString()), "Announcements");
                }
            }


            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(
                x => x.Id == currentUserId);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            if (comment.User == currentUser)
            {
                return View(comment);
            }
            else
            {
                return View("NotAuthorised");
            }
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,DateTime,Author,Content,AnnouncementId")] Comment comment)
        {
            

            if (ModelState.IsValid)
            {

                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }

                return View(comment);

        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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

        private IEnumerable<Comment> GetMyComments()
        {
            //get user identity 
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(
                x => x.Id == currentUserId);
            return db.Comments.ToList().Where(x => x.User == currentUser);
        }
        //Ajax used to create comments
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate([Bind(Include = "CommentId,Author,Content")] Comment comment, int id)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);

                comment.User = currentUser;
                comment.DateTime = DateTime.Now;
                comment.Author = currentUser.Name + " " + currentUser.Surname;
                comment.AnnouncementId = id;

                db.Comments.Add(comment);
                db.SaveChanges();
                //return RedirectToAction("Details/" + (id.ToString()), "Announcements");
                //return RedirectToAction("_CommentTable", db.Comments.Where(x => x.AnnouncementId == id).ToList());
            }

            //return View(comment);
            return PartialView("_CommentTable", db.Comments.Where(x => x.AnnouncementId == id).ToList());
        }

        public ActionResult BuildCommentTable()
        {
            /*
            //get user identity 
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(
                x => x.Id == currentUserId);
               
            */
            return PartialView("_CommentTable", db.Comments.ToList());

        }
    }
}
