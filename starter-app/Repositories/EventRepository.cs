using starter_app.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace starter_app.Repositories
{
    public class EventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetEventsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(e => e.AttendeeId == userId)
                .Select(e => e.Event)
                .Include(g => g.Genre)
                .Include(a => a.Artist)
                .ToList();
        }
    }
}