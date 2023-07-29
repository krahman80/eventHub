using Microsoft.AspNet.Identity;
using starter_app.Dtos;
using starter_app.Models;
using System.Linq;
using System.Web.Http;

namespace starter_app.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        public ApplicationDbContext _context { get; set; }

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(a => a.FollowerId == userId && a.FolloweeId == dto.FolloweeId))
                return BadRequest("the Attendance already exist");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
