using AutoFixture;
using BaseTest;
using BlogAPI.Services;
using Core.Entity;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.CategoriesRepository;

namespace ServicesTest
{
    [TestClass]
    public class CategoriesServiceTest:BaseTest<CategoriesService>
    {
        [TestMethod]
        public void Create_WhenCategoryNull_ReturnNull()
        {
            //act
            var result = InstanceTest.Create(null);

            //assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Create_WhenCategoryNotNull_CallService()
        {
            //act
            var result = InstanceTest.Create(AutoFixture.Create<Category>());

            //assert
            result.Should().NotBeNull();

            GetMock<ICategoriesRepository>()
                .Verify(v => v.Add(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod]
        public void Delete_WhenCategoryDoesntExists_DontCallRemove()
        {
            //arrange
            GetMock<ICategoriesRepository>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns((Category)null);

            //act
            InstanceTest.Delete(1);

            //assert
            GetMock<ICategoriesRepository>()
                .Verify(v => v.Remove(It.IsAny<Category>()), Times.Never);
        }

        [TestMethod]
        public void Delete_WhenCategoryExists_CallRemove()
        {
            //arrange
            GetMock<ICategoriesRepository>()
                .Setup(s => s.GetById(It.IsAny<int>()))
                .Returns(AutoFixture.Create<Category>());

            //act
            InstanceTest.Delete(1);

            //assert
            GetMock<ICategoriesRepository>()
                .Verify(v => v.Remove(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod]
        public void GetAll_Always_CallGetAll()
        {
            //act
            InstanceTest.GetAll();

            //assert
            GetMock<ICategoriesRepository>()
                .Verify(v => v.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetById_Always_CallGetById()
        {
            //act
            InstanceTest.GetById(1);

            //assert
            GetMock<ICategoriesRepository>()
                .Verify(v => v.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetByTitle_Always_CallGetByTitle()
        {
            //act
            InstanceTest.GetByTitle("mockTitle");

            //assert
            GetMock<ICategoriesRepository>()
                .Verify(v => v.GetByTitle(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Update_Always_CallUpdate()
        {
            //act
            InstanceTest.Update(AutoFixture.Create<Category>());

            //assert
            GetMock<ICategoriesRepository>()
                .Verify(v => v.Update(It.IsAny<Category>()), Times.Once);
        }
    }
}
