using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    /// <summary>
    /// Used for storing all the text which appears in the game
    /// </summary>
    class Scenario
    {
        /// <summary>
        /// Main Menu Text
        /// </summary>
        public static void MainMenu()
        {
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
        }
        /// <summary>
        /// Default Menu Text
        /// </summary>
        /// <param name="money">Player's money</param>
        /// <param name="acthp">Player's health points</param>
        /// <param name="score">Player's score</param>
        /// <param name="clas">Player's character class</param>
        public static void DefaultMenu(int money, int acthp, int score, string clas)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("Money: " + money + " Dragon Coins");
            Console.WriteLine("HP: " + acthp);
            Console.WriteLine("Score: " + score);

            Console.WriteLine("What would you like to do now? *PRESS [ESC] to go back to main menu*");
            Console.WriteLine("1. Open Equipment");
            Console.WriteLine("2. Open Map");

            if (clas == Program.Class['s'])
            {
                Console.WriteLine("3. Teleport");
            }
            else
            {
                Console.WriteLine("3. Go to the teleporter");
            }
            Console.Write("Select: ");
        }
        /// <summary>
        /// Walk menu
        /// </summary>
        /// <param name="user">Object needed for basic informations</param>
        public static void WhereToGoMenu(Character user)
        {
            Console.Clear();
            Console.WriteLine("Money: " + user.Money_player);
            Console.WriteLine("HP: " + user.Actual_hp);
            Console.WriteLine("Score: " + user.Score);
            Console.WriteLine("Where would you like to go? (Press [ESC] to go back)");
            Console.WriteLine("1. Royal tavern");
            Console.WriteLine("2. Lake of truth");
            Console.WriteLine("3. Local shop");
            Console.WriteLine("4. Queen Ziva");
            Console.Write("Select: ");
        }
        /// <summary>
        ///Text after the decision
        /// </summary>
        /// <param name="data">Data to save</param>
        /// <param name="user">Object needed for basic informations</param>
        public static void EndOfDecision(Data data, Character user)
        {
            user.Score += 500;
            Console.Clear();
            Console.WriteLine("You have made your choice: +500 points");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("Game will remember that!");
            System.Threading.Thread.Sleep(2000);
            Console.Clear(); data.AutoSaveGame(data);

        }
        /// <summary>
        /// Chapter 1
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter1(Data data, Character user)
        {
            user.Score += 2000;
            user.UpdateScore(user, data);
            Program.Print("Chapter 1: Newbie", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("Guardian: Who are you?", 'y');
            Program.Print(user.name + ": I'm " + Convert.ToString(user.name) + ". I was asked by your Queen to...", 'b');
            Program.Print("Guardian: I don't care. Do you have a trespass?", 'y');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("MAKE YOUR CHOICE!", 'w');
            Program.MakeYourChoice(user, 13, data, "*You see a huge rock sliding through the roof*", "*Back off quietly*", "Watch out!", "...", "Maybe ask your queen about that");
        }
        /// <summary>
        /// Chapter 1: Alternative path when player saves Guardian
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter1a(Data data, Character user)
        {
            Program.Print("Guardian: You saved my life! Thank you. But still, rules are rules. You need to make an appointment", 'y');
            Program.Print(user.name + ":No problem, I'd like to talk with her as soon as possible", 'b');
            Console.Clear();
            Program.Print("Next day", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("Ziva: Firstly, I would like to welcome you in my Kingdom. Last time, I had a problem with the mermaids in the  ", 'y');
            Program.Print("\"Lake of Truth\". Could you please get rid of them for me?", 'y');
            Program.Print(user.name + ":It will be done, Your Highness", 'b');
            System.Threading.Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Chapter 1: Alternative path when player doesn't save Guardian
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter1b(Data data, Character user)
        {
            Program.Print("You killed the guardian and ended up in jail. However, queen visited you in your cell", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("Ziva: If you want to be in my Kingdom you need to stop causing trouble. I called you to help me with various ", 'y');
            Program.Print("problems and it would be impossible if you were stuck forever in this stinky cell. I'll release you on one ", 'y');
            Program.Print("condition. You have to help me protect the Kingdom. It is not asking for serving me, but a simple deal.", 'y');
            Program.Print(user.name + ":Yes, I'm sorry for causing trouble, Your Highness", 'b');
            Program.Print("Ziva: RONALD, Release him!", 'y');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("Next day", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("Ziva: I have called you to see if you are useful. Could you get rid of meramids in the \"Lake of Truth\"? ", 'y');
            Program.Print("Oh, I know you will do it. Get to work, fast!", 'y');
            Program.Print(user.name + ":It will be done, Your Highness", 'b');
            System.Threading.Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Chapter 2
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter2(Data data, Character user)
        {

            user.Score += 2000;
            user.UpdateScore(user, data);
            Program.Print("Chapter 2: King of Fire Kingdom", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("*Knock, knock*", 'w');
            Program.Print(user.name + ":I got rid of the mermaids, my Queen", 'b');
            if (data.gamedata[13] == Program.Decisions["1b"])
            {
                Program.Print("Ziva: Great job, here's your payment", 'y');
                Console.WriteLine("+780");
                user.AddMoney(780, user, data);
                user.UpdateMoney(user, data);
            }
            else
            {
                Program.Print("Ziva: Good.", 'y');
            }
            Program.Print("Ziva: Now I want you to know what is going on outside of our Kingdom. I want to know everything.", 'y');
            Program.Print(user.name + ":Do you want me to spy?", 'b');
            Program.Print("Ziva: Yes, If you get caught like most of my spies. Do not tell anything... Now, do you mind if I ask you to", 'y');
            Program.Print("leave? I need to make important Program.Decisions for the Kingdom", 'y');
            Console.Clear();
            Program.Print("Chapter 3: The Kingdom of Fire", 'w');
            Program.Print("*Knock, knock*", 'w');
            Program.Print(user.name + ":Your Majesty, I am the new housekeeper for your castle", 'b');
            Program.Print("King Zorg: Yes, yes... Clean up the table. I have a very important meeting", 'r');
            Console.Clear();
            Program.Print("Few rub-rubs later... ;)", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("King Zorg: Don't let me down, pour my guests best wine in the beginning of the meeting. When they will get drunk,", 'r');
            Program.Print(" pour the lowest class possible", 'r');
            Program.Print(user.name + ": Yes, your Majesty", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("Few hours later...", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("King Zorg: It is going to happen. The Kingdom of Omaghan will fall. My dragons are hungry and soon I will send ", 'r');
            Program.Print("the most powerful one to the town. There will be no other Kingdom except the our Kingdom! Hahaha... Let's drink for ", 'r');
            Program.Print("that!", 'r');
            Program.Print("*chubby guests drink the wine*", 'w');
            Program.MakeYourChoice(user, 14, data, "*Oh no.. Bottle of wine is empty. What are you going to do?*", "Pour the finest wine", "Pour the medium wine", "...", "Pour the worst wine");
            if (data.Gamedata[14] == Program.Decisions["2d"])
            {
                Program.Print("*One of guests splits out the wine*", 'w');
                Program.Print("Guest: The wine tastes terrible. Is this how you treat your guests? This is the end of our deal! *Leaves*", 'g');
                Program.Print("King Zorg: *Grunts*.. Speaking of terrible wine, for me it is a piece of art. As you probably know, I am the ", 'r');
                Program.Print("connoisseur of wine and It remainds me my trip to the Empire of Kaahan before I had destroyed it", 'r');

            }
            else
            {
                Program.Print("*Guests continue the feast*", 'w');
            }

            Console.Clear();
            Program.Print("Next day...", 'w');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();

        }
        /// <summary>
        /// Alternative path if player chooses wrong wine 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter2a(Data data, Character user)
        {
            Program.Print("King Zorg: All you had to do was to pour the wine and you blew it. You are the worst! I sentence you to death. GUAAAARDS!", 'r');
            if (data.Gamedata[1] == Program.Class['w'])
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
                user.Score += 2000;
                user.UpdateScore(user, data);
            }
        }
        /// <summary>
        /// Alternative path if player chooses good wine 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter2b(Data data, Character user)
        {
            Program.Print("King Zorg: Everything was perfect. As much perfect as me! Maybe someday you will be my assistant..", 'r');
            Program.Print(user.name + ":*In mind* It would be a great honour, Your Majesty", 'b');
            Program.Print("*King left the room*", 'w');
            Console.Clear();
            user.Score += 2000;
            user.UpdateScore(user, data);
        }
        /// <summary>
        /// Alternative path for Sorcerer when he will teleport away from the king
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter3a(Data data, Character user)
        {
            Console.WriteLine("Chapter 3: Training");
            System.Threading.Thread.Sleep(2000);
            Program.Print("Master of all elements: Who are you?", 'm');
            Program.Print(user.name + ":*I am " + user.name + " and I would like you to teach me. I want to slain a dragon", 'b');
            Program.Print("Master of all elements: I cannot. You did not born as a wizard. You are just a sorcerer. You supposed ", 'm');
            Program.Print("to practise your magical power", 'm');
            Program.MakeYourChoice(user, 15, data, "Unless you pay me some money. I will not teach you for free, because you serve the queen!", "Okay, I will pay you [Cost:1250/" + Convert.ToString(user.Money_player) + "]", "Just tell me what to do", "...", "I want you to teach me for free");
            Console.Clear();
            if (data.Gamedata[15] == "Okay, I will pay you [Cost:1250/" + Convert.ToString(user.Money_player) + " Dragon Coins]")
            {
                data.Gamedata[15] = "Okay, I will pay you";
                if (user.Money_player - 1250 >= 0)
                {
                    Program.Print("Master of all elements: Okay, let's do it!", 'm');
                    System.Threading.Thread.Sleep(2000); Console.Clear();
                    Program.Print("Wizard go on the training with you. Told you his secrets and \"poofed\" away", 'w');
                    System.Threading.Thread.Sleep(2000); Console.Clear();
                    user.RemoveMoney(1250, user, data);
                    user.Score += 4000;
                    user.UpdateScore(user, data);
                    Console.WriteLine("+4000 points");
                    System.Threading.Thread.Sleep(2000); Console.Clear();
                }
                else
                {
                    Program.Print("Master of all elements: You don't have enough money *POOFS*", 'm');
                    data.Gamedata[15] = Program.Decisions["3Ad"];
                    System.Threading.Thread.Sleep(2000); Console.Clear();
                }
            }
            else if (data.Gamedata[15] == Program.Decisions["3Ab"])
            {
                Program.Print("Master of all elements: You should train your spells against monsters. Then you will learn how ", 'm');
                Program.Print("to kill the dragon. If you want to find the dragon, you need dragon's shell. Now, excuse me. I have", 'm');
                Program.Print("important job to do *POOFS*", 'm');
            }
            else
            {
                Program.Print("Master of all elements: HAHAHA! You are funny, so funny *POOFS*", 'm');
            }
            Program.Print("Author of the game suggest you to practise more, because the dragon is a very hard boss ;)", 'w');
            System.Threading.Thread.Sleep(2000); Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

        }

        /// <summary>
        /// Alternative path when player will not spoil the party
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter3b(Data data, Character user)
        {
            Console.WriteLine("Chapter 3: Training: The choosen one");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("You started to ask yourself if you should get rid of the king for queen's safety", 'w');
            Program.MakeYourChoice(user, 16, data, "Do you want to do it?", "Yes, by poisoning the wine", "No, I will left him alive and see what happens", "...", "Yes, I will use my spells against him");
            if (data.Gamedata[16] == Program.Decisions["3Ba"])
            {
                Program.Print("Unfortunately, the king knew better about wine than you and saw your plan through you. The King is still alive, however, you managed to escape", 'w');
            }
            else if (data.Gamedata[16] == Program.Decisions["3Bd"])
            {
                Program.Print("You killed the King and escaped by teleportation, however, one of the dragons is going to terrorize the city.", 'w');
            }
            else
            {
                Program.Print("You decided not to kill the king. The queen send you a letter to go back to her Kingdom", 'w');
            }


            Program.Print("Author of the game suggest you to practise more, because the dragon is a very hard boss ;)", 'w');
            System.Threading.Thread.Sleep(2000); Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Alternative path when warrior will give wrong wine
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter3c(Data data, Character user)
        {
            Console.WriteLine("Chapter 3: The choosen one");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Program.Print("You started to ask yourself if you should get rid of the king for queen's safety", 'w');
            Program.MakeYourChoice(user, 17, data, "Do you want to do it?", "Yes, by poisoning the wine", "No, I will left him alive and see what happens", "...", "Yes, I will do it in old-fashioned way");
            if (data.Gamedata[17] == Program.Decisions["3Ba"])
            {
                Program.Print("Unfortunately, the king knew better about wine than you and saw your plan through you. The King is still alive, however, you managed to escape", 'w');
            }
            else if (data.Gamedata[17] == Program.Decisions["3Cd"])
            {
                Program.Print("You killed the King and escaped, however, one of the dragons is going to terrorize the city.", 'w');
            }
            else
            {
                Program.Print("You decided not to kill the king. The queen send you a letter to go back to her Kingdom", 'w');
            }



            Program.Print("Author of the game suggest you to practise more, because the dragon is a very hard boss ;)", 'w');
            System.Threading.Thread.Sleep(2000); Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Chapter 4
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public static void Chapter4(Data data, Character user)
        {
            bool isQueenAlive = true;
            if (data.Gamedata[17] != Program.Decisions["null"] && data.Gamedata[16] != Program.Decisions["null"] && data.Gamedata[15] == Program.Decisions["null"])
            {
                isQueenAlive = true;
            }
            Console.WriteLine("Final Chapter: Dragon's shell");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();

            if (isQueenAlive == true)
            {
                if (data.Gamedata[13] != Program.Decisions["1b"])
                {
                    Program.Print(user.name + ": Your Maj-.. Highness! I came here, because the King-..", 'b');
                    Program.Print("Ziva: I do not care! You dissapointed me. Your only task was to spy, nothing more!", 'y');
                    Program.MakeYourChoice(user, 18, data, "", "I apologise for my behaviour, Your Highness", "I had a reason", "...", "It is not the right time for discussion, your life is in danger!");
                    if (data.Gamedata[18] == Program.Decisions["4d"])
                    {
                        Program.MakeYourChoice(user, 19, data, "What? I am the princess of this Kingdom, I am unstoppable!", "That is your biggest disadvantage!", "What about king of Fire you asked me to spy?", "...", "The dragon will destroy the town!");
                        if (data.Gamedata[19] == Program.Decisions["5a"])
                        {
                            Program.Print("Ziva: How dare you talk to me like that?! I am the queen!", 'y');
                            data.Gamedata[20] = Program.Decisions["6b"];
                        }
                        else if (data.Gamedata[19] == Program.Decisions["5b"])
                        {
                            Program.Print("Ziva: ...", 'y');
                            Program.Print(user.name + ": Please, listen to me. It is for your Kingdom not for me. I know he is dangerous. He has a secret weapon and you need to hide.", 'b');
                            Program.Print("Ziva: GUARDS! Take him away! We need to prepare to fight!", 'y');
                            Program.Print("Your highness, the Kingdom is ging to be attacked by Zoorg's secret weapon. I need dragon's shell to protect your Kingdom!", 'b');
                            Program.Print("The prediction, came true. Our Kingdom has a real fighter! Take my ring! It is made of dragon's shell. Find the dragon, and kill", 'y');
                            Program.Print("*Go to teleporter whenever you will be ready*", 'w');
                            data.Gamedata[20] = Program.Decisions["6a"];
                        }
                        else if (data.Gamedata[19] == Program.Decisions["5d"])
                        {
                            Program.Print("Ziva: What? A dragon? Haha.. I do not believe you. You know what? You are lucky that I did not sentence you to death in the cell...", 'y');
                            data.Gamedata[20] = Program.Decisions["6b"];
                        }
                    }
                    else if (data.Gamedata[18] == Program.Decisions["4a"])
                    {
                        Program.Print("Ziva: Now, Guards! Take him away...", 'y');
                        Program.Print(user.name + ": But-...", 'b');
                        Program.Print("*You lost consciousness*", 'w');
                        data.Gamedata[20] = Program.Decisions["6b"];
                    }
                    else if (data.Gamedata[18] == Program.Decisions["4b"])
                    {
                        Program.Print("Ziva: You only execute my orders! There is no way for your own ideas!", 'y');
                        data.Gamedata[20] = Program.Decisions["6b"];
                    }
                }
                else
                {
                    Program.Print(user.name + ": Your highness, the Kingdom is ging to be attacked by Zoorg's secret weapon. I need dragon's shell to protect your Kingdom!", 'b');
                    Program.Print("The prediction, came true. Our Kingdom has a real fighter! Take my ring! It is made of dragon's shell. Find the dragon, and kill", 'y');
                    Program.Print("*Go to teleporter whenever you will be ready*", 'w');
                    data.Gamedata[20] = Program.Decisions["6a"];
                }

            }
            else
            {
                data.Gamedata[20] = Program.Decisions["6b"];
                Program.Print("*When you came to the Kingdom, whole city has been already on fire. You have found the Queen's ring decorated with dragon's shell. Now you can fight against dragon. You have failed, but your dreams may still come true!*", 'w');

            }
        }
        /// <summary>
        /// "Good" ending
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        public static void QueenSafeEnding(Data data, Character user, Item mythings)
        {
            if (data != null && data.Gamedata[20] == Program.Decisions["6a"])
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
        }
        /// <summary>
        /// Title for random event
        /// </summary>
        public static void Title()
        {
            Program.Print("RANDOM EVENT!", 'r');
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }
        /// <summary>
        /// Event script when player finds money
        /// </summary>
        /// <returns></returns>
        public static int EventMoneyFound()
        {
            Title();
            Program.Print("You found money!", 'w');
            Random rnd2 = new Random();
            int random2 = rnd2.Next(500, 4500);
            Console.WriteLine("+" + random2 + " Dragon Coins");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            return random2;
        }
        /// <summary>
        /// Event script showing player being robbed by someone
        /// </summary>
        /// <returns></returns>
        public static int ThiefEvent()
        {
            Title();
            Program.Print("Unfortunately someone robbed you during sleep...", 'w');
            Random rnd = new Random();
            int random = rnd.Next(500, 4500);
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            return random;
        }
        /// <summary>
        /// Event featured character known from fairy tale
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        public static void GirlWithMatches(Character user, Item mythings, Data data)
        {
            Title();
            Program.Print("*It's so cold... Is that snow? I need to find some warm place to rest. You noticed a child behind the old shop*", 'w');
            Program.Print(user.name + ": Who are you?", 'b');
            Program.Print("Girl: Hello, wanna buy some matches? My father won't let me warm until I sell everything. [Your account ballance: " + user.Money_player + "Dragon Coins]", 'm');
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. I'll buy one then [cost:10 Dragon Coins]");
            Console.WriteLine("2. I'll take all of them [cost:250 Dragon Coins]");
            Console.WriteLine("3. Sorry, I need to go");
            bool isscarf = user.FindInEquipment(mythings, user, "Scarf");
            if (isscarf == true)
            {
                Console.WriteLine("4. Take my scarf");
            }

            char choice = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (choice)
            {
                case '1':
                    Program.Print(user.name + ": Okay, I'll buy one", 'b');
                    if (10 < user.Money_player)
                    {
                        Program.Print("Girl: You are a good man, thank you.", 'm');
                        user.RemoveMoney(10, user, data);
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[11] = Program.Decisions["Add1a"];
                    }
                    else
                    {
                        Program.Print("Girl: I see you are broke too. Thank you for trying anyway", 'm');
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[11] = Program.Decisions["Add1c"];
                    }

                    break;
                case '2':
                    Program.Print(user.name + ": I'll take everything then", 'b');
                    if (250 < user.Money_player)
                    {
                        user.Money_player -= 250;
                        Program.Print("Girl: You are a good man, thank you.", 'm');
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[11] = Program.Decisions["Add1a"];
                    }
                    else
                    {
                        Program.Print("Girl: I see you are broke too. Thank you for trying anyway", 'm');
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[11] = Program.Decisions["Add1c"];
                    }


                    break;
                case '3':

                    Program.Print(user.name + ": Sorry, I need to go... I have a very important mission", 'b');
                    Program.Print("Girl: Okay, Sorry for disturbing you.", 'm');
                    data.Gamedata[11] = Program.Decisions["Add1b"];
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case '4':
                    if (isscarf == true)
                    {
                        Program.Print(user.name + ": Please, take my scarf and come with me. We will find you home", 'b');
                        Program.Print("Girl: You are a good man, thank you.", 'm');
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[11] = Program.Decisions["Add1a"];
                    }
                    else
                    {
                        Program.Print(user.name + ": ...", 'b');
                        Program.Print("Girl: Okay, Sorry for disturbing you", 'm');
                        Console.ReadKey();
                        data.Gamedata[11] = Program.Decisions["Add1b"];
                    }
                    break;
                default:
                    Program.Print(user.name + ": ...", 'b');
                    Program.Print("Girl: Okay, Sorry for disturbing you", 'm');
                    data.Gamedata[11] = Program.Decisions["Add1b"];
                    Console.ReadKey();
                    break;
            }
            Console.Clear();
            Console.WriteLine("Game will remember that...");
            System.Threading.Thread.Sleep(2000);
            Console.Clear(); data.AutoSaveGame(data);
        }
        /// <summary>
        /// Event with Mergoth cousin
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        public static void Mergoth(Character user, Item mythings, Data data)
        {
            Title();
            Program.Print("-Cousin! It's me, Mergoth. Listen... I need some money [2000 Dragon Coins]", 'm');
            Console.WriteLine("1. Okay, take them");
            Console.WriteLine("2. Go away, Mergoth");
            Console.WriteLine("3. How do I know you will give them back?");
            char choice = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (choice)
            {
                case '1':
                    Program.Print(user.name + ": Okay, take my money", 'b');
                    if (2000 < user.Money_player)
                    {
                        Program.Print("Mergoth: You are a good man, thank you.", 'm');
                        user.Money_player -= 2000;
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[12] = "+You helped Mergoth";
                    }
                    else
                    {
                        Program.Print("Mergoth: I see you are broke too. Thank you for trying anyway", 'm');
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[12] = "+You tried to help Mergoth";
                    }

                    break;
                case '2':
                    {
                        Program.Print(user.name + ": I'm busy, go away. You'll never give them back!", 'b');
                        Program.Print("Mergoth: Okay", 'm');
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[12] = "+You didn't help Mergoth";
                    }
                    break;
                case '3':
                    Program.Print(user.name + ": How do I know you will give them back?", 'b');
                    Program.Print("Mergoth: Please, I pinky-promise!", 'm');
                    Console.WriteLine("1. Okay");
                    Console.WriteLine("2. Go away");
                    char option2 = Console.ReadKey().KeyChar;
                    switch (option2)
                    {
                        case '1':
                            Program.Print(user.name + ": Okay, take my money", 'b');
                            if (2000 < user.Money_player)
                            {
                                Program.Print("Mergoth: You are a good man, thank you.", 'm');
                                user.Money_player -= 2000;
                                Console.ReadKey();
                                Console.Clear();
                                data.Gamedata[12] = "+You helped Mergoth";
                            }
                            else
                            {
                                Program.Print("Mergoth: I see you are broke too. Thank you for trying anyway", 'm');
                                Console.ReadKey();
                                Console.Clear();
                                data.Gamedata[12] = "+You tried to help Mergoth";
                            }
                            break;
                        case '2':
                            Program.Print(user.name + ": I'm busy, go away. You'll never give them back!", 'b');
                            Program.Print("Mergoth: Okay", 'm');
                            Console.ReadKey();
                            Console.Clear();
                            data.Gamedata[12] = "+You didn't help Mergoth";
                            break;
                        default:
                            Program.Print(user.name + ": ...", 'b');
                            Program.Print("Mergoth: Okay, Sorry for disturbing you", 'm');
                            data.Gamedata[12] = "+You didn't help Mergoth";
                            Console.ReadKey();
                            break;
                    }
                    if (2000 < user.Money_player)
                    {
                        user.Money_player -= 2000;
                        Program.Print("Mergoth: You are a good man, thank you.",'m');
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[12] = "+You helped Mergoth";
                    }
                    else
                    {
                        Program.Print("Mergoth: I see you are broke too. Thank you for trying anyway", 'm');
                        Console.ReadKey();
                        Console.Clear();
                        data.Gamedata[12] = "+You tried to help Mergoth";
                    }


                    break;
                default:
                    Program.Print(user.name + ": ...",'b');
                    Program.Print("Mergoth: Okay, Sorry for disturbing you", 'm');
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
        /// <summary>
        /// Random event
        /// </summary>
        public static void Ambe()
        {
            Title();
            Program.Print("-Hey, do you remember me? It's me,Ambe! You saved my life, I'll never forget this. Take my money as a small token of our gratitude", 'm');
            Console.WriteLine("+1000 Dragon Coins");
            System.Threading.Thread.Sleep(2000);
        }
        /// <summary>
        /// Inkeeper quotes
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Depending on your choices, the reaction differ</returns>
        public static int BartenderQuotes(Data data)
        {
            Console.Clear();
            Random rnd = new Random();
            int howmuch = rnd.Next(50, 150);
            if (data.gamedata[13] != Program.Decisions["1b"] && howmuch <= 70 && data.gamedata[13] != Program.Decisions["null"] && data.gamedata[20] == Program.Decisions["null"])
            {
                Program.Print("Innkeeper: I heard that you made our princess upset. I have an advice for you for your safety do what she wants.", 'y');
            }
            else if (data.gamedata[20] == Program.Decisions["6a"])
            {
                Program.Print("Innkeeper: You are a hero! You saved our queen! It's on me! [Y/N]", 'y');
                howmuch = 0;
            }
            else
            {
                Program.Print("Innkeeper: Tough day, huh? Maybe wanna have some beer? Only " + Convert.ToString(howmuch) + " Dragon Coins *Type \"y\" for yes or \"n\" for no*", 'y');
            }
            return howmuch;
        }
        /// <summary>
        /// Quotes which use shopkeeper
        /// </summary>
        /// <param name="data">Depending on your choices, the reaction differ</param>
        /// <returns></returns>
        public static int ShopkeeperQuotes(Data data)
        {
            int additionalprize = 0;
            if (data.gamedata[13] != Program.Decisions["1b"] && data.gamedata[13] != Program.Decisions["null"] && data.gamedata[20] == Program.Decisions["null"])
            {
                Program.Print("Shopkeeper: I don't like people like you. Stay away from our Kingdom!", 'y');
                additionalprize = 110;
            }
            else if (data.gamedata[20] == Program.Decisions["6a"] && data.gamedata[13] != Program.Decisions["1b"])
            {
                Program.Print("Shopkeeper: You are a hero! You saved our queen! I've just realized how much I was wrong", 'y');
            }
            else
            {
                Program.Print("Shopkeeper: Welcome in my shop, traveler, what do you need?", 'y');
            }
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            return additionalprize;
        }

        /// <summary>
        /// Quotes when sorcerer randomly spawn a fish
        /// </summary>
        public static void FishyQuotes()
        {
            Console.Clear();
            Console.WriteLine("You summoned the goldfish! Now, make your wish...");
            Console.WriteLine("1. Kill the monster");
            Console.WriteLine("2. I want money");
            Console.WriteLine("3. Leave me alone");
            Console.WriteLine("4. I'll finish you!");
            Console.Write("Select: ");
        }
        /// <summary>
        /// Prologue
        /// </summary>
        /// <param name="data"></param>
        public static void Prologue(Data data)
        {
            char classification;

            Console.Clear();
            Program.Print("-Hello traveler, what's your name?", 'm');
            string name = Console.ReadLine();
            data.gamedata[0] = name;
            do
            {
                Program.Print("Nice to meet you, " + name + "! I'm Neth and let me introduce you our" +
                    " kingdom of Omaghan. During your journey you can serve our queen as a soldier or as a warrior. By the way, could you tell me what's your actual speciality?", 'm');

                Program.Print("1. I'm a warrior.", 'b');
                Program.Print("2. I'm a sorcerer.", 'b');
                Console.Write("Select: ");
                classification = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (classification == '1')
                {
                    data.gamedata[1] = Program.Class['w'];
                }
                else if (classification == '2')
                {
                    data.gamedata[1] = Program.Class['s'];
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
    
