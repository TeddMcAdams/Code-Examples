using System.Collections.Generic;
using BaseballLeague.MODELS;

namespace BaseballLeague.CONTRACTS.Repository
{
    public interface ITeamMgrRepository
    {
        List<TeamMgr> LoadAll();
        TeamMgr Load(int managerId);
        TeamMgr Add(TeamMgr managerToAdd);
        TeamMgr Edit(TeamMgr managerToEdit);
        int Remove(int managerId);
    }
}