using System.Collections.Generic;
using FlooringProgram.Models;

namespace FlooringProgram.Contracts
{
    public interface IStateTaxRepository
    {
        void AddStateTax(StateTax stateTaxToAdd);
        void EditStateTax(StateTax stateTaxToUpdate);
        void RemoveStateTax(string stateAbbreviationToRemove);
        StateTax LoadStateTax(string stateAbbreviation);
        List<StateTax> LoadAllStateTax();
    }
}