using starter_app.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace starter_app.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingEvent = _context.Events
                .Include(e => e.Artist)
                .Where(e => e.DateTime > DateTime.Now);

            return View(upcomingEvent);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}