using NuGet.Packaging;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            _context.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;


            game.Devices.Clear();
            game.Devices.AddRange(model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }));

            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = await _context.SaveChangesAsync(); 

            if (effectedRows > 0)
            {
                if (hasNewCover && !string.IsNullOrEmpty(oldCover))
                {
                    var coverPath = Path.Combine(_imagePath, oldCover);
                    if (File.Exists(coverPath))
                    {
                        File.Delete(coverPath);
                    }
                }
                return game;
            }
            else
            {
                if (hasNewCover && !string.IsNullOrEmpty(game.Cover))
                {
                    var newCoverPath = Path.Combine(_imagePath, game.Cover);
                    if (File.Exists(newCoverPath))
                    {
                        File.Delete(newCoverPath);
                    }
                }
                return null;
            }
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(d => d.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(d => d.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return false;
            }

            _context.Games.Remove(game);
            var affectedRows = await _context.SaveChangesAsync(); 

            if (affectedRows > 0)
            {
                var coverPath = Path.Combine(_imagePath, game.Cover);
                if (File.Exists(coverPath))
                {
                    File.Delete(coverPath);
                }
                return true;
            }

            return false;
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagePath, coverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }

    }
}
