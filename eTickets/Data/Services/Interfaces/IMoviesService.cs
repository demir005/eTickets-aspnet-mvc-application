﻿using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System.Threading.Tasks;

namespace eTickets.Data.Services.MainInterfaces
{
    public interface IMoviesService : IEntityBaseRepository<Movies>
    {
        Task<Movies> GetMoviesById(int id);
        Task<NewMoviesDropdownVM> GetNewMovieDropdownValues();
        Task AddNewMoviesAsync(NewMoviesVM data);
    }
}
