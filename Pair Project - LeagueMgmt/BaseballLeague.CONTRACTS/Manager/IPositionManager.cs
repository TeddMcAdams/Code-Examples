using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseballLeague.MODELS;

namespace BaseballLeague.CONTRACTS.Manager
{
    public interface IPositionManager
    {
        Response<List<Position>> LoadAll();
    }
}
