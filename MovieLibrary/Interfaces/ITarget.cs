using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Interfaces
{
    interface ITarget
    {
        Task<List<Movie>> GetMovies();
    }
}
