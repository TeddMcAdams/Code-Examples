using System;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.StateTaxWorkflows
{
    internal class AddStateTaxWorkflow
    {
        internal void Execute()
        {
            var manager = new StateTaxManager();

            // Ask for state abbreviation
            string stateAbbreviation = AdminPrompts.AdminAskForStateAbbreviation(MenuChoices.Add, AdminChoices.StateTax);
            if (Prompts.CheckForCancel(stateAbbreviation))
                return;

            // Ask for state name
            string stateName = AdminPrompts.AdminAskForStateName(MenuChoices.Add, AdminChoices.StateTax, stateAbbreviation, string.Empty);
            if (Prompts.CheckForCancel(stateName))
                return;

            // Ask for tax rate
            string taxRate = AdminPrompts.AdminAskForTaxRate(MenuChoices.Add, AdminChoices.StateTax, stateAbbreviation, string.Empty);
            if (Prompts.CheckForCancel(taxRate))
                return;

            // Create state tax
            var stateTaxToAdd = new StateTax()
            {
                StateAbbreviation = stateAbbreviation,
                StateName = stateName,
                TaxRate = decimal.Parse(taxRate)
            };

            // Display product and ask to submit
            Console.Clear();
            Displays.DrawTitle(MenuChoices.Add, AdminChoices.StateTax);
            StateTaxDisplays.DrawStateTax(stateTaxToAdd);

            Console.Write("\n\n{0,30} {1} ", "Add new state?", ":");

            if (Prompts.Confirmation())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Response<StateTax> response = manager.AddStateTax(stateTaxToAdd);
                Console.Write("\n\n  {0}  Press any key to return. ", response.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n  Add state cancelled.  Press any key to return. ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey();
        }
    }
}