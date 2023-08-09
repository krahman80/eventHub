using Microsoft.AspNet.Identity;
using starter_app.Models;
using starter_app.ViewModels;
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

        public ActionResult Index(string query = null)
        {

            var upcomingEvents = _context.Events
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .Where(e => e.DateTime > DateTime.Now && !e.isCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents
                    .Where(e =>
                    e.Artist.Name.Contains(query) ||
                    e.Genre.Name.Contains(query) ||
                    e.Venue.Contains(query));
            }
            var userId = User.Identity.GetUserId();
            var attendances = _context.Attendances
                .Where(u => u.AttendeeId == userId && u.Event.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(u => u.EventId);

            var viewModel = new HomeViewModel
            {
                UpcomingEvents = upcomingEvents,
                ShowActions = User.Identity.IsAuthenticated,
                SearchTerm = query,
                Attendances = attendances
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var eventInDB = _context.Events
                .Include(g => g.Genre)
                .Include(a => a.Artist)
                .SingleOrDefault(e => e.Id == id);

            if (eventInDB == null)
                return HttpNotFound();

            var viewModel = new EventDetailsViewModel()
            {
                Event = eventInDB
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.EventId == id && a.AttendeeId == userId);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Search(HomeViewModel viewModel)
        {
            return RedirectToAction("Index", "home", new { query = viewModel.SearchTerm });
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