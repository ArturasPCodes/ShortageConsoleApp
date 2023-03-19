using ResourceShortageApp.Models;

namespace ResourceShortageApp.Services
{
    public interface IShortageService
    {
        bool AddShortage(Shortage shortage);

        bool DeleteShortageByTitle(List<Shortage> listToDeleteFrom, string title);

        List<Shortage> GetShortages(string name);
    }
}
