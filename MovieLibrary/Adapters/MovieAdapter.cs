using MovieLibrary.Adaptees;
using MovieLibrary.Interfaces;
using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Adapters
{
    public class MovieAdapter : ITarget
    {
        Adaptee adaptee;
        public MovieAdapter(Adaptee adaptee)
        {
            this.adaptee = adaptee;
        }
        public async Task<List<Movie>> GetMovies()
        {
            List<DetailedMovie> detailedMovies = await adaptee.GetDetailedMovies();
            List<Movie> movies = new List<Movie>();
            detailedMovies.ForEach(detailedMovie => movies.Add(new Movie()
            {
                id = detailedMovie.id,
                rated =
                detailedMovie.imdbRating.ToString(),
                title = detailedMovie.title
            }));
            return movies;
        }
    }
}
