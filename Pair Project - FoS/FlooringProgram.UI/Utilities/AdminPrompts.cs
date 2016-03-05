using System;
using System.Linq;
using FlooringProgram.BLL;
using FlooringProgram.Models.Enums;

namespace FlooringProgram.UI.Utilities
{
    internal static class AdminPrompts
    {
        internal static AdminChoices AskForAdminMenuChoice()
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Admin, AdminChoices.Quit);
                Displays.DrawAdminChoices();
                Console.Write("\n  Enter selection: ");
                string input = Console.ReadLine();
                if (input != null && input.ToUpper() == "PASSWORD")
                    return AdminChoices.ChangePassword;
                if ((input != null) && ((input.ToUpper() == "Q") || (input.ToUpper() == "QUIT")))
                    return AdminChoices.Quit;
                AdminChoices choice;
                if ((input != null) && Enum.TryParse(input, out choice) && (Int32.Parse(input) > 0) && (Int32.Parse(input) < 3))
                    return choice;
                Prompts.InvalidInput();
            } while (true);
        }

        internal static string AdminAskForProductType(MenuChoices choice, AdminChoices adminChoice)
        {
            var mananger = new ProductManager();
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Add New Product");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Enter product type: ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && ( (inputA.ToUpper() == "X") || (inputA.Length > 1) ) && !mananger.LoadAllProducts()
                                                                                                         .Data
                                                                                                         .Select(names => names.ProductType.ToUpper())
                                                                                                         .ToList()
                                                                                                         .Contains(inputA.ToUpper()))
                        {
                            return inputA.Substring(0, 1).ToUpper() + inputA.Substring(1).ToLower();
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Edit Product\n");
                        ProductDisplays.DrawProductChoices();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter product type: ");
                        string inputE = Console.ReadLine();
                        if (inputE != null &&
                            ( (inputE.ToUpper() == "X") || mananger.LoadAllProducts()
                                .Data
                                .Select(names => names.ProductType.ToUpper())
                                .ToList()
                                .Contains(inputE.ToUpper())))
                        {
                            if (inputE.Length > 1)
                            {
                                return inputE.Substring(0, 1).ToUpper() + inputE.Substring(1).ToLower();
                            }
                            return inputE;
                        }
                        Prompts.InvalidInput();
                        break;
                    case MenuChoices.Lookup:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Lookup Product\n");
                        ProductDisplays.DrawProductChoices();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter product type: ");
                        string inputL = Console.ReadLine();
                        if (inputL != null &&
                            (inputL.ToUpper() == "X" || mananger.LoadAllProducts()
                                .Data
                                .Select(names => names.ProductType.ToUpper())
                                .ToList()
                                .Contains(inputL.ToUpper())))
                        {
                            if (inputL.Length > 1)
                            {
                                return inputL.Substring(0, 1).ToUpper() + inputL.Substring(1).ToLower();
                            }
                            return inputL;
                        }
                        Prompts.InvalidInput();
                        break;
                    case MenuChoices.Remove:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Remove Product\n");
                        ProductDisplays.DrawProductChoices();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter product type to remove: ");
                        string inputR = Console.ReadLine();
                        if (inputR != null &&
                            (inputR.ToUpper() == "X" || mananger.LoadAllProducts()
                                .Data
                                .Select(names => names.ProductType.ToUpper())
                                .ToList()
                                .Contains(inputR.ToUpper())))
                        {
                            if (inputR.Length > 1)
                            {
                                return inputR.Substring(0, 1).ToUpper() + inputR.Substring(1).ToLower();
                            }
                            return inputR;
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }

        internal static string AdminAskForCostPerSqFt(MenuChoices choice, AdminChoices adminChoice, string productType, string costPerSqFt)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                decimal costPer;
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Create New Product");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Enter product's cost per square foot: $ ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && ( (inputA.ToUpper() == "X") || ( decimal.TryParse(inputA, out costPer) && (costPer > 0M)) ))
                        {
                            return inputA;
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Edit Product : {0}\n", productType);
                        Console.Write("\n  Current cost per square foot: {0:C}", decimal.Parse(costPerSqFt));
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Press enter to keep current value, or modify here: $ ");
                        string inputE = Console.ReadLine();
                        if (inputE != null && ( (inputE.ToUpper() == "X") || (inputE == string.Empty) || ( decimal.TryParse(inputE, out costPer) && (costPer > 0)) ))
                        {
                            return inputE;
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }

        internal static string AdminAskForLaborCostPerSqFt(MenuChoices choice, AdminChoices adminChoice, string productType, string laborCostPerSqFt)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                decimal laborCostPer;
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Create New Product");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Enter product's labor cost per square foot: $ ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && ( (inputA.ToUpper() == "X") || (decimal.TryParse(inputA, out laborCostPer) && (laborCostPer > 0M)) ))
                        {
                            return inputA;
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Edit Product : {0}\n", productType);
                        Console.Write("\n  Current labor cost per square foot: {0:C}", decimal.Parse(laborCostPerSqFt));
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Press enter to keep current value, or modify here: $ ");
                        string inputE = Console.ReadLine();
                        if (inputE != null && ( (inputE.ToUpper() == "X") || (inputE == string.Empty) || ( decimal.TryParse(inputE, out laborCostPer) && (laborCostPer > 0)) ))
                        {
                            return inputE;
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }

        public static string AdminAskForStateAbbreviation(MenuChoices choice, AdminChoices adminChoice)
        {
            var mananger = new StateTaxManager();
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                Console.ForegroundColor = ConsoleColor.Cyan;
                switch (choice)
                {
                    case MenuChoices.Edit:
                        Console.Write("\n  Admin : Edit State\n");
                        StateTaxDisplays.DrawStateTaxChoices();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter state abbreviation: ");
                        string inputE = Console.ReadLine();
                        if (inputE != null &&
                            (inputE.ToUpper() == "X" || mananger.LoadAllStateTax()
                                .Data
                                .Select(state => state.StateAbbreviation.ToUpper())
                                .ToList()
                                .Contains(inputE.ToUpper())))
                        {
                            return inputE.ToUpper();
                        }
                        Prompts.InvalidInput();
                        break;
                    case MenuChoices.Remove:
                        Console.Write("\n  Admin : Remove State\n");
                        StateTaxDisplays.DrawStateTaxChoices();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter state abbreviation: ");
                        string inputR = Console.ReadLine();
                        if (inputR != null &&
                            (inputR.ToUpper() == "X" || mananger.LoadAllStateTax()
                                .Data
                                .Select(state => state.StateAbbreviation.ToUpper())
                                .ToList()
                                .Contains(inputR.ToUpper())))
                        {
                            return inputR.ToUpper();
                        }
                        Prompts.InvalidInput();
                        break;
                    case MenuChoices.Add:
                        Console.Write("\n  Admin : Add New State\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter state abbreviation: ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && (inputA.ToUpper() == "X" || (inputA.Length > 1) && !mananger.LoadAllStateTax()
                                                                                .Data
                                                                                .Select(state => state.StateAbbreviation.ToUpper())
                                                                                .ToList()
                                                                                .Contains(inputA.ToUpper())))
                        {
                            return inputA.ToUpper();
                        }
                        Prompts.InvalidInput();
                        break;
                    case MenuChoices.Lookup:
                        Console.Write("\n  Admin : Lookup State\n");
                        StateTaxDisplays.DrawStateTaxChoices();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter state abbreviation: ");
                        string inputL = Console.ReadLine();
                        if (inputL != null &&
                            (inputL.ToUpper() == "X" || mananger.LoadAllStateTax()
                                .Data
                                .Select(state => state.StateAbbreviation.ToUpper())
                                .ToList()
                                .Contains(inputL.ToUpper())))
                        {
                            return inputL.ToUpper();
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }

        internal static string AdminAskForStateName(MenuChoices choice, AdminChoices adminChoice, string stateAbbreviation, string stateName)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Add New State");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Enter State Name: ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && ((inputA.ToUpper() == "X") || (inputA.Length > 3)) )
                        {
                            return inputA;
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Edit State : {0}\n", stateAbbreviation);
                        Console.Write("\n  Current state name: {0}", stateName);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Press enter to keep current value, or modify here: ");
                        string inputE = Console.ReadLine();
                        if (inputE != null && ((inputE.ToUpper() == "X") || (inputE == string.Empty) || inputE.Length > 3))
                        {
                            return inputE;
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }

        internal static string AdminAskForTaxRate(MenuChoices choice, AdminChoices adminChoice, string stateAbbreviation, string taxRate)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                decimal tax;
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Add New State");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Enter Tax Rate: ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && ((inputA.ToUpper() == "X") || decimal.TryParse(inputA.Replace("%",""), out tax)))
                        {
                            return inputA.Replace("%", "");
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Admin : Edit State : {0}\n", stateAbbreviation);
                        Console.Write("\n  Current Tax Rate: {0}%", taxRate);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Press enter to keep current value, or modify here: ");
                        string inputE = Console.ReadLine();
                        if (inputE != null && ((inputE.ToUpper() == "X") || (inputE == string.Empty) || decimal.TryParse(inputE.Replace("%", ""), out tax)))
                        {
                            return inputE.Replace("%", "");
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }
    }
}