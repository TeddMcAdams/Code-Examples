using System.Collections.Generic;
using BaseballLeague.MODELS;

namespace BaseballLeague.CONTRACTS.Manager
{
    public interface ITeamManager
    {
        Response<List<Team>> LoadAll();
        Response<Team> Load(int teamId);
        Response<Team> Add(Team teamToAdd);
        Response<Team> Edit(int teamId, Team teamToEdit);
        Response<int> Remove(int teamId);
    }
}