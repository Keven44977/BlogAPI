using BaseTest;
using BlogAPI.Controllers;
using BlogAPI.Services;
using Core.Entity;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using Moq;
using Core.Dto;

namespace ControllersTest
{
    [TestClass]
    public class CategoriesControllerTest : BaseTest<CategoriesController>
    {
        [TestMethod]
        public void GetAll_WhenCategoriesNull_ReturnNoContent()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetAll())
                .Returns((List<Category>)null);

            //act
            var result = InstanceTest.GetAll();

            //assert
            result.Should().BeOfType(typeof(NoContentResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAll_WhenCategoriesEmpty_ReturnNoContent()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetAll())
                .Returns(new List<Category>());

            //act
            var result = InstanceTest.GetAll();

            //assert
            result.Should().BeOfType(typeof(NoContentResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAll_WhenCategories_ReturnOk()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetAll())
                .Returns(AutoFixture.CreateMany<Category>().ToList());

            //act
            var result = InstanceTest.GetAll();

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetById_WhenCategoryDoesntExists_ReturnNotFound()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns((Category)null);

            //act
            var result = InstanceTest.Get(1);

            //assert
            result.Should().BeOfType(typeof(NotFoundResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetById_WhenCategoryExists_ReturnOkResult()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Category>());

            //act
            var result = InstanceTest.Get(1);

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetPostsByCategory_WhenCategoriesNull_ReturnNoContent()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetByCategory(It.IsAny<int>()))
                .Returns((List<Post>)null);

            //act
            var result = InstanceTest.GetPostsByCategory(1);

            //assert
            result.Should().BeOfType(typeof(NoContentResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByCategory(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetPostsByCategory_WhenCategoriesEmpty_ReturnNoContent()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetByCategory(It.IsAny<int>()))
                .Returns(new List<Post>());

            //act
            var result = InstanceTest.GetPostsByCategory(1);

            //assert
            result.Should().BeOfType(typeof(NoContentResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByCategory(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetPostsByCategory_WhenCategories_ReturnOkResult()
        {
            //arrange
            GetMock<IPostsService>()
                .Setup(s => s.GetByCategory(It.IsAny<int>()))
                .Returns(AutoFixture.CreateMany<Post>().ToList());

            //act
            var result = InstanceTest.GetPostsByCategory(1);

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<IPostsService>()
                .Verify(v => v.GetByCategory(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Create_WhenTitleNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Create(null);

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Create_WhenExists_ReturnBadRequest()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns(AutoFixture.Create<Category>());

            //act
            var result = InstanceTest.Create("mockTitle");

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.Create(It.IsAny<Category>()), Times.Never);
        }

        [TestMethod]
        public void Create_WhenDoesntExists_ReturnOkResult()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns((Category)null);

            //act
            var result = InstanceTest.Create("mockTitle");

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.Create(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod]
        public void Update_WhenNull_ReturnBadRequest()
        {
            //act
            var result = InstanceTest.Update(null);

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void Update_WhenDoesntExists_ReturnNotFound()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns((Category)null);

            //act
            var result = InstanceTest.Update(AutoFixture.Create<DtoCategory>());

            //assert
            result.Should().BeOfType(typeof(NotFoundResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Update_WhenSameTitleExistsNotSamePost_ReturnBadResuqest()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Category>());

            GetMock<ICategoriesService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns(AutoFixture.Build<Category>().With(s => s.Id, 2).Create());

            //act
            var result = InstanceTest.Update(AutoFixture.Build<DtoCategory>().With(s => s.Id, 1).Create());

            //assert
            result.Should().BeOfType(typeof(BadRequestResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.Update(It.IsAny<Category>()), Times.Never);
        }

        [TestMethod]
        public void Update_WhenSameTitleExistsSamePost_ReturnOkResult()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Category>());

            GetMock<ICategoriesService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns(AutoFixture.Build<Category>().With(s => s.Id, 1).Create());

            //act
            var result = InstanceTest.Update(AutoFixture.Build<DtoCategory>().With(s => s.Id, 1).Create());

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.Update(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod]
        public void Update_WhenSameTitleDoesntExists_ReturnOkResult()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Category>());

            GetMock<ICategoriesService>()
                .Setup(s => s.GetByTitle(It.IsAny<string>()))
                .Returns((Category)null);

            //act
            var result = InstanceTest.Update(AutoFixture.Build<DtoCategory>().With(s => s.Id, 1).Create());

            //assert
            result.Should().BeOfType(typeof(OkObjectResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);

            GetMock<ICategoriesService>()
                .Verify(v => v.Update(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WhenDoesntExists_ReturnNotFound()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns((Category)null);

            //act
            var result = InstanceTest.Delete(1);

            //assert
            result.Should().BeOfType(typeof(NotFoundResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.GetByCategory(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void Delete_WhenExists_ReturnOkResult()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Category>());

            //act
            var result = InstanceTest.Delete(1);

            //assert
            result.Should().BeOfType(typeof(OkResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.GetByCategory(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WhenPostsInCategory_DeletePosts()
        {
            //arrange
            GetMock<ICategoriesService>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Category>());

            GetMock<IPostsService>()
                .Setup(s => s.GetByCategory(It.IsAny<int>()))
                .Returns(AutoFixture.CreateMany<Post>().ToList());

            //act
            var result = InstanceTest.Delete(1);

            //assert
            result.Should().BeOfType(typeof(OkResult));

            GetMock<ICategoriesService>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.GetByCategory(It.IsAny<int>()), Times.Once);

            GetMock<IPostsService>()
                .Verify(v => v.Delete(It.IsAny<int>()), Times.Exactly(3));
        }
    }
}
