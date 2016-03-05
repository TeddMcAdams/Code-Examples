using System;
using System.Configuration;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.AdminWorkflows.ProductWorkflows;
using FlooringProgram.UI.AdminWorkflows.StateTaxWorkflows;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows
{
    class AdminMenu
    {
        internal void Execute()
        {
            do
            {
                AdminChoices choice = AdminPrompts.AskForAdminMenuChoice();
                switch (choice)
                {
                    case AdminChoices.Product:
                        var productMenu = new ProductMenu();
                        productMenu.Execute();
                        break;
                    case AdminChoices.StateTax:
                        var stateTaxMenu = new StateTaxMenu();
                        stateTaxMenu.Execute();
                        break;
                    case AdminChoices.ChangePassword:
                        Console.Write("\n  Current password: {0}", ConfigurationManager.AppSettings.Get("Admin"));
                        Console.Write("\n\n  Enter new password: ");
                        string input = Console.ReadLine();
                        Console.Write("\n\n Change password to: \"{0}\"? ", input);
                        if (Prompts.Confirmation())
                        {
                            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            config.AppSettings.Settings.Remove("Admin");
                            config.AppSettings.Settings.Add("Admin", input);
                            config.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("appSettings");
                        }
                        break;
                    case AdminChoices.Quit:
                        return;
                }
            } while (true);
        }
    }
}