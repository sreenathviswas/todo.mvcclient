using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using ToDo.MvcClientApp.Services;

namespace ToDo.MvcClientApp.Test
{
    [TestClass]
    public class ToDoServiceTest
    {
        Mock<ITokenAcquisition> _tokenAcquisition;
        IConfigurationRoot _configuration;

        [TestInitialize]
        public void Setup()
        {
            var scope = new string[] { };
            _tokenAcquisition = new Mock<ITokenAcquisition>();
            _tokenAcquisition.Setup(x => x.GetAccessTokenOnBehalfOfUserAsync(scope, ""))
               .ReturnsAsync(string.Empty);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        [TestMethod]
        public async Task todo_add_returns_saved_todo()
        {
            var fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };

            fakeHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
                                  .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("{}")
                                  });

            var httpClient = new HttpClient(fakeHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5001/"),
            };

            var service = new TodoService(_tokenAcquisition.Object, httpClient, _configuration);

           var result = await service.AddAsync(new ToDo());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task todo_edit_returns_saved_data()
        {
            var fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };

            fakeHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
                                  .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("{}")
                                  });

            var httpClient = new HttpClient(fakeHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5001/"),
            };

            var service = new TodoService(_tokenAcquisition.Object, httpClient, _configuration);

            var result = await service.EditAsync(new ToDo());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task todo_delete_removes_record()
        {
            var fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };

            fakeHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
                                  .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("{}")
                                  });

            var httpClient = new HttpClient(fakeHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5001/"),
            };

            var service = new TodoService(_tokenAcquisition.Object, httpClient, _configuration);

            await service.DeleteAsync(1);
        }

        [TestMethod]
        public async Task todo_get_returns_particular_data()
        {
            var fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };

            fakeHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
                                  .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("{}")
                                  });

            var httpClient = new HttpClient(fakeHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5001/"),
            };

            var service = new TodoService(_tokenAcquisition.Object, httpClient, _configuration);

            var result = await service.GetAsync(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task todo_get_all_returns_all_data()
        {
            var fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };

            fakeHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
                                  .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("[]")
                                  });

            var httpClient = new HttpClient(fakeHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5001/"),
            };

            var service = new TodoService(_tokenAcquisition.Object, httpClient, _configuration);

            var result = await service.GetAsync();

            Assert.IsNotNull(result);
        }
    }
}
