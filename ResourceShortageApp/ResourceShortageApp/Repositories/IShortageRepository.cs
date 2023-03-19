using ResourceShortageApp.Models;

namespace ResourceShortageApp.Repositories
{
    public interface IShortageRepository
    {
        List<Shortage> GetAll();

        void SaveAll(List<Shortage> shortages);
    }
}
