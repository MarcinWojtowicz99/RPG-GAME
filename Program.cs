using System;
using System.IO;

namespace RPG_GAME
{
    class Program
    {
        public static int basic_hp = 1000;
        public static int maxequipment = 10;
        public static int basic_damage = 50;
        public static int hpboost = 500;
        public static int nbofdata = 50;
        public static int Basic_hp { get { return basic_hp; } }
        static void Main(string[] args)
        {
            //listofitems
            //below <20 non-weapons items
            //item index cannot equal zero
            Data data = new Data(nbofdata);
            //load nemesis and their attacks
            Enemy Dragon = new Enemy(200, 1000, 3);
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Enemy Mermaid = new Enemy(100, 350, 3);
            Mermaid.Stack(new Attack("Tail punch", 300, "THAT'S A LOT OF DAMAGE!"));
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Enemy Human = new Enemy(basic_hp, 100, 1);
            Human.Stack(new Attack("Drunk punch", 100, "I'll show you my real power!"));

            //gameplay
            OpenMainMenu(data);
            string name = data.ReadData(0);
            string clas = data.ReadData(1);

            if (clas == "Class Warrior")
            {
                Warrior user = new Warrior(name, basic_hp, maxequipment, hpboost, basic_damage);
                DefaultMenuWarrior(user,data);
            }
            else
            {
                Sorcerer user = new Sorcerer(name, basic_hp, maxequipment, basic_damage);
                DefaultMenuSorcerer(user,data);
            }
            
            //listofattacksforwarrior



            //listofattacksforsorcerer

        }

        public static string path = Directory.GetCurrentDirectory(); //universal way to save file and load
        public static string Path { get { return path; } }


        public static bool autosave = true;
        public static bool Autosave { get { return autosave; } }

        
        
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
        public static void OpenMainMenu(Data data)
        {

            char option;
            do
            {
                Console.Clear();
                option = Menu();
                switch (option)
                {
                    case '1':
                        NewGame(data);
                        break;
                    case '2':
                        data.LoadGame(data);
                        break;
                    case '3':

                        data.SaveGame(data);
                        break;
                    case '4':
                        Options();
                        break;

                    case '0':
                        Console.WriteLine("Bye!");
                        System.Threading.Thread.Sleep(1000);
                        Environment.Exit(0);//terminate console

                        break;
                    default:
                        Console.WriteLine("Unknown option!");
                        break;
                }
                Console.WriteLine("Hit [Any] key...");
                Console.ReadKey();
            }
            while (option != '1' || option != '2');


        }
        

        public static void NewGame(Data data)
        {
            Prologue(data);
            Console.Clear();
           

        }
        public static void DefaultMenuSorcerer(Sorcerer user, Data data)
        {
            Console.WriteLine("What would you like to do now?");

            Console.WriteLine("1. Open Equipment");
            Console.WriteLine("2. Open Map");
            Console.WriteLine("3. Main Menu");
            Console.WriteLine("4. Teleport");
            Console.Write("Select: ");

            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            do
            {
                Console.Clear();
                switch (key)
                {
                    case '1':
                        user.ViewEquipment();
                        break;
                    case '2':
                        user.Walk();
                        break;
                    case '3':

                        OpenMainMenu(data);

                        break;
                    case '4':
                        user.Teleport();
                        break;

                    default:
                        Console.WriteLine("Unknown option!");
                        break;
                }

            }
            while (key != '1' || key != '2' || key != '3');
        }
        public static void DefaultMenuWarrior(Warrior user,Data data)
            {

            Console.WriteLine("What would you like to do now?");
          
            Console.WriteLine("1. Open Equipment");
            Console.WriteLine("2. Open Map");
            Console.WriteLine("3. Main Menu");
            Console.Write("Select: ");

            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            do
            {
                Console.Clear();
                switch (key)
                {
                    case '1':
                        user.ViewEquipment();
                        break;
                    case '2':
                        user.Walk();
                        break;
                    case '3':

                        OpenMainMenu(data);
                        break;
                    

                    default:
                        Console.WriteLine("Unknown option!");
                        break;
                }
                
            }
            while (key != '1' || key != '2'||key!='3');

        }
        public static string name;
        public static DateTime localDate = DateTime.Now;
        public static string loc1 = localDate.ToString();
        public static string loc = loc1.Replace(':', '_');
      
        public static string fileName = path + "\\" + name + loc + ".txt";
        public static string FILENAME { get { return fileName; } }


        public static void Prologue(Data data)
        {
            Console.Clear();
            Console.WriteLine("-Hello traveler, what's your name?");
            string name = Console.ReadLine();
            data.AddtoData(name, 0);
            char classification;
            Console.WriteLine("Nice to meet you, {0}! I'm Neth and let me introduce you our" +
                " kingdom of Omaghan. During your journey you can serve our queen as a soldier or as a warrior. By the way, could you tell me what's your actual speciality?", name);
            
            do
            {
                Console.WriteLine("(Type \"a\" to say you're a warrior or \"b\" to say that you're a sorcerer)");

                classification = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (classification == 'a')
                {
                    data.AddtoData("Class Warrior", 1);
                }
                else if (classification == 'b')
                {
                    data.AddtoData("Class Sorcerer", 1);
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                }


            } while (classification != 'b' && classification != 'a');
            
                data.AutoSaveGame(data);
            
        }
        public static void MainStory()
        {

        }
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
    