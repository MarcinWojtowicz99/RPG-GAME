using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Collections.Generic;

using System.Collections;

using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    /// <summary>
    /// Main story of the game
    /// </summary>
    class Program
    { //basic game values
        public static int basichp = 2500; //easy changable nb of basic HP of character
        public static int maxequipment = 9; //max nb of equipment character can carry
        public static int startmoney = 1000;
        public static bool autosave = true;

        //  public static int index_border = 10; //border of indexes between Potion and Weapon
        public static int basic_damage = 50; //character "punch" damage
        public static int hpboost = 500; //extra hp for warrior
        public static int nbofdata = 21;//nb of data stored-changable in case future game plot changes
        public static int nbofitems = 17;//nb of items
        public static int nbofshopkeeperequipment = 3;//nb of items which shopkeeper can show you during one visit
        public static Dictionary<char, string> clas = new Dictionary<char, string>();
        public static Dictionary<char, string> Class { get { return clas; } set { Class = value; } }
        public static Dictionary<string, string> decisions = new Dictionary<string, string>();
        public static Dictionary<string, string> Decisions { get { return decisions; } set { Decisions = value; } }
        public static int textflow = 100;
        public static int TextFlowSpeed { get { return textflow; } set { TextFlowSpeed = value; } }
        public int NbOfShopkeeperEQ{get{return nbofshopkeeperequipment;} set{nbofshopkeeperequipment=value;} }
        public bool Autosave { get { return autosave; }set { autosave = value; } }
        /// <summary>
        /// Starting method. Stores basic settings and all the methods for simplifying the use of program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "Dragon's shell v1.0";
            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
            Class.Add('w', "Class Warrior");
            Class.Add('s', "Class Sorcerer");
           bool DataAddedToDictionary= AddData();
            //listofitems
            Item mythings = new Item(nbofitems);
          bool itemsloaded= LoadItems(mythings);
            //load user data
            Data data = new Data(nbofdata);
            //load nemesis and their attacks
            Enemy Dragon=null;
            Enemy Mermaid = null;
            Enemy Human = null;
          bool enemyloaded=LoadEnemy(out Dragon, out Mermaid,out Human);
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
            while (true)
            {
                bool newbe = OpenMainMenu(data);
                if (data.gamedata[1] == Class['w'])
                {

                    Warrior user_Warrior = new Warrior(data.gamedata[0], basichp, maxequipment, hpboost, basic_damage, startmoney);
                    Sorcerer user_Sorcerer = null;
                    Character user = user_Warrior;
                    Updater(data, user, newbe, Mermaid, Human, Dragon);
                    data.ReadData(data, user, user_Warrior, mythings, user_Sorcerer, Dragon, Human, Mermaid);

                    DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, Mermaid, Dragon, Human);
                }
                else
                {
                    Sorcerer user_Sorcerer = new Sorcerer(data.gamedata[0], basichp, maxequipment, basic_damage, startmoney);
                    Warrior user_Warrior = null;
                    Character user = user_Sorcerer;
                    Updater(data, user, newbe, Mermaid, Human, Dragon);
                    data.ReadData(data, user, user_Warrior, mythings, user_Sorcerer, Dragon, Human, Mermaid);

                    DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, Mermaid, Dragon, Human);
                }

            }
        }
        /// <summary>
        /// Loads all the enemies
        /// </summary>
        /// <param name="Dragon"></param>
        /// <param name="Mermaid"></param>
        /// <param name="Human"></param>
        /// <returns>Returns if operation was completed succesfully</returns>
        public static bool LoadEnemy(out Enemy Dragon, out Enemy Mermaid, out Enemy Human)
        {
            Dragon = new Enemy("Dragon", 5000, 10000, 7);
            Dragon.Stack(new Attack("Dragon fire", 700, "HURRY UP! HE'S GOING TO DESTROY THE VILLAGE"));
            Dragon.Stack(new Attack("Eternal flame", 800, "YOU ARE ON FIRE!"));
            Dragon.Stack(new Attack("Dragon force", 600, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Deadly claw", 510, "OH NO! YOU ARE BLEEDING!"));
            Dragon.Stack(new Attack("Tail punch", 300, "DRAGON: \"GRRRRRRRRRR!\""));
            Dragon.Stack(new Attack("Bite", 400, "DRAGON: \"RRRRROOAAAARRRRR!\""));
            Dragon.Stack(new Attack("Air punch", 500, "YOU CAN'T SEE ANYTHING"));
            Mermaid = new Enemy("Mermaid", 650, 350, 4);
            Mermaid.Stack(new Attack("Tail punch", 300, "OUCH!"));
            Mermaid.Stack(new Attack("Waterfall", 600, "*BUBBLE* *BUBBLE*"));
            Mermaid.Stack(new Attack("Water clutch", 250, "LEAVE ME ALONE!"));
            Mermaid.Stack(new Attack("Voice crack", 750, "SHOCKWAVE"));
            Human = new Enemy("Human", basichp, 100, 1);
            Human.Stack(new Attack("Drunk punch", 100, "I'll show you my real power!"));
            return true;
        }
        /// <summary>
        /// Load items
        /// </summary>
        /// <param name="mythings"></param>
        /// <returns>Returns if operation was completed succesfully</returns>
        public static bool LoadItems(Item mythings)
        {
            mythings.Stack(new Unit("Rubber Duck", 12, 25, 0, 1, "Rubbish"));//1
            mythings.Stack(new Unit("Jewel", 3500, 4000, 0, 1, "Rubbish"));//2
            mythings.Stack(new Unit("Small potion", 45, 500, 400, 1, "Potion"));//3
            mythings.Stack(new Unit("Medium potion", 450, 1100, 670, 1, "Potion"));//4
            mythings.Stack(new Unit("Large potion", 1120, 1500, 1200, 1, "Potion"));//5
            mythings.Stack(new Unit("Mermaid shell", 450, 1500, 0, 1, "Rubbish"));//6
            mythings.Stack(new Unit("Gold tooth", 600, 1300, 0, 1, "Rubbish"));//7
            mythings.Stack(new Unit("Mermaid shell", 450, 1500, 0, 1, "Rubbish"));//8
            mythings.Stack(new Unit("Apple", 450, 1500, 250, 1, "Potion"));//9
            mythings.Stack(new Unit("Drunk-man's medallion", 450, 1500, 0, 1, "Rubbish"));//10
            mythings.Stack(new Unit("Holy water", 150, 250, 0, 1, "Special Item"));//Gives 100% chance to kill the demon 11
            mythings.Stack(new Unit("Scarf", 150, 250, 0, 1, "Special Item"));//You can give this item to the little girl 12
            //Weapons
            mythings.Stack(new Unit("Broken sword", 45, 125, 20, 1, "Sword"));//13
            mythings.Stack(new Unit("Sword of truth", 945, 1550, 120, 1, "Sword"));//14
            mythings.Stack(new Unit("Excalibur", 9450, 15500, 2200, 1, "Sword"));//15
            mythings.Stack(new Unit("Queen's ring", int.MaxValue - 2, int.MaxValue - 1, 0, int.MaxValue - 2, "Special Item"));//16
            mythings.Stack(new Unit("Queen's Medallion", int.MaxValue - 2, int.MaxValue - 1, 0, int.MaxValue - 2, "Special Item"));//17
            return true;

        }
        /// <summary>
        /// Method for printing using a typing effect and text color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="col"></param>
        public static void Print(string text, char col)
        {

            string color = TextColors(col);
            if (color == "white")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (color == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (color == "yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (color == "blue")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (color == "magenta")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (color == "green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            WriteEffect(text);
        }
        /// <summary>
        /// Typing effect
        /// </summary>
        /// <param name="text"></param>
        public static void WriteEffect(string text)
        {
            char[] ArrayOfChars = text.ToCharArray();
            for (int i = 0; i < ArrayOfChars.Length; i++)
            {
                Console.Write(ArrayOfChars[i]);
                System.Threading.Thread.Sleep(TextFlowSpeed);
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }//Writing effect
        /// <summary>
        /// Dictionary which store color simplifications
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string TextColors(char color)
        {
            Dictionary<char, string> col = new Dictionary<char, string>();
            col.Add('w', "white");
            col.Add('r', "red");
            col.Add('b', "blue");
            col.Add('y', "yellow");
            col.Add('m', "magenta");
            col.Add('g', "green");
            return col[color];
        }
        /// <summary>
        /// Dictionary for simplifying choices
        /// </summary>
        /// <returns></returns>
        public static bool AddData()
        {
            Decisions.Add("null", "NO DATA");
            Decisions.Add("1b","Watch out!");
            Decisions.Add("2a", "Pour the finest wine");
            Decisions.Add("2b", "Pour the medium wine");
            Decisions.Add("2d", "Pour the worst wine");
            Decisions.Add("3Aa", "Okay, I will pay you");
            Decisions.Add("3Ab", "Just tell me what to do");
            Decisions.Add("3Ad", "I want you to teach me for free");
            Decisions.Add("3Ba", "Yes, by poisoning the wine");
            Decisions.Add("3Bb", "No, I will left him alive and see what happens");
            Decisions.Add("3Bd", "Yes, I will use my spells against him");
            Decisions.Add("3Ca", "Yes, by poisoning the wine");
            Decisions.Add("3Cb", "No, I will left him alive and see what happens");
            Decisions.Add("3Cd", "Yes, I will do it in old-fashioned way");
            Decisions.Add("4a", "I apologise for my behaviour, Your Highness");
            Decisions.Add("4b", "I had a reason");
            Decisions.Add("4d", "It is not the right time for discussion, your life is in danger!");
            Decisions.Add("5a", "That is your biggest disadvantage!");
            Decisions.Add("5b", "What about king of Fire you asked me to spy?");
            Decisions.Add("5d", "The dragon will destroy the town!");
            Decisions.Add("6a", "+You saved the Queen");
            Decisions.Add("6b", "+You didn't save the Queen");

            Decisions.Add("Add1a", "+You helped the little girl");
            Decisions.Add("Add1b", "+You didn't help the little girl");
            Decisions.Add("Add1c", "+You tried to help the girl");
            Decisions.Add("Add2a", "+You helped Mergoth");
            Decisions.Add("Add2b", "+You didn't help Mergoth");
            Decisions.Add("Add2c", "+You tried to help Mergoth");
           
            return true;
        }
        /// <summary>
        /// Manages the whole plot
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="Mermaids"></param>
        /// <param name="user_Sorcerer"></param>
        /// <param name="user_Warrior"></param>
        /// <param name="mythings"></param>
        /// <param name="mermaid"></param>
        /// <param name="dragon"></param>
        /// <param name="Human"></param>
        public static void PlotManager(Data data, Character user, Enemy Mermaids, Sorcerer user_Sorcerer, Warrior user_Warrior, Item mythings, Enemy mermaid, Enemy dragon, Enemy Human)
        {
            Console.Clear();
            if (data.Gamedata[13] == Decisions["null"])//Chapter 1
            {
                Scenario.Chapter1(data, user);
                if (data.gamedata[13] == Decisions["1b"]) Scenario.Chapter1a(data, user);
                else Scenario.Chapter1b(data, user);

                Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
            }
            else if (data.Gamedata[14] == Decisions["null"])
            {
                if (Mermaids.EnemyKilled < 3)
                {
                    Console.WriteLine("You did not fulfill the task, come back later.");
                    System.Threading.Thread.Sleep(2000);
                    Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
                }
                else
                {
                    Scenario.Chapter2(data, user);
                    if (data.Gamedata[14] !=Decisions["2b"]) Scenario.Chapter2a(data, user);
                    
                    else Scenario.Chapter2b(data, user);

                    if (data.Gamedata[15] == Decisions["null"] && data.Gamedata[1] == Class['s'] && data.Gamedata[14] != Decisions["2b"])//Chapter 3 sorcerer #1
                    {
                        Scenario.Chapter3a(data, user);
                        Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
                    }
                    else if (data.Gamedata[16] == Decisions["null"] && data.Gamedata[1] == Class['s'] && data.Gamedata[15] == Decisions["null"])//Chapter 3 sorcerer #2
                    {
                        Scenario.Chapter3b(data, user);
                        Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
                    }
                    else if (data.Gamedata[17] == Decisions["null"] && data.Gamedata[1] == Class['w'])//Chapter 3 warrior
                    {
                        Scenario.Chapter3c(data, user);
                        Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
                    }
                }
            }
            
            else if (data.Gamedata[18] == Decisions["null"])//Chapter 5
            {

                Scenario.Chapter4(data, user);
                bool add = user.AddToEquipment(15, mythings, user, data);
                if (add == false)
                    user.Equipment[0, 0] = 15;
                Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
            }
            else
            {
                Console.WriteLine("Game Complete!");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
            }
        }
        /// <summary>
        /// Game ending screen
        /// </summary>
        /// <param name="user"></param>
        /// <param name="data"></param>
        /// <param name="mythings"></param>
        public static void ending_screen(Character user, Data data, Item mythings)
        {
            Enemy.DragonDeath();
            Scenario.QueenSafeEnding(data, user, mythings);
            Console.ForegroundColor = ConsoleColor.White;
            char[][] JaggedArrayOfChars=new char[10][];
            JaggedArrayOfChars[0] = "Congratulations, traveler, You defeated the dragon!".ToCharArray();
            JaggedArrayOfChars[1] = ("Money gain: " + user.Money_player + " Dragon Coins").ToCharArray();
            if(data != null)
            {
                JaggedArrayOfChars[2] = (data.Gamedata[7]).ToCharArray();
                JaggedArrayOfChars[3] = (data.Gamedata[8]).ToCharArray();
                JaggedArrayOfChars[4] = (data.Gamedata[9]).ToCharArray();
                JaggedArrayOfChars[5] = (data.Gamedata[10]).ToCharArray();
            }
            JaggedArrayOfChars[6] = ("Your final score: " + user.Score).ToCharArray();
            JaggedArrayOfChars[7] = "Thank you for playing!".ToCharArray();
            JaggedArrayOfChars[8]= "Game made by Marcin Wojtowicz".ToCharArray();
            System.Threading.Thread.Sleep(3000);
            JaggedArrayOfChars[9] = "Press any key to continue".ToCharArray();
            user.Score += user.Money_player *12;
            for(int i=0; i<JaggedArrayOfChars.Length;i++)
            {
                if (i==JaggedArrayOfChars.Length-1)
                {
                    System.Threading.Thread.Sleep(3000);
                }
                if(JaggedArrayOfChars[i]!=null)
                {
                    for (int j = 0; j < JaggedArrayOfChars[i].Length; j++)
                    {

                        Console.Write(JaggedArrayOfChars[i][j]);
                        System.Threading.Thread.Sleep(100);
                    }
                    Console.WriteLine();
                }
                    
            }
            MyDecisions(data);
            Console.ReadKey();
            
            OpenMainMenu(data);
        }
        /// <summary>
        /// Method for showing chatacter's decisions during the end of a game
        /// </summary>
        /// <param name="data"></param>
        private static void MyDecisions(Data data)
        {
            Console.Clear();
            if (data == null) data = new Data(nbofdata);
            Console.WriteLine("Your choices: ");
            if (data.Gamedata[13] == Decisions["1b"])
                data.Gamedata[13] = "+You rescued Guardian";

            else data.Gamedata[13] = "+You didn't rescue Guardian";
            if (data.Gamedata[14] ==Decisions["2b"])
                data.Gamedata[14] = "+You poured the right wine";
            else data.Gamedata[14] = "+You poured the wrong wine";
            if (data.Gamedata[15] == "Okay, I will pay you")
                data.Gamedata[15] = "+You paid the wizard for training";
            else data.Gamedata[15] = "+You didn't pay wizard for training";
            if (data.Gamedata[16] ==Decisions["3Bd"] || data.Gamedata[17] ==Decisions["3Cd"])
                data.Gamedata[16] = "+You killed the King";
            else data.Gamedata[16] = "+You didn't kill the King";

            if (data.Gamedata[11] != Decisions["null"])
            {
                Console.WriteLine(data.Gamedata[11]);
            }
            if (data.Gamedata[12] != Decisions["null"])
            {
                Console.WriteLine(data.Gamedata[12]);
            }
            for (int i = 13; i <= 20; i++)
            {
                if (data.Gamedata[i] != Decisions["null"])
                    Console.WriteLine(data.Gamedata[i]);
            }
            Console.ReadKey();
            if (data.Gamedata[12] ==Decisions["Add2a"] && data.Gamedata[11] ==Decisions["Add1a"] && data.Gamedata[20] ==Decisions["6a"])
            {
                Console.WriteLine("Achievement get: Helper");
                Console.ReadKey();
            }
            if (data.Gamedata[12] ==Decisions["Add2a"] && data.Gamedata[11] ==Decisions["Add1a"] && data.Gamedata[20] ==Decisions["6a"])
            {
                Console.WriteLine("Achievement get: King slayer");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Updates informations to data object
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="newbe"></param>
        /// <param name="Mermaid"></param>
        /// <param name="Human"></param>
        /// <param name="Dragon"></param>
        /// <returns></returns>
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
                for(int i=11; i<data.Gamedata.Length;i++)
                {
                    data.Gamedata[i] = Decisions["null"];
                }
                data.AutoSaveGame(data);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Manages random events
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        public static void RandomThings(Character user, Item mythings, Data data)
        {
            Random rnd = new Random();
            int random = rnd.Next(100);
            if(random==0)
            {
                user.AddMoney(Scenario.EventMoneyFound(),user,data);
            }
            else if(random>0&&random<2)
            {
                user.RemoveMoney(Scenario.ThiefEvent(), user, data);
            }
            else if (random == 6 )
            {
                Scenario.GirlWithMatches(user,mythings,data);
            }
            else if(random==7)
            {
                Scenario.Mergoth(user, mythings, data);
            }
            else if(random==8&&data.Gamedata[13]== Decisions["1b"])
            {
                Scenario.Ambe();
                user.AddMoney(10000,user,data);
            }

        }
        /// <summary>
        /// Method for making choices by a player and saving to data object
        /// </summary>
        /// <param name="user"></param>
        /// <param name="toOverWrite"></param>
        /// <param name="data"></param>
        /// <param name="description"></param>
        /// <param name="optionA"></param>
        /// <param name="optionB"></param>
        /// <param name="optionC"></param>
        /// <param name="optionD"></param>
        public static void MakeYourChoice(Character user,int toOverWrite, Data data, string description, string optionA, string optionB, string optionC, string optionD)
        {
            bool toReturn = false;
            do
            {
                Console.Clear();
                Print(description, 'g');
                Print("1. " + optionA, 'g');
                Print("2. " + optionB, 'g');
                Print("3. " + optionC, 'g');
                Print("4. " + optionD, 'g');
                Print("Select: ", 'w');
                char choice = ' ';
                choice = Console.ReadKey().KeyChar;


                if (choice != '1' && choice != '2' && choice != '3' && choice != '4')
                {
                    Print("Unknown option", 'w');
                    System.Threading.Thread.Sleep(2000); Console.Clear();
                }
                else
                {

                    if (choice == '1')
                    {
                        data.gamedata[toOverWrite] = optionA;
                        Console.WriteLine();
                        Print(optionA, 'g');
                        break;
                    }
                    else if (choice == '2')
                    {
                        data.gamedata[toOverWrite] = optionB;
                        Console.WriteLine();
                        Print(optionB, 'g');
                        break;
                    }
                    else if (choice == '3')
                    {
                        data.gamedata[toOverWrite] = optionA;
                        Console.WriteLine();
                        Print(optionA, 'g');
                        break;
                    }
                    else
                    {
                        data.gamedata[toOverWrite] = optionD;
                        Console.WriteLine();
                        Print(optionD, 'g');
                        break;
                    }

                }

                Console.WriteLine();

            }
            while (toReturn == false);
            Scenario.EndOfDecision(data, user);



        }
        /// <summary>
        /// In-game options
        /// </summary>
        private static void Options()
        {
            bool correct;
            do
            {
                Print("Autosaves turned on? Type \"y\" or \"n\"...",'w');
                Print("Your answer: ",'w');
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
                    Print("Unknown command, try again...",'w');
                    correct = false;
                }
            } while (correct != true);
        }
        /// <summary>
        /// Easter egg. After typing "/" user finish game
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public static bool Hackermode(char option)
        {
            if (option == '/')
            {
                Console.Clear();
                Console.Title = "HACKERMODE";
                Print(@"\Hello world!", 'g');
                System.Threading.Thread.Sleep(4000);
                LoadingEffect("Hacking pentagon servers, please wait", "Connected! :D");
                Character user2 = new Character("IT", 9999, 9, 9999, 9999);
                Item mythings2 = new Item(1);
                Data data = null;
                ending_screen(user2, data, mythings2);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Effect for loading
        /// </summary>
        /// <param name="nameofprocess"></param>
        /// <param name="endofprocess"></param>
        public static void LoadingEffect(string nameofprocess,string endofprocess)
        {
            Console.Clear();
            for (int i = 0; i < 2; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\r"+nameofprocess);
                WaitDots();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
            }
            System.Threading.Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\r"+endofprocess);
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }
        /// <summary>
        /// Opens Main Menu of the game
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool OpenMainMenu(Data data)
        {
                bool newbie = false;
                int counter = 0;//easter egg
                bool Toreturn = true;
                char option=' ';
                do
                {
                    Toreturn = true;
                Scenario.MainMenu();
                 option = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.Clear();
                Hackermode(option);//Easter egg
                    switch (option)
                    {
                        case '1':
                            newbie = NewGame(data);
                            Console.Clear();
                        return true;
                        case '2':
                            data.LoadGame(data);
                        LoadingEffect("Loading","Loading complete!");
                            break;
                        case '3':
                        if (data.gamedata[0] != null)
                        {
                            data.SaveGame(data);
                            Toreturn = false;
                        }
                        else
                        {
                            Toreturn = false;
                            Console.WriteLine("You have nothing to save");
                            System.Threading.Thread.Sleep(2000);

                        }
                            break;

                        case '4':
                            Options();
                            Toreturn = false;
                            break;
                        case '0':
                            Console.WriteLine("Bye!");
                            System.Threading.Thread.Sleep(1000);
                            Environment.Exit(0);//terminates console
                            break;
                        default:
                            Console.WriteLine("Unknown option!");
                            counter++;
                        ClickFeature(counter);
                            Toreturn = false;
                            break;
                    }
                }
                while (Toreturn != true);
                return newbie;
        }
        /// <summary>
        /// Easter egg. After clicking wrong keys in main menu, user receives a message to stop doing this
        /// </summary>
        /// <param name="counter">Counts number of clicks</param>
        public static void ClickFeature(int counter)
        {
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
        }
        /// <summary>
        /// Loads new game
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool NewGame(Data data)
        {
            Scenario.Prologue(data);
            Console.Clear();
            return true;
        }
        /// <summary>
        /// Pause menu
        /// </summary>
        /// <param name="user"></param>
        /// <param name="user_Sorcerer"></param>
        /// <param name="user_Warrior"></param>
        /// <param name="data"></param>
        /// <param name="mythings"></param>
        /// <param name="mermaid"></param>
        /// <param name="dragon"></param>
        /// <param name="Human"></param>
        public static void DefaultMenu(Character user, Sorcerer user_Sorcerer,Warrior user_Warrior, Data data, Item mythings, Enemy mermaid, Enemy dragon, Enemy Human)
        {
            bool ToReturn = true ;
            Program.RandomThings(user, mythings, data);
            
            do
            {
                Scenario.DefaultMenu(user.Money_player,user.Actual_hp,user.Score,data.gamedata[1]);
                var a = Console.ReadKey();
                if (a.Key == ConsoleKey.Escape)
                {
                    //OpenMainMenu(data);
                    ToReturn = false;
                    break;
                }
                char key = a.KeyChar;
                Console.WriteLine();
                Console.Clear();
                switch (key)
                {
                    case '1':
                        if (data.gamedata[1] != Class['w'])
                        {
                            user_Sorcerer.ViewEquipment(mythings,user, user_Sorcerer, user_Warrior, data,mermaid, dragon, Human,false);
                        }
                        else
                        {
                            user_Warrior.ViewEquipment(mythings, user, user_Sorcerer, user_Warrior, data, mermaid, dragon,Human,false);
                        }
                        break;
                    case '2':
                        if (data.gamedata[1] != Class['w'])
                        {
                            user_Sorcerer.Walk(user, data, user_Sorcerer,user_Warrior, mythings, mermaid, dragon,Human);
                        }
                        else
                        {
                            user_Warrior.Walk(user, data, user_Sorcerer, user_Warrior, mythings, mermaid, dragon,Human);
                        }
                        break;
                    case '3':
                        if (data.gamedata[1] != Class['w'])
                        {
                            user_Sorcerer.Teleport(mythings, user, user_Sorcerer, user_Warrior, data, mermaid, dragon, Human);
                        }
                        else
                        {
                            user_Warrior.Teleporter(mythings,user,user_Sorcerer,user_Warrior,data,mermaid,dragon,Human);
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
        /// <summary>
        /// Method for loading something
        /// </summary>
      public static void WaitDots()
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine();
        }
        
    }
    
}
    