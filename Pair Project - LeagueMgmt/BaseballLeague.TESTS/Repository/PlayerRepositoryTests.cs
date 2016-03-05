using System.Collections.Generic;
using System.Linq;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA;
using BaseballLeague.MODELS;
using NUnit.Framework;

namespace BaseballLeague.TESTS.Repository
{
    [TestFixture]
    public class PlayerRepositoryTests
    {
        private IPlayerRepository _playerRepo;
        private Player _testPlayer;

        [SetUp]
        public void TestSetUp()
        {
            _playerRepo = RepositoryFactory.GetPlayerRepository();
            _testPlayer = new Player();
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanLoadAll()
        {
            List<Player> actual = _playerRepo.LoadAll();

            Assert.AreEqual(72, actual.Count);
        }

        [Test]
        public void CanLoad()
        {
            Player actual = _playerRepo.Load(2);

            Assert.AreEqual(2 , actual.PlayerId);
        }

        [Test]
        public void CanAddPlayer()
        {
            _testPlayer.TeamId = 2;
            _testPlayer.FirstName = "Bob";
            _testPlayer.LastName = "Smith";
            _testPlayer.JerseyNumber = 77;
            _testPlayer.RookieYear = 2016;
            _testPlayer.Position.PositionId = 5;

            Player actual = _playerRepo.Add(_testPlayer);
            
            Assert.AreEqual(73, _playerRepo.LoadAll().Count);
            Assert.AreEqual(73, actual.PlayerId);
        }

        [Test]
        public void CanEditPlayer()
        {
            _testPlayer = _playerRepo.Load(20);
            _testPlayer.FirstName = "TEST";

            Player actual = _playerRepo.Edit(20, _testPlayer);

            Assert.AreEqual("TEST", actual.FirstName);
            Assert.AreEqual(72, _playerRepo.LoadAll().Count);
        }

        [Test]
        public void CanRemovePlayer()
        {
            int actual = _playerRepo.Remove(10);

            Assert.AreEqual(10, actual);
            Assert.AreEqual(71, _playerRepo.LoadAll().Count);
            Assert.IsFalse(_playerRepo.LoadAll().Select(p => p.PlayerId).Contains(10));
        }
    }
}