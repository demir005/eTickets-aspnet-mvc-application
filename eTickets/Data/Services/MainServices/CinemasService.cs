using eTickets.Data.Base.Repositories;
using eTickets.Data.Services.MainInterfaces;
using eTickets.Models;

namespace eTickets.Data.Services.MainServices
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context) { }
    }
}
