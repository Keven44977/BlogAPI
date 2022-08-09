using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BaseTest
{
    public class BaseTest<TTypeInstance> where TTypeInstance : class
    {
        private Autofac.Extras.Moq.AutoMock? _mockProvider;
        private TTypeInstance? _instanceTest;

        private Fixture? _autoFixture;

        [TestInitialize]
        public void BaseInitialize()
        {
            _mockProvider = Autofac.Extras.Moq.AutoMock.GetLoose();
            InitializeAutoFixture();
        }

        [TestCleanup]
        public void BaseCleanUp()
        {
            _mockProvider?.Dispose();
            _mockProvider = null;
        }

        public TTypeInstance InstanceTest
        {
            get { return _instanceTest ??= _mockProvider?.Create<TTypeInstance>(); }
        }

        public Fixture AutoFixture
        {
            get { return _autoFixture ??= new Fixture(); }
        }

        public Mock<TType> GetMock<TType>() where TType : class
        {
            return _mockProvider?.Mock<TType>();
        }

        private void InitializeAutoFixture()
        {
            AutoFixture.Customize<decimal>(c => c.FromFactory<int>(i => (decimal)(i * 1.33)));
            AutoFixture.Behaviors.Remove(AutoFixture.Behaviors.FirstOrDefault(b => b.GetType() == typeof(ThrowingRecursionBehavior)));
            AutoFixture.Behaviors.Add(new OmitOnRecursionBehavior());
            AssertionOptions.AssertEquivalencyUsing(options =>
            {
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, new TimeSpan(0, 0, 0, 100))).WhenTypeIs<DateTime>();
                options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, new TimeSpan(0, 0, 0, 100))).WhenTypeIs<DateTimeOffset>();
                return options;
            });
        }
    }
}