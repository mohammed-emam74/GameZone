namespace GameZone.Services
{
    public interface IUpcomingGameService
    {
        Task<IEnumerable<UpcomingGame>> GetUpcomingGamesAsync();
        Task AddUpcomingGameAsync(UpcomingGame upcomingGame);
        Task<List<UpcomingGame>> GetAllUpcomingGamesAsync();
    }

   

}
