using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorService;

        public ActorsController(IActorsService actorService)
        {
            _actorService = actorService;
        }

        public async Task<ActionResult> Index()
        {
            var data = await _actorService.GetAllAsync();
            return View(data);
        }

        //Get method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorService.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);

            if (actorDetails == null)  return View("NotFound");
            return View(actorDetails);
        }

        //Get method
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorService.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }

        //Get Delete/Actors/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _actorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
