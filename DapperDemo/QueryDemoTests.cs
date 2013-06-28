using System.Linq;
using NUnit.Framework;
using Dapper;

namespace DapperDemo
{
    [TestFixture]
    public class QueryDemoTests : DatabaseTests
    {
        [Test]
        public void SelectAll()
        {
            var dogs = TestConnection.Query<Dog>("select * from Dog").ToList();
            Assert.That(dogs, Has.Count.EqualTo(3));
        }

        [Test]
        public void SelectWhere()
        {
            var dogs = TestConnection.Query<Dog>("select * from Dog where Age >= @Age", new { Age = 2 }).ToList();
            Assert.That(dogs.Count, Is.EqualTo(2));
        }

    }
}
