using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public string ViewHeading { get; set; }

        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        [Required]
        public int? NumberInStock { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public int? GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public MovieFormViewModel(IEnumerable<Genre> genres)
        {
            this.Id = 0;
            this.Genres = genres;
            this.ViewHeading = "New Movie";
        }

        public MovieFormViewModel(Movie movie, IEnumerable<Genre> genres)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.ReleaseDate = movie.ReleaseDate;
            this.GenreId = movie.GenreId;
            this.NumberInStock = movie.NumberInStock;
            this.Genres = genres;
            this.ViewHeading = "Edit Movie";
        }

    }
}