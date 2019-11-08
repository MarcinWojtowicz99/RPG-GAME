using System;
using System.IO;

namespace RPG_GAME
{
    class Program
    {
        
        
        public static int basic_hp;
        public static int Basic_hp { get { return basic_hp; } }
        public static void Dragon()
        {
            Enemy Dragon = new Enemy(200, 1000, 3);
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
        }
        public static void Mermaid()
        {
            Enemy Mermaid = new Enemy(100, 350, 3);
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
        }
        public static void Human()
        {
            Enemy Human= new Enemy(basic_hp, 100, 1);
            Human.Stack(new Attack("Drunk punch", 100, "I'll show you my real power!"));
        }
        
        static void Main()
        {
             OpenMainMenu();
            Console.Clear();
            Console.WriteLine("-Hello traveler, what's your name?");
            string name = Console.ReadLine();
            char classification;
            Console.WriteLine("Nice to meet you, {0}! I'm Neth and let me introduce you our" +
                " kingdom of Omaghan. During your journey you can serve our queen as a soldier or as a warrior. By the way, could you tell me what's your actual speciality?", name);
            Character[] user = new Character[1];
            do
            {
                Console.WriteLine("(Type \"a\" to say you're a sorcerer or \"b\" to say that you're a sorcerer)");

                classification = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (classification == 'a')
                {
                  //  Warrior soldier = new Warrior(name);
                }
                else if (classification == 'b')
                {
                   // Sorcerer mag = new Sorcerer(name);
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                }


            } while (classification != 'b' && classification != 'a');
            if (autosave == true)
            {
                Data.AddtoData(name, 0);
                if (classification == 'b')
                {
                    Data.AddtoData("Class:Sorcerer", 1);
                }

                else
                {
                    Data.AddtoData("Class:Warrior", 1);
                }
            }
            //load nemesis and their attacks

            //listofnemesis


            //listofitems

            //below <20 non-weapons items
            //item index cannot equal zero

            //listofattacksforwarrior



            //listofattacksforsorcerer

        }

        public static string path = Directory.GetCurrentDirectory(); //universal way to save file and load
        static void Pause()
        {
            Console.ReadKey();
        }
        static void Clear()
        {
            Console.Clear();
        }

        public static bool autosave = true;
        public static bool Autosave { get { return autosave; } }
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
                Console.Clear();
                option = Menu();
                switch (option)
                {
                    case '1':
                        Main();
                       // NewGame();
                        break;
                    case '2':
                        LoadGame();
                        break;
                    case '3':

                        SaveGame();
                        break;
                    case '4':
                        Options();
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
                    string finalpath = path + "\\" + pather + ".txt";
                    StreamReader reader = new StreamReader(finalpath);
                    for (int i = 0; i < gamedata.Length; i++)
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
            
        }
        public static string name = Character.Nick();
        public static DateTime localDate = DateTime.Now;
        public static string loc1 = localDate.ToString();
        public static string loc = loc1.Replace(':', '_');
      
        public static string fileName = path + "\\" + name + loc + ".txt";
        public static string FILENAME { get { return fileName; } }


        
        //public void Choice(int dataindex, string question, string optionA, string optionB, string optionC, string optionD)
        //{
        //    char classification;
        //    do
        //    {
        //        Console.WriteLine(question);

        //        classification = Console.ReadKey().KeyChar;
        //        Console.WriteLine();
        //        if (classification == 'a')
        //        {

        //        }
        //        else if (classification == 'b')
        //        {

        //        }
        //        if (classification == 'c')
        //        {

        //        }
        //        else if (classification == 'd')
        //        {

        //        }
        //        else if (classification == 'e')
        //        {

        //        }
        //        else
        //        {
        //            Console.WriteLine("unknown command, try again...");
        //        }


        //    } while (classification != 'b' && classification != 'a' && classification != 'c' && classification != 'd' && classification != 'e' );
        //    if (autosave == true)
        //    {
        //        Data.AddtoData(classification.ToString(), dataindex);
        //    }
        //    static void Checkpoint()
        //    {
        //        if (autosave == true)
        //        {

        //            StreamWriter writer = new StreamWriter(fileName);
        //            for (int i = 0; i < gamedata.Length; i++)
        //            {
        //                writer.WriteLine(gamedata[i]);
        //            }

        //            writer.Close();


        //        }
        //    }
        // }
    }


}
    