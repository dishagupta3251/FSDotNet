using BusTicketingApp.Contexts;
using Microsoft.EntityFrameworkCore;
using BusTicketingApp.Repositories;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;
using System.Text;

namespace UnitTesting.RepositoryTest
{
    public class UserRepositoryTest
    {
        DbContextOptions options;
        TicketingContext context;
        UserRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestRepo")
                .Options;

            context = new TicketingContext(options);
            repository = new UserRepository(context);
        }

        private User AddDetails()
        {
            return new User
            {
                FirstName = "test",
                Password = Encoding.UTF8.GetBytes("Password"),
                PasswordHash = Encoding.UTF8.GetBytes("TestPassword"),
                Role = Roles.Customer
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var user = AddDetails();
            var result = await repository.Add(user);

            Assert.AreEqual(result.FirstName, user.FirstName);
            Assert.AreEqual(result.Username, "test8901");
        }

        [Test]
        public void TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(null));
        }

        [Test]
        public async Task TestDelete()
        {
            var user = AddDetails();
            var addedUser = await repository.Add(user);

            var deletedUser = await repository.Delete(addedUser.Username);
            Assert.AreEqual(deletedUser.Username, addedUser.Username);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete("nonExistentUsername"));
        }

        [Test]
        public async Task TestGet()
        {
            var user = AddDetails();
            var addedUser = await repository.Add(user);

            var fetchedUser = await repository.Get(addedUser.Username);
            Assert.AreEqual(fetchedUser.Username, addedUser.Username);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get("nonExistentUsername"));
        }

        [Test]
        public async Task GetAllTest()
        {
            context.Users.RemoveRange(context.Users); // Ensure clean slate.
            var user = AddDetails();
            await repository.Add(user);

            var users = await repository.GetAll();
            Assert.IsNotEmpty(users);
        }

        [Test]
        public void GetAllTestException()
        {
            context.Users.RemoveRange(context.Users); // Ensure no data in the context.
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task UpdateTest()
        {
            var user = AddDetails();
            var addedUser = await repository.Add(user);

            user.FirstName = "UpdatedName";
            var updatedUser = await repository.Update(user, addedUser.Username);

            Assert.AreEqual(updatedUser.FirstName, "UpdatedName");
        }

        [Test]
        public void UpdateTestException()
        {
            var invalidUser = new User
            {
                Username = "nonExistentUsername",
                FirstName = "Name"
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(invalidUser, "nonExistentUsername"));
        }
    }
}
