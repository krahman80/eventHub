using Microsoft.AspNet.Identity;
using starter_app.Models;
using starter_app.ViewModels;
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
            };
            return View(viewModel);
        }

        //POST: Events/Save
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("New", viewModel);
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

            return RedirectToAction("Index", "Home");
        }
    }
}