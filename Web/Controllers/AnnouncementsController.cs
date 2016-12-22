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
    public class AnnouncementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: Announcements
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Lecturer))
            {
                return View("Index");
            }else
            {
                return View("IndexStudent");
            }
            
        }

        // GET: Announcements/Details/5
        public ActionResult Details(ViewedPost viewedPost,int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(
                x => x.Id == currentUserId);

            

            //if user is not a lecturer add it to the ViewwedPost db
            if (!User.IsInRole(RoleName.Lecturer))
            {
                var tmp = db.ViewedPosts.Any(x => x.AnnouncementId == id && x.User.Id == currentUserId);
        
                if (tmp)
                {
                    //do nothing the student is already in there
                }
                else
                {                   
                    announcement.User = currentUser;
                    viewedPost.User = currentUser;
                    viewedPost.AnnouncementId = (int)id;
                    db.ViewedPosts.Add(viewedPost);
                    db.SaveChanges();
                }

            }
           
            AnnouncementView AV = populateAnnouncementViewModel((int)id);
            return View(AV);
        }

        // GET: Announcements/Create
        [Authorize(Roles = RoleName.Lecturer)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = RoleName.Lecturer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,DateTime,Author,Content")] Announcement announcement)
        {
            if (User.IsInRole(RoleName.Lecturer))
            {
                if (ModelState.IsValid)
                {
                    //get user identity 
                    string currentUserId = User.Identity.GetUserId();
                    ApplicationUser currentUser = db.Users.FirstOrDefault(
                        x => x.Id == currentUserId);
                    announcement.User = currentUser;
                    //Setting the date automatically
                    announcement.DateTime = DateTime.Now;

                    db.Announcements.Add(announcement);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(announcement);
            }
            else
            {
                return RedirectToAction("NotAuthorised");
            }
        }


        // GET: Announcements/Edit/5
        [Authorize(Roles = RoleName.Lecturer)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            if (CanYou((int)id))
            {
                return View(announcement);
            }
            else
            {
                return View("NotAuthorised");
            }
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = RoleName.Lecturer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,DateTime,Author,Content")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        [Authorize(Roles = RoleName.Lecturer)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            if (CanYou((int)id))
            {
                return View(announcement);
            }
            else
            {
                return View("NotAuthorised");
            }
            
        }

        // POST: Announcements/Delete/5
        [Authorize(Roles = RoleName.Lecturer)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
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

        public ActionResult BuildAnnouncementTable()
        {
           
            if (User.IsInRole(RoleName.Lecturer))
            {
                return PartialView("_AnnouncementTable", db.Announcements.ToList());
            }
            else
            {
                //return table with limited access for students
                return PartialView("_AnnouncementTableStudent", db.Announcements.ToList());
            }
           
        }

        [Authorize(Roles = RoleName.Lecturer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "Id,Title,Author,Content")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                //get user identity 
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);
                announcement.User = currentUser;
                //Setting the date automatically
                announcement.DateTime = DateTime.Now;
                announcement.Author = currentUser.Name + " " + currentUser.Surname;

                db.Announcements.Add(announcement);
                db.SaveChanges();

            }

            return PartialView("_AnnouncementTable", db.Announcements.ToList());
        }

        //populate announcements with comments
        public AnnouncementView populateAnnouncementViewModel(int id)
        {
            AnnouncementView AV = new AnnouncementView();
            int currentAnnouncementId = id;
            AV.Announcement = db.Announcements.FirstOrDefault(x => x.Id == id);
            AV.Comments = db.Comments.Where(x => x.AnnouncementId == currentAnnouncementId).ToList();
            return AV;
        }
       
        private IEnumerable<Announcement> GetMyAnnouncements()
        {
            //get user identity 
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(
                x => x.Id == currentUserId);
            return db.Announcements.ToList().Where(x => x.User == currentUser);
        }


        //generate the list of users that have seen the announcement
        [Authorize(Roles = RoleName.Lecturer)]
        public ActionResult SeenList(ViewedPost viewedPost, int id)
        {
            if (ModelState.IsValid)
            {
                return View("SeenList", GetSeen(id));
            }
           
            return View("SeenList", GetSeen(id));
        }

        public bool CanYou(int id)
        {
            if (GetMyAnnouncements().Any(x => x.Id == id))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private IEnumerable<ViewedPost> GetSeen(int id)
        {

            IEnumerable<ViewedPost> seenPosts = db.ViewedPosts.Where(x => x.AnnouncementId == id).ToList();
            IEnumerable<ApplicationUser> students = db.Users.ToList(); //Where(x=> x.Roles.Equals(RoleName.Lecturer)).ToList();
            int counter = 0;
            foreach (ApplicationUser student in students)
            {
                if(seenPosts.Any(x => x.User.Id == student.Id))
                {
                    counter++;
                }
            }

            ViewBag.Percent = Math.Round(100f * ((float)counter / (float)students.Count()));

            return seenPosts;
        }

    }
}
