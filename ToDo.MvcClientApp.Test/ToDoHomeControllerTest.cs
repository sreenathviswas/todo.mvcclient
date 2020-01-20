using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDo.MvcClientApp.Controllers;
using ToDo.MvcClientApp.Services;

namespace ToDo.MvcClientApp.Test
{
    [TestClass]
    public class ToDoHomeControllerTest
    {
        Mock<IToDoService> service;

        [TestMethod]
        public async Task home_index_returns_view()
        {
            service = new Mock<IToDoService>();

            service.Setup(x => x.GetAsync())
               .ReturnsAsync(new List<ToDo>
           {
                new ToDo(),
                new ToDo()
           });

            var controller = new HomeController(service.Object);
            var result = await controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task home_details_returns_view()
        {
            service = new Mock<IToDoService>();

            service.Setup(x => x.GetAsync(1))
               .ReturnsAsync(new ToDo());

            var controller = new HomeController(service.Object);
            var result = await controller.Details(1);

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void home_create_returns_view()
        {
            service = new Mock<IToDoService>();

            var controller = new HomeController(service.Object);
            var result = controller.Create();

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public async Task home_create_post_returns_view()
        {
            var todo = new ToDo();

            service = new Mock<IToDoService>();
            service.Setup(x => x.AddAsync(todo))
             .ReturnsAsync(todo);

            var controller = new HomeController(service.Object);
            var result = await controller.Create(todo);

            result.Should().BeOfType<RedirectToActionResult>();
        }

        [TestMethod]
        public async Task home_edit_returns_view()
        {
            service = new Mock<IToDoService>();

            service.Setup(x => x.GetAsync(1))
              .ReturnsAsync(new ToDo());

            var controller = new HomeController(service.Object);
            var result = await controller.Edit(1);

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public async Task home_edit_returns_notfound_when_todo_is_null()
        {
            service = new Mock<IToDoService>();

            service.Setup(x => x.GetAsync(1))
              .ReturnsAsync(default(ToDo));

            var controller = new HomeController(service.Object);
            var result = await controller.Edit(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task home_edit_post_returns_view()
        {
            var todo = new ToDo();

            service = new Mock<IToDoService>();

            service.Setup(x => x.EditAsync(todo))
              .ReturnsAsync(todo);

            var controller = new HomeController(service.Object);
            var result = await controller.Edit(1,todo);

            result.Should().BeOfType<RedirectToActionResult>();
        }

        [TestMethod]
        public async Task home_delete_returns_view()
        {
            service = new Mock<IToDoService>();

            service.Setup(x => x.GetAsync(1))
               .ReturnsAsync(new ToDo());

            var controller = new HomeController(service.Object);
            var result = await controller.Delete(1);

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public async Task home_delete_returns_notfound_when_todo_is_null()
        {
            service = new Mock<IToDoService>();

            service.Setup(x => x.GetAsync(1))
              .ReturnsAsync(default(ToDo));

            var controller = new HomeController(service.Object);
            var result = await controller.Delete(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task home_delete_post_returns_view()
        {
            var todo = new ToDo();

            service = new Mock<IToDoService>();

            service.Setup(x => x.DeleteAsync(1));

            var controller = new HomeController(service.Object);
            var result = await controller.Delete(1,todo);

            result.Should().BeOfType<RedirectToActionResult>();
        }

        [TestMethod]
        public void home_error_returns_view()
        {
            var controller = new HomeController(null);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                   TraceIdentifier = Guid.NewGuid().ToString()
                }
            };
            var result =  controller.Error();

            result.Should().BeOfType<ViewResult>();
        }
    }
}
