using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Tests.Repository
{
    public class CommentRepositoryTest
    {
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            
            if (await databaseContext.Comments.CountAsync() <= 0)
            {
                //seeding

                var user = new User()
                {
                    Id = "1sds"
                };

                databaseContext.Comments.Add(
                        new Comment()
                        {
                            Id = 1,
                            Text = "comment",
                            Post = new Post()
                            {
                                Id = 1,
                                Title = "Title",
                                Text = "Text",
                                Author = user
                            },
                            Author = user
                        });
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        [Fact]
        public async void CommentRepository_CommentExists_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            var dbContext = await GetDatabaseContext();
            var commentRepository = new CommentRepository(dbContext);

            //Act
            var result = commentRepository.CommentExists(id);

            //Assert
            result.Should().BeTrue();
        }
    }
}
