using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;

namespace FlooringProgram.UI.Utilities
{
    internal static class OrderPrompts
    {
        internal static MenuChoices AskForMenuChoice(MenuChoices choice, AdminChoices adminChoice)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                Displays.DrawMenuChoices();
                Console.Write("\n  Enter selection: ");
                string input = Console.ReadLine();
                if ( (input != null) && ( (input.ToUpper() == "Q") || (input.ToUpper() == "QUIT") ) )
                    return MenuChoices.Quit;
                if ((input != null) && (input == ConfigurationManager.AppSettings.Get("Admin")) )
                    return MenuChoices.Admin;
                MenuChoices inputChoice;
                if ( (input != null) && Enum.TryParse(input, out inputChoice) && (int.Parse(input) > 0) && (int.Parse(input) < 5) )
                    return inputChoice;
                Prompts.InvalidInput();
            } while (true);
        }

        internal static string AskForDate(MenuChoices choice, AdminChoices adminChoice)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                DateTime date;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n  Press enter for today.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\n\n  Enter a date: ");
                string inputE = Console.ReadLine();
                if (inputE != null && inputE == "")
                    return DateTime.Today.ToString("MM/dd/yyyy");
                if (inputE != null && inputE.ToUpper() == "X")
                    return inputE;
                if (DateTime.TryParse(inputE, out date))
                {
                    return date.ToString("MM/dd/yyyy");
                }
                Prompts.InvalidInput();
            } while (true);
        }

        internal static string AskForCustomerName(MenuChoices choice, AdminChoices adminChoice, string customerName)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Full name or company name.\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter customer name: ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && (inputA.ToUpper() == "X" || inputA.Length > 1))
                        {
                            return inputA;
                        }
                        break;
                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Current customer name: {0}", customerName);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Press enter to keep current value, or modify here: ");
                        string inputE = Console.ReadLine();
                        if (inputE != null && (inputE.ToUpper() == "X" || inputE == string.Empty || inputE.Length > 1))
                        {
                            return inputE;
                        }
                        break;
                }
                Prompts.InvalidInput();
            } while (true);
        }

        internal static string AskForStateAbbreviation(MenuChoices choice, AdminChoices adminChoice, string stateAbbreviation)
        {
            var mananger = new StateTaxManager();
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                Console.ForegroundColor = ConsoleColor.Cyan;
                StateTaxDisplays.DrawStateTaxChoices();
                Console.ForegroundColor = ConsoleColor.Gray;
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.Write("\n  Enter state abbreviation: ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && (inputA.ToUpper() == "X" || mananger.LoadAllStateTax()
                                                                                .Data
                                                                                .Select(state => state.StateAbbreviation.ToUpper())
                                                                                .ToList()
                                                                                .Contains(inputA.ToUpper())))
                        {
                            return inputA.ToUpper();
                        }
                        Prompts.InvalidInput();
                        break;
                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Current state abbreviation: {0}", stateAbbreviation);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Press enter to keep current value, or modify here: ");
                        string inputE = Console.ReadLine();
                        if (inputE != null && (inputE.ToUpper() == "X" || inputE == string.Empty || mananger.LoadAllStateTax()
                                                                                                        .Data
                                                                                                        .Select(state => state.StateAbbreviation.ToUpper())
                                                                                                        .ToList()
                                                                                                        .Contains(inputE.ToUpper())))
                        {
                            return inputE.ToUpper();
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }

        internal static string AskForProductType(MenuChoices choice, AdminChoices adminChoice, string productType)
        {
            var mananger = new ProductManager();
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                Console.ForegroundColor = ConsoleColor.Cyan;
                ProductDisplays.DrawProductChoices();
                Console.ForegroundColor = ConsoleColor.Gray;
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.Write("\n  Enter product type: ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && (inputA.ToUpper() == "X" || mananger.LoadAllProducts()
                                                                            .Data
                                                                            .Select(names => names.ProductType.ToUpper())
                                                                            .ToList()
                                                                            .Contains(inputA.ToUpper())))
                        {
                            return inputA.Substring(0,1).ToUpper() + inputA.Substring(1).ToLower();
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Current product type: {0}", productType);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Press enter to keep current value, or modify here: ");
                        string inputE = Console.ReadLine();
                        if (inputE != null && (inputE.ToUpper() == "X" || inputE == string.Empty || mananger.LoadAllProducts()
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
                }
            } while (true);
        }

        internal static string AskForTotalArea(MenuChoices choice, string totalArea)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Add, AdminChoices.Quit);
                decimal area;
                switch (choice)
                {
                    case MenuChoices.Add:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Square feet only.\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n  Enter total area: ");
                        string inputA = Console.ReadLine();
                        if (inputA != null && (inputA.ToUpper() == "X" || Decimal.TryParse(inputA, out area) && area > 0))
                        {
                            return inputA;
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  Current total area: {0}", totalArea);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  Press enter to keep current value, or modify here: ");
                        string inputE = Console.ReadLine();
                        if (inputE != null && (inputE.ToUpper() == "X" || inputE == string.Empty || ( Decimal.TryParse(inputE, out area) && area > 0)))
                        {
                            return inputE;
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }

        internal static string AskForOrderNumber(MenuChoices choice, AdminChoices adminChoice, List<Order> orders, string date)
        {
            do
            {
                Console.Clear();
                Displays.DrawTitle(choice, adminChoice);
                switch (choice)
                {
                    case MenuChoices.Lookup:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  {0} : Enter order number for details.", date);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  {0,-10}{1,-30}{2,-8}{3,10}\n", "Order #", " Customer Name", "State", "Total Price");
                        foreach (Order order in orders)
                        {
                            OrderDisplays.DrawOrderSmall(order);
                        }
                        Console.Write("\n\n  Enter order number: ");
                        string inputL = Console.ReadLine();
                        int orderNumberL;
                        if (inputL != null && (inputL.ToUpper() == "X" || int.TryParse(inputL, out orderNumberL) && orders
                                                                                                                    .Select(order => order.OrderNumber)
                                                                                                                    .ToList()
                                                                                                                    .Contains(orderNumberL)))
                        {
                            return inputL;
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Remove:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  {0} : Select order to remove.", date);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  {0,-10}{1,-30}{2,-8}{3,10}\n", "Order #", " Customer Name", "State", "Total Price");
                        foreach (Order order in orders)
                        {
                            OrderDisplays.DrawOrderSmall(order);
                        }
                        Console.Write("\n\n  Enter order number: ");
                        string inputR = Console.ReadLine();
                        int orderNumberR;
                        if (inputR != null && (inputR.ToUpper() == "X" || int.TryParse(inputR, out orderNumberR) && orders
                                                                                                                    .Select(order => order.OrderNumber)
                                                                                                                    .ToList()
                                                                                                                    .Contains(orderNumberR)))
                        {
                            return inputR;
                        }
                        Prompts.InvalidInput();
                        break;

                    case MenuChoices.Edit:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n  {0} : Select order to edit.", date);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\n\n  {0,-10}{1,-30}{2,-8}{3,10}\n", "Order #", " Customer Name", "State", "Total Price");
                        foreach (Order order in orders)
                        {
                            OrderDisplays.DrawOrderSmall(order);
                        }
                        Console.Write("\n\n  Enter order number: ");
                        string inputE = Console.ReadLine();
                        int orderNumberE;
                        if (inputE != null && (inputE.ToUpper() == "X" || int.TryParse(inputE, out orderNumberE) && orders
                                                                                                                    .Select(order => order.OrderNumber)
                                                                                                                    .ToList()
                                                                                                                    .Contains(orderNumberE)))
                        {
                            return inputE;
                        }
                        Prompts.InvalidInput();
                        break;
                }
            } while (true);
        }
    }
}