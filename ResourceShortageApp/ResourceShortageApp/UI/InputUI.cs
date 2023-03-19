using ResourceShortageApp.Models;

namespace ResourceShortageApp.UI
{
    public class InputUI
    {
        public static Shortage CreateNewShortage()
        {
            return new Shortage
            {
                Title = GetInputValue("Enter a title: "),
                Name = GetInputValue("Enter your name: "),
                Room = GetRoomType(),
                Category = GetCategoryType(),
                Priority = GetPriorityLevel(),
                CreatedOn = DateTime.Now
            };
        }

        public static string GetInputValue(string promptPrinted)
        {
            while (true)
            {
                Console.Write(promptPrinted);
                var title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                {
                    return title;
                }
                else
                {
                    Console.WriteLine("Cannot be empty, try again!");
                }
            }
        }

        public static RoomType GetRoomType()
        {
            Console.WriteLine("Choose Roomtype: ");
            ListEnumValues<RoomType>();

            while (true)
            {
                Console.Write("Your choice: ");

                switch (Console.ReadLine())
                {
                    case "0":
                        return RoomType.MeetingRoom;
                    case "1":
                        return RoomType.Kitchen;
                    case "2":
                        return RoomType.Bathroom;
                    default:
                        Console.WriteLine("Such choice does not exist, please choose again");
                        break;
                }
            }
        }

        public static CategoryType GetCategoryType()
        {
            Console.WriteLine("Choose a category type: ");
            ListEnumValues<CategoryType>();

            while (true)
            {
                Console.Write("Your choice: ");

                switch (Console.ReadLine())
                {
                    case "0":
                        return CategoryType.Electronics;
                    case "1":
                        return CategoryType.Food;
                    case "2":
                        return CategoryType.Other;
                    default:
                        Console.WriteLine("Such choice does not exist, please choose again");
                        break;
                }
            }
        }

        public static void ListShortages(List<Shortage> shortages)
        {
            if (shortages.Count == 0)
            {
                Console.WriteLine("There is nothing here!");
                return;
            }

            foreach (var shortage in shortages)
            {
                Console.WriteLine(shortage);
            }
        }

        public static int ListAndGetPossibleFiltersChoice()
        {
            Console.WriteLine("What filters would you like to apply?");

            Console.WriteLine("0 - None, get me back to the main menu!");
            Console.WriteLine("1 - Search by title");
            Console.WriteLine("2 - Search by date interval");
            Console.WriteLine("3 - Search by category");
            Console.WriteLine("4 - Search by room");

            int choice;
            do
            {
                Console.Write("Your choice: ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Choice must be a whole number between 1 and 5, try again!");
                }
            } while (choice < 0 || choice > 4);

            return choice;
        }

        public static DateTime GetDate(string propmtPrinted)
        {
            Console.Write(propmtPrinted);
            var date = Console.ReadLine();

            while (true)
            {
                if (!DateTime.TryParse(date, out DateTime result))
                {
                    Console.WriteLine("The format is incorrect, try again! (YYYY-MM-DD)");
                }
                else
                {
                    return result;
                }
            }
        }

        private static void ListEnumValues<TEnum>() where TEnum : Enum
        {
            int index = 0;

            foreach (var enumValue in Enum.GetValues(typeof(TEnum)))
            {

                Console.WriteLine($"{index++} - {enumValue}");
            }
        }

        private static int GetPriorityLevel()
        {
            int priorityLevel;

            do
            {
                Console.Write("Enter a priority level from 1 (lowest) to 10 (highest): ");

                if (!int.TryParse(Console.ReadLine(), out priorityLevel))
                {
                    Console.WriteLine("Selection must be a whole number");
                    Console.WriteLine("Try again!");
                }

            } while (priorityLevel < 1 || priorityLevel > 10);

            return priorityLevel;
        }
    }
}
