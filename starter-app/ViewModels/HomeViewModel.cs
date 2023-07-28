using starter_app.Models;
using System.Collections.Generic;

namespace starter_app.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Event> UpcomingEvents { get; set; }
        public bool ShowActions { get; set; }
    }
}