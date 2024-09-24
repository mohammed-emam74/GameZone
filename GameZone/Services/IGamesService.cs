
using Microsoft.CodeAnalysis.Differencing;

namespace GameZone.Services
{
    public interface IGamesService
    {
        Task Create(CreateGameFormViewModel model);
        Game? GetById(int id);
        IEnumerable<Game> GetAll();
        Task<Game?> Update(EditGameFormViewModel model);
        Task<bool> Delete (int id);
    }
}
