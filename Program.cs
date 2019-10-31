using System;

namespace RPG_GAME
{
    class Program
    {
        static void Pause()
        {
            Console.ReadKey();
        }
        static void Clear()
        {
            Console.Clear();
        }

        static void Main(string[] args)
        {
            bool autosave = false;
            private static Autosave{get{return autosave;}

Data data = null;
            Results results = null;
            char option;
            do
            {
                Clear();
                option = MainMenu();
                switch (option)
                {
                    case '1':
                        NewGame();
                        break;
                    case '2':
                        data = LoadData();
                        break;
                    case '3':
                        results = Options(data);
                        break;
                    case '4':
                        
                        SaveResults(results);
                        break;
                    case '0':
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("Unknown option!");
                        break;
                }
                Console.WriteLine("Hit [Any] key...");
                Console.ReadKey();
            }
            while (option != '0');
        }

        private static void Options(bool autosave)
        {
            // is some checking here nedded?
            string decision;
            Console.WriteLine("Autosaves turned on? Type yes or no...");
            decision = Console.ReadLine();
            if (decision == "yes")
            {
                autosave = true;
            }

        }
        

        private static void SaveResults(Results results)
        {
            // is some checking here nedded?

            Console.Write("Enter filename: ");
            string filename = Console.ReadLine();

            Results.SaveToFile(filename, results);

            Console.WriteLine("OK!");
        }

       

        

        private static Data LoadData()
        {
            Console.Write("Enter filename: ");
            string filename = Console.ReadLine();
            Data data = Data.ReadFromFile(filename);
            Console.WriteLine("OK!");
            return data;
        }

        private static char MainMenu()
        {
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Load Game");
            Console.WriteLine("3. Options");
            Console.WriteLine("0. Quit");
            Console.Write("Select: ");

            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return key;
        }
    }
    }