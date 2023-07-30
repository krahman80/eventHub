using Microsoft.AspNet.Identity;
using starter_app.Models;
using starter_app.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace starter_app.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET: Events/New
        [Authorize]
        public ActionResult New()
        {
            var viewModel = new EventFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add Event"
            };
            return View("EventForm", viewModel);
        }

        //GET: Events/New
        [Authorize]
        public ActionResult Edit(int Id)
        {
            var userId = User.Identity.GetUserId();
            var data = _context.Events.Single(e => e.Id == Id && e.ArtistId == userId);

            var viewModel = new EventFormViewModel
            {
                Id = data.Id,
                Genres = _context.Genres.ToList(),
                Date = data.DateTime.ToString("d MMM yyyy"),
                Time = data.DateTime.ToString("HH:mm"),
                Venue = data.Venue,
                GenreId = data.GenreId,
                Heading = "Edit Event"
            };

            return View("EventForm", viewModel);
        }

        //POST: Events/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("EventForm", viewModel);
            }

            var newEvent = new Event
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.GenreId,
                Venue = viewModel.Venue,
            };

            _context.Events.Add(newEvent);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }

        //POST: Events/Update
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("EventForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var eventInDB = _context.Events.Single(e => e.Id == viewModel.Id && e.ArtistId == userId);
            eventInDB.Venue = viewModel.Venue;
            eventInDB.DateTime = viewModel.GetDateTime();
            eventInDB.GenreId = viewModel.GenreId;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var events = _context.Attendances
                .Where(e => e.AttendeeId == userId)
                .Select(e => e.Event)
                .Include(g => g.Genre)
                .Include(a => a.Artist)
                .ToList();

            var viewModel = new EventViewModel
            {
                AttendingEvent = events,
                ShowActions = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var events = _context.Events
                .Where(e => e.ArtistId == userId && e.DateTime > DateTime.Now)
                .Include(g => g.Genre).ToList();

            return View(events);
        }
    }
}