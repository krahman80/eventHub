using starter_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace starter_app.Repositories
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendance(string userId)
        {
            return _context.Attendances
                            .Where(u => u.AttendeeId == userId && u.Event.DateTime > DateTime.Now)
                            .ToList();
        }

    }
}