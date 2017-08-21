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
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents for Course
        public ActionResult CourseDocIndex(int id)
        {
            Course Courses;
            Courses = db.Courses.Find(id);
            return View(Courses);
        }

        // GET: Documents for Module
        public ActionResult ModuleDocIndex(int id)
        {
            Module Module;
            Module = db.Modules.Find(id);
            return View(Module);
        }

        // GET: Documents for Activities
        public ActionResult ActivitiesDocIndex(int id)
        {
            Activity Activity;
            Activity = db.Activities.Find(id);
            return View(Activity);
        }

        // GET: Documents
        public ActionResult Index(int id)
        {
            IQueryable<Document> documents;
            documents = db.Documents.Where(d => d.CourseId == id);
            ViewBag.ID = id;
            return View(documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }


        // GET: Documents
        public ActionResult StudentDocuments(int id)
        {
            IQueryable<Document> documents;
            documents = db.Documents.Where(d => d.CourseId == id);
            ViewBag.ID = id;
            return View(documents.ToList());
        }




        // GET: Documents/Create/Course
        public ActionResult Create(int id)
        {
            Course course;
            course = db.Courses.Find(id);
            ViewBag.CourseName = course.Name;
            ViewBag.ID = id;
            return View();
        }

        // POST: Documents/Create/Course
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string DocName, string Description, int id, HttpPostedFileBase file)
        {
            Document document = new Document();
            if (Request.Files != null && Request.Files.Count == 1)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, file.ContentLength);
                    document.file = content;
                    string extension = Path.GetExtension(file.FileName);
                    document.CourseId = id;
                    document.DocName = DocName + extension;
                    document.Description = Description;
                    document.CreatedDate = DateTime.Now;
                    document.CreatedBy = User.Identity.Name;
                    db.Documents.Add(document);
                    db.SaveChanges();
                    return RedirectToAction("CourseDocIndex", new { id = id });
                }
            }

            return RedirectToAction("Create");

        }


        // GET: Documents/Create/Module
        public ActionResult ModuleDocCreate(int id)
        {
            Module Module;
            Module = db.Modules.Find(id);
            ViewBag.ModuleName = Module.Name;
            ViewBag.ID = id;
            return View();
        }

        // POST: Documents/Create/Course
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModuleDocCreate(string DocName, string Description, int id, HttpPostedFileBase file)
        {
            Document document = new Document();
            if (Request.Files != null && Request.Files.Count == 1)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, file.ContentLength);
                    document.file = content;
                    string extension = Path.GetExtension(file.FileName);
                    document.ModuleId = id;
                    document.DocName = DocName + extension;
                    document.Description = Description;
                    document.CreatedDate = DateTime.Now;
                    document.CreatedBy = User.Identity.Name;
                    db.Documents.Add(document);
                    db.SaveChanges();
                    return RedirectToAction("ModuleDocIndex", new { id = id });
                }
            }

            return RedirectToAction("ModuleDocCreate");

        }

        // GET: Documents/Create/Activity
        public ActionResult ActivityDocCreate(int id)
        {
            Activity Activity;
            Activity = db.Activities.Find(id);
            ViewBag.ActivityName = Activity.Name;
            ViewBag.ID = id;
            ViewBag.ActivityType = Activity.ActivityType.Name;
            return View();
        }

        // POST: Documents/Create/Activity
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivityDocCreate(string DocName, string Description, int id, HttpPostedFileBase file, DateTime? SubmitBy)
        {
            Document document = new Document();
            if (Request.Files != null && Request.Files.Count == 1)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, file.ContentLength);
                    document.file = content;
                    string extension = Path.GetExtension(file.FileName);
                    document.ActivityId = id;
                    document.DocName = DocName + extension;
                    document.Description = Description;
                    document.CreatedDate = DateTime.Now;
                    document.CreatedBy = User.Identity.Name;
                    document.SubmitBy = SubmitBy;
                    db.Documents.Add(document);
                    db.SaveChanges();
                    return RedirectToAction("ActivitiesDocIndex", new { id = id });
                }
            }

            return RedirectToAction("ActivityDocCreate");

        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DocName,Description,file,CourseId,ModuleId,ActivityId")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            var DocCourseId = document.CourseId;
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("CourseDocIndex", new { id = DocCourseId });
        }

        //Delect the Module Documents

        // GET: Documents/Delete/5
        public ActionResult DeleteModuleDoc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }


        // POST: Documents/Delete/5
        [HttpPost, ActionName("DeleteModuleDoc")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteModuleDocConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            var DocModuleId = document.ModuleId;
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("ModuleDocIndex", new { id = DocModuleId });
        }


        //Delect the Activities Documents

        // GET: Documents/Delete/5
        public ActionResult DeleteActivityDoc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("DeleteActivityDoc")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteActivityDocConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            var DocActivityId = document.ActivityId;
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("ActivitiesDocIndex", new { id = DocActivityId });
        }





        public FileResult DownloadFile(int id)
        {
            var document = db.Documents.Find(id);
            string fileName = document.DocName;
            return File(document.file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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
