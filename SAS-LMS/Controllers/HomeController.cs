using System.Web.Mvc;

namespace SAS_LMS.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Courses");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The SAS - Learning Management System.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}