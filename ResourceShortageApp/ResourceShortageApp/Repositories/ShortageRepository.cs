using Newtonsoft.Json;
using ResourceShortageApp.Models;

namespace ResourceShortageApp.Repositories
{
    public class ShortageRepository : IShortageRepository
    {
        private readonly string _filePath;

        public ShortageRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Shortage> GetAll()
        {
            // If file doesn't exist, return empty list
            if (!File.Exists(_filePath))
            {
                return new List<Shortage>();
            }

            List<Shortage> shortages = new List<Shortage>();
            using (StreamReader streamReader = new StreamReader(_filePath))
            {
                shortages = JsonConvert.DeserializeObject<List<Shortage>>(streamReader.ReadToEnd());
            }

            return shortages;
        }

        public void SaveAll(List<Shortage> shortages)
        {
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                string json = JsonConvert.SerializeObject(shortages);
                streamWriter.WriteLine(json);
            }
        }
    }
}
