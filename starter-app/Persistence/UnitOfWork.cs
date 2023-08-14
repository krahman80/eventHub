using starter_app.Models;
using starter_app.Repositories;

namespace starter_app.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public EventRepository Events { get; private set; }
        public AttendanceRepository Attendances { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Events = new EventRepository(_context);
            Attendances = new AttendanceRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}