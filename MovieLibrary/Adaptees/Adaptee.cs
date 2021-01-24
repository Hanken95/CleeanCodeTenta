using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieLibrary.Adaptees
{
    public class Adaptee
    {
        public async Task<List<DetailedMovie>> GetDetailedMovies()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json");
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DetailedMovie>>(responseContent);
        }
    }
}
