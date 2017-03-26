using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Genre of the movie is required")]
        public int GenreId { get; set; } // foreign key

        [Required(ErrorMessage = "Required: Release date of the movie.")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Required: Date the movie was added to store.")]
        public DateTime DateAdded { get; set; }

        public int NumberInStock { get; set; }

    }
    

}