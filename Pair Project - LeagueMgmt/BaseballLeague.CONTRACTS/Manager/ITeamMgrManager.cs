using System.Collections.Generic;
using BaseballLeague.MODELS;

namespace BaseballLeague.CONTRACTS.Manager
{
    public interface ITeamMgrManager
    {
        Response<List<TeamMgr>> LoadAll();
        Response<TeamMgr> Load(int teamMgrId);
        Response<TeamMgr> Add(TeamMgr teamMgrToAdd);
        Response<TeamMgr> Edit(TeamMgr teamMgrToEdit);
        Response<int> Remove(int teamMgrId);
    }
}