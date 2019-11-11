using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
namespace RPG_GAME
{
    class Program
    { //basic game values
        public static int basic_hp = 1000; //easy changable nb of basic HP of character
        public static int maxequipment = 10; //max nb of equipment character can carry
        public static int startmoney = 1000;
        public static int basic_damage = 50; //character "punch" damage
        public static int hpboost = 500; //extra hp for warrior
        public static int nbofdata = 10;//nb of data stored-changable in case future game plot changes
        public static int nbofitems = 5;//nb of items
        public static int nbofshopkeeperequipment = 3;//nb of items which shopkeeper can show you during one visit
        public int NbOfShopkeeperEQ{get{return nbofshopkeeperequipment;} set{nbofshopkeeperequipment=value;} }
        public static int Basic_hp { get { return basic_hp; } }
       
        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch

            //listofitems
            Item mythings = new Item(nbofitems);
            mythings.Stack(new Unit("Rubber Duck",12,25,0,int.MaxValue,"Rubbish"));
            mythings.Stack(new Unit("Broken sword", 45, 125, 20, int.MaxValue, "Sword"));
            mythings.Stack(new Unit("Small potion", 45, 500, 50, 1, "Potion"));
            mythings.Stack(new Unit("Medium potion", 45, 1100, 50, 1, "Potion"));
            mythings.Stack(new Unit("Large potion", 45, 1500, 50, 1, "Potion"));
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
            
            //Loading NPC
            // NPC shopkeeper = new NPC(nbofshopkeeperequipment,mythings);

            stopwatch.Stop();
            for (int i = 0; i <= 100; ++i)
            {
                Console.Write("\rLoading: {0}%", i);
                System.Threading.Thread.Sleep(10);
            }
            Console.WriteLine();
            Console.WriteLine("It took me: {0}ms to start! :D", stopwatch.ElapsedMilliseconds);
            System.Threading.Thread.Sleep(2000); 
            //gameplay
            OpenMainMenu(data);
            string name = data.ReadData(0);
            string clas = data.ReadData(1);

            if (clas == "Class Warrior")
            {
                Warrior user_Warrior = new Warrior(name, basic_hp, maxequipment, hpboost, basic_damage,startmoney);
                Sorcerer user_Sorcerer = new Sorcerer("FAKE", 0,0,0,0);

                DefaultMenu(user_Sorcerer, user_Warrior, data, mythings,Mermaid,Dragon);
            }
            else
            {
                Sorcerer user_Sorcerer = new Sorcerer(name, basic_hp, maxequipment, basic_damage,startmoney);
                Warrior user_Warrior = new Warrior("NAN", 0, 0, 0, 0,0);
                DefaultMenu(user_Sorcerer,user_Warrior,data, mythings, Mermaid, Dragon);
            }

            //listofattacksforwarrior



            //listofattacksforsorcerer

            bool iscompleted = doPlot(data);
            if(iscompleted==true)
            {
              //  ending_screen(user);
            }
        }

        public static string path = Directory.GetCurrentDirectory(); //universal way to save file and load
        public static string Path { get { return path; } }


        public static bool autosave = true;
        public static bool Autosave { get { return autosave; } }

        public static bool doPlot(Data data)
        {

            return true;
        }
        private static void ending_screen(Character user)
        {
            Console.WriteLine("Congratulations, traveler, You defeated the dragon!");
           // Console.WriteLine("Your final score: "+score);
        }
        public static void MakeYourChoice(Data data, bool IsTimeLimit, string description, string optionA, string optionB, string optionC, string optionD)
        {
            bool toReturn = false;
            do
            {
                Console.Clear();
                
            Console.WriteLine(description);
           
            
            Console.WriteLine("1. "+optionA);
            Console.WriteLine("2. "+optionB);
            Console.WriteLine("3. "+optionC);
            Console.WriteLine("4. "+optionD);
            Console.Write("Select: ");
                int toOverWrite =data.WhereIsNull(data);
            char choice = Console.ReadKey().KeyChar;
                if (IsTimeLimit == true)
                {
                    Console.WriteLine();
                    for (int i = 10; i >= 0; i--)
                    {
                        Console.Write("\rTime left: {0}", i);
                        System.Threading.Thread.Sleep(1000);
                    }
                    if(choice!='1'|| choice != '2' || choice != '3' || choice != '4' )
                    {
                        Random rnd = new Random();
                        int random = rnd.Next(4);
                        if(random==0)
                        {
                            choice = '1';
                        }
                        else if(random == 1)
                        {
                            choice = '2';
                        }
                        else if (random == 2)
                        {
                            choice = '3';
                        }
                        else
                        {
                            choice ='4';
                        }
                    }
                }
                Console.WriteLine();
            
                
                switch (choice)
                {
                    case '1':
                        data.AddtoData(optionA,toOverWrite);
                        break;
                    case '2':
                        data.AddtoData(optionB, toOverWrite);
                        break;
                    case '3':
                        data.AddtoData(optionC, toOverWrite);
                        break;
                    case '4':
                        data.AddtoData(optionD, toOverWrite);
                        break;

                    default:
                        toReturn = true;
                        Console.WriteLine("Unknown option!");
                        break;
                }

            }
            while (toReturn==false);
        }
        private static void Options()
        {
            string decision;
            bool correct;
            do
            {
                Console.WriteLine("Autosaves turned on? Type \"yes\" or \"no\"...");
                Console.Write("Your answer: ");
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

       
        public static void OpenMainMenu(Data data)
        {
            bool Toreturn= true;
            char option;
            do
            {
                Toreturn = true;
                Console.Clear();
                Console.WriteLine("1. New Game");
                Console.WriteLine("2. Load Game");
                Console.WriteLine("3. Save Game");
                Console.WriteLine("4. Options");
                Console.WriteLine("0. Quit");
                Console.Write("Select: ");

                option = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.Clear();
                switch (option)
                {
                    case '1':
                        NewGame(data);
                        Console.Clear();
                        break;
                    case '2':
                        data.LoadGame(data);
                        Console.WriteLine("Loaded.");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case '3':

                        data.SaveGame(data);
                        Toreturn = false;
                        break;
                    case '4':
                        Options();
                        Toreturn = false;
                        break;

                    case '0':
                        Console.WriteLine("Bye!");
                        System.Threading.Thread.Sleep(1000);
                        Environment.Exit(0);//terminate console

                        break;
                    default:
                        Console.WriteLine("Unknown option!");
                        Toreturn = false;
                        System.Threading.Thread.Sleep(1000);
                        break;
                }
                
                
            }
            while (Toreturn != true);


        }
        

        public static void NewGame(Data data)
        {
            Prologue(data);
            Console.Clear();
           

        }
        public static void DefaultMenu(Sorcerer user_Sorcerer,Warrior user_Warrior, Data data, Item mythings, Enemy mermaid, Enemy dragon)
        {
            bool ToReturn = true ;
            do
            {
                Console.Clear();
                if (data.gamedata[1] == "Class Warrior")
                {
                    Console.WriteLine("Money: " + user_Warrior.Money_player);
                    Console.WriteLine("HP: " + user_Warrior.Actual_hp);
                    Console.WriteLine("Score: " + user_Warrior.Score);
                }
                else
                {
                    Console.WriteLine("Money: " + user_Sorcerer.Money_player);
                    Console.WriteLine("HP: " + user_Sorcerer.Actual_hp);
                    Console.WriteLine("Score: " + user_Sorcerer.Score);
                }
                Console.WriteLine("What would you like to do now?");
                
                Console.WriteLine("1. Open Equipment");
            Console.WriteLine("2. Open Map");
            Console.WriteLine("3. Main Menu");
            if(data.gamedata[1]!= "Class Warrior")
            {
                Console.WriteLine("4. Teleport");
            }
            Console.Write("Select: ");

            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            
                Console.Clear();
                switch (key)
                {
                    case '1':
                        if (data.gamedata[1] != "Class Warrior")
                        {
                            user_Sorcerer.ViewEquipment(mythings, user_Sorcerer, data,mermaid, dragon);
                        }
                        else
                        {
                            user_Warrior.ViewEquipment(mythings, user_Warrior, data, mermaid, dragon);
                        }
                        
                        break;
                    case '2':
                        if (data.gamedata[1] != "Class Warrior")
                        {
                            user_Sorcerer.Walk(data, user_Sorcerer,user_Warrior, mythings, mermaid, dragon);

                        }
                        else
                        {
                            user_Warrior.Walk(data, user_Sorcerer, user_Warrior, mythings, mermaid, dragon);
                        }
                        break;
                    case '3':

                        OpenMainMenu(data);

                        break;
                    case '4':
                        if (data.gamedata[1] != "Class Warrior")
                        {
                            user_Sorcerer.Teleport();
                        }
                        else
                        {
                            ToReturn = false;
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown option!");
                        ToReturn = false;
                        break;
                }

            }
            while (ToReturn!=true);
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
        
       
    }


}
    