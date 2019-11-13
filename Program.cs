using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    class Program
    { //basic game values
        public static int basichp = 1000; //easy changable nb of basic HP of character
        public static int maxequipment = 9; //max nb of equipment character can carry
        public static int startmoney = 1000;
        public static bool autosave = false;
        public static int index_border = 10;//border of indexes between Potion and Weapon
        public static int basic_damage = 50; //character "punch" damage
        public static int hpboost = 500; //extra hp for warrior
        public static int nbofdata = 11;//nb of data stored-changable in case future game plot changes
        public static int nbofitems = 10;//nb of items
        public static int nbofshopkeeperequipment = 3;//nb of items which shopkeeper can show you during one visit
        public int NbOfShopkeeperEQ{get{return nbofshopkeeperequipment;} set{nbofshopkeeperequipment=value;} }
        public bool Autosave { get { return autosave; }set { autosave = value; } }
        public static int BorderBetweenIndexes { get { return index_border; } }
        static void Main(string[] args)
        {
            
            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
            //listofitems
            Item mythings = new Item(nbofitems);
            mythings.Stack(new Unit("Rubber Duck",12,25,0,1,"Rubbish"));
            mythings.Stack(new Unit("Small potion", 45, 500, 400, 1, "Potion"));
            mythings.Stack(new Unit("Medium potion", 450, 1100, 670, 1, "Potion"));
            mythings.Stack(new Unit("Large potion", 1120, 1500, 1200, 1, "Potion"));
            mythings.Stack(new Unit("Mermaid shell", 450, 1500, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Gold tooth", 600, 1300, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Mermaid shell", 450, 1500, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Mermaid shell", 450, 1500, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Mermaid shell", 450, 1500, 0, 1, "Rubbish"));

            mythings.Stack(new Unit("Broken sword", 45, 125, 20, 1, "Sword"));
            //below <20 non-weapons items
            //item index cannot equal zero
            Data data = new Data(nbofdata);
            //load nemesis and their attacks
            Enemy Dragon = new Enemy("Dragon",1450, 1000, 3);
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Enemy Mermaid = new Enemy("Mermaid",650, 350, 3);
            Mermaid.Stack(new Attack("Tail punch", 300, "THAT'S A LOT OF DAMAGE!"));
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Enemy Human = new Enemy("Human", basichp, 100, 1);
            Human.Stack(new Attack("Drunk punch", 100, "I'll show you my real power!"));
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
            bool newbe=OpenMainMenu(data);
            if (data.gamedata[1] == "Class Warrior")
            {

                Warrior user_Warrior = new Warrior(data.gamedata[0], basichp, maxequipment, hpboost, basic_damage,startmoney);
                Sorcerer user_Sorcerer = new Sorcerer("FAKE", 0,0,0,0);
                Character user = user_Warrior;
                Updater(data,user,newbe,Mermaid,Human,Dragon);
                data.ReadData(data,user, user_Warrior, mythings);
                
                DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings,Mermaid,Dragon,Human);
            }
            else
            {
                Sorcerer user_Sorcerer = new Sorcerer(name, basichp, maxequipment, basic_damage,startmoney);
                Warrior user_Warrior = new Warrior("NAN", 0, 0, 0, 0,0);
                Character user = user_Sorcerer;
                Updater(data, user, newbe, Mermaid, Human, Dragon);
                data.ReadData(data, user, user_Warrior, mythings);

                DefaultMenu(user,user_Sorcerer,user_Warrior,data, mythings, Mermaid, Dragon,Human);
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
        
        public static bool doPlot(Data data)
        {
            return true;
        }
        private static void ending_screen(Character user)
        {
            Console.WriteLine("Congratulations, traveler, You defeated the dragon!");
           // Console.WriteLine("Your final score: "+score);
        }
        private static bool Updater(Data data, Character user,bool newbe, Enemy Mermaid, Enemy Human, Enemy Dragon)
        {
            if (newbe == true)
            {
                string name = data.gamedata[0];
                string clas = data.gamedata[1];
                user.UpdateEnemyKilled(Dragon, data);
                user.UpdateEnemyKilled(Mermaid, data);
                user.UpdateEnemyKilled(Human, data);
                data.gamedata[10] = "Unknowns: 0";
                user.UpdateHealth(user, data);
                user.UpdateScore(user, data);
                user.UpdateMoney(user, data);
                data.gamedata[6] = "Equipped: No";
                data.gamedata[5] = "Items: ";
                for (int i = 0; i < user.Equipment.Length / 2; i++)
                {
                    data.gamedata[5] += user.Equipment[i, 0] + "-";
                    data.gamedata[5] += user.Equipment[i, 1] + ";";
                }
                data.AutoSaveGame(data);
                return true;
            }
            return false;
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
                        data.gamedata[toOverWrite]="Choice"+toOverWrite+": "+optionA;
                        break;
                    case '2':
                        data.gamedata[toOverWrite] = "Choice" + toOverWrite + ": " + optionB;
                        break;
                    case '3':
                        data.gamedata[toOverWrite] = "Choice" + toOverWrite + ": " + optionC;
                        break;
                    case '4':
                        data.gamedata[toOverWrite] = "Choice" + toOverWrite + ": " + optionD;
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
            
            bool correct;
            do
            {
                Console.WriteLine("Autosaves turned on? Type \"y\" or \"n\"...");
                Console.Write("Your answer: ");
                char decision = Console.ReadKey().KeyChar;
                if (decision == 'y')
                {
                    autosave = true;
                    correct = true;
                }
                else if (decision == 'n')
                {
                    autosave = false;
                    correct = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Unknown command, try again...");
                    correct = false;
                }
            } while (correct != true);
        }
        public static bool OpenMainMenu(Data data)
        {
            
            
                bool newbie = false;
                int counter = 0;//easter egg
                bool Toreturn = true;
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
                if (option == '/') 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.Title="HACKERMODE";
                    Console.WriteLine(@"\Hello world!");
                    System.Threading.Thread.Sleep(4000);

                    for (int i=0; i<26;i++)
                    {

                        Console.Write("\rHacking pentagon servers, please wait");
                        
                        for(int j=0;j<3;j++)
                        {
                            Console.Write(".");
                            System.Threading.Thread.Sleep(1000);
                        }
                        Console.Clear();
                    }
                    Console.WriteLine("Connected! :D");
                    System.Threading.Thread.Sleep(1000);
                    Character user2 = new Character("IT",9999,9,9999,9999);
                    ending_screen(user2);
                }
                Console.WriteLine();
                    Console.Clear();
                    switch (option)
                    {
                        case '1':
                            newbie = NewGame(data);
                            Console.Clear();
                        return true;

                          
                        case '2':
                            data.LoadGame(data);

                            Console.Write("\rLoading.");

                            System.Threading.Thread.Sleep(100);
                            Console.Write("\rLoading..");
                            System.Threading.Thread.Sleep(100);
                            Console.Write("\rLoading...");
                            System.Threading.Thread.Sleep(100);
                            Console.Write("\rLoading....");
                            System.Threading.Thread.Sleep(100);
                            Console.Write("\rLoading complete!");
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
                            counter++;

                            Toreturn = false;
                            System.Threading.Thread.Sleep(1000);
                            if (counter == 10)//if user click wrong key 10 times easter egg starts...
                            {
                                Console.Clear();
                                Console.WriteLine("Oh, are you trying to break the game?” :c");
                                System.Threading.Thread.Sleep(2000);
                            }
                            if (counter == 15)
                            {
                                Console.Clear();
                                Console.WriteLine("C'mon stop...");
                                System.Threading.Thread.Sleep(2000);
                            }
                            if (counter == 16)
                            {
                                Console.Clear();
                                Console.WriteLine("Pls stop...");
                                System.Threading.Thread.Sleep(2000);
                            }
                            if (counter == 17)
                            {
                                Console.Clear();
                                Console.WriteLine("Okay... I'll crash...");
                                System.Threading.Thread.Sleep(2000);
                            }
                            if (counter == 18)
                            {
                                Console.Clear();
                                Console.WriteLine("Just kidding..");
                                System.Threading.Thread.Sleep(2000);

                            }
                            if (counter == 19)
                            {
                                Console.Clear();
                                Console.WriteLine("I can do this all they long...");
                                System.Threading.Thread.Sleep(2000);

                            }
                            if (counter == 20)
                            {
                                Console.Clear();
                                Console.WriteLine("I can do this all they long...");
                                System.Threading.Thread.Sleep(2000);

                            }
                            if (counter == 25)
                            {
                                Console.Clear();
                                Console.WriteLine("Okay, you won...");
                                System.Threading.Thread.Sleep(4000);
                                Environment.Exit(0);
                            }
                            break;
                    }
                }
                while (Toreturn != true);
                return newbie;
            
        }
        public static bool NewGame(Data data)
        {
            Prologue(data);
            Console.Clear();
            return true;
        }
        public static void DefaultMenu(Character user, Sorcerer user_Sorcerer,Warrior user_Warrior, Data data, Item mythings, Enemy mermaid, Enemy dragon, Enemy Human)
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
                            user_Sorcerer.ViewEquipment(mythings,user, user_Sorcerer, user_Warrior, data,mermaid, dragon, Human);
                        }
                        else
                        {
                            user_Warrior.ViewEquipment(mythings, user, user_Sorcerer, user_Warrior, data, mermaid, dragon,Human);
                        }
                        break;
                    case '2':
                        if (data.gamedata[1] != "Class Warrior")
                        {
                            user_Sorcerer.Walk(user, data, user_Sorcerer,user_Warrior, mythings, mermaid, dragon,Human);
                        }
                        else
                        {
                            user_Warrior.Walk(user, data, user_Sorcerer, user_Warrior, mythings, mermaid, dragon,Human);
                        }
                        break;
                    case '3':
                        OpenMainMenu(data);
                        ToReturn = false;
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
            char classification;
            do
            {
                Console.Clear();
            Console.WriteLine("-Hello traveler, what's your name?");
            string name = Console.ReadLine();
            data.gamedata[0]=name;
            
            Console.WriteLine("Nice to meet you, {0}! I'm Neth and let me introduce you our" +
                " kingdom of Omaghan. During your journey you can serve our queen as a soldier or as a warrior. By the way, could you tell me what's your actual speciality?", name);
            
                Console.WriteLine("1. I'm a warrior.");
                Console.WriteLine("2. I'm a sorcerer.");
                Console.Write("Select: ");
                classification = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (classification == '1')
                {
                    data.gamedata[1]="Class Warrior";
                }
                else if (classification == '2')
                {
                    data.gamedata[1] = "Class Sorcerer";
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                }
            } while (classification != '1' && classification != '2');

            
                data.AutoSaveGame(data);
        }
    }
}
    