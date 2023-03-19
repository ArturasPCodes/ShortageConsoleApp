using ResourceShortageApp.Models;
using ResourceShortageApp.Repositories;
using ResourceShortageApp.Services;
using ResourceShortageApp.UI;

namespace ResourceShortageApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("****************************");
                Console.WriteLine("What would you like to do?");

                Console.WriteLine("1 - Register a shortage");
                Console.WriteLine("2 - Delete a shortage");
                Console.WriteLine("3 - Check shortages");
                Console.WriteLine("4 - Quit");

                Console.Write("Your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Input must be a whole number");
                    continue;
                }

                IShortageRepository shortageRepository = new ShortageRepository("shortages.json");
                IShortageService service = new ShortageService(shortageRepository);

                switch (choice)
                {
                    case 1:
                        AddShortage(service);
                        break;
                    case 2:
                        DeleteShortage(service);
                        break;
                    case 3:
                        ListAndFilterShortages(service);
                        break;
                    case 4:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Such option does not exist;");
                        break;
                }
            }
        }

        private static void AddShortage(IShortageService service)
        {
            var newShortage = InputUI.CreateNewShortage();

            if (service.AddShortage(newShortage))
            {
                Console.WriteLine("Warning! Such shortage is already registered!");
            }

            Console.WriteLine("Your inquiry was noted!");
        }

        private static void DeleteShortage(IShortageService service)
        {
            var name = InputUI.GetInputValue("Enter your name: ");
            var shortages = service.GetShortages(name);

            if (shortages.Count == 0)
            {
                Console.WriteLine("You don't have any registered shortages, thus there is nothing to delete");
                return;
            }

            InputUI.ListShortages(shortages);
            var titleToDelete = InputUI.GetInputValue("Enter the title of the one you would like to delete: ");

            if (service.DeleteShortageByTitle(shortages, titleToDelete))
            {
                Console.WriteLine("A shortage was deleted!");
            }
            else
            {
                Console.WriteLine("Something went wrong, check spelling!");
            }
        }

        private static void ListAndFilterShortages(IShortageService service)
        {
            var name = InputUI.GetInputValue("Enter your name: ");
            var shortages = service.GetShortages(name);

            if (shortages.Count == 0)
            {
                Console.WriteLine("You don't have any registered shortages");
                return;
            }

            InputUI.ListShortages(shortages);
            var chosenFilter = InputUI.ListAndGetPossibleFiltersChoice();

            if (chosenFilter == 0)
            {
                return;
            }

            var filteredShortages = FilterListByChoice(shortages, chosenFilter);

            InputUI.ListShortages(filteredShortages);
        }

        private static List<Shortage> FilterListByChoice(List<Shortage> shortages, int choice)
        {
            while (true)
            {
                switch (choice)
                {
                    case 1:
                        var title = InputUI.GetInputValue("Enter a title: ");
                        return ShortageFilterService.GetByTitle(shortages, title);
                    case 2:
                        var datePeriod = new DatePeriod(
                            InputUI.GetDate("Enter 'from' date: "),
                            InputUI.GetDate("Enter 'to' date: ")
                        );
                        return ShortageFilterService.GetByDates(shortages, datePeriod);
                    case 3:
                        var categoryType = InputUI.GetCategoryType();
                        return ShortageFilterService.GetByCategory(shortages, categoryType);
                    case 4:
                        var roomType = InputUI.GetRoomType();
                        return ShortageFilterService.GetByRoom(shortages, roomType);
                    default:
                        Console.WriteLine("Such option does not exist;");
                        break;
                }
            }
        }
    }
}