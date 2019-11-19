using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Timers;

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
      //  public static int index_border = 10;//border of indexes between Potion and Weapon
        public static int basic_damage = 50; //character "punch" damage
        public static int hpboost = 500; //extra hp for warrior
        public static int nbofdata = 21;//nb of data stored-changable in case future game plot changes
        public static int nbofitems = 17;//nb of items
        public static int nbofshopkeeperequipment = 3;//nb of items which shopkeeper can show you during one visit
        public int NbOfShopkeeperEQ{get{return nbofshopkeeperequipment;} set{nbofshopkeeperequipment=value;} }
        public bool Autosave { get { return autosave; }set { autosave = value; } }
        static void Main(string[] args)
        {
            Console.Title = "Dragon's shell v1.0";
            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
            //listofitems
            Item mythings = new Item(nbofitems);
            mythings.Stack(new Unit("Rubber Duck",12,25,0,1,"Rubbish"));//1
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
            mythings.Stack(new Unit("Queen's ring", int.MaxValue-2, int.MaxValue-1, 0, int.MaxValue-2, "Special Item"));//16
            mythings.Stack(new Unit("Queen's Medallion", int.MaxValue-2, int.MaxValue-1, 0, int.MaxValue-2, "Special Item"));//17
            
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
            while (true)
            {


                bool newbe = OpenMainMenu(data);
                if (data.gamedata[1] == "Class Warrior")
                {

                    Warrior user_Warrior = new Warrior(data.gamedata[0], basichp, maxequipment, hpboost, basic_damage, startmoney);
                    Sorcerer user_Sorcerer = new Sorcerer("FAKE", 0, 0, 0, 0);
                    Character user = user_Warrior;
                    Updater(data, user, newbe, Mermaid, Human, Dragon);
                    data.ReadData(data, user, user_Warrior, mythings, user_Sorcerer, Dragon, Human, Mermaid);

                    DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, Mermaid, Dragon, Human);
                }
                else
                {
                    Sorcerer user_Sorcerer = new Sorcerer(data.gamedata[0], basichp, maxequipment, basic_damage, startmoney);
                    Warrior user_Warrior = new Warrior("NAN", 0, 0, 0, 0, 0);
                    Character user = user_Sorcerer;
                    Updater(data, user, newbe, Mermaid, Human, Dragon);
                    data.ReadData(data, user, user_Warrior, mythings, user_Sorcerer, Dragon, Human, Mermaid);

                    DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, Mermaid, Dragon, Human);
                }

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
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            char[] ArrayOfChars = text.ToCharArray();
            for (int i = 0; i < ArrayOfChars.Length; i++)
            {
                Console.Write(ArrayOfChars[i]);
                System.Threading.Thread.Sleep(90);

            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }//Writing effect
        public static void doPlot(Data data, Character user, Enemy Mermaids, Sorcerer user_Sorcerer, Warrior user_Warrior, Item mythings, Enemy mermaid, Enemy dragon, Enemy Human)
        {
            Console.Clear();
            if(data.Gamedata[13]=="NO DATA")//Chapter 1
            {
                user.Score += 2000;
                user.UpdateScore(user,data);
                Print("Chapter 1: Newbe","white");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Print("Guardian: Who are you?", "yellow");
                Print(user.name + ": I'm " + Convert.ToString(user.name) + ". I was asked by your Queen to...", "blue");
                Print("Guardian: I don't care. Do you have a trespass?","yellow");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("MAKE YOUR CHOICE!", "white");
                Program.MakeYourChoice(user,13,data,"*You see a huge rock sliding through the roof*","*Back off quietly*", "Watch out!","...","Maybe ask your queen about that");
                if(data.gamedata[13]== "Watch out!")
                {
                    Print("Guardian: You saved my life! Thank you. But still, rules are rules. You need to make an appointment", "yellow");
                    Print(user.name+":No problem, I'd like to talk with her as soon as possible","blue");
                    Console.Clear();
                    Print("Next day", "white");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    Print("Ziva: Firstly, I would like to welcome you in my Kingdom. Last time, I had a problem with the mermaids in the  ", "yellow");
                    Print("\"Lake of Truth\". Could you please get rid of them for me?", "yellow");
                    Print(user.name + ":It will be done, Your Highness", "blue");
                    System.Threading.Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
                }
                else
                {
                    Print("You killed the guardian and ended up in jail. However, queen visited you in your cell","white");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();

                    Print("Ziva: If you want to be in my Kingdom you need to stop causing trouble. I called you to help me with various ", "yellow"); 
                        Print("problems and it would be impossible if you were stuck frever in this stinky cell. I'll release you on one ", "yellow"); 
                        Print("condition. You have to help me protect the Kingdom. It is not asking for serving me, but a simple deal.", "yellow");
                    Print(user.name + ":Yes, I'm sorry for causing trouble, Your Highness", "blue");
                    Print("Ziva: RONALD!, Release him!","yellow");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    Print("Next day", "white");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    Print("Ziva: I have called you to see if you are useful. Could you get rid of meramids in the \"Lake of Truth\"? ", "yellow");
                        Print("Oh, I know you will do it. Get to work, fast!", "yellow");
                    Print(user.name + ":It will be done, Your Highness", "blue");
                    System.Threading.Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.DefaultMenu(user, user_Sorcerer,user_Warrior,data,mythings,mermaid, dragon, Human);
                }

            }
            else if(data.Gamedata[14] == "NO DATA")//Chapter 2
            {
                
               if (Mermaids.EnemyKilled < 3)
                {
                    Console.WriteLine("You did not fulfill the task, come back later.");
                    System.Threading.Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.White;
                    Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);

                }
                user.Score += 2000;
                user.UpdateScore(user, data);
                Print("Chapter 2: King of Fire Kingdom","white");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Print("*Knock, knock*", "white");
                Print(user.name + ":I got rid of the mermaids, my Queen", "blue");
                if(data.gamedata[13] == "Watch out!")
                {
                    Print("Ziva: Great job, here's your payment", "yellow");
                    Console.WriteLine("+780");
                    user.AddMoney(780,user,data);
                    user.UpdateMoney(user, data);
                }
                else
                {
                    Print("Ziva: Good.", "yellow");
                }
                Print("Ziva: Now I want you to know what is going on outside of our Kingdom. I want to know everything.", "yellow");
                Print(user.name + ":Do you want from me to spy?", "blue");
                Print("Ziva: Yes, If you get caught like most of my spies. Do not tell anything... Now, do you mind if I ask you to", "yellow");
                Print("leave? I need to make important decisions for the Kingdom", "yellow");
                Console.Clear();
                Print("Chapter 3: The Kingdom of Fire", "white");
                Print("*Knock, knock*", "white");
                Print(user.name + ":Your Majesty, I am the new housekeeper for your castle", "blue");
                Print("King Zorg: Yes, yes... Clean up the table. I have a very important meeting","red");
                Console.Clear();
                Print("Few rub-rubs later... ;)", "white");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Print("King Zorg: Don't let me down, pour my guests best wine in the beginning of the meeting. When they will get drunk,", "red");
                Print(" pour the lowest class possible", "red");
                Print(user.name + "Yes, your Majesty", "white");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Print("Few hours later...", "white");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Print("King Zorg: It is going to happen. The Kingdom of Omaghan will fall. My dragons are hungry and soon I will send ", "red");
                Print("the most powerful one to the town. There will be no other Kingdom except the our Kingdom! Hahaha... Let's drink for ", "red");
                Print("that!", "red");
                Print("*chubby guests drink the wine*","white");
                Program.MakeYourChoice(user,14, data, "*Oh no.. Bottle of wine is empty. What are you going to do?*","Pour the finest wine","Pour the medium wine","...","Pour the worst wine");

                if(data.Gamedata[14] == "Pour the medium wine")
                {
                    Print("*One of guests splits out the wine*", "white");
                    Print("Guest: The wine tastes terrible. Is this how you treat your guests? This is the end of our deal! *Leaves*", "green");
                    Print("King Zorg: *Grunts*.. Speaking of terrible wine, for me it is a piece of art. As you probably know, I am the ", "red");
                    Print("connoisseur of wine and It remainds me my trip to the Empire of Kaahan before I had destroyed it", "red");
                   
                }
                else
                {
                    Print("*Guests continue the feast*", "white");
                }
                
                Console.Clear();
                Print("Next day...", "white");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                if (data.Gamedata[14] != "Pour the medium wine")
                {
                    Print("King Zorg: All you had to do was to pour the wine and you blew it. You are the worst! I sentence you to death. GUAAAARDS!","red");
                    if(data.Gamedata[1]=="Class Warrior")
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("YOU ARE DEAD!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("YOUR SCORE WAS: " + user.Score);
                        Console.WriteLine("Press \"r\" to go back to main menu or \"ESC\" to quit");
                        var key = Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();

                        if (key.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Bye!");
                            System.Threading.Thread.Sleep(5000);
                            Environment.Exit(0);//terminate console
                        }
                        else if (key.KeyChar == 'r')
                        {
                            Program.OpenMainMenu(data);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Unknown option!");
                            System.Threading.Thread.Sleep(5000);
                        }
                    }//Dead screen
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You teleported away from the king");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                    }
                }
                else
                {
                    Print("King Zorg: Everything was perfect. As much perfect as me! Maybe someday you will be my assistant..", "red");
                    Print(user.name + ":*In mind* It would be a great honour, Your Majesty", "blue");
                    Print("*King left the room*", "white");
                    Console.Clear();
                }
                user.Score += 2000;
                user.UpdateScore(user, data);
            }
            
            else if (data.Gamedata[15] == "NO DATA"&&data.Gamedata[1]=="Class Sorcerer"&& data.Gamedata[14] != "Pour the medium wine")//Chapter 3 sorcerer #1
            {
                Console.WriteLine("Chapter 3: Training");
                System.Threading.Thread.Sleep(2000);
                Print("Master of all elements: Who are you?","orange");
                Print(user.name + ":*I am "+user.name+" and I would like you to teach me. I want to slain a dragon", "blue");
                Print("Master of all elements: I cannot. You did not born as a wizard. You are just a sorcerer. You supposed ", "orange");
                Print("to practise your magical power", "orange");
                Program.MakeYourChoice(user, 15, data, "Unless you pay me some money. I will not teach you for free, because you serve the queen!","Okay, I will pay you [Cost:1250/"+Convert.ToString(user.Money_player)+"]","Just tell me what to do","...","I want you to teach me for free");
                Console.Clear();
                if(data.Gamedata[15]== "Okay, I will pay you [Cost:1250/" + Convert.ToString(user.Money_player) + " Dragon Coins]")
                {
                    data.Gamedata[15] = "Okay, I will pay you";
                    if (user.Money_player-1250>=0)
                    {
                        Print("Master of all elements: Okay, let's do it!", "orange");
                        System.Threading.Thread.Sleep(2000);Console.Clear();
                        Print("Wizard go on the training with you. Told you his secrets and \"poofed\" away", "White");
                        System.Threading.Thread.Sleep(2000); Console.Clear();
                        user.RemoveMoney(1250,user,data);
                        user.Score += 4000;
                        user.UpdateScore(user, data);
                        Console.WriteLine("+4000 points");
                        System.Threading.Thread.Sleep(2000); Console.Clear();
                    }
                    else
                    {
                        Print("Master of all elements: You don't have enough money *POOFS*", "orange");
                        data.Gamedata[15] = "I want you to teach me for free";
                        System.Threading.Thread.Sleep(2000); Console.Clear();
                    }
                }
                else if(data.Gamedata[15] =="Just tell me what to do")
                {
                    Print("Master of all elements: You should train your spells against monsters. Then you will learn how ", "orange");
                    Print("to kill the dragon. If you want to find the dragon, you need dragon's shell. Now, excuse me. I have", "orange");
                    Print("important job to do *POOFS*", "orange");
                }
                else
                {
                    Print("Master of all elements: HAHAHA! You are funny, so funny *POOFS*", "orange");
                }
                Print("Author of the game suggest you to practise more, because the dragon is a very hard boss ;)","white");
                System.Threading.Thread.Sleep(2000); Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;

                Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
            }
            else if (data.Gamedata[16] == "NO DATA" && data.Gamedata[1] == "Class Sorcerer"&& data.Gamedata[15] == "NO DATA")//Chapter 3 sorcerer #2
            {
                Console.WriteLine("Chapter 3: Training: The choosen one");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Print("You started to ask yourself if you should get rid of the king for queen's safety","white");
                Program.MakeYourChoice(user,16,data,"Do you want to do it?","Yes, by poisoning the wine","No, I will left him alive and see what happens","...","Yes, I will use my spells against him");
                if(data.Gamedata[16]== "Yes, by poisoning the wine")
                {
                    Print("Unfortunately, the king knew better about wine than you and saw your plan through you. The King is still alive, however, you managed to escape","white");
                }
                else if(data.Gamedata[16] == "Yes, I will use my spells against him")
                {
                    Print("You killed the King and escaped by teleportation, however, one of the dragons is going to terrorize the city.", "white");
                }
                else
                {
                    Print("You decided not to kill the king. The queen send you a letter to go back to her Kingdom","white");
                }
                
                
                Print("Author of the game suggest you to practise more, because the dragon is a very hard boss ;)", "white");
                System.Threading.Thread.Sleep(2000); Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
            }

            else if (data.Gamedata[17] == "NO DATA" && data.Gamedata[1] == "Class Warrior")//Chapter 3 warrior
            {
                Console.WriteLine("Chapter 3: The choosen one");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Print("You started to ask yourself if you should get rid of the king for queen's safety", "white");
                Program.MakeYourChoice(user, 17, data, "Do you want to do it?", "Yes, by poisoning the wine", "No, I will left him alive and see what happens", "...", "Yes, I will do it in old-fashioned way");
                if (data.Gamedata[17] == "Yes, by poisoning the wine")
                {
                    Print("Unfortunately, the king knew better about wine than you and saw your plan through you. The King is still alive, however, you managed to escape", "white");
                }
                else if (data.Gamedata[17] == "Yes, I will do it in old - fashioned way")
                {
                    Print("You killed the King and escaped, however, one of the dragons is going to terrorize the city.", "white");
                }
                else
                {
                    Print("You decided not to kill the king. The queen send you a letter to go back to her Kingdom", "white");
                }



                Print("Author of the game suggest you to practise more, because the dragon is a very hard boss ;)", "white");
                System.Threading.Thread.Sleep(2000); Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
            }
         
            else if (data.Gamedata[18] == "NO DATA")//Chapter 5
            {
                
                bool isQueenAlive = true;
                if(data.Gamedata[17]!="NO DATA"&& data.Gamedata[16] != "NO DATA"&&data.Gamedata[15]=="NO DATA")
                {
                    isQueenAlive = true;
                }
                Console.WriteLine("Final Chapter: Dragon's shell");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                
                if (isQueenAlive ==true)
                {
                    if(data.Gamedata[13]!="Watch out!")
                    {
                        Print(user.name + ": Your Maj-.. Highness! I came here, because the King-..", "blue");
                        Print("Ziva: I do not care! You dissapointed me. Your only task was to spy, nothing more!","yellow");
                        Program.MakeYourChoice(user, 18,data,"","I apologise for my behaviour, Your Highness","I had a reason","...","It is not the right time for discussion, your life is in danger!");
                        if(data.Gamedata[18]== "It is not the right time for discussion, your life is in danger!")
                        {
                            Program.MakeYourChoice(user, 19, data, "What? I am the princess of this Kingdom, I am unstoppable!", "That is your biggest disadvantage!", "What about king of Fire you asked me to spy?", "...", "The dragon will destroy the town!");
                                if(data.Gamedata[19] == "That is your biggest disadvantage!")
                            {
                                Print("Ziva: How dare you talk to me like that?! I am the queen!", "yellow");
                                data.Gamedata[20] = "+You didn't save the Queen";
                            }
                                else if(data.Gamedata[19] == "What about king of Fire you asked me to spy?")
                            {
                                Print("Ziva: ...", "yellow");
                                Print(user.name + ": Please, listen to me. It is for your Kingdom not for me. I know he is dangerous. He has a secret weapon and you need to hide.", "blue");
                                Print("Ziva: GUARDS! Take him away! We need to prepare to fight!", "yellow");
                                Print("Your highness, the Kingdom is ging to be attacked by Zoorg's secret weapon. I need dragon's shell to protect your Kingdom!", "blue");
                        Print("The prediction, came true. Our Kingdom has a real fighter! Take my ring! It is made of dragon's shell. Find the dragon, and kill", "yellow");
                                Print("*Go to teleporter whenever you will be ready*", "white");
                                data.Gamedata[20] = "+You saved the Queen";
                            }
                                else if(data.Gamedata[19] == "The dragon will destroy the town!")
                            {
                                Print("Ziva: What? A dragon? Haha.. I do not believe you. You know what? You are lucky that I did not sentence you to death in the cell...", "yellow");
                                data.Gamedata[20] = "+You didn't save the Queen";
                            }
                        }
                        else if(data.Gamedata[18] == "I apologise for my behaviour, Your Highness")
                        {
                            Print("Ziva: Now, Guards! Take him away...", "yellow");
                            Print(user.name + ": But-...", "blue");
                            Print("*You lost consciousness*","white");
                            data.Gamedata[20] = "+You didn't save the Queen";
                        }
                        else if(data.Gamedata[18] == "I had a reason")
                        {
                            Print("Ziva: You only execute my orders! There is no way for your own ideas!", "yellow");
                            data.Gamedata[20] = "+You didn't save the Queen";
                        }
                    }
                    else
                    {
                        Print(user.name + ": Your highness, the Kingdom is ging to be attacked by Zoorg's secret weapon. I need dragon's shell to protect your Kingdom!", "blue");
                        Print("The prediction, came true. Our Kingdom has a real fighter! Take my ring! It is made of dragon's shell. Find the dragon, and kill","yellow");
                        Print("*Go to teleporter whenever you will be ready*","white");
                        data.Gamedata[20] = "+You saved the Queen";
                    }
                   
                }
                else
                {
                    data.Gamedata[20] = "+You didn't save the Queen";
                    Print("*When you came to the Kingdom, whole city has been already on fire. You have found the Queen's ring decorated with dragon's shell. Now you can fight against dragon. You have failed, but your dreams may still come true!*", "white");
                
                }
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
            JaggedArrayOfChars[1] = ("Money gain: " + user.Money_player + " Dragon Coins").ToCharArray();
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
            if (data.Gamedata[13] == "Watch out!")
                data.Gamedata[13] = "+You rescued Guardian";
            else data.Gamedata[13] = "+You didn't rescue Guardian";
            if (data.Gamedata[14] == "Pour the medium wine")
                data.Gamedata[14] = "+You poured the right wine";
            else data.Gamedata[14] = "+You poured the wrong wine";
            if (data.Gamedata[15] == "Okay, I will pay you")
                data.Gamedata[15] = "+You paid the wizard for training";
            else data.Gamedata[15] = "+You didn't pay wizard for training";
            if (data.Gamedata[16] == "Yes, I will use my spells against him"|| data.Gamedata[16] == "Yes, I will do it in old-fashioned way")
                data.Gamedata[16] = "+You killed the King";
            else data.Gamedata[16] = "+You didn't kill the King";

            if (data.Gamedata[11]!="NO DATA")
            {
                Console.WriteLine(data.Gamedata[11]);
            }
            if (data.Gamedata[12] != "NO DATA")
            {
                Console.WriteLine(data.Gamedata[12]);
            }
            for (int i=13;i<=20;i++)
            {
                if (data.Gamedata[i] != "NO DATA")
                    Console.WriteLine(data.Gamedata[i]);
            }
            Console.ReadKey();
            if(data.Gamedata[12]== "+You helped Mergoth"&&data.Gamedata[11]== "+You helped the little girl"&& data.Gamedata[20]== "+You saved the Queen")
            {
                Console.WriteLine("Achievement get: Helper");
                Console.ReadKey();
            }
            if (data.Gamedata[12] == "+You helped Mergoth" && data.Gamedata[11] == "+You helped the little girl" && data.Gamedata[20] == "+You saved the Queen")
            {
                Console.WriteLine("Achievement get: King slayer");
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
                Print("You found money!","white");
                Random rnd2 = new Random();
             int random2 = rnd.Next(500,4500);
                Console.WriteLine("+"+random2+ " Dragon Coins");
                user.Money_player += random2;
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                data.AutoSaveGame(data);
            }
            else if(random>0&&random<6)
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
                Print("Unfortunately someone robbed you during sleep...","white");
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
                data.AutoSaveGame(data);
                System.Threading.Thread.Sleep(2000);

            }
            else if (random == 6 )
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
                Print("*It's so cold... Is that snow? I need to find some warm place to rest. You noticed a child behind the old shop*","white");
                Print(user.name+": Who are you?","blue");
                Print("Girl: Hello, wanna buy some matches? My father won't let me warm until I sell everything. [Your account ballance: "+ user.Money_player+ "Dragon Coins]","Orange");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. I'll buy one then [cost:10 Dragon Coins]");
                Console.WriteLine("2. I'll take all of them [cost:250 Dragon Coins]");
                Console.WriteLine("3. Sorry, I need to go");
                bool isscarf = user.FindInEquipment(mythings, user, "Scarf");
                if(isscarf==true)
                {
                    Console.WriteLine("4. Take my scarf");
                }

                char choice = Console.ReadKey().KeyChar;
                Console.Clear();
                    switch (choice)
                    {
                        case '1':
                        Print(user.name + ": Okay, I'll buy one","blue");
                        if(10<user.Money_player)
                        {
                            Print("Girl: You are a good man, thank you.","orange");
                            user.Money_player-=10;
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You helped the little girl";
                        }
                        else
                        {
                            Print("Girl: I see you are broke too. Thank you for trying anyway", "orange");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You tried to help the girl";
                        }
                            
                            break;
                        case '2':
                        Print(user.name + ": I'll take everything then","blue");
                        if (250<user.Money_player)
                        {
                            user.Money_player -= 250;
                            Print("Girl: You are a good man, thank you.", "orange");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You helped the little girl";
                        }
                        else
                        {
                            Print("Girl: I see you are broke too. Thank you for trying anyway", "orange");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You tried to help the girl";
                        }
                        
                        
                        break;
                        case '3':

                        Print(user.name + ": Sorry, I need to go... I have a very important mission","blue");
                        Print("Girl: Okay, Sorry for disturbing you.", "orange");
                        data.Gamedata[11] = "+You didn't help the little girl";
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case '4':
                        if (isscarf == true)
                        {
                            Print(user.name + ": Please, take my scarf and come with me. We will find you home","blue");
                            Print("Girl: You are a good man, thank you.", "orange");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[11] = "+You helped the little girl";
                        }
                        else
                        {
                            Print(user.name + ": ...","blue");
                            Print("Girl: Okay, Sorry for disturbing you","orange");
                            Console.ReadKey();
                            data.Gamedata[11] = "+You didn't help the little girl";
                        }
                        break;
                        default:
                        Print(user.name + ": ...", "blue");
                        Print("Girl: Okay, Sorry for disturbing you", "orange");
                        data.Gamedata[11] = "+You didn't help the little girl";
                        Console.ReadKey();
                        break;
                    }
                Console.Clear();
                Console.WriteLine("Game will remember that...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear(); data.AutoSaveGame(data);
            }
            else if(random==7)
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
               Print("-Cousin! It's me, Mergoth. Listen... I need some money [2000 Dragon Coins]","orange");
                Console.WriteLine("1. Okay, take them");
                Console.WriteLine("2. Go away, Mergoth");
                Console.WriteLine("3. How do I know you will give them back?");
                char choice = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (choice)
                {
                    case '1':
                        Print(user.name + ": Okay, take my money", "blue");
                        if (2000 < user.Money_player)
                        {
                            Print("Mergoth: You are a good man, thank you.","orange");
                            user.Money_player -= 2000;
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You helped Mergoth";
                        }
                        else
                        {
                            Print("Mergoth: I see you are broke too. Thank you for trying anyway","orange");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You tried to help Mergoth";
                        }

                        break;
                    case '2':
                        {
                            Print(user.name + ": I'm busy, go away. You'll never give them back!", "blue");
                            Print("Mergoth: Okay","orange");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You didn't help Mergoth";
                        }
                        break;
                    case '3':
                        Print(user.name + ": How do I know you will give them back?", "blue");
                        Print("Mergoth: Please, I pinky-promise!", "orange");
                        Console.WriteLine("1. Okay");
                        Console.WriteLine("2. Go away");
                        char option2 = Console.ReadKey().KeyChar;
                        switch(option2)
                        {
                            case '1':
                                Print(user.name + ": Okay, take my money", "blue");
                                if (2000 < user.Money_player)
                                {
                                    Print("Mergoth: You are a good man, thank you.", "orange");
                                    user.Money_player -= 2000;
                                    Console.ReadKey();
                                    Console.Clear();
                                    data.Gamedata[12] = "+You helped Mergoth";
                                }
                                else
                                {
                                    Print("Mergoth: I see you are broke too. Thank you for trying anyway", "orange");
                                    Console.ReadKey();
                                    Console.Clear();
                                    data.Gamedata[12] = "+You tried to help Mergoth";
                                }
                                break;
                            case '2':
                                Print(user.name + ": I'm busy, go away. You'll never give them back!", "blue");
                                Print("Mergoth: Okay","orange");
                                Console.ReadKey();
                                Console.Clear();
                                data.Gamedata[12] = "+You didn't help Mergoth";
                                break;
                            default:
                               Print(user.name + ": ...", "blue");
                                Print("Mergoth: Okay, Sorry for disturbing you","orange");
                                data.Gamedata[12] = "+You didn't help Mergoth";
                                Console.ReadKey();
                                break;
                        }
                        if (2000 < user.Money_player)
                        {
                            user.Money_player -= 2000;
                            Print("Mergoth: You are a good man, thank you.", "orange");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You helped Mergoth";
                        }
                        else
                        {
                            Print("Mergoth: I see you are broke too. Thank you for trying anyway", "orange");
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You tried to help Mergoth";
                        }


                        break;
                    default:
                        Print(user.name + ": ...", "blue");
                        Print("Mergoth: Okay, Sorry for disturbing you","orange");
                        data.Gamedata[12] = "+You didn't help Mergoth";
                        Console.ReadKey();
                        break;
                }
                        data.Gamedata[12] = "+You helped your cousin";
                Console.Clear();
                Console.WriteLine("Game will remember that...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear(); data.AutoSaveGame(data);
            }
            else if(random==8&&data.Gamedata[13]=="Watch out!")
            {
                Console.WriteLine("RANDOM EVENT!");
                System.Threading.Thread.Sleep(2000);
                Print("-Hey, do you remember me? It's me,Ambe! You saved my life, I'll never forget this. Take my money as a small token of our gratitude","orange");
                Console.WriteLine("+1000 Dragon Coins");
                user.Money_player += 1000; data.AutoSaveGame(data);
                System.Threading.Thread.Sleep(2000);

            }

        }
        public static void MakeYourChoice(Character user,int toOverWrite, Data data, string description, string optionA, string optionB, string optionC, string optionD)
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
               // ConsoleKeyInfo key;
                char choice=' ';
                choice=Console.ReadKey().KeyChar;
                


                Console.ForegroundColor = ConsoleColor.White;
                    if (choice!='1'&& choice != '2' && choice != '3' && choice != '4' )
                    {
                    Console.WriteLine("Unknown option");
                    System.Threading.Thread.Sleep(2000);Console.Clear(); Console.ForegroundColor = ConsoleColor.White;
                }
                    else
                    {
                        
                            if(choice=='1')
                    {
                        data.gamedata[toOverWrite] = optionA;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(optionA);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                            else if(choice=='2')
                    {
                        data.gamedata[toOverWrite] = optionB;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(optionB);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                            else if(choice=='3')
                    {
                        data.gamedata[toOverWrite] = optionC;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(optionC);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                            else
                    {
                        data.gamedata[toOverWrite] = optionD;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(optionD);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                                
                    }
                
                Console.WriteLine();
                
            }
            while (toReturn==false);
            user.Score += 500;
            Console.Clear();
            Console.WriteLine("You have made your choice: +500 points"); 
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("Game will remember that!");
            System.Threading.Thread.Sleep(2000);
            Console.Clear(); data.AutoSaveGame(data);

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
                char option=' ';
                do
                {

                    Toreturn = true;
                    Console.Clear();
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Dragon's shell v1.0");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
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
            Console.ForegroundColor=ConsoleColor.White;
            do
            {
                
                Console.Clear();
                if (data.gamedata[1] == "Class Warrior")
                {
                    Console.WriteLine("Money: " + user_Warrior.Money_player + " Dragon Coins");
                    Console.WriteLine("HP: " + user_Warrior.Actual_hp);
                    Console.WriteLine("Score: " + user_Warrior.Score);
                }
                else
                {
                    Console.WriteLine("Money: " + user_Sorcerer.Money_player+ " Dragon Coins");
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
                   // OpenMainMenu(data);
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
                            data.AutoSaveGame(data);
                            user_Sorcerer.Walk(user, data, user_Sorcerer,user_Warrior, mythings, mermaid, dragon,Human);
                        }
                        else
                        {
                            data.AutoSaveGame(data);
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
    