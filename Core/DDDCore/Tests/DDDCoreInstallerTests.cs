using NUnit.Framework;
using Zenject;

namespace Rino.GameFramework.DDDCore.Tests
{
    [TestFixture]
    public class DDDCoreInstallerTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            DDDCoreInstaller.Install(Container);
        }

        [Test]
        public void Install_WhenResolvingIEventBus_ReturnsEventBusInstance()
        {
            // Act
            var eventBus = Container.Resolve<IEventBus>();

            // Assert
            Assert.That(eventBus, Is.Not.Null);
            Assert.That(eventBus, Is.TypeOf<EventBus>());
        }

        [Test]
        public void Install_WhenResolvingPublisher_ReturnsPublisherInstance()
        {
            // Act
            var publisher = Container.Resolve<Publisher>();

            // Assert
            Assert.That(publisher, Is.Not.Null);
        }

        [Test]
        public void Install_WhenResolvingSubscriber_ReturnsSubscriberInstance()
        {
            // Act
            var subscriber = Container.Resolve<Subscriber>();

            // Assert
            Assert.That(subscriber, Is.Not.Null);
        }

        [Test]
        public void Install_WhenResolvingMultipleTimes_ReturnsSameInstance()
        {
            // Act
            var eventBus1 = Container.Resolve<IEventBus>();
            var eventBus2 = Container.Resolve<IEventBus>();
            var publisher1 = Container.Resolve<Publisher>();
            var publisher2 = Container.Resolve<Publisher>();
            var subscriber1 = Container.Resolve<Subscriber>();
            var subscriber2 = Container.Resolve<Subscriber>();

            // Assert
            Assert.That(eventBus1, Is.SameAs(eventBus2));
            Assert.That(publisher1, Is.SameAs(publisher2));
            Assert.That(subscriber1, Is.SameAs(subscriber2));
        }
    }
}
