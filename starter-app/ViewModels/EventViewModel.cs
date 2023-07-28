using starter_app.Models;
using System.Collections.Generic;

namespace starter_app.ViewModels
{
    public class EventViewModel
    {
        public IEnumerable<Event> AttendingEvent { get; set; }
        public bool ShowActions { get; set; }
    }
}