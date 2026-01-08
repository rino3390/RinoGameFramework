using System.Linq;
using NUnit.Framework;
using Rino.GameFramework.Core.DDDCore.Domain;

namespace Rino.GameFramework.Core.Tests
{
	[TestFixture]
	public class RepositoryTest
	{
		private class TestEntity: Entity
		{
			public string Name { get; }

			public TestEntity(string id, string name = null): base(id)
			{
				Name = name ?? id;
			}
		}

		private Repository<TestEntity> repository;

		[SetUp]
		public void Setup()
		{
			repository = new Repository<TestEntity>();
		}

		[Test]
		public void Save_Should_Add_Entity_When_NotExist()
		{
			var entity = new TestEntity("Player");
			var entity2 = new TestEntity("Player2");

			repository.Save(entity);
			repository.Save(entity2);

			Assert.AreEqual(2, repository.Count);
			Assert.AreEqual(entity, repository["Player"]);
			Assert.AreEqual(entity2, repository["Player2"]);
		}

		[Test]
		public void Save_Should_Override_Entity_When_Exist()
		{
			var entity = new TestEntity("Player", "First");
			var entity2 = new TestEntity("Player", "Second");

			repository.Save(entity);
			repository.Save(entity2);

			Assert.AreEqual(1, repository.Count);
			Assert.AreEqual(entity2, repository["Player"]);
		}

		[Test]
		public void Save_Should_Ignore_Null_Entity()
		{
			repository.Save(null);

			Assert.AreEqual(0, repository.Count);
		}

		[Test]
		public void DeleteById_Should_Remove_Entity()
		{
			var entity = new TestEntity("Player");
			var entity2 = new TestEntity("Player2");
			repository.Save(entity);
			repository.Save(entity2);

			repository.DeleteById("Player");

			Assert.AreEqual(1, repository.Count);
			Assert.AreEqual(entity2, repository.Values.First());
		}

		[Test]
		public void DeleteById_Should_Do_Nothing_When_Id_NotExist()
		{
			var entity = new TestEntity("Player");
			repository.Save(entity);

			repository.DeleteById("NotExist");

			Assert.AreEqual(1, repository.Count);
		}

		[Test]
		public void DeleteAll_Should_Remove_All_Entities()
		{
			repository.Save(new TestEntity("Player"));
			repository.Save(new TestEntity("Player2"));

			repository.DeleteAll();

			Assert.AreEqual(0, repository.Count);
		}

		[Test]
		public void TryGet_Should_Return_True_And_Entity_When_Exist()
		{
			var entity = new TestEntity("Player");
			repository.Save(entity);

			var result = repository.TryGet("Player", out var found);

			Assert.IsTrue(result);
			Assert.AreEqual(entity, found);
		}

		[Test]
		public void TryGet_Should_Return_False_When_NotExist()
		{
			var result = repository.TryGet("NotExist", out var found);

			Assert.IsFalse(result);
			Assert.IsNull(found);
		}

		[Test]
		public void Keys_Should_Return_All_Entity_Ids()
		{
			repository.Save(new TestEntity("Player"));
			repository.Save(new TestEntity("Player2"));

			var keys = repository.Keys.ToArray();

			Assert.AreEqual(2, keys.Length);
			Assert.Contains("Player", keys);
			Assert.Contains("Player2", keys);
		}

		[Test]
		public void Indexer_Should_Return_Null_When_Id_NotExist()
		{
			var result = repository["NotExist"];

			Assert.IsNull(result);
		}

		[Test]
		public void Find_Should_Return_First_Matching_Entity()
		{
			repository.Save(new TestEntity("id1", "Alice"));
			repository.Save(new TestEntity("id2", "Bob"));
			repository.Save(new TestEntity("id3", "Alice"));

			var result = repository.Find(e => e.Name == "Alice");

			Assert.IsNotNull(result);
			Assert.AreEqual("Alice", result.Name);
		}

		[Test]
		public void Find_Should_Return_Null_When_No_Match()
		{
			repository.Save(new TestEntity("id1", "Alice"));
			repository.Save(new TestEntity("id2", "Bob"));

			var result = repository.Find(e => e.Name == "Charlie");

			Assert.IsNull(result);
		}

		[Test]
		public void Find_Should_Return_Null_When_Repository_Is_Empty()
		{
			var result = repository.Find(e => e.Name == "Alice");

			Assert.IsNull(result);
		}

		[Test]
		public void Find_Should_Return_Null_When_Predicate_Is_Null()
		{
			repository.Save(new TestEntity("id1", "Alice"));

			var result = repository.Find(null);

			Assert.IsNull(result);
		}
	}
}