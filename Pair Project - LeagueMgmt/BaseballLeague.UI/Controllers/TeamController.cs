using System.Web.Mvc;
using BaseballLeague.BLL;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.MODELS;
using BaseballLeague.UI.Models;

namespace BaseballLeague.UI.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamManager _teamManager;
        private readonly ITeamMgrManager _teamMgrManager;
        private readonly IPlayerManager _playerManager;

        public TeamController()
        {
            _teamManager = ManagerFactory.GetTeamManager();
            _teamMgrManager = ManagerFactory.GetTeamMgrManager();
            _playerManager = ManagerFactory.GetPlayerManager();
        }

        public ActionResult Details(int id)
        {
            Response<Team> response = _teamManager.Load(id);

            if (!response.Success)
            {
                ViewData["Alert"] = "alert-danger";
                ViewData["Message"] = response.Message;
            }
            return View(response.Data);
        }

        public ActionResult Create()
        {
            return View(new Team(1));
        }

        [HttpPost]
        public ActionResult Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View(new Team(1));
            }

            var addResponse = _teamMgrManager.Add(team.Manager);
            if (addResponse.Success)
            {
                team.Manager.ManagerId = addResponse.Data.ManagerId;

                var teamResponse = _teamManager.Add(team);
                if (teamResponse.Success)
                {
                    TempData["Alert"] = "alert-success";
                    TempData["Message"] = teamResponse.Message;

                    return RedirectToAction("Details", "Team", new {id = teamResponse.Data.TeamId});
                }
                else
                {
                    ViewData["Alert"] = "alert-danger";
                    ViewData["Message"] = teamResponse.Message;
                }
            }
            else
            {
                ViewData["Alert"] = "alert-danger";
                ViewData["Message"] = addResponse.Message;
            }
            return View(new Team(1));
        }

        [HttpPost]
        public ActionResult Trade(int playerId, int tradeToTeamId)
        {
            var playerResponse = _playerManager.Load(playerId);
            var playerTeamResponse = _teamManager.Load(playerResponse.Data.TeamId);
            var tradeToTeamResponse = _teamManager.Load(tradeToTeamId);

            var vm = new TradePlayerVm
            {
                Player = playerResponse.Data,
                PlayersCurrTeam = playerTeamResponse.Data,
                TradeToTeam = tradeToTeamResponse.Data,
            };
            if (playerResponse.Success && playerTeamResponse.Success && tradeToTeamResponse.Success) return View(vm);
            ViewData["Alert"] = "alert-danger";
            ViewData["Message"] = "";
            if (!playerResponse.Success)
            {
                ViewData["Message"] += $"{playerResponse.Message} ";
            }
            if (!playerTeamResponse.Success)
            {
                ViewData["Message"] += $"{playerResponse.Message} ";
            }
            if (!tradeToTeamResponse.Success)
            {
                ViewData["Message"] += $"{playerResponse.Message}";
            }
            return View(vm);
        }
    }
}
