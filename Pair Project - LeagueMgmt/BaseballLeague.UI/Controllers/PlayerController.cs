using System.Collections.Generic;
using System.Web.Mvc;
using BaseballLeague.BLL;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.MODELS;
using BaseballLeague.UI.Models;

namespace BaseballLeague.UI.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerManager _playerManager;
        private readonly ITeamManager _teamManager;
        private readonly IPositionManager _positionManager;

        public PlayerController()
        {
            _playerManager = ManagerFactory.GetPlayerManager();
            _teamManager = ManagerFactory.GetTeamManager();
            _positionManager = ManagerFactory.GetPosition();
        }


        public ActionResult Delete(int playerId, int teamId)
        {
            Response<int> removePlayer = _playerManager.Remove(playerId);

            if (removePlayer.Success)
            {
                TempData["Alert"] = "alert-success";
                TempData["Message"] = "Player removal successful.";

                return RedirectToAction("Details", "Team", new {id = teamId} );
            }
            TempData["Alert"] = "alert-danger";
            TempData["Message"] = removePlayer.Message;

            return RedirectToAction("Details", "Team", new { id = teamId });

        }

        public ActionResult Details(int id)
        {
            Response<Player> loadPlayer = _playerManager.Load(id);
            Response<List<Team>> loadTeams = _teamManager.LoadAll();

            var vm = new PlayerDetailsVm
            {
                Player = loadPlayer.Data, 
                Teams = loadTeams.Data
            };

            if (loadPlayer.Success && loadTeams.Success) return View(vm);

            ViewData["Alert"] = "alert-danger";
            ViewData["Message"] = "";

            if (!loadPlayer.Success)
            {
                ViewData["Message"] += $"{loadPlayer.Message} ";
            }

            if (!loadTeams.Success)
            {
                ViewData["Message"] += $"{loadTeams.Message}";
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult Trade(int playerId, int playerTeamId, int tradeToTeamId, int playerToTradeForId)
        {
            // Load players and their team
            Response<Player> loadPlayer = _playerManager.Load(playerId);
            Response<Team> loadPlayerTeam = _teamManager.Load(playerTeamId);

            Response<Player> loadPlayerToTradeFor = _playerManager.Load(playerToTradeForId);
            Response<Team> loadPlayerToTradeForTeam = _teamManager.Load(playerToTradeForId);

            if (loadPlayer.Success && loadPlayerTeam.Success && loadPlayerToTradeFor.Success && loadPlayerToTradeForTeam.Success)
            {
                // Swap team id's and edit in db
                loadPlayer.Data.TeamId = tradeToTeamId;
                loadPlayerToTradeFor.Data.TeamId = playerTeamId;

                Response<Player> editPlayer = _playerManager.Edit(playerId, loadPlayer.Data);
                Response<Player> editPlayerToTradeFor = _playerManager.Edit(playerToTradeForId, loadPlayerToTradeFor.Data);

                if (editPlayer.Success && editPlayerToTradeFor.Success)
                {
                    TempData["Alert"] = "alert-success";
                    TempData["Message"] = "Trade successful.";
                    return RedirectToAction("Details", "Team", new { id = playerTeamId });
                }

                TempData["Alert"] = "alert-danger";
                TempData["Message"] = "";
                if (!editPlayer.Success)
                {
                    TempData["Message"] += $"{editPlayer.Message} ";
                }
                if (!editPlayerToTradeFor.Success)
                {
                    TempData["Message"] += $"{editPlayerToTradeFor.Message} ";
                }
                return RedirectToAction("Details", "Team", new { id = playerTeamId });
            }

            TempData["Alert"] = "alert-danger";
            TempData["Message"] = "";
            if (!loadPlayer.Success)
            {
                TempData["Message"] += $"{loadPlayer.Message} ";
            }
            if (!loadPlayerToTradeFor.Success)
            {
                TempData["Message"] += $"{loadPlayer.Message} ";
            }
            return RedirectToAction("Details", "Team", new { id = playerTeamId });
        }


        public ActionResult Create()
        {
            

            var vm = new CreatePlayerVm();
            Response<List<Position>> loadPositions = _positionManager.LoadAll();
            Response<List<Team>> loadTeams = _teamManager.LoadAll();

            if (loadPositions.Success && loadTeams.Success)
            {
                vm.AllPositions = _positionManager.LoadAll().Data;
                vm.AllTeams = _teamManager.LoadAll().Data;
            }
            else
            {
                ViewData["Alert"] = "alert-danger";
                ViewData["Message"] = "";

                if (!loadPositions.Success)
                {
                    ViewData["Message"] += $"{loadPositions.Message} ";
                }

                if (!loadTeams.Success)
                {
                    ViewData["Message"] += $"{loadTeams.Message}";
                }
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(Player playerToAdd)
        {
            if (!ModelState.IsValid)
            {
                var vm = new CreatePlayerVm();
                Response<List<Position>> loadPositions = _positionManager.LoadAll();
                Response<List<Team>> loadTeams = _teamManager.LoadAll();

                if (loadPositions.Success && loadTeams.Success)
                {
                    vm.AllPositions = _positionManager.LoadAll().Data;
                    vm.AllTeams = _teamManager.LoadAll().Data;
                }
                else
                {
                    ViewData["Alert"] = "alert-danger";
                    ViewData["Message"] = "";

                    if (!loadPositions.Success)
                    {
                        ViewData["Message"] += $"{loadPositions.Message} ";
                    }

                    if (!loadTeams.Success)
                    {
                        ViewData["Message"] += $"{loadTeams.Message}";
                    }
                }
                return View(vm);
            }

            Response<Player> addResponse = _playerManager.Add(playerToAdd);

            if (addResponse.Success)
            {
                TempData["Alert"] = "alert-success";
                TempData["Message"] = addResponse.Message;

                return RedirectToAction("Details", "Player", new {id = addResponse.Data.PlayerId});
            }

            if (!addResponse.Success)
            {
                TempData["Alert"] = "alert-danger";
                TempData["Message"] = addResponse.Message;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}