using NUnit.Framework;
using Rino.GameFramework.Core.DDDCore.Domain;
using Rino.GameFramework.Core.DDDCore.Event;

namespace Rino.GameFramework.Core.Tests
{
    [TestFixture]
    public class EntityTest
    {
        private class TestEntity : Entity
        {
            public TestEntity(string id) : base(id) { }
        }

        private class TestEvent : IEvent { }

        [Test]
        public void Entity_Should_Have_Id()
        {
            var entity = new TestEntity("test-id");

            Assert.AreEqual("test-id", entity.Id);
        }

        [Test]
        public void AddDomainEvent_Should_Add_Event_To_List()
        {
            var entity = new TestEntity("test-id");
            var domainEvent = new TestEvent();

            entity.AddDomainEvent(domainEvent);

            Assert.AreEqual(1, entity.DomainEvents.Count);
            Assert.AreEqual(domainEvent, entity.DomainEvents[0]);
        }

        [Test]
        public void AddDomainEvent_Should_Add_Multiple_Events()
        {
            var entity = new TestEntity("test-id");
            var event1 = new TestEvent();
            var event2 = new TestEvent();

            entity.AddDomainEvent(event1);
            entity.AddDomainEvent(event2);

            Assert.AreEqual(2, entity.DomainEvents.Count);
        }

        [Test]
        public void ClearDomainEvents_Should_Remove_All_Events()
        {
            var entity = new TestEntity("test-id");
            entity.AddDomainEvent(new TestEvent());
            entity.AddDomainEvent(new TestEvent());

            entity.ClearDomainEvents();

            Assert.AreEqual(0, entity.DomainEvents.Count);
        }

        [Test]
        public void DomainEvents_Should_Return_Empty_List_Initially()
        {
            var entity = new TestEntity("test-id");

            Assert.IsNotNull(entity.DomainEvents);
            Assert.AreEqual(0, entity.DomainEvents.Count);
        }

        [Test]
        public void AddDomainEvent_Should_Ignore_Null_Event()
        {
            var entity = new TestEntity("test-id");

            entity.AddDomainEvent(null);

            Assert.AreEqual(0, entity.DomainEvents.Count);
        }
    }
}
