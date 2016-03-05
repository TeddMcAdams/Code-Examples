using System.Collections.Generic;
using System.Linq;
using BaseballLeague.BLL;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.MODELS;
using NUnit.Framework;

namespace BaseballLeague.TESTS.Manager
{
    [TestFixture]
    class TeamMgrManagerTests
    {
        private ITeamMgrManager _teamMgrManager;
        private ITeamManager _teamManager;
        private TeamMgr _testTeamMgr;

        [SetUp]
        public void SetUp()
        {
            _teamMgrManager = ManagerFactory.GetTeamMgrManager();
            _teamManager = ManagerFactory.GetTeamManager();
            _testTeamMgr = new TeamMgr();
            Utilities.RebuildTestDb();
        }

        [Test]
        public void CanLoadAll()
        {
            Response<List<TeamMgr>> actual = _teamMgrManager.LoadAll();

            Assert.AreEqual(8, actual.Data.Count);
        }

        [Test]
        public void CanLoad()
        {
            Response<TeamMgr> actual = _teamMgrManager.Load(5);

            Assert.AreEqual(5, actual.Data.ManagerId);
        }

        [Test]
        public void CanAddManager()
        {
            _testTeamMgr.FirstName = "Flip";
            _testTeamMgr.LastName = "Wilson";

            Response<TeamMgr> actual = _teamMgrManager.Add(_testTeamMgr);

            Assert.Less(8, actual.Data.ManagerId);
            Assert.AreEqual(9, _teamMgrManager.LoadAll().Data.Count);
        }

        [Test]
        public void CanEditManager()
        {
            _testTeamMgr = _teamMgrManager.Load(5).Data;
            _testTeamMgr.FirstName = "Luke";
            _testTeamMgr.LastName = "Wilson";

            _teamMgrManager.Edit(_testTeamMgr);

            var actual = _teamMgrManager.Load(5);

            Assert.AreEqual("Luke", actual.Data.FirstName);
            Assert.AreEqual("Wilson", actual.Data.LastName);
        }

        [Test]
        public void CanRemoveManager()
        {
            // Remove manager from his team first.
            var team = _teamManager.Load(7).Data;
            team.Manager.ManagerId = 8;
            _teamManager.Edit(7, team);

            _teamMgrManager.Remove(7);
            
            Assert.AreEqual(7, _teamMgrManager.LoadAll().Data.Count);
            Assert.IsFalse(_teamMgrManager.LoadAll().Data.Select(m => m.ManagerId).Contains(7));
        }
    }
}
