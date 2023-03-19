using ResourceShortageApp.Models;

namespace ResourceShortageApp.Services
{
    public class ShortageFilterService
    {
        public static List<Shortage> GetByTitle(List<Shortage> shortages, string title)
        {
            if (shortages == null || string.IsNullOrWhiteSpace(title))
            {
                return new List<Shortage>();
            }

            return shortages.Where(
                s => s.Title
                .ToLower()
                .Contains(
                    title.ToLower()))
                .ToList();
        }

        public static List<Shortage> GetByDates(List<Shortage> shortages, DatePeriod datePeriod)
        {
            if (shortages == null)
            {
                return new List<Shortage>();
            }

            return shortages
                .Where(s => s.CreatedOn >= datePeriod.DateFrom && s.CreatedOn <= datePeriod.DateTo)
                .ToList();
        }

        public static List<Shortage> GetByCategory(List<Shortage> shortages, CategoryType categoryType)
        {
            if (shortages == null)
            {
                return new List<Shortage>();
            }

            return shortages
                .Where(s => s.Category == categoryType)
                .ToList();
        }

        public static List<Shortage> GetByRoom(List<Shortage> shortages, RoomType roomType)
        {
            if (shortages == null)
            {
                return new List<Shortage>();
            }

            return shortages
                .Where(s => s.Room == roomType)
                .ToList();
        }
    }
}