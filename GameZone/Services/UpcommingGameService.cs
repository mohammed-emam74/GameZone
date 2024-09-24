namespace GameZone.Services
{
    public class UpcomingGameService : IUpcomingGameService
    {
        private readonly ApplicationDbContext _context;

        public UpcomingGameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UpcomingGame>> GetUpcomingGamesAsync()
        {
            return await _context.UpcomingGames
                .OrderBy(g => g.ReleaseDate)
                .ToListAsync();
        }
        public async Task AddUpcomingGameAsync(UpcomingGame upcomingGame)
        {
            _context.UpcomingGames.Add(upcomingGame);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UpcomingGame>> GetAllUpcomingGamesAsync()
        {
            return await _context.UpcomingGames.ToListAsync();
        }
    }
}
