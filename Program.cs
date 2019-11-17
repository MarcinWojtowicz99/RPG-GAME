using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections;

using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    class Program
    { //basic game values
        
        public static int basichp = 2000; //easy changable nb of basic HP of character
        public static int maxequipment = 9; //max nb of equipment character can carry
        public static int startmoney = 1000;
        public static bool autosave = true;
        public static int index_border = 10;//border of indexes between Potion and Weapon
        public static int basic_damage = 50; //character "punch" damage
        public static int hpboost = 500; //extra hp for warrior
        public static int nbofdata = 20;//nb of data stored-changable in case future game plot changes
        public static int nbofitems = 12;//nb of items
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
            mythings.Stack(new Unit("Jewel", 3500, 4000, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Small potion", 45, 500, 400, 1, "Potion"));
            mythings.Stack(new Unit("Medium potion", 450, 1100, 670, 1, "Potion"));
            mythings.Stack(new Unit("Large potion", 1120, 1500, 1200, 1, "Potion"));
            mythings.Stack(new Unit("Mermaid shell", 450, 1500, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Gold tooth", 600, 1300, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Mermaid shell", 450, 1500, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Apple", 450, 1500, 250, 1, "Potion"));
            mythings.Stack(new Unit("Drunk-man's medallion", 450, 1500, 0, 1, "Rubbish"));
            mythings.Stack(new Unit("Holy water", 150, 250, 0, 1, "Special Item"));//Gives 100% chance to kill the demon
            mythings.Stack(new Unit("Scarf", 150, 250, 0, 1, "Special Item"));//You can give this item to the little girl
            //Weapons
            mythings.Stack(new Unit("Broken sword", 45, 125, 20, 1, "Sword"));
            mythings.Stack(new Unit("Sword of truth", 945, 1550, 120, 1, "Sword"));
            mythings.Stack(new Unit("Excalibur", 9450, 15500, 2200, 1, "Sword"));
            mythings.Stack(new Unit("Queen's ring", int.MaxValue, int.MaxValue, 0, int.MaxValue, "Special Item"));
            mythings.Stack(new Unit("Queen's Medallion", int.MaxValue, int.MaxValue, 0, int.MaxValue, "Special Item"));
            
            //load user data
            Data data = new Data(nbofdata);
            //load nemesis and their attacks
            Enemy Dragon = new Enemy("Dragon",5000, 10000, 7);
            Dragon.Stack(new Attack("Dragon fire", 700, "HURRY UP! HE'S GOING TO DESTROY THE VILLAGE"));
            Dragon.Stack(new Attack("Eternal flame", 800, "YOU ARE ON FIRE!"));
            Dragon.Stack(new Attack("Dragon force", 600, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Deadly claw", 510, "OH NO! YOU ARE BLEEDING!"));
            Dragon.Stack(new Attack("Tail punch", 300, "DRAGON: \"GRRRRRRRRRR!\""));
            Dragon.Stack(new Attack("Bite", 400, "DRAGON: \"RRRRROOAAAARRRRR!\""));
            Dragon.Stack(new Attack("Air punch", 500, "YOU CAN'T SEE ANYTHING"));
            Enemy Mermaid = new Enemy("Mermaid",650, 350, 4);
            Mermaid.Stack(new Attack("Tail punch", 300, "OUCH!"));
            Mermaid.Stack(new Attack("Waterfall", 600, "*BUBBLE* *BUBBLE*"));
            Mermaid.Stack(new Attack("Water clutch", 250, "LEAVE ME ALONE!"));
            Mermaid.Stack(new Attack("Voice crack", 750, "SHOCKWAVE"));
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
                data.ReadData(data,user, user_Warrior, mythings, user_Sorcerer,Dragon,Human,Mermaid);
                
                DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings,Mermaid,Dragon,Human);
            }
            else
            {
                Sorcerer user_Sorcerer = new Sorcerer(data.gamedata[0], basichp, maxequipment, basic_damage,startmoney);
                Warrior user_Warrior = new Warrior("NAN", 0, 0, 0, 0,0);
                Character user = user_Sorcerer;
                Updater(data, user, newbe, Mermaid, Human, Dragon);
                data.ReadData(data, user, user_Warrior, mythings, user_Sorcerer,Dragon, Human, Mermaid);

                DefaultMenu(user,user_Sorcerer,user_Warrior,data, mythings, Mermaid, Dragon,Human);
            }
          
            
        }
        public static void Print(string text, string color)
        {
            if(color=="white")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if(color=="red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if(color=="yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if(color=="blue")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            char[] ArrayOfChars = text.ToCharArray();
            for (int i = 0; i < ArrayOfChars.Length; i++)
            {
                Console.Write(ArrayOfChars[i]);
                System.Threading.Thread.Sleep(100);

            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }//Writing effect
        public static void doPlot(Data data, Character user, Enemy Mermaids)
        {
            string decision="";
            if(data.Gamedata[13]=="NO DATA")//Chapter 1
            {
                Print("Chapter 1","white");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Print("Guardian: Who are you?", "yellow");
                Print(" - I'm {0}. I was asked by your Queen to..."+ Convert.ToString(data.gamedata[0]), "blue");
                Print("Guardian: I don't care. Do you have a trespass?","yellow");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("MAKE YOUR CHOICE!", "white");
                Program.MakeYourChoice(user,data,true,"*You see a huge rock sliding through the roof*","*Back off quietly*", "Watch out!","...","Maybe ask your queen about that");
                if(data.gamedata[13]== "Watch out!")
                {
                    Print("Guardian: You saved my life! Thank you. But still, rules are rules. You need to make an appointment", "yellow");
                    Print("-No problem, I'd like to talk with her as soon as possible","blue");
                }
                else
                {
                    Print("You killed the guardian and ended up in jail. However, queen visited you in your cell","white");
                }

            }
            else if(data.Gamedata[14] == "NO DATA")//Chapter 2
            {
                Print("Chapter 2","white");
                System.Threading.Thread.Sleep(2000);
                if (data.gamedata[13] == "Watch out!")
                {

                }
                    Console.Clear();




                data.Gamedata[14] = decision;
            }
            else if (data.Gamedata[15] == "NO DATA" && Mermaids.EnemyKilled >= 3)//Chapter 3
            {
                Console.WriteLine("Chapter 3");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();



                data.Gamedata[15] = decision;
            }
            else if (data.Gamedata[16] == "NO DATA")//Chapter 4
            {
                Console.WriteLine("Chapter 4");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();


                data.Gamedata[16] = decision;
            }
            else if (data.Gamedata[17] == "NO DATA")//Chapter 5
            {
                Console.WriteLine("Chapter 5");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();


                data.Gamedata[17] = decision;
            }
            else if (data.Gamedata[18] == "NO DATA")//Chapter 6
            {
                Console.WriteLine("Chapter 6");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();


                data.Gamedata[18] = decision;
            }
            else if (data.Gamedata[19] == "NO DATA")//Chapter 7
            {
                Console.WriteLine("Chapter 7");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();



                data.Gamedata[19] = decision;
            }
            else if (data.Gamedata[20] == "NO DATA")//Chapter 8
            {
                Console.WriteLine("Chapter 8");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();

                data.Gamedata[20] = decision;
            }
            
          
        }
        public static void ending_screen(Character user, Data data, Item mythings)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dragon: AAAAAAAAAAAAAAAAAAARRRRRGH!");
            System.Threading.Thread.Sleep(1400);
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < 100; i++)
                Console.WriteLine();
            System.Threading.Thread.Sleep(400);
            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 0; i < 100; i++)
                Console.WriteLine();
            System.Threading.Thread.Sleep(400);
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < 100; i++)
                Console.WriteLine();
            System.Threading.Thread.Sleep(400);
            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 0; i < 100; i++)
                Console.WriteLine();
            System.Threading.Thread.Sleep(400);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            System.Threading.Thread.Sleep(4000);
            Console.ForegroundColor = ConsoleColor.Blue;
            if(data.Gamedata[20]=="+You saved the Queen")
            {
                Console.WriteLine("-QUEEEN! Are you okay?");
                System.Threading.Thread.Sleep(4000);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-Yes, I thought he'll kill us all. Thank you for saving my kingdom. Take this medallion as my token of gratitude");
                System.Threading.Thread.Sleep(4000);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("*YOU RECEIVED MEDALLION!*");
                user.Equipment[0, 0] = 12;
                bool add = user.AddToEquipment(12, mythings, user, data);
                if (add != true)
                {
                    user.Equipment[0, 0] = 12;
                }
                Console.ReadKey();
                Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.White;
            char[][] JaggedArrayOfChars=new char[10][];
            JaggedArrayOfChars[0] = "Congratulations, traveler, You defeated the dragon!".ToCharArray();
            JaggedArrayOfChars[1] = ("Money gain: " + user.Money_player).ToCharArray();
            JaggedArrayOfChars[2] = (data.Gamedata[7]).ToCharArray();
            JaggedArrayOfChars[3] = (data.Gamedata[8]).ToCharArray();
            JaggedArrayOfChars[4] = (data.Gamedata[9]).ToCharArray();
            JaggedArrayOfChars[5] = (data.Gamedata[10]).ToCharArray();
            JaggedArrayOfChars[6] = ("Your final score: " + user.Score).ToCharArray();
            JaggedArrayOfChars[7] = "Thank you for playing!".ToCharArray();
            JaggedArrayOfChars[8]= "Game made by Marcin Wojtowicz".ToCharArray();
            System.Threading.Thread.Sleep(6000);
            JaggedArrayOfChars[9] = "Press any key to continue".ToCharArray();
            user.Score += user.Money_player *12;
            for(int i=0; i<JaggedArrayOfChars.Length;i++)
            {
                if (i==JaggedArrayOfChars.Length-1)
                {
                    System.Threading.Thread.Sleep(6000);
                }
                    for (int j=0;j<JaggedArrayOfChars[i].Length;j++)
                {
                    
                    Console.Write(JaggedArrayOfChars[i][j]);
                    System.Threading.Thread.Sleep(200);
                }
                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Your choices: ");//
            if(data.Gamedata[11]!="NO DATA")
            {
                Console.WriteLine(data.Gamedata[11]);
            }
            for(int i=12;i<21;i++)
            {
                Console.WriteLine(data.Gamedata[i]);
            }
            Console.ReadKey();
            if(data.Gamedata[12]== "+You helped Mergoth"&&data.Gamedata[11]== "+You helped the little girl"&& data.Gamedata[20]== "+You saved the Queen")
            {
                Console.WriteLine("Achievement get: Helper");
                Console.ReadKey();
            }
            OpenMainMenu(data);
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
                for(int i=11; i<data.Gamedata.Length;i++)
                {
                    data.Gamedata[i] = "NO DATA";
                }
                data.AutoSaveGame(data);
                return true;
            }
            return false;
        }
        public static void RandomThings(Character user, Item mythings, Data data)
        {
            
            
            
            Random rnd = new Random();
            int random = rnd.Next(100);
            if(random==0)
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("You found money!");
                Random rnd2 = new Random();
             int random2 = rnd.Next(500,4500);
                Console.WriteLine("+"+random2);
                user.Money_player += random2;
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
            }
            else if(random>0&&random<6)
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Unfortunately someone robbed you during sleep...");
                Random rnd2 = new Random();
                int random2 = rnd.Next(500, 4500);
                if(user.Money_player-random2<0)
                {
                    user.Money_player = 0;
                }
                else
                {
                    user.Money_player -= random2;
                }
                
            }
            else if (random == 6 )
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("*It's so cold... Is that snow? I need to find some warm place to rest. You noticed a child behind the old shop*");
                Console.WriteLine("-Who are you?");
                Console.WriteLine("-Hello, wanna buy some matches? My father won't let me warm until I sell everything. [Your account ballance: {0}]",user.Money_player);
                Console.WriteLine("1. I'll buy one then [cost:10]");
                Console.WriteLine("2. I'll take all of them [cost:250]");
                bool isscarf = user.FindInEquipment(mythings, user, "Scarf");
                if(isscarf==true)
                {
                    Console.WriteLine("3. Sorry, I need to go");
                }
                Console.WriteLine("4. Take my scarf");

                char choice = Console.ReadKey().KeyChar;
                Console.Clear();
                    switch (choice)
                    {
                        case '1':
                         Console.WriteLine("-Okay, I'll buy one");
                        if(10<user.Money_player)
                        {
                            Console.WriteLine("-You are a good man, thank you.");
                            user.Money_player-=10;
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You helped the little girl";
                        }
                        else
                        {
                            Console.WriteLine("-I see you are broke too. Thank you for trying anyway");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You tried to help the girl";
                        }
                            
                            break;
                        case '2':
                        Console.WriteLine("-I'll take everything then");
                        if (250<user.Money_player)
                        {
                            user.Money_player -= 250;
                            Console.WriteLine("-You are a good man, thank you.");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You helped the little girl";
                        }
                        else
                        {
                            Console.WriteLine("-I see you are broke too. Thank you for trying anyway");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You tried to help the girl";
                        }
                        
                        
                        break;
                        case '3':
                        
                        Console.WriteLine("-Sorry, I need to go... I have a very important mission");
                        Console.WriteLine("-Okay, Sorry for disturbing you.");
                        data.Gamedata[11] = "+You didn't help the little girl";
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case '4':
                        if (isscarf == true)
                        {
                            Console.WriteLine("-Please, take my scarf and come with me. We will find you home");
                            Console.WriteLine("-You are a good man, thank you");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You helped the little girl";
                        }
                        else
                        {
                            Console.WriteLine("-...");
                            Console.WriteLine("-Okay, Sorry for disturbing you");
                            Console.ReadKey();
                            data.Gamedata[11] = "+You didn't help the little girl";
                        }
                        break;
                        default:
                        Console.WriteLine("-...");
                        Console.WriteLine("-Okay, Sorry for disturbing you");
                        data.Gamedata[11] = "+You didn't help the little girl";
                        Console.ReadKey();
                        break;
                    }
                Console.Clear();
                Console.WriteLine("Game will remember that...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
            }
            else if(random==7)
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("-Cousin! It's me, Mergoth. Listen... I need some money [2000]");
                Console.WriteLine("1. Okay, take them");
                Console.WriteLine("2. Go away, Mergoth");
                Console.WriteLine("3. How do I know you will give them back?");
                char choice = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (choice)
                {
                    case '1':
                        Console.WriteLine("-Okay, take my money");
                        if (2000 < user.Money_player)
                        {
                            Console.WriteLine("-You are a good man, thank you.");
                            user.Money_player -= 2000;
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You helped Mergoth";
                        }
                        else
                        {
                            Console.WriteLine("-I see you are broke too. Thank you for trying anyway");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You tried to help Mergoth";
                        }

                        break;
                    case '2':
                        { 
                        Console.WriteLine("-I'm busy, go away. You'll never give them back!");
                            Console.WriteLine("-Okay");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You didn't help Mergoth";
                        }
                        break;
                    case '3':
                        Console.WriteLine("-How do I know you will give them back?");
                        Console.WriteLine("-Please, I pinky-promise!");
                        Console.WriteLine("1. Okay");
                        Console.WriteLine("2. Go away");
                        char option2 = Console.ReadKey().KeyChar;
                        switch(option2)
                        {
                            case '1':
                                Console.WriteLine("-Okay, take my money");
                                if (2000 < user.Money_player)
                                {
                                    Console.WriteLine("-You are a good man, thank you.");
                                    user.Money_player -= 2000;
                                    Console.ReadKey();
                                    Console.Clear();
                                    data.Gamedata[12] = "+You helped Mergoth";
                                }
                                else
                                {
                                    Console.WriteLine("-I see you are broke too. Thank you for trying anyway");
                                    Console.ReadKey();
                                    Console.Clear();
                                    data.Gamedata[12] = "+You tried to help Mergoth";
                                }
                                break;
                            case '2':
                                Console.WriteLine("-I'm busy, go away. You'll never give them back!");
                                Console.WriteLine("-Okay");
                                Console.ReadKey();
                                Console.Clear();
                                data.Gamedata[12] = "+You didn't help Mergoth";
                                break;
                            default:
                                Console.WriteLine("-...");
                                Console.WriteLine("-Okay, Sorry for disturbing you");
                                data.Gamedata[12] = "+You didn't help Mergoth";
                                Console.ReadKey();
                                break;
                        }
                        if (2000 < user.Money_player)
                        {
                            user.Money_player -= 2000;
                            Console.WriteLine("-You are a good man, thank you.");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You helped Mergoth";
                        }
                        else
                        {
                            Console.WriteLine("-I see you are broke too. Thank you for trying anyway");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You tried to help Mergoth";
                        }


                        break;
                    default:
                        Console.WriteLine("-...");
                        Console.WriteLine("-Okay, Sorry for disturbing you");
                        data.Gamedata[12] = "+You didn't help Mergoth";
                        Console.ReadKey();
                        break;
                }
                        data.Gamedata[12] = "+You helped your cousin";
                Console.Clear();
                Console.WriteLine("Game will remember that...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
            }
            else if(random==8&&data.Gamedata[13]=="b")
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("-Hey, do you remember me? It's me,Ambe! You saved my life, I'll never forget this. Take my money as a small token of our gratitude");
                Console.WriteLine("+1000");
                user.Money_player += 1000;
            }

        }
        public static void MakeYourChoice(Character user, Data data, bool IsTimeLimit, string description, string optionA, string optionB, string optionC, string optionD)
        {
            bool toReturn = false;
            do
            {
            Console.Clear();
            Console.WriteLine(description);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. "+optionA);
            Console.WriteLine("2. "+optionB);
            Console.WriteLine("3. "+optionC);
            Console.WriteLine("4. "+optionD);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Select: ");
                Console.WriteLine();
             //   char choice = ' ';
                ConsoleKeyInfo key;
                char choice=' ';
                if (IsTimeLimit == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    for (int i = 10; i >= 0; i--)
                    {

                        Console.Write("\rTime left: {0}", i);
                        System.Threading.Thread.Sleep(1000);
                        key = Console.ReadKey(true);
                        choice = key.KeyChar;
                        if (choice == '1' || choice == '2' || choice == '3' || choice == '4') break;

                        //choice == '1' || choice == '2' || choice == '3' || choice == '4'


                    }
                }
                int toOverWrite =data.WhereIsNull(data);


                Console.ForegroundColor = ConsoleColor.White;
                    if (choice!='1'|| choice != '2' || choice != '3' || choice != '4' )
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
                    else
                    {
                        switch (choice)
                        {
                            case '1':
                                data.gamedata[toOverWrite] = "Choice" + toOverWrite + ": " + optionA;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(optionA);
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case '2':
                                data.gamedata[toOverWrite] = "Choice" + toOverWrite + ": " + optionB;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(optionB);
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case '3':
                                data.gamedata[toOverWrite] = "Choice" + toOverWrite + ": " + optionC;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write( optionC);
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case '4':
                                data.gamedata[toOverWrite] = "Choice" + toOverWrite + ": " + optionD;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(optionD);
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            default:
                                toReturn = true;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Unknown option!");
                                break;
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
            user.Score += 500;
            Console.WriteLine("You have made your choice: +500 points"); 
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("Game will remember that!");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();

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
                    Item mythings2 = new Item(1);
                    ending_screen(user2, data, mythings2);
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
            Program.RandomThings(user, mythings, data);
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
                Console.WriteLine("What would you like to do now? (PRESS [ESC]to go back to main menu)");
                Console.WriteLine("1. Open Equipment");
            Console.WriteLine("2. Open Map");

            if(data.gamedata[1]!= "Class Warrior")
            {
                Console.WriteLine("3. Teleport");
            }
            else
                {
                    Console.WriteLine("3. Go to the teleporter");
                }
            Console.Write("Select: ");
                var a = Console.ReadKey();
                if (a.Key == ConsoleKey.Escape)
                {
                    OpenMainMenu(data);
                    ToReturn = false;
                    break;
                }
                char key = a.KeyChar;
                Console.WriteLine();
                Console.Clear();
                switch (key)
                {
                    case '1':
                        if (data.gamedata[1] != "Class Warrior")
                        {
                            user_Sorcerer.ViewEquipment(mythings,user, user_Sorcerer, user_Warrior, data,mermaid, dragon, Human,false);
                        }
                        else
                        {
                            user_Warrior.ViewEquipment(mythings, user, user_Sorcerer, user_Warrior, data, mermaid, dragon,Human,false);
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
                        if (data.gamedata[1] != "Class Warrior")
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

      
        public static void Prologue(Data data)
        {
            char classification;
            
                Console.Clear();
            Console.WriteLine("-Hello traveler, what's your name?");
            string name = Console.ReadLine();
            data.gamedata[0]=name;
            do { 
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
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                }
            } while (classification != '1' && classification != '2');

            
                data.AutoSaveGame(data);
        }
    }
    
}
    