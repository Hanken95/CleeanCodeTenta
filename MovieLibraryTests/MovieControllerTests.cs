using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using MovieLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieLibraryTests
{
    [TestClass]
    public class MovieControllerTests
    {
        MovieController movieController;
        [TestInitialize]
        public void MyTestMethod()
        {
            movieController = new MovieController();
        }

        [TestMethod]
        public async Task GetMovieList_CallWithTrueParameter_ReturnAscencingSortedList()
        {
            //arrange
            bool input = true;

            //act
            var movieList = await movieController.GetMovieList(input);
            Movie firstMovie = new Movie()
            {
                id = "tt0087843",
                title = "Once Upon a Time in America",
                rated = "7,6"
            };
            Movie lastMovie = new Movie()
            {
                id = "tt0050083",
                title = "12 Angry Men",
                rated = "9,2"
            };
            var expectedLast = movieList[movieList.Count - 1];

            //assert
            Assert.IsNotNull(movieList);
            Assert.IsTrue(movieList.Count > 0);
            Assert.AreEqual(firstMovie.id, movieList[0].id);
            Assert.AreEqual(lastMovie.id, movieList[movieList.Count - 1].id);
            Assert.AreEqual(firstMovie.rated, movieList[0].rated);
            Assert.AreEqual(lastMovie.rated, movieList[movieList.Count - 1].rated);
            Assert.AreEqual(firstMovie.title, movieList[0].title);
            Assert.AreEqual(lastMovie.title, movieList[movieList.Count - 1].title);
        }

        [TestMethod]
        public async Task GetMovieList_CallWithFalseParameter_ReturnDescencingSortedList()
        {
            //arrange
            bool input = false;

            //act
            var movieList = await movieController.GetMovieList(input);
            Movie firstMovie = new Movie()
            {
                id = "tt0111161",
                title = "The Shawshank Redemption",
                rated = "9,2"
            };
            Movie lastMovie = new Movie()
            {
                id = "tt0097576",
                title = "Indiana Jones and the Last Crusade",
                rated = "7,6"
            };
            var expectedLast = movieList[movieList.Count - 1];

            //assert
            Assert.IsNotNull(movieList);
            Assert.IsTrue(movieList.Count > 0);
            Assert.AreEqual(firstMovie.id, movieList[0].id);
            Assert.AreEqual(lastMovie.id, movieList[movieList.Count - 1].id);
            Assert.AreEqual(firstMovie.rated, movieList[0].rated);
            Assert.AreEqual(lastMovie.rated, movieList[movieList.Count - 1].rated);
            Assert.AreEqual(firstMovie.title, movieList[0].title);
            Assert.AreEqual(lastMovie.title, movieList[movieList.Count - 1].title);
        }

        [TestMethod]
        public async Task GetMovieById_EnterValidId_ReturnExpectedMovie()
        {
            //arrange
            var input = "tt0087843";

            //act
            var movieResponse = await movieController.GetMovieById(input);
            Movie movie = movieResponse.Value;

            Movie testMovie = new Movie()
            {
                id = "tt0087843",
                title = "Once Upon a Time in America",
                rated = "7,6"
            };

            //assert
            Assert.IsNotNull(movie);
            Assert.AreEqual(testMovie.id, movie.id);
            Assert.AreEqual(testMovie.rated, movie.rated);
            Assert.AreEqual(testMovie.title, movie.title);
        }

        [TestMethod]
        public async Task GetMovieById_EnterInvalidId_ReturnError()
        {
            //arrange
            var input = "tdsadasd";

            //act
            var actualResponse = await movieController.GetMovieById(input);
            var status = (NoContentResult)actualResponse.Result;
            var expectedResponse = new NoContentResult();

            //assert
            Assert.AreEqual(status.StatusCode, expectedResponse.StatusCode);
        }
    }
}
