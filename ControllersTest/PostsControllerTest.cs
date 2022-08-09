using BaseTest;
using BlogAPI.Controllers;
using BlogAPI.Services;
using Core.Entity;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using Moq;
using Core.Model.Request;

namespace ControllersTest
{
    [TestClass]
    public class PostsControllerTest : BaseTest<PostsController>
    {
        [TestMethod]
        public void GetAll_WhenPostsNull_ReturnNoContent()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetAllPriorOrTodayDescending())
                .Returns((List<Post>)null);

            //act
            var result = InstanceTest.GetAll();

            //assert
            result.Should().BeOfType(typeof(NoContentResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetAllPriorOrTodayDescending(), Times.Once);
        }

        [TestMethod]
        public void GetAll_WhenPostsEmpty_ReturnNoContent()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetAllPriorOrTodayDescending())
                .Returns(new List<Post>());

            //act
            var result = InstanceTest.GetAll();

            //assert
            result.Should().BeOfType(typeof(NoContentResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetAllPriorOrTodayDescending(), Times.Once);
        }

        [TestMethod]
        public void GetAll_WhenPostsExists_ReturnOkResult()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetAllPriorOrTodayDescending())
                .Returns(AutoFixture.CreateMany<Post>().ToList());

            //act
            var result = InstanceTest.GetAll();

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetAllPriorOrTodayDescending(), Times.Once);
        }

        [TestMethod]
        public void Get_WhenNoPosts_ReturnNotFound()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns((Post)null);

            //act
            var result = InstanceTest.Get(1);

            //assert
            result.Should().BeOfType(typeof(NotFoundResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Get_WhenPostExists_ReturnOkResult()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Post>());

            //act
            var result = InstanceTest.Get(1);

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Create_WhenPostNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Create(null);

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Create_WhenTitleNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Create(AutoFixture.Build<PostModel>().With(s => s.Title, (string)null).Create());

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Create_WhenContentNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Create(AutoFixture.Build<PostModel>().With(s => s.Content, (string)null).Create());

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Create_WhenDateNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Create(AutoFixture.Build<PostModel>().With(s => s.PublicationDate, (string)null).Create());

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Create_WhenPostExists_ReturnConflictResult()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns(AutoFixture.Create<Post>());

            //act
            var result = InstanceTest.Create(AutoFixture.Create<PostModel>());

            //assert
            result.Should().BeOfType(typeof(ConflictResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.Create(It.IsAny<Post>()), Times.Never);
        }

        [TestMethod]
        public void Create_WhenPostDoesntExists_ReturnOkResult()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns((Post)null);

            //act
            var result = InstanceTest.Create(AutoFixture.Build<PostModel>().With(s=>s.PublicationDate, "2000-12-31").Create());

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.Create(It.IsAny<Post>()), Times.Once);
        }

        [TestMethod]
        public void Update_WhenPostNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Update(null);

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Update_WhenTitleNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Update(AutoFixture.Build<PostModel>().With(s => s.Title, (string)null).Create());

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Update_WhenContentNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Update(AutoFixture.Build<PostModel>().With(s => s.Content, (string)null).Create());

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Update_WhenDateNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Update(AutoFixture.Build<PostModel>().With(s => s.PublicationDate, (string)null).Create());

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void Update_WhenPostDoesntExists_ReturnNotFound()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns((Post)null);

            //act
            var result = InstanceTest.Update(AutoFixture.Create<PostModel>());

            //assert
            result.Should().BeOfType(typeof(NotFoundResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }
        [TestMethod]
        public void Update_WhenPostTitleExists_ReturnConflictResult()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Post>());

            GetMock<IPostsService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns(AutoFixture.Create<Post>());

            //act
            var result = InstanceTest.Update(AutoFixture.Create<PostModel>());

            //assert
            result.Should().BeOfType(typeof(ConflictResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.Update(It.IsAny<Post>()), Times.Never);
        }

        [TestMethod]
        public void Update_WhenPostDoesntExists_ReturnOkResult()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Post>());

            GetMock<IPostsService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns((Post)null);

            //act
            var result = InstanceTest.Update(AutoFixture.Build<PostModel>().With(s => s.PublicationDate, "2000-12-31").Create());

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.Update(It.IsAny<Post>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WhenPostDoesntExists_ReturnNotFound()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns((Post)null);

            //act
            var result = InstanceTest.Delete(1);

            //assert
            result.Should().BeOfType(typeof(NotFoundResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.Delete(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void Delete_WhenPostExists_ReturnOkResult()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Post>());

            //act
            var result = InstanceTest.Delete(1);

            //assert
            result.Should().BeOfType(typeof(OkResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
