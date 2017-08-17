using Microsoft.AspNet.Identity;
using SAS_LMS.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SAS_LMS.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Courses
        public ActionResult Index()
        {
            if (User.IsInRole("Teacher"))
            {
                IQueryable<Course> courses;
                courses = db.Courses.Where(c => c.EndDate > DateTime.Now)
                                    .OrderBy(c => c.StartDate);
                if (courses.Count() == 0)
                    ViewBag.Error = "Sorry!!! There are no upcoming courses";
                return View(courses.ToList());
            }
            else
            {
                ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
                Course course = db.Courses.Find(user.CourseId);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Details", new { id = course.Id });
            }

        }


        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Courses/CourseHistory
        public ActionResult CourseHistory()
        {

            IQueryable<Course> courses;
            courses = db.Courses.Where(c => c.EndDate < DateTime.Now);
            if (courses.Count() == 0)
                ViewBag.Error = "Sorry!!! There are no courses which has ended";
            return View(courses.ToList());
        }


        // View Course Details of Finished Courses
        public ActionResult CourseHistoryView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {

            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }


        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = course.Id });
            }
            return View(course);
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Courses/ViewStudent


        public ActionResult ViewStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        [HttpGet]
        public ActionResult UploadDocument(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost]
        public ActionResult UploadDocument(HttpPostedFileBase file, int? id, string DocDesc)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    Document document = new Models.Document();
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                    ViewBag.Message = "File Uploaded Successfully!!";
                    //document.DocPath = _path;
                    document.Description = DocDesc;
                    document.DocName = _FileName;
                    document.CourseId = id;
                    db.Documents.Add(document);
                    db.SaveChanges();
                }
                Course course = db.Courses.Find(id);
                return View(course);
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                Course course = db.Courses.Find(id);
                return View(course);
            }
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
