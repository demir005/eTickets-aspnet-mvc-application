using eTickets.Data.Base.Repositories;
using eTickets.Data.Services.MainInterfaces;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services.Services
{
    public class MoviesServices : EntityBaseRepository<Movies>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesServices(AppDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<Movies> GetMoviesById(int id)
        {
            var moviesDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies)
                .ThenInclude(a => a.Actor).FirstOrDefaultAsync(n => n.Id == id);

            return moviesDetails;
        }

        public async Task<NewMoviesDropdownVM> GetNewMovieDropdownValues()
        {
            var response = new NewMoviesDropdownVM
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Producer = await _context.Producers.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
            };
            return response;
        }

        public async Task AddNewMoviesAsync(NewMoviesVM data)
        {
            var newMovies = new Movies()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                ProducerId = data.ProducerId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory
            };
            await _context.Movies.AddAsync(newMovies);
            await _context.SaveChangesAsync();

            foreach (var actorId in data.ActorsIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovies.Id,
                    ActorId = actorId
                };

                await _context.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();
        }
    }
}
