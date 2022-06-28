using eTickets.Data;
using eTickets.Data.Services.MainInterfaces;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesServices;

        public MoviesController(IMoviesService moviesServices)
        {
            _moviesServices = moviesServices;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _moviesServices.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        //GET Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _moviesServices.GetMoviesByIdAsync(id);
            return View(movieDetail);
        }

        public async Task<IActionResult> Create()
        {
            var movieDropDownData = await _moviesServices.GetNewMovieDropdownValues();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producer, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMoviesVM movies)
        {
            if (!ModelState.IsValid)
            {
                var movieDropDownData = await _moviesServices.GetNewMovieDropdownValues();
                ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropDownData.Producer, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
                return View(movies);
            }
            await _moviesServices.AddNewMoviesAsync(movies);
            return RedirectToAction(nameof(Index));
        }


        // GET Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _moviesServices.GetMoviesByIdAsync(id);
            if (movieDetails == null)
            {
                return View("NotFound");
            }

            var response = new NewMoviesVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorsIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };
            var movieDropDownData = await _moviesServices.GetNewMovieDropdownValues();
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownData.Producer, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id,NewMoviesVM movies)
        {
            if(Id != movies.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                var movieDropDownData = await _moviesServices.GetNewMovieDropdownValues();
                ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropDownData.Producer, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
                return View(movies);
            }
            await _moviesServices.UpdateMoviesAsync(movies);
            return RedirectToAction(nameof(Index));
        }


    }
}
