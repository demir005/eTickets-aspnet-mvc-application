using eTickets.Models;
using System.Collections.Generic;

namespace eTickets.Data.ViewModels
{
    public class NewMoviesDropdownVM
    {
        public NewMoviesDropdownVM()
        {
            Producer = new List<Producer>();
            Actors = new List<Actor>();
            Cinemas = new List<Cinema>();

        }

        public List<Producer> Producer { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Cinema> Cinemas { get; set; }
    }
}
