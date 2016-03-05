using System.Collections.Generic;
using System.Web.Mvc;
using BaseballLeague.BLL;
using BaseballLeague.CONTRACTS.Manager;
using BaseballLeague.MODELS;
using BaseballLeague.UI.Models;

namespace BaseballLeague.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamManager _teamManager;
        private readonly IPlayerManager _playerManager;

        public HomeController()
        {
            _teamManager = ManagerFactory.GetTeamManager();
            _playerManager = ManagerFactory.GetPlayerManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Teams()
        {
            Response<List<Team>> response = _teamManager.LoadAll();

            if (response.Success)
            {
                return View(response.Data);

            }
            ViewData["Alert"] = "alert-danger";
            ViewData["Message"] = response.Message;
            return View();
        }

        public ActionResult Players()
        {
            var vm = new ViewAllPlayersVm();

            Response<List<Player>> playersResponse = _playerManager.LoadAll();
            Response<List<Team>> teamsResponse = _teamManager.LoadAll();

            if (playersResponse.Success && teamsResponse.Success)
            {
                vm.Players = playersResponse.Data;
                vm.Teams = teamsResponse.Data;
                return View(vm);
            }
            ViewData["Alert"] = "alert-danger";
            ViewData["Message"] = $"{playersResponse.Message} {teamsResponse.Message}";
            return View(vm);
        }
    }
}