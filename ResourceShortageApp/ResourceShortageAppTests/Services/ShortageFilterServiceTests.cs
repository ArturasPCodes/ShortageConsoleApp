using ResourceShortageApp.Models;
using ResourceShortageApp.Services;
using Xunit;

namespace ResourceShortageAppTests.Services
{
    public class ShortageFilterServiceTests
    {
        [Fact]
        public void GetByTitle_ListIsNull_ReturnsEmptyList()
        {
            // Arrange
            var expectedList = new List<Shortage>();

            // Act
            var actualList = ShortageFilterService.GetByTitle(null, "title");

            // Assert
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetByTitle_TitleIsNull_ReturnsEmptyList()
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

            var expectedList = new List<Shortage>();

            // Act
            var actualList = ShortageFilterService.GetByTitle(shortages, null);

            // Assert
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetByTitle_TitleIsWhiteSpace_ReturnsEmptyList()
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

            var expectedList = new List<Shortage>();

            // Act
            var actualList = ShortageFilterService.GetByTitle(shortages, " ");

            // Assert
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetByTitle_ListAndTitleAreNotNull_ReturnsFilteredList()
        {
            // Arrange
            var shortages = new List<Shortage>()
            {
                new Shortage
                {
                    Title = "BIGLETTERS",
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
                    Room = RoomType.MeetingRoom,
                    Category = CategoryType.Food,
                    Priority = 7,
                    CreatedOn = new DateTime(2023,1,1)
                }
            };

            var expectedList = new List<Shortage>()
                {
                    new Shortage
                    {
                        Title = "BIGLETTERS",
                        Name = "name1",
                        Room = RoomType.Kitchen,
                        Category = CategoryType.Food,
                        Priority = 7,
                        CreatedOn = new DateTime(2023,1,1)
                    }
                };

            // Act
            var actualList = ShortageFilterService.GetByTitle(shortages, "letters");

            // Assert
            Assert.Equal(expectedList.Count(), actualList.Count());
            Assert.Equal(expectedList[0].Title, actualList[0].Title);
            Assert.Equal(expectedList[0].Name, actualList[0].Name);
            Assert.Equal(expectedList[0].Room, actualList[0].Room);
            Assert.Equal(expectedList[0].Category, actualList[0].Category);
            Assert.Equal(expectedList[0].Priority, actualList[0].Priority);
            Assert.Equal(expectedList[0].CreatedOn, actualList[0].CreatedOn);
        }

        [Fact]
        public void GetByDates_ListIsNull_ReturnsEmptyList()
        {
            // Arrange
            var expectedList = new List<Shortage>();
            var datePeriod = new DatePeriod(new DateTime(2023, 1, 1), new DateTime(2023, 1, 2));

            // Act
            var actualList = ShortageFilterService.GetByDates(null, datePeriod);

            // Assert
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetByDates_ListIsNotNull_ReturnsFilteredList()
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
                    CreatedOn = new DateTime(2023,1,14)
                },
                new Shortage
                {
                    Title = "title2",
                    Name = "name2",
                    Room = RoomType.MeetingRoom,
                    Category = CategoryType.Food,
                    Priority = 7,
                    CreatedOn = new DateTime(2023,2,1)
                },
                new Shortage
                {
                    Title = "title3",
                    Name = "name3",
                    Room = RoomType.MeetingRoom,
                    Category = CategoryType.Food,
                    Priority = 7,
                    CreatedOn = new DateTime(2023,3,1)
                }
            };

            var expectedList = new List<Shortage>()
                {
                    new Shortage
                    {
                        Title = "title1",
                        Name = "name1",
                        Room = RoomType.Kitchen,
                        Category = CategoryType.Food,
                        Priority = 7,
                        CreatedOn = new DateTime(2023,1,14)
                    },
                    new Shortage
                    {
                        Title = "title2",
                        Name = "name2",
                        Room = RoomType.MeetingRoom,
                        Category = CategoryType.Food,
                        Priority = 7,
                        CreatedOn = new DateTime(2023,2,1)
                    },
                };

            var datePeriod = new DatePeriod(new DateTime(2023, 1, 1), new DateTime(2023, 2, 1));

            // Act
            var actualList = ShortageFilterService.GetByDates(shortages, datePeriod);

            // Assert
            Assert.Equal(expectedList.Count(), actualList.Count());
            Assert.Equal(expectedList[0].Title, actualList[0].Title);
            Assert.Equal(expectedList[0].Name, actualList[0].Name);
            Assert.Equal(expectedList[0].Room, actualList[0].Room);
            Assert.Equal(expectedList[0].Category, actualList[0].Category);
            Assert.Equal(expectedList[0].Priority, actualList[0].Priority);
            Assert.Equal(expectedList[0].CreatedOn, actualList[0].CreatedOn);
        }

        [Fact]
        public void GetByCategory_ListIsNull_ReturnsEmptyList()
        {
            // Arrange
            var expectedList = new List<Shortage>();

            // Act
            var actualList = ShortageFilterService.GetByCategory(null, CategoryType.Electronics);

            // Assert
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetByCategory_ListIsNotNull_ReturnsFilteredList()
        {
            // Arrange
            var shortages = new List<Shortage>()
                {
                    new Shortage
                    {
                        Title = "title1",
                        Name = "name1",
                        Room = RoomType.Kitchen,
                        Category = CategoryType.Other,
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

            var expectedList = new List<Shortage>()
                {
                    new Shortage
                    {
                        Title = "title1",
                        Name = "name1",
                        Room = RoomType.Kitchen,
                        Category = CategoryType.Other,
                        Priority = 7,
                        CreatedOn = new DateTime(2023,1,1)
                    },
                };

            // Act
            var actualList = ShortageFilterService.GetByCategory(shortages, CategoryType.Other);

            // Assert
            Assert.Equal(expectedList.Count(), actualList.Count());
            Assert.Equal(expectedList[0].Title, actualList[0].Title);
            Assert.Equal(expectedList[0].Name, actualList[0].Name);
            Assert.Equal(expectedList[0].Room, actualList[0].Room);
            Assert.Equal(expectedList[0].Category, actualList[0].Category);
            Assert.Equal(expectedList[0].Priority, actualList[0].Priority);
            Assert.Equal(expectedList[0].CreatedOn, actualList[0].CreatedOn);
        }

        [Fact]
        public void GetByRoom_ListIsNull_ReturnsEmptyList()
        {
            // Arrange
            var expectedList = new List<Shortage>();

            // Act
            var actualList = ShortageFilterService.GetByRoom(null, RoomType.MeetingRoom);

            // Assert
            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetByRoom_ListIsNotNull_ReturnsFilteredList()
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

            var expectedList = new List<Shortage>()
                {
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

            // Act
            var actualList = ShortageFilterService.GetByRoom(shortages, RoomType.MeetingRoom);

            // Assert
            Assert.Equal(expectedList.Count(), actualList.Count());
            Assert.Equal(expectedList[0].Title, actualList[0].Title);
            Assert.Equal(expectedList[0].Name, actualList[0].Name);
            Assert.Equal(expectedList[0].Room, actualList[0].Room);
            Assert.Equal(expectedList[0].Category, actualList[0].Category);
            Assert.Equal(expectedList[0].Priority, actualList[0].Priority);
            Assert.Equal(expectedList[0].CreatedOn, actualList[0].CreatedOn);
        }
    }
}
