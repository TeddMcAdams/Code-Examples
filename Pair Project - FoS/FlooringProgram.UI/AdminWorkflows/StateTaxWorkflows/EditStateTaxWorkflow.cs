using System;
using System.Globalization;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.StateTaxWorkflows
{
    internal class EditStateTaxWorkflow
    {
        internal void Execute()
        {
            var manager = new StateTaxManager();


            string inputStateAbbreviation = AdminPrompts.AdminAskForStateAbbreviation(MenuChoices.Edit, AdminChoices.StateTax);
            if (Prompts.CheckForCancel(inputStateAbbreviation))
                return;

            Response<StateTax> loadResponse = manager.LoadStateTax(inputStateAbbreviation);

            if (loadResponse.Success)
            {
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Edit, AdminChoices.StateTax);
                StateTaxDisplays.DrawStateTax(loadResponse.Data);
                Console.Write("\n\n{0,30} {1} ", "Edit this state?", ":");
                if (!Prompts.Confirmation())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\n  Edit state cancelled.  Press any key to return. ");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return;
                }
            }
            else
            {
                Console.Write("\n  {0}  Press any key to return. ", loadResponse.Message);
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Displays.DrawTitle(MenuChoices.Edit, AdminChoices.StateTax);

            // Ask to edit state name
            string editedStateName = AdminPrompts.AdminAskForStateName(MenuChoices.Edit, AdminChoices.StateTax, inputStateAbbreviation,
                loadResponse.Data.StateName);
            if (Prompts.CheckForCancel(editedStateName))
                return;
            if (Prompts.CheckForNotEmpty(editedStateName))
                loadResponse.Data.StateName = editedStateName;

            // Ask to edit state tax rate
            string editedTaxRate = AdminPrompts.AdminAskForTaxRate(MenuChoices.Edit, AdminChoices.StateTax, inputStateAbbreviation,
                loadResponse.Data.TaxRate.ToString(CultureInfo.CurrentCulture));
            if (Prompts.CheckForCancel(editedTaxRate))
                return;
            if (Prompts.CheckForNotEmpty(editedTaxRate))
                loadResponse.Data.TaxRate = decimal.Parse(editedTaxRate);

            Console.Clear();
            Displays.DrawTitle(MenuChoices.Edit, AdminChoices.StateTax);
            StateTaxDisplays.DrawStateTax(loadResponse.Data);

            Console.Write("\n\n{0,30} {1} ", "Submit edited state?", ":");

            if (Prompts.Confirmation())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Response<StateTax> editRepsonse = manager.EditStateTax(loadResponse.Data);
                Console.Write("\n\n  {0}  Press any key to return. ", editRepsonse.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n  State edit cancelled.  Press any key to return. ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey();
        }
    }
}