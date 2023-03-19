using ResourceShortageApp.Models;
using ResourceShortageApp.Repositories;
using ResourceShortageApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceShortageAppTests.Services
{
    public class ShortageServiceTests
    {
        private readonly string _testFilePath = "test_shortageServices.json";

        private IShortageRepository repository => new ShortageRepository(_testFilePath);
        private IShortageService shortageService => new ShortageService(repository);

        [Fact]
        public void AddShortage_ShortageIsNew_AddsShortage()
        {
            // Arrange
            File.Delete(_testFilePath);
            var expectedShortage = new Shortage
            {
                Title = "newTitle",
                Name = "newName",
                Room = RoomType.Kitchen,
                Category = CategoryType.Other,
                Priority = 7,
                CreatedOn = new DateTime(2023, 1, 1)
            };

            var expectedCount = repository.GetAll().Count;

            // Act
            shortageService.AddShortage(expectedShortage);
            var actualList = repository.GetAll();

            // Assert
            Assert.Equal(expectedCount, actualList.Count - 1);
            Assert.Equal(expectedShortage.Title, actualList[0].Title);
            Assert.Equal(expectedShortage.Name, actualList[0].Name);
            Assert.Equal(expectedShortage.Room, actualList[0].Room);
            Assert.Equal(expectedShortage.Category, actualList[0].Category);
            Assert.Equal(expectedShortage.Priority, actualList[0].Priority);
            Assert.Equal(expectedShortage.CreatedOn, actualList[0].CreatedOn);
        }

        [Fact]
        public void AddShortage_ShortagePriorityIsHigher_UpdatesShortage()
        {
            // Arrange
            File.Delete(_testFilePath);
            var shortage = new Shortage
            {
                Title = "newTitle",
                Name = "newName",
                Room = RoomType.Kitchen,
                Category = CategoryType.Other,
                Priority = 7,
                CreatedOn = new DateTime(2023, 1, 1)
            };

            shortageService.AddShortage(shortage);

            var expectedNewShortage = new Shortage
            {
                Title = "newTitle",
                Name = "nameToCheck",
                Room = RoomType.Kitchen,
                Category = CategoryType.Other,
                Priority = 8,
                CreatedOn = new DateTime(2023, 1, 1)
            };

            // Act
            var isWarningDisplayed = shortageService.AddShortage(expectedNewShortage);
            var actualShortages = repository.GetAll();

            // Assert
            Assert.True(isWarningDisplayed);
            Assert.Equal(expectedNewShortage.Title, actualShortages.Last().Title);
            Assert.Equal(expectedNewShortage.Name, actualShortages.Last().Name);
            Assert.Equal(expectedNewShortage.Room, actualShortages.Last().Room);
            Assert.Equal(expectedNewShortage.Category, actualShortages.Last().Category);
            Assert.Equal(expectedNewShortage.Priority, actualShortages.Last().Priority);
            Assert.Equal(expectedNewShortage.CreatedOn, actualShortages.Last().CreatedOn);
        }

        [Fact]
        public void AddShortage_ShortagePriorityIsNotHigher_DoesNotUpdateShortage()
        {
            // Arrange
            File.Delete(_testFilePath);
            var expectedShortage = new Shortage
            {
                Title = "newTitle",
                Name = "nameDidNotChange",
                Room = RoomType.Kitchen,
                Category = CategoryType.Other,
                Priority = 7,
                CreatedOn = new DateTime(2023, 1, 1)
            };

            shortageService.AddShortage(expectedShortage);

            var newShortage = new Shortage
            {
                Title = "newTitle",
                Name = "nameToCheck",
                Room = RoomType.Kitchen,
                Category = CategoryType.Other,
                Priority = 1,
                CreatedOn = new DateTime(2023, 1, 1)
            };

            // Act
            var isWarningDisplayed = shortageService.AddShortage(newShortage);


            // Assert
            Assert.True(isWarningDisplayed);
            Assert.Equal(expectedShortage.Name, "nameDidNotChange");
            Assert.Equal(expectedShortage.Priority, 7);
        }

        [Fact]
        public void DeleteShortageByTitle_ListIsNull_ReturnsFalse()
        {
            // Arrange

            // Act
            var didDeletionWork = shortageService.DeleteShortageByTitle(null, "shortageToDelete");

            // Assert
            Assert.False(didDeletionWork);
        }

        [Fact]
        public void DeleteShortageByTitle_TitleIsNull_ReturnsFalse()
        {
            // Arrange
            var shortages = new List<Shortage>()
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
            };

            // Act
            var didDeletionWork = shortageService.DeleteShortageByTitle(shortages, null);

            // Assert
            Assert.False(didDeletionWork);
        }

        [Fact]
        public void DeleteShortageByTitle_TitleIsWhiteSpace_ReturnsFalse()
        {
            // Arrange
            var shortages = new List<Shortage>()
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
            };

            // Act
            var didDeletionWork = shortageService.DeleteShortageByTitle(shortages, " ");

            // Assert
            Assert.False(didDeletionWork);
        }

        [Fact]
        public void DeleteShortageByTitle_TitleDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var shortages = new List<Shortage>()
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
            };

            // Act
            var didDeletionWork = shortageService.DeleteShortageByTitle(shortages, "titleDoesNotExist");

            // Assert
            Assert.False(didDeletionWork);
        }

        [Fact]
        public void DeleteShortageByTitle_TitleExists_ReturnsTrue()
        {
            // Arrange
            var shortages = new List<Shortage>()
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
            };

            // Act
            var didDeletionWork = shortageService.DeleteShortageByTitle(shortages, "title1");

            // Assert
            Assert.True(didDeletionWork);
        }


        [Theory]
        [InlineData(null, 0)]
        [InlineData(" ", 0)]
        [InlineData("NameDoesNotExist", 0)]
        public void GetShortages_NameIsNullOrWhiteSpaceOrDoesntExist_ReturnsEmptyList(string name, int count)
        {
            var actualShortages = shortageService.GetShortages(name);
            Assert.Equal(count, actualShortages.Count);
        }

        [Fact]
        public void GetShortages_NameExists_ReturnsFilteredList()
        {
            // Arrange
            File.Delete(_testFilePath);
            var shortage = new Shortage
            {
                Title = "title1",
                Name = "Arturas",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 10,
                CreatedOn = new DateTime(2023, 1, 1)
            };

            shortageService.AddShortage(shortage);

            // Act
            var actualShortages = shortageService.GetShortages("Arturas");

            // Assert
            Assert.Equal(1, actualShortages.Count);
            Assert.Equal(shortage.Name, actualShortages[0].Name);
        }

        [Fact]
        public void GetShortages_NameIsAdministrator_ReturnsEveryShortage()
        {
            // Arrange
            File.Delete(_testFilePath);
            var shortage = new Shortage
            {
                Title = "title1",
                Name = "Justas",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 10,
                CreatedOn = new DateTime(2023, 1, 1)
            };

            var shortage2 = new Shortage
            {
                Title = "title1",
                Name = "Ieva",
                Room = RoomType.MeetingRoom,
                Category = CategoryType.Other,
                Priority = 5,
                CreatedOn = new DateTime(2023, 1, 1)
            };

            shortageService.AddShortage(shortage);
            shortageService.AddShortage(shortage2);

            // Act
            var actualShortages = shortageService.GetShortages("Administrator");

            // Assert
            Assert.Equal(2, actualShortages.Count);
            Assert.Equal(shortage.Name, actualShortages[0].Name);
            Assert.Equal(shortage2.Name, actualShortages[1].Name);
        }
    }
}
