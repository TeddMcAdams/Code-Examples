using FlooringProgram.Models.Enums;
using FlooringProgram.UI.AdminWorkflows;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.Workflows
{
    internal class MainMenu
    {
        internal void Execute()
        {
            do
            {
                MenuChoices choice = OrderPrompts.AskForMenuChoice(MenuChoices.Empty, AdminChoices.Empty);
                switch (choice)
                {
                    case MenuChoices.Lookup:
                        var lookupOrderWorkflow = new LookupOrderWorkflow();
                        lookupOrderWorkflow.Execute();
                        break;
                    case MenuChoices.Add:
                        var addOrderWorkflow = new AddOrderWorkflow();
                        addOrderWorkflow.Execute();
                        break;
                    case MenuChoices.Edit:
                        var editOrderWorkflow = new EditOrderWorkflow();
                        editOrderWorkflow.Execute();
                        break;
                    case MenuChoices.Remove:
                        var removeOrderWorkflow = new RemoveOrderWorkflow();
                        removeOrderWorkflow.Execute();
                        break;
                    case MenuChoices.Admin:
                        var adminMenu = new AdminMenu();
                        adminMenu.Execute();
                        break;
                    case MenuChoices.Quit:
                        return;
                }
            } while (true);
        }
    }
}