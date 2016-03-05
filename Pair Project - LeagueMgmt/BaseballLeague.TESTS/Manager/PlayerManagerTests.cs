using System.Collections.Generic;
using System.Linq;
using BaseballLeague.BLL;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.MODELS;
using NUnit.Framework;

namespace BaseballLeague.TESTS.Manager
{
    [TestFixture]
    class PlayerManagerTests
    {
        private IPlayerManager _playerManager;
        private Player _testPlayer;

        [SetUp]
        public void TestSetUp()
        {
            _playerManager = ManagerFactory.GetPlayerManager();
            _testPlayer = new Player();
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanLoadAll()
        {
            Response<List<Player>> actual = _playerManager.LoadAll();

            Assert.AreEqual(72, actual.Data.Count);
        }

        [Test]
        public void CanLoad()
        {
            Response<Player> actual = _playerManager.Load(2);

            Assert.AreEqual(2, actual.Data.PlayerId);
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

            Response<Player> actual = _playerManager.Add(_testPlayer);

            Assert.AreEqual(73, _playerManager.LoadAll().Data.Count);
            Assert.AreEqual(73, actual.Data.PlayerId);
        }

        [Test]
        public void CanEditPlayer()
        {
            _testPlayer = _playerManager.Load(20).Data;
            _testPlayer.FirstName = "TEST";

            Response<Player> actual = _playerManager.Edit(20, _testPlayer);

            Assert.AreEqual("TEST", actual.Data.FirstName);
            Assert.AreEqual(72, _playerManager.LoadAll().Data.Count);
        }

        [Test]
        public void CanRemovePlayer()
        {
            int actual = _playerManager.Remove(10).Data;

            Assert.AreEqual(10, actual);
            Assert.AreEqual(71, _playerManager.LoadAll().Data.Count);
            Assert.IsFalse(_playerManager.LoadAll().Data.Select(p => p.PlayerId).Contains(10));
        }
    }
}