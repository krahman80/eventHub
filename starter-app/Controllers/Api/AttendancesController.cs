using Microsoft.AspNet.Identity;
using starter_app.Dtos;
using starter_app.Models;
using System.Linq;
using System.Web.Http;

namespace starter_app.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context { get; set; }

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        // /api/Attendances/Attend
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.EventId == dto.EventId))
                return BadRequest("the Attendance already exist");

            var attendance = new Attendance()
            {
                EventId = dto.EventId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
