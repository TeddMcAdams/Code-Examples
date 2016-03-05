using System.Collections.Generic;
using BaseballLeague.MODELS;

namespace BaseballLeague.CONTRACTS.Repository
{
    public interface IPlayerRepository
    {
        List<Player> LoadAll();
        Player Load(int playerId);
        Player Add(Player playerToAdd);
        Player Edit(int playerId, Player playerToEdit);
        int Remove(int playerId);
    }
}