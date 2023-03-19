using ResourceShortageApp.Models;
using ResourceShortageApp.Repositories;
using System.Data;

namespace ResourceShortageApp.Services
{
    public class ShortageService : IShortageService
    {
        private readonly IShortageRepository _repository;

        public ShortageService(IShortageRepository repository)
        {
            _repository = repository;
        }

        public bool AddShortage(Shortage newShortage)
        {
            var shortages = _repository.GetAll();

            var shouldWarningBeDisplayed = DoesShortageExistAndAddOrUpdate(shortages, newShortage);
            _repository.SaveAll(shortages);

            return shouldWarningBeDisplayed;
        }

        public bool DeleteShortageByTitle(List<Shortage> shortages, string title)
        {
            if (shortages == null || string.IsNullOrEmpty(title))
            {
                return false;
            }

            var isDeletionSuccessfull = IsDeletedFromList(shortages, title);
            _repository.SaveAll(shortages);

            return isDeletionSuccessfull;
        }

        public List<Shortage> GetShortages(string name)
        {
            var shortages = _repository.GetAll();

            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<Shortage>();
            }
            else if (name.ToLower() == "administrator")
            {
                return shortages
                    .OrderByDescending(shortage => shortage.Priority)
                    .ToList();
            }
            else
            {
                var filteredList = shortages
                    .Where(s => s.Name.ToLower() == name.ToLower())
                    .OrderByDescending(shortage => shortage.Priority)
                    .ToList();

                return filteredList;
            }
        }

        private static bool DoesShortageExistAndAddOrUpdate(List<Shortage> shortages, Shortage newShortage)
        {
            foreach (var existingShortage in shortages)
            {
                if (existingShortage.Title == newShortage.Title && existingShortage.Room == newShortage.Room)
                {
                    // Update shortage if needed
                    if (existingShortage.Priority < newShortage.Priority)
                    {
                        existingShortage.Name = newShortage.Name;
                        existingShortage.Priority = newShortage.Priority;
                    }

                    return true;
                }
            }

            // If shortage did not exist, add it
            shortages.Add(newShortage);
            return false;
        }

        private static bool IsDeletedFromList(List<Shortage> shortages, string title)
        {
            int countDeleted = shortages.RemoveAll(shortage => shortage.Title.ToLower() == title.ToLower());

            if (countDeleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
