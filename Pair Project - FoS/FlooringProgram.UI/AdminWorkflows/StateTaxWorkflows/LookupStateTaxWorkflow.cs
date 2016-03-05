using System;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.StateTaxWorkflows
{
    class LookupStateTaxWorkflow
    {
        public void Execute()
        {
            var manager = new StateTaxManager();

            string inputStateTax = AdminPrompts.AdminAskForStateAbbreviation(MenuChoices.Lookup, AdminChoices.StateTax);
            if (Prompts.CheckForCancel(inputStateTax))
                return;

            Response<StateTax> response = manager.LoadStateTax(inputStateTax);
            Console.Clear();
            Displays.DrawTitle(MenuChoices.Lookup, AdminChoices.StateTax);
            if (response.Success)
            {
                StateTaxDisplays.DrawStateTax(response.Data);
                Console.Write("\n\n  Press any key to return. ");
            }
            else
            {
                Console.Write("\n  {0}  Press any key to return. ", response.Message);
            }
            Console.ReadKey();
        }
    }
}