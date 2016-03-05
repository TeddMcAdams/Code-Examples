using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.ProductWorkflows
{
    internal class ProductMenu
    {
        internal void Execute()
        {
            do
            {
                MenuChoices choice = OrderPrompts.AskForMenuChoice(MenuChoices.Product, AdminChoices.Product);
                switch (choice)
                {
                    case MenuChoices.Lookup:
                        var lookup = new LookupProductWorkflow();
                        lookup.Execute();
                        break;
                    case MenuChoices.Add:
                        var add = new AddProductWorkflow();
                        add.Execute();
                        break;
                    case MenuChoices.Edit:
                        var edit = new EditProductWorkflow();
                        edit.Execute();
                        break;
                    case MenuChoices.Remove:
                        var remove = new RemoveProductWorkflow();
                        remove.Execute();
                        break;
                    case MenuChoices.Quit:
                        return;
                }
            } while (true);
        }
    }
}