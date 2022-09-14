using ADFS_Plugin;
using System;

namespace A_La_ADFS
{
    class Program
    {
        static void Main(string[] args)
        {
            UserAccountInputValidator userInputValidator = new UserAccountInputValidator();
            UserAccountChecker userAccountChecker = new UserAccountChecker();

            Console.WriteLine("Type 'start' to run the app");
            string userInput = Console.ReadLine();

            if (userInput == "start")
            {
                while (userInput != "stop")
                {
                    Console.WriteLine("Type user in format: 'username@domain'");

                    string user = Console.ReadLine();

                    if (userInputValidator.IsUserInputValid(user))
                    {
                        bool authResult = userAccountChecker.LookupAndAuthenticate(user).GetAwaiter().GetResult();

                        if (authResult)
                        {
                            Console.WriteLine("Authentication Successfull - ADFS handles the rest.");
                            Console.WriteLine("Type 'stop' to close the app or any key to run again.");
                        }
                        else
                        {
                            Console.WriteLine("Authentication Failed.");
                        }
                    }

                    userInput = Console.ReadLine();
                }
            }

            
        }
    }
}
