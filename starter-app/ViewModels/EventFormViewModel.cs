using starter_app.Models;
using System;
using System.Collections.Generic;

namespace starter_app.ViewModels
{
    public class EventFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public DateTime DateTime
        {
            get
            {
                return DateTime.Parse(String.Format("{0} {1}", Date, Time));
            }
        }
    }
}