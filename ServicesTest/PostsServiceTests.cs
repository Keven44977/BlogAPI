using AutoFixture;
using BaseTest;
using BlogAPI.Services;
using Core.Entity;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.PostsRepository;

namespace ServicesTest
{
    [TestClass]
    public class PostsServiceTests : BaseTest<PostsService>
    {
        [TestMethod]
        public void Create_WhenPostNull_ReturnNull()
        {
            //act
            var result = InstanceTest.Create(null);

            //assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Create_WhenPostNotNull_CallService()
        {
            //act
            var result = InstanceTest.Create(AutoFixture.Create<Post>());

            //assert
            result.Should().NotBeNull();

            GetMock<IPostsRepository>()
                .Verify(v => v.Add(It.IsAny<Post>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WhenPostDoesntExists_DontCallRemove()
        {
            //arrange
            GetMock<IPostsRepository>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns((Post)null);

            //act
            InstanceTest.Delete(1);

            //assert
            GetMock<IPostsRepository>()
                .Verify(v => v.Remove(It.IsAny<Post>()), Times.Never);
        }

        [TestMethod]
        public void Delete_WhenPostExists_CallRemove()
        {
            //arrange
            GetMock<IPostsRepository>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Post>());

            //act
            InstanceTest.Delete(1);

            //assert
            GetMock<IPostsRepository>()
                .Verify(v => v.Remove(It.IsAny<Post>()), Times.Once);
        }

        [TestMethod]
        public void GetAll_Always_CallGetAll()
        {
            //act
            InstanceTest.GetAll();

            //assert
            GetMock<IPostsRepository>()
                .Verify(v => v.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAllPriorOrTodayDescending_Always_CallGetAllPriorOrTodayDescending()
        {
            //act
            InstanceTest.GetAllPriorOrTodayDescending();

            //assert
            GetMock<IPostsRepository>()
                .Verify(v => v.GetAllPriorOrTodayDescending(), Times.Once);
        }

        [TestMethod]
        public void GetByCategory_Always_CallGetByCategory()
        {
            //act
            InstanceTest.GetByCategory(1);

            //assert
            GetMock<IPostsRepository>()
                .Verify(v => v.GetByCategory(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetById_Always_CallGetById()
        {
            //act
            InstanceTest.GetById(1);

            //assert
            GetMock<IPostsRepository>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetByTitle_Always_CallGetByTitle()
        {
            //act
            InstanceTest.GetByTitle("mockTitle");

            //assert
            GetMock<IPostsRepository>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Update_Always_CallUpdate()
        {
            //act
            InstanceTest.Update(AutoFixture.Create<Post>());

            //assert
            GetMock<IPostsRepository>()
                .Verify(v => v.Update(It.IsAny<Post>()), Times.Once);
        }
    }
}