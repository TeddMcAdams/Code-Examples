using System.Collections.Generic;
using BaseballLeague.MODELS;

namespace BaseballLeague.CONTRACTS.Repository
{
    public interface ITeamRepository
    {
        List<Team> LoadAll();
        Team Load(int teamId);
        Team Add(Team teamToAdd);
        Team Edit(int teamId, Team teamToEdit);
        int Remove(int teamId);
    }
}