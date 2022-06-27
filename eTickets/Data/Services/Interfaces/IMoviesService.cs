using eTickets.Data.Base;
using eTickets.Models;
using System.Threading.Tasks;

namespace eTickets.Data.Services.MainInterfaces
{
    public interface IMoviesService : IEntityBaseRepository<Movies>
    {
        Task<Movies> GetMoviesById(int id);
    }
}
