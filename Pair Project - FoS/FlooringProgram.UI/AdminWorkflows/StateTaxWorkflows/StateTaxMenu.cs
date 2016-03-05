using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.StateTaxWorkflows
{
    internal class StateTaxMenu
    {
        internal void Execute()
        {
            do
            {
                MenuChoices choice = OrderPrompts.AskForMenuChoice(MenuChoices.StateTax, AdminChoices.StateTax);
                switch (choice)
                {
                    case MenuChoices.Lookup:
                        var lookup = new LookupStateTaxWorkflow();
                        lookup.Execute();
                        break;
                    case MenuChoices.Add:
                        var add = new AddStateTaxWorkflow();
                        add.Execute();
                        break;
                    case MenuChoices.Edit:
                        var edit = new EditStateTaxWorkflow();
                        edit.Execute();
                        break;
                    case MenuChoices.Remove:
                        var remove = new RemoveStateTaxWorkflow();
                        remove.Execute();
                        break;
                    case MenuChoices.Quit:
                        return;
                }
            } while (true);
        }
    }
}