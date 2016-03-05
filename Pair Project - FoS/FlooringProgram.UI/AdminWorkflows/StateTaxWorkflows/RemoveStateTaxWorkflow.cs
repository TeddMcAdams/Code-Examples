using System;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.StateTaxWorkflows
{
    internal class RemoveStateTaxWorkflow
    {
        internal void Execute()
        {
            var manager = new StateTaxManager();

            var inputStateAbbreviation = AdminPrompts.AdminAskForStateAbbreviation(MenuChoices.Remove, AdminChoices.StateTax);
            if (Prompts.CheckForCancel(inputStateAbbreviation))
                return;

            Response<StateTax> loadResponse = manager.LoadStateTax(inputStateAbbreviation);
            if (loadResponse.Success)
            {
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Remove, AdminChoices.StateTax);
                StateTaxDisplays.DrawStateTax(loadResponse.Data);
                Console.Write("\n\n{0,30} {1} ", "Remove state?", ":");
                if (Prompts.Confirmation())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Response<StateTax> removeResponse = manager.RemoveStateTax(inputStateAbbreviation);
                    Console.Write("\n\n  {0}  Press any key to return. ", removeResponse.Message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\n  Remove state cancelled.  Press any key to return. ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.Write("\n  {0}  Press any key to return. ", loadResponse.Message);
            }
            Console.ReadKey();
        }
    }
} 