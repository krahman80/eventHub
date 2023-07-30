using System;
using System.ComponentModel.DataAnnotations;

namespace starter_app.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public bool isCanceled { get; set; }
    }
}