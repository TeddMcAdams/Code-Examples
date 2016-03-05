using System;
using System.Collections.Generic;
using System.Linq;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Utilities
{
    internal static class StateTaxDisplays
    {
        internal static void DrawStateTaxChoices()
        {
            var manager = new StateTaxManager();

            Response<List<StateTax>> response = manager.LoadAllStateTax();

            if (response.Success)
            {
                List<string> states = response.Data.Select(state => state.StateAbbreviation).ToList();
                Console.Write("\n  Choices: ");
                foreach (string stateAbbreviation in states)
                {
                    Console.Write((stateAbbreviation == states.Last()) ? "{0}.\n" : "{0}, ", stateAbbreviation);
                }
            }
        }

        internal static void DrawStateTax(StateTax stateTax)
        {
            Console.Write("\n{0,30} {1} {2}", stateTax.StateName, ":", "State");
            Console.Write("\n{0,30} {1} {2}", stateTax.StateAbbreviation, ":", "Abbreviation");
            Console.Write("\n{0,29}% {1} {2}", stateTax.TaxRate, ":", "Tax Rate");
        }
    }
}