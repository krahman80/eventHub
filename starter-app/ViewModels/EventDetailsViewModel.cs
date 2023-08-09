using starter_app.Models;

namespace starter_app.ViewModels
{
    public class EventDetailsViewModel
    {
        public Event Event { get; set; }
        public bool IsAttending { get; internal set; }
    }
}