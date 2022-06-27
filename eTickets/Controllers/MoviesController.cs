using eTickets.Data;
using eTickets.Data.Services.MainInterfaces;
using Microsoft.AspNetCore.Mvc;
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
            var movieDetail = await _moviesServices.GetMoviesById(id);
            return View(movieDetail);
        }
    }
}
