using System.Collections.Generic;
using BaseballLeague.MODELS;

namespace BaseballLeague.CONTRACTS.Repository
{
    public interface IPositionRepository
    {
        List<Position> LoadAll();
    }
}
