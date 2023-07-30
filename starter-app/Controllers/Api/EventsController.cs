using Microsoft.AspNet.Identity;
using starter_app.Models;
using System.Linq;
using System.Web.Http;

namespace starter_app.Controllers.Api
{
    [Authorize]
    public class EventsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var eventInDB = _context.Events.Single(e => e.Id == id && e.ArtistId == userId);
            if (eventInDB.isCanceled)
                return NotFound();

            eventInDB.isCanceled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
