using ResourceShortageApp.Models;
using ResourceShortageApp.Repositories;

namespace ResourceShortageAppTests.Repositories
{
    public class ShortageRepositoryTests
    {
        private readonly string _testFilePath = "test_shortages.json";
        private IShortageRepository shortageRepository => new ShortageRepository(_testFilePath);

        [Fact]
        public void GetAll_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            File.Delete(_testFilePath);

            // Act
            var result = shortageRepository.GetAll();

            // Asert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_WhenFileDoesExist_ReturnsListOfShortages()
        {
            // Arrange
            var expectedList = new List<Shortage>()
            {
                new Shortage
                {
                    Title = "Coffee",
                    Name = "Arturas",
                    Room = RoomType.Kitchen,
                    Category = CategoryType.Food,
                    Priority = 7,
                    CreatedOn = new DateTime(2023,1,1)
                },
                new Shortage
                {
                    Title = "title2",
                    Name = "name2",
                    Room = RoomType.MeetingRoom,
                    Category = CategoryType.Food,
                    Priority = 7,
                    CreatedOn = new DateTime(2023,1,1)
                }
            };

            shortageRepository.SaveAll(expectedList);

            // Act
            var actualList = shortageRepository.GetAll();

            // Assert
            Assert.Equal(expectedList.Count, actualList.Count);
        }

        [Fact]
        public void SaveAll_SavesShortagesToFile()
        {
            //Arrange 
            var expectedList = new List<Shortage>()
            {
                new Shortage
                {
                    Title = "title1",
                    Name = "name1",
                    Room = RoomType.Kitchen,
                    Category = CategoryType.Food,
                    Priority = 7,
                    CreatedOn = new DateTime(2023,1,1)
                },
                new Shortage
                {
                    Title = "title2",
                    Name = "name2",
                    Room = RoomType.Kitchen,
                    Category = CategoryType.Food,
                    Priority = 7,
                    CreatedOn = new DateTime(2023,1,1)
                }
            };

            //Act
            shortageRepository.SaveAll(expectedList);
            var actualList = shortageRepository.GetAll();

            //Assert
            Assert.Equal(expectedList.Count, actualList.Count);
        }
    }
}
