using System.Collections.Generic;
using BaseballLeague.BLL;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.MODELS;
using NUnit.Framework;

namespace BaseballLeague.TESTS.Manager
{
    [TestFixture]
    class TeamManagerTests
    {
        private ITeamManager _teamManager;
        private Team _testTeam;

        [SetUp]
        public void TestSetUp()
        {
            _teamManager = ManagerFactory.GetTeamManager();
            _testTeam = new Team();
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanLoadAll()
        {
            Response<List<Team>> actual = _teamManager.LoadAll();

            Assert.AreEqual(8, actual.Data.Count);
        }

        [Test]
        public void CanLoad()
        {
            Response<Team> actual = _teamManager.Load(7);

            Assert.AreEqual(7, actual.Data.TeamId);
            Assert.AreEqual("Rodrigues", actual.Data.Manager.LastName);
            Assert.AreEqual(9, actual.Data.Players.Count);
        }

        [Test]
        public void CanAddTeam()
        {
            _testTeam.LeagueId = 1;
            _testTeam.Manager.ManagerId = 1;
            _testTeam.Name = "TEST TEAM";

            Response<Team> actual = _teamManager.Add(_testTeam);

            Assert.AreEqual(9, _teamManager.LoadAll().Data.Count);
            Assert.AreEqual(9, actual.Data.TeamId);
            Assert.AreEqual("TEST TEAM", actual.Data.Name);
        }

        [Test]
        public void CanEditTeam()
        {
            _testTeam = _teamManager.Load(4).Data;
            _testTeam.Manager.ManagerId = 4;
            _testTeam.Name = "EDIT TEST";

            Response<Team> actual = _teamManager.Edit(4, _testTeam);

            Assert.AreEqual("EDIT TEST", actual.Data.Name);
            Assert.AreEqual(8, _teamManager.LoadAll().Data.Count);
        }

        ////todo Implement in repo first.
        //[Test]
        //public void CanRemoveTeam()
        //{
        //    
        //}
    }
}
