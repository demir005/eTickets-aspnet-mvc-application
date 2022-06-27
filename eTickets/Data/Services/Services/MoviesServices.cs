using eTickets.Data.Base.Repositories;
using eTickets.Data.Services.MainInterfaces;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
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
    }
}
