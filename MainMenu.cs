using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RPG_GAME
{
    class MainMenu
    {

      
        public static string path = Directory.GetCurrentDirectory(); //universal way to save file and load
     public static string Path { get { return path; } }
        static void Pause()
        {
            Console.ReadKey();
        }
        static void Clear()
        {
            Console.Clear();
        }

        public static bool autosave = true;
        public static bool Autosave{get{return autosave;} }
        public static string[] data;
        public static string[] gamedata { get { return data; } }
        public static void SaveGame()
        {
            
                Console.WriteLine("Type the name of your save: ");
                string nameoffile = Console.ReadLine();
                string finalpathforsave = path + "\\" + nameoffile + ".txt";
                StreamWriter writer = new StreamWriter(finalpathforsave);
                for (int i = 0; i < gamedata.Length; i++)
                {
                    writer.WriteLine(gamedata[i]);
                }

                writer.Close();
            
            
        }
        private static void Options()
        {
            string decision;
            bool correct;
            do
            {
                Console.WriteLine("Autosaves turned on? Type \"yes\" or \"no\"...");
                decision = Console.ReadLine();
                if (decision == "yes")
                {
                    autosave = true;
                    correct = true;
                }
                else if (decision == "no")
                {
                    autosave = false;
                    correct = true;
                }
                else
                {
                    Console.WriteLine("Unknown command, try again...");
                    correct = false;
                }


            } while (correct != true);
            
        }
      
        private static char Menu()
        {
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Load Game");
            Console.WriteLine("3. Save Game");
            Console.WriteLine("4. Options");
            Console.WriteLine("0. Quit");
            Console.Write("Select: ");

            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return key;
        }
        public static void OpenMainMenu()
        {
            
            char option;
            do
            {
                Clear();
                option = Menu();
                switch (option)
                {
                    case '1':
                        MainMenu.NewGame();
                        break;
                    case '2':
                        MainMenu.LoadGame();
                        break;
                    case '3':

                        MainMenu.SaveGame();
                        break;
                    case '4':
                        MainMenu.Options();
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
            Environment.Exit(0);//terminate console

        }
        public static void LoadGame()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(path, "*.txt");
                if (filePaths.Length > 1)
                {
                    Console.WriteLine("Which save would you like to read? Type only the name of text file .txt");
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        Console.WriteLine(filePaths[i]);
                    }
                    string pather = Console.ReadLine();
                    string finalpath =path+"\\"+pather+".txt";
                    StreamReader reader = new StreamReader(finalpath);
                    for(int i=0; i<gamedata.Length;i++)
                    {
                        gamedata[i] = reader.ReadLine();
                    }
                    reader.Close();

                }
                else
                {
                    StreamReader reader = new StreamReader(filePaths[0]);
                    for (int i = 0; i < gamedata.Length; i++)
                    {
                        gamedata[i] = reader.ReadLine();
                    }
                    reader.Close();

                    //string descr = reader.ReadLine();
                    //int count = int.Parse(reader.ReadLine());
                    //double[] samples = new double[count];
                    //for (int i = 0; i < count; i++)
                    //{
                    //    samples[i] = double.Parse(reader.ReadLine());
                    //}
                }
            }
            catch (FormatException e)
            {
                Exception bex = new Exception("The file is corrupted!", e);
                throw bex;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("You have no savegame :c");
            }
        }
       
        public static void NewGame()
        {
            Plot.Prologue(autosave);
        }
    }
}
