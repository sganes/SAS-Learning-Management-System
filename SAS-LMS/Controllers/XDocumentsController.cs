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
    public class XDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Documents for Course
        //public ActionResult CourseDocIndex(int id)
        //{
        //    Course Courses;
        //    Courses = db.Courses.Find(id);
        //    return View(Courses);
        //}

        //// GET: Documents for Module
        //public ActionResult ModuleDocIndex(int id)
        //{
        //    Module Module;
        //    Module = db.Modules.Find(id);
        //    return View(Module);
        //}

        //// GET: Documents for Activities
        //public ActionResult ActivitiesDocIndex(int id)
        //{
        //    Activity Activity;
        //    Activity = db.Activities.Find(id);
        //    return View(Activity);
        //}

        // GET: Documents
        public ActionResult Index(int? CourseId, int? ActivityId, int? ModuleId)
        {
            IQueryable<Document> documents;

            if (CourseId != null)
            {
                Course course = db.Courses.Find(CourseId);
                ViewBag.Name = course.Name;
                ViewBag.Id = CourseId;
                ViewBag.DocumentFor = "Course";
                documents = db.Documents.Where(d => d.CourseId == CourseId);
                return View(documents.ToList());
            }
            if (ModuleId != null)
            {
                Module Module = db.Modules.Find(ModuleId);
                ViewBag.Name = Module.Name;
                ViewBag.Id = ModuleId;
                ViewBag.DocumentFor = "Module";
                ViewBag.ModuleCourseId = Module.CourseId;
                documents = db.Documents.Where(d => d.ModuleId == ModuleId);
                return View(documents.ToList());
            }
            if (ActivityId != null)
            {
                Activity Activity = db.Activities.Find(ActivityId);
                ViewBag.Name = Activity.Name;
                ViewBag.Id = ActivityId;
                ViewBag.DocumentFor = "Activity";
                ViewBag.ActivityType = Activity.ActivityType.Name;
                ViewBag.ActivityModuleId = Activity.ModuleId;
                documents = db.Documents.Where(d => d.ActivityId == ActivityId);
                return View(documents.ToList());
            }

            return View();
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


        // GET: Course Documents for Students
        public ActionResult StudentsCourseDocuments(int id)
        {
            IQueryable<Document> documents;
            documents = db.Documents.Where(d => d.CourseId == id);
            ViewBag.ID = id;
            return View(documents.ToList());
        }

        // GET: Module Documents for Students
        public ActionResult StudentsModuleDocuments(int id)
        {
            IQueryable<Document> documents;
            documents = db.Documents.Where(d => d.ModuleId == id);
            ViewBag.ID = id;
            return View(documents.ToList());
        }

        // GET: Activity Documents for Students
        public ActionResult StudentsActivityDocuments(int id)
        {
            IQueryable<Document> documents;
            documents = db.Documents.Where(d => d.ActivityId == id);
            ViewBag.ID = id;
            return View(documents.ToList());
        }


        // GET: Documents/Create/Course
        public ActionResult Create(int? CourseId, int? ModuleId, int? ActivityId)
        {
            if (CourseId != null)
            {
                Course course = db.Courses.Find(CourseId);
                ViewBag.Name = course.Name;
            }
            if (ModuleId != null)
            {
                Module Module = db.Modules.Find(ModuleId);
                ViewBag.Name = Module.Name;
            }
            if (ActivityId != null)
            {
                Activity Activity = db.Activities.Find(ActivityId);
                ViewBag.Name = Activity.Name;
            }

            var document = new Document { CourseId = CourseId, ModuleId = ModuleId, ActivityId = ActivityId };

            return View(document);
        }

        // POST: Documents/Create/Course
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string DocName, string Description, int? CourseId, int? ModuleId, int? ActivityId, HttpPostedFileBase file)
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
                    document.CourseId = CourseId;
                    document.ModuleId = ModuleId;
                    document.ActivityId = ActivityId;
                    document.DocName = DocName + extension;
                    document.Description = Description;
                    document.CreatedDate = DateTime.Now;
                    document.CreatedBy = User.Identity.Name;
                    db.Documents.Add(document);
                    db.SaveChanges();
                    if (CourseId != null) return RedirectToAction("Index", new { CourseId = CourseId });
                    if (ModuleId != null) return RedirectToAction("Index", new { ModuleId = ModuleId });
                    if (ActivityId != null) return RedirectToAction("Index", new { ActivityId = ActivityId });
                }
            }

            return RedirectToAction("Create");

        }


        //// GET: Documents/Create/Module
        //public ActionResult ModuleDocCreate(int id)
        //{
        //    Module Module;
        //    Module = db.Modules.Find(id);
        //    ViewBag.ModuleName = Module.Name;
        //    ViewBag.ID = id;
        //    return View();
        //}

        // POST: Documents/Create/Course
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ModuleDocCreate(string DocName, string Description, int id, HttpPostedFileBase file)
        //{
        //    Document document = new Document();
        //    if (Request.Files != null && Request.Files.Count == 1)
        //    {
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            var content = new byte[file.ContentLength];
        //            file.InputStream.Read(content, 0, file.ContentLength);
        //            document.file = content;
        //            string extension = Path.GetExtension(file.FileName);
        //            document.ModuleId = id;
        //            document.DocName = DocName + extension;
        //            document.Description = Description;
        //            document.CreatedDate = DateTime.Now;
        //            document.CreatedBy = User.Identity.Name;
        //            db.Documents.Add(document);
        //            db.SaveChanges();
        //            return RedirectToAction("ModuleDocIndex", new { id = id });
        //        }
        //    }

        //    return RedirectToAction("ModuleDocCreate");

        //}

        //// GET: Documents/Create/Activity
        //public ActionResult ActivityDocCreate(int id)
        //{
        //    Activity Activity;
        //    Activity = db.Activities.Find(id);
        //    ViewBag.ActivityName = Activity.Name;
        //    ViewBag.ID = id;
        //    ViewBag.ActivityType = Activity.ActivityType.Name;
        //    return View();
        //}

        //// POST: Documents/Create/Activity
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ActivityDocCreate(string DocName, string Description, int id, HttpPostedFileBase file, DateTime? SubmitBy)
        //{
        //    Document document = new Document();
        //    if (Request.Files != null && Request.Files.Count == 1)
        //    {
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            var content = new byte[file.ContentLength];
        //            file.InputStream.Read(content, 0, file.ContentLength);
        //            document.file = content;
        //            string extension = Path.GetExtension(file.FileName);
        //            document.ActivityId = id;
        //            document.DocName = DocName + extension;
        //            document.Description = Description;
        //            document.CreatedDate = DateTime.Now;
        //            document.CreatedBy = User.Identity.Name;
        //            document.SubmitBy = SubmitBy;
        //            db.Documents.Add(document);
        //            db.SaveChanges();
        //            return RedirectToAction("ActivitiesDocIndex", new { id = id });
        //        }
        //    }

        //    return RedirectToAction("ActivityDocCreate");

        //}

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
            var courseid = document.CourseId;
            var moduleid = document.ModuleId;
            var activityid = document.ActivityId;
            db.Documents.Remove(document);
            db.SaveChanges();
            if (courseid != null)
            {
                return RedirectToAction("Index", new { CourseId = courseid });
            }
            else if (moduleid != null)
            {
                return RedirectToAction("Index", new { ModuleId = moduleid });
            }
            else
            {
                return RedirectToAction("Index", new { ActivityId = activityid });
            }
        }


        ////Delect the Module Documents

        //// GET: Documents/Delete/5
        //public ActionResult DeleteModuleDoc(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Document document = db.Documents.Find(id);
        //    if (document == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(document);
        //}


        //// POST: Documents/Delete/5
        //[HttpPost, ActionName("DeleteModuleDoc")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteModuleDocConfirmed(int id)
        //{
        //    Document document = db.Documents.Find(id);
        //    var DocModuleId = document.ModuleId;
        //    db.Documents.Remove(document);
        //    db.SaveChanges();
        //    return RedirectToAction("ModuleDocIndex", new { id = DocModuleId });
        //}


        ////Delect the Activities Documents

        //// GET: Documents/Delete/5
        //public ActionResult DeleteActivityDoc(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Document document = db.Documents.Find(id);
        //    if (document == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(document);
        //}

        //// POST: Documents/Delete/5
        //[HttpPost, ActionName("DeleteActivityDoc")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteActivityDocConfirmed(int id)
        //{
        //    Document document = db.Documents.Find(id);
        //    var DocActivityId = document.ActivityId;
        //    db.Documents.Remove(document);
        //    db.SaveChanges();
        //    return RedirectToAction("ActivitiesDocIndex", new { id = DocActivityId });
        //}

        //public FileResult DownloadFile(int id)
        //{
        //    var document = db.Documents.Find(id);
        //    string fileName = document.DocName;
        //    return File(document.file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}



        public ActionResult SubmissionUploadFile(int id)
        {
            Activity Activity;
            Activity = db.Activities.Find(id);
            ViewBag.ActivityName = Activity.Name;
            ViewBag.ID = id;
            ViewBag.ActivityType = Activity.ActivityType.Name;
            return View();

        }

        // POST: Documents/Create/Course
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmissionUploadFile(string DocName, string Description, int ActivityId, HttpPostedFileBase file)
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
                    document.ActivityId = ActivityId;
                    document.DocName = DocName + extension;
                    document.Description = Description;
                    document.CreatedDate = DateTime.Now;
                    document.CreatedBy = User.Identity.Name;
                    db.Documents.Add(document);
                    db.SaveChanges();
                    Activity activity;
                    activity = db.Activities.Find(ActivityId);
                    return RedirectToAction("CourseHistoryView", "Courses", new { id = activity.Module.Course.Id });
                }
            }

            return RedirectToAction("Create");

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
