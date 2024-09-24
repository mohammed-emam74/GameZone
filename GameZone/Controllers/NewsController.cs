using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    [Authorize(Roles ="Admin")]
    public class NewsController : Controller
    {
        private readonly IUpcomingGameService _upcomingGameService;

        public NewsController(IUpcomingGameService upcomingGameService)
        {
            _upcomingGameService = upcomingGameService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var upcomingGames = await _upcomingGameService.GetUpcomingGamesAsync();
            return View(upcomingGames);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View(); 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UpcomingGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var platformString = string.Join(", ", model.Platforms ?? new List<string>()); // Keep this logic for selecting multiple platforms

            var upcomingGame = new UpcomingGame
            {
                GameName = model.Title,
                Description = model.Description,
                ReleaseDate = model.ReleaseDate,
                Platform = platformString,  
                CreatedAt = DateTime.Now
            };

            await _upcomingGameService.AddUpcomingGameAsync(upcomingGame);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var upcomingGame = _upcomingGameService.GetUpcomingGamesAsync()
                .Result.FirstOrDefault(game => game.Id == id);
            if (upcomingGame == null)
            {
                return NotFound();
            }

            return View(upcomingGame);
        }
    }
}
