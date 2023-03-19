using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ResourceShortageApp.Models
{
    public class Shortage
    {
        public string Title { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RoomType Room { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryType Category { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedOn { get; set; }

        public override string ToString()
        {
            return $"Title: {Title} | Registered by: {Name} | Room: {Room} " +
                $"| Category: {Category} | Priority level: {Priority} | Created on: {CreatedOn}";
        }
    }
}
