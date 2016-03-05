using System.Collections.Generic;
using System.Linq;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA;
using BaseballLeague.MODELS;
using NUnit.Framework;

namespace BaseballLeague.TESTS.Repository
{
    [TestFixture]
    public class TeamRepositoryTests
    {
        private ITeamRepository _teamRepo;
        //private IPlayerRepository _playerRepo;
        private Team _testTeam;

        [SetUp]
        public void TestSetUp()
        {
            _teamRepo = RepositoryFactory.GetTeamRepository();
            //_playerRepo = RepositoryFactory.GetPlayerRepository();
            _testTeam = new Team();
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanLoadAllTeams()
        {
            List<Team> actual = _teamRepo.LoadAll();

            Assert.AreEqual(8, actual.Count);
            Assert.AreEqual("Bucks", actual.Single(t => t.TeamId == 8).Name);
            Assert.AreEqual(9, actual.Single(t => t.TeamId == 8).Players.Count);
            Assert.AreEqual("Lee", actual.Single(t => t.TeamId == 8).Manager.LastName);
        }

        [Test]
        public void CanLoadTeam()
        {
            Team actual = _teamRepo.Load(4);
            
            Assert.AreEqual(4, actual.TeamId);
            Assert.AreEqual("Sampson", actual.Manager.LastName);
            Assert.AreEqual(9, actual.Players.Count);
        }

        [Test]
        public void CanAddTeam()
        {
            _testTeam.LeagueId = 1;
            _testTeam.Manager.ManagerId = 1;
            _testTeam.Name = "TEST TEAM";

            var actual = _teamRepo.Add(_testTeam);

            Assert.AreEqual(9, _teamRepo.LoadAll().Count);
            Assert.AreEqual(9, actual.TeamId);
            Assert.AreEqual("TEST TEAM", actual.Name);
        }

        [Test]
        public void CanEditTeam()
        {
            _testTeam = _teamRepo.Load(4);
            _testTeam.Manager.ManagerId = 4;
            _testTeam.Name = "EDIT TEST";

            var actual = _teamRepo.Edit(4, _testTeam);

            Assert.AreEqual("EDIT TEST", actual.Name);
            Assert.AreEqual(8, _teamRepo.LoadAll().Count);
        }

        //[Test]
        //public void CanRemoveTeam()
        //{
        //    // Remove players linked to team.
        //    _playerRepo.Remove(3);
        //    _playerRepo.Remove(11);
        //    _playerRepo.Remove(19);
        //    _playerRepo.Remove(27);
        //    _playerRepo.Remove(35);
        //    _playerRepo.Remove(43);
        //    _playerRepo.Remove(51);
        //    _playerRepo.Remove(59);
        //    _playerRepo.Remove(67);
        //    //todo Need to create games repo to remove games associated with team.
        //    // Remove games linked to team.

        //    int actual = _teamRepo.Remove(3);

        //    Assert.AreEqual(3, actual);
        //    Assert.AreEqual(7, _teamRepo.LoadAll().Count);
        //}
    }
}