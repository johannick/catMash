using System.Web.Mvc;

namespace CatMash.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ranking()
        {
            return View();
        }
    }
}
