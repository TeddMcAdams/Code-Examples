using System.Collections.Generic;
using BaseballLeague.MODELS;

namespace BaseballLeague.CONTRACTS.Manager
{
    public interface IPlayerManager
    {
        Response<List<Player>> LoadAll();
        Response<Player> Load(int playerId);
        Response<Player> Add(Player playerToAdd);
        Response<Player> Edit(int playerId, Player playerToEdit);
        Response<int> Remove(int playerId);
    }
}
