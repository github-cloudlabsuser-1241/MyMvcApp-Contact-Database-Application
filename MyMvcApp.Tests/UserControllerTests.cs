using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfUsers()
        {
            // Arrange: Set up the controller and initialize the user list with one user
            var controller = new UserController();
            UserController.userlist = new List<User> { new User { Id = 1, Name = "John Doe", Email = "john@example.com" } };

            // Act: Call the Index method without a search string
            var result = controller.Index(string.Empty);

            // Assert: Verify that the result is a ViewResult and the model is a list of users containing exactly one user
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<User>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Index_ReturnsFilteredUsers_WhenSearchStringIsProvided()
        {
            // Arrange: Set up the controller and initialize the user list with multiple users
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
            };

            // Act: Call the Index method with a search string
            var result = controller.Index("Jane");

            // Assert: Verify that the result is a ViewResult and the model is a list of users containing only the matching user
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<User>>(viewResult.ViewData.Model);
            Assert.Single(model);
            Assert.Equal("Jane Smith", model.First().Name);
        }

        [Fact]
        public void Details_ReturnsViewResult_WithUser()
        {
            // Arrange: Set up the controller and initialize the user list with one user
            var controller = new UserController();
            UserController.userlist = new List<User> { new User { Id = 1, Name = "John Doe", Email = "john@example.com" } };

            // Act: Call the Details method with the ID of the user
            var result = controller.Details(1);

            // Assert: Verify that the result is a ViewResult and the model is the user with the specified ID
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange: Set up the controller with an empty user list
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act: Call the Details method with an ID that does not exist
            var result = controller.Details(1);

            // Assert: Verify that the result is a NotFoundResult
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Arrange: Set up the controller
            var controller = new UserController();

            // Act: Call the Create method (GET)
            var result = controller.Create();

            // Assert: Verify that the result is a ViewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange: Set up the controller and create a valid user model
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };

            // Act: Call the Create method (POST) with the valid user model
            var result = controller.Create(user);

            // Assert: Verify that the result is a RedirectToActionResult redirecting to the Index action
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Create_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange: Set up the controller, add a model error, and create an invalid user model
            var controller = new UserController();
            controller.ModelState.AddModelError("Name", "Required");
            var user = new User { Id = 1, Name = "", Email = "john@example.com" };

            // Act: Call the Create method (POST) with the invalid user model
            var result = controller.Create(user);

            // Assert: Verify that the result is a ViewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_ReturnsViewResult_WithUser()
        {
            // Arrange: Set up the controller and initialize the user list with one user
            var controller = new UserController();
            UserController.userlist = new List<User> { new User { Id = 1, Name = "John Doe", Email = "john@example.com" } };

            // Act: Call the Edit method (GET) with the ID of the user
            var result = controller.Edit(1);

            // Assert: Verify that the result is a ViewResult and the model is the user with the specified ID
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Edit_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange: Set up the controller with an empty user list
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act: Call the Edit method (GET) with an ID that does not exist
            var result = controller.Edit(1);

            // Assert: Verify that the result is a NotFoundResult
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange: Set up the controller, initialize the user list with one user, and create a valid user model
            var controller = new UserController();
            UserController.userlist = new List<User> { new User { Id = 1, Name = "John Doe", Email = "john@example.com" } };
            var user = new User { Id = 1, Name = "Jane Doe", Email = "jane@example.com" };

            // Act: Call the Edit method (POST) with the ID and the valid user model
            var result = controller.Edit(1, user);

            // Assert: Verify that the result is a RedirectToActionResult redirecting to the Index action
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange: Set up the controller, initialize the user list with one user, add a model error, and create an invalid user model
            var controller = new UserController();
            UserController.userlist = new List<User> { new User { Id = 1, Name = "John Doe", Email = "john@example.com" } };
            controller.ModelState.AddModelError("Name", "Required");
            var user = new User { Id = 1, Name = "", Email = "jane@example.com" };

            // Act: Call the Edit method (POST) with the ID and the invalid user model
            var result = controller.Edit(1, user);

            // Assert: Verify that the result is a ViewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_ReturnsViewResult_WithUser()
        {
            // Arrange: Set up the controller and initialize the user list with one user
            var controller = new UserController();
            UserController.userlist = new List<User> { new User { Id = 1, Name = "John Doe", Email = "john@example.com" } };

            // Act: Call the Delete method (GET) with the ID of the user
            var result = controller.Delete(1);

            // Assert: Verify that the result is a ViewResult and the model is the user with the specified ID
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange: Set up the controller with an empty user list
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act: Call the Delete method (GET) with an ID that does not exist
            var result = controller.Delete(1);

            // Assert: Verify that the result is a NotFoundResult
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Post_RedirectsToIndex()
        {
            // Arrange: Set up the controller and initialize the user list with one user
            var controller = new UserController();
            UserController.userlist = new List<User> { new User { Id = 1, Name = "John Doe", Email = "john@example.com" } };

            // Act: Call the Delete method (POST) with the ID of the user
            var result = controller.Delete(1, null);

            // Assert: Verify that the result is a RedirectToActionResult redirecting to the Index action
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}