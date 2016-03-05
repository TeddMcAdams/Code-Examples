using System;
using FlooringProgram.Models.Enums;

namespace FlooringProgram.UI.Utilities
{
    internal static class Displays
    {
        // ┌ ┐└ ┘ ┴ ─ │ ├ ┬ ┴
        internal static void DrawTitle(MenuChoices choice, AdminChoices adminChoice)
        {
            Console.Write("  ┌───────────────────┐");
            Console.Write("\n  │  {0,-15}  │  {1,-10} {2,26}", DrawTitleLabel(choice, adminChoice), DateTime.Today.ToString("MM/dd/yyyy"), DrawQuitMsg(choice));
            if (choice == MenuChoices.Empty || choice == MenuChoices.Product || choice == MenuChoices.StateTax)
            {
                Console.Write("\n ┌┴─────────────┬─────┴────────┬──────────────┬──────────────┐");
                return;
            }
            if (choice == MenuChoices.Admin)
            {
                Console.Write("\n ┌┴───────────────────┴────────┬─────────────────────────────┐");
                return;
            }
            Console.Write("\n  └───────────────────┴───────────────────────────────────────\n");
        }

        private static string DrawTitleLabel(MenuChoices choice, AdminChoices adminChoice)
        {
            string label;
            switch (adminChoice)
            {
                case AdminChoices.Product:
                    label = "Product";
                    break;
                case AdminChoices.StateTax:
                    label = "State";
                    break;
                default:
                    label = "Order";
                    break;
            }
            switch (choice)
            {
                case MenuChoices.Product:
                    return "Product DB";
                case MenuChoices.StateTax:
                    return "State Tax DB";
                case MenuChoices.Lookup:
                    return $"Lookup {label}";
                case MenuChoices.Add:
                    return $"Add {label}";
                case MenuChoices.Edit:
                    return $"Edit {label}";
                case MenuChoices.Remove:
                    return $"Remove {label}";
                case MenuChoices.Admin:
                    return "Administrator";
                default:
                    return "Flooring Orders";
            }
        }

        private static string DrawQuitMsg(MenuChoices choice)
        {
            switch (choice)
            {
                case MenuChoices.Product:
                    return "Enter Q to quit ";
                case MenuChoices.StateTax:
                    return "Enter Q to quit ";
                case MenuChoices.Admin:
                    return "Enter Q to quit ";
                case MenuChoices.Empty:
                    return "Enter Q to quit ";
                default:
                    return "Enter X to cancel ";
            }
        }

        internal static void DrawMenuChoices()
        {
            Console.Write("\n │  [1] Lookup  │   [2] Add    │  [3]  Edit   │  [4] Remove  │");
            Console.Write("\n └──────────────┴──────────────┴──────────────┴──────────────┘\n");
        }

        internal static void DrawAdminChoices()
        {
            Console.Write("\n │       [1] Product DB        │      [2] State Tax DB       │");
            Console.Write("\n └─────────────────────────────┴─────────────────────────────┘\n");
        }
    }
}