namespace ResourceShortageApp.Models
{
    public class DatePeriod
    {
        public DatePeriod(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }
    }
}
