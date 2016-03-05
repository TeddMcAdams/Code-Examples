using System.Collections.Generic;
using System.Linq;
using FlooringProgram.Contracts;
using FlooringProgram.Models;

namespace FlooringProgram.Data.Repositories
{
    internal class StateTaxRepository : IStateTaxRepository
    {
        public void AddStateTax(StateTax stateTaxToAdd)
        {
            List<StateTax> allStateTax = LoadAllStateTax();

            allStateTax.Add(stateTaxToAdd);
            CsvWriter.WriteAllStateTax(allStateTax);
        }

        public void EditStateTax(StateTax stateTaxToEdit)
        {
            List<StateTax> allStateTax = LoadAllStateTax();

            allStateTax = allStateTax.Where(stateTax => stateTax.StateAbbreviation != stateTaxToEdit.StateAbbreviation).ToList();
            allStateTax.Add(stateTaxToEdit);
            CsvWriter.WriteAllStateTax(allStateTax);
        }

        public void RemoveStateTax(string stateAbbreviationToRemove)
        {
            List<StateTax> allStateTax = LoadAllStateTax();

            allStateTax = allStateTax.Where(stateTax => stateTax.StateAbbreviation != stateAbbreviationToRemove).ToList();
            CsvWriter.WriteAllStateTax(allStateTax);
        }

        public StateTax LoadStateTax(string stateAbbreviation)
        {
            return LoadAllStateTax().Single(stateTax => stateTax.StateAbbreviation == stateAbbreviation);
        }

        public List<StateTax> LoadAllStateTax()
        {
            return CsvWriter.ReadAllStateTax();
        }
    }
}