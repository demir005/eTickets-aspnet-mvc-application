using eTickets.Data.Base.Repositories;
using eTickets.Data.Services.MainInterfaces;
using eTickets.Models;

namespace eTickets.Data.Services.MainServices
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context) { }
    }
}
