using System.Web.Mvc;

namespace starter_app.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        //GET: Events/New
        public ActionResult New()
        {
            return View();
        }
    }
}