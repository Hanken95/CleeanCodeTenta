using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Adapters;
using MovieLibrary.Interfaces;
using MovieLibrary.Models;

namespace MovieLibrary.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieController
    {
        static HttpClient client = new HttpClient();

        [HttpGet]
        [Route("/getmovielist")]
        public async Task<List<Movie>> GetMovieList(bool sortAscencing = true)
        {
            ITarget target = new MovieAdapter(new Adaptees.Adaptee());
            List<Movie> movieList = await GetMovies();
            List<Movie> castedMovieList = await target.GetMovies();
            castedMovieList.ForEach(movie =>
            {
                if (!movieList.Any(castedMovie => castedMovie.title == movie.title))
                {
                    movieList.Add(movie);
                }
            });
            if (sortAscencing)
            {
                movieList = movieList.OrderBy(movie => movie.rated).ToList();
            }
            else
            {
                movieList = movieList.OrderByDescending(movie => movie.rated).ToList();
            }
            return movieList;
        }

        private static async Task<List<Movie>> GetMovies()
        {
            HttpResponseMessage response = await client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json");
            var responseContent = await response.Content.ReadAsStringAsync();
            var movieList = JsonSerializer.Deserialize<List<Movie>>(responseContent);
            return movieList;
        }

        [HttpGet]
        [Route("/movie")]
        public async Task<ActionResult<Movie>> GetMovieById(string id) 
        {
            HttpResponseMessage response = await client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json");
            var responseContent = await response.Content.ReadAsStringAsync();
            var movieList = JsonSerializer.Deserialize<List<Movie>>(responseContent);
            var movie = movieList.Find(movie => movie.id == id);
            if (movie == null)
            {
                return new NoContentResult();
            }
            return movie;
        }
    }
}