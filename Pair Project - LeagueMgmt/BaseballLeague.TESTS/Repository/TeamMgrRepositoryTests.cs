using System.Collections.Generic;
using BaseballLeague.CONTRACTS.Repository;
using BaseballLeague.DATA;
using BaseballLeague.MODELS;
using NUnit.Framework;

namespace BaseballLeague.TESTS.Repository
{
    [TestFixture]
    public class TeamMgrRepositoryTests
    {
        private ITeamMgrRepository _teamMgrRepository;
        private ITeamRepository _teamRepository;
        private TeamMgr _testTeamMgr;

        [SetUp]
        public void SetUp()
        {
            _teamMgrRepository = RepositoryFactory.GetManagerRepository();
            _teamRepository = RepositoryFactory.GetTeamRepository();
            _testTeamMgr = new TeamMgr();
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanLoadAll()
        {
            List<TeamMgr> actual = _teamMgrRepository.LoadAll();

            Assert.AreEqual(8, actual.Count);
        }

        [Test]
        public void CanLoad()
        {
            TeamMgr actual = _teamMgrRepository.Load(5);

            Assert.AreEqual(5, actual.ManagerId);
        }

        [Test]
        public void CanAddManager()
        {
            _testTeamMgr.FirstName = "Flip";
            _testTeamMgr.LastName = "Wilson";

            TeamMgr actual = _teamMgrRepository.Add(_testTeamMgr);

            Assert.Less(8, actual.ManagerId);
            Assert.AreEqual(9, _teamMgrRepository.LoadAll().Count);
        }

        [Test]
        public void CanEditManager()
        {
            _testTeamMgr = _teamMgrRepository.Load(5);
            _testTeamMgr.FirstName = "Luke";
            _testTeamMgr.LastName = "Wilson";

            _teamMgrRepository.Edit(_testTeamMgr);

            TeamMgr actual = _teamMgrRepository.Load(5);

            Assert.AreEqual("Luke", actual.FirstName);
            Assert.AreEqual("Wilson", actual.LastName);
        }

        [Test]
        public void CanRemoveManager()
        {
            // Remove manager from his team first.
            var team = _teamRepository.Load(7);
            team.Manager.ManagerId = 8;
            _teamRepository.Edit(7, team);

            int actual = _teamMgrRepository.Remove(7);

            Assert.AreEqual(7, actual);
            Assert.AreEqual(7, _teamMgrRepository.LoadAll().Count);
        }
    }
}
