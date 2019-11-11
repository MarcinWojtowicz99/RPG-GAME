using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    class NPC
    {
        static int money;
        static int money_player;
        public static int moneyPlayer { get { return money_player; } }
        int nbofequipment;
        static int actual_hp;
        static int basic_hp;
        public static int Actual { get { return actual_hp; } }
        public static int Basic { get { return basic_hp; } }
        int[,] equipment;



        int[] arrayofindexes;
        public NPC(int nbofequipment, Item mythings)
        {
            arrayofindexes = new int[nbofequipment];
            Random rnd = new Random();

            for (int i = 0; i < nbofequipment; i++)
            {
                Random rnd2 = new Random();
                arrayofindexes[i] = rnd.Next(1, mythings.Nb_Of_Items);
            }

        }
        public void ShowItems(Sorcerer user,Warrior war, Item mythings,Data data,Enemy Mermaid, Enemy Dragon)
        {
            if(data.gamedata[1]=="Class Sorcerer")
            {
                
                while (true)
                {
                    Console.Clear();
                    Random rnd = new Random();
                    money = rnd.Next(100, 1000);
                    Console.WriteLine("Shopkeeper account value: " + money);
                    Console.WriteLine("Your account value: " + user.Money_player);
                    Console.WriteLine("Today I can offer you: ");
                    int[] value = new int[arrayofindexes.Length];
                    for (int i = 0; i < arrayofindexes.Length; i++)
                    {
                        value[i] = mythings.unit[arrayofindexes[i]].Generate_Value();
                        Console.WriteLine("{0}. {1}: {2}PLN", i, mythings.unit[arrayofindexes[i]].nameofitem, value[i]);
                    }
                    Console.WriteLine("{0}. I wanna sell you something", arrayofindexes.Length + 1);
                    Console.WriteLine("{0}. No, thank you", arrayofindexes.Length + 2);
                    Console.Write("Select: ");
                    int key = CharUnicodeInfo.GetDecimalDigitValue(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                    Console.WriteLine(key);


                    if (key <= arrayofindexes.Length)
                    {
                        for (int i = 0; i < arrayofindexes.Length; i++)
                        {
                            if (key == i)
                            {
                                user.Pay(value[i], arrayofindexes[i], mythings,user, war);
                            }

                        }
                       

                    }
                    else if (key == arrayofindexes.Length + 1)
                    {
                        bool toReturn2 = false;
                        do
                        {
                            equipment =user.Equipment();
                            Console.Clear();
                            Console.WriteLine("Shopkeeper account value: " + money);
                            Console.WriteLine("Your account value: " + user.Money_player);
                            Console.WriteLine("What do you wanna sell? ");
                            Console.WriteLine("Your equipment: ");
                            int[] value2 = new int[equipment.Length / 2];
                            for (int i = 0; i < equipment.Length / 2; i++)
                            {
                                if (equipment[i, 0] != 0)
                                {
                                    value[i] = mythings.unit[equipment[i, 0]].Generate_Value();
                                    Console.WriteLine("{0}. {1}: {2}PLN", i, mythings.unit[equipment[i, 0]].nameofitem, value2[i]);
                                }

                            }
                            Console.WriteLine("{0}. I resign", arrayofindexes.Length + 1);
                            Console.Write("Select: ");
                            char nb2 = Console.ReadKey().KeyChar;
                            int key2 = CharUnicodeInfo.GetDecimalDigitValue(nb2);
                            Console.WriteLine();


                            if (key2 <= arrayofindexes.Length)
                            {
                                for (int i = 0; i < arrayofindexes.Length; i++)
                                {
                                    if (key2 == i)
                                    {
                                        user.Sell(value[i], i);
                                    }

                                }
                             

                            }
                            else if (key == arrayofindexes.Length + 1)
                            {
                                break;
                            }

                            else
                            {


                                Console.WriteLine("Unknown option!");

                            }
                        } while (toReturn2 == false);

                    }

                    else if (key == arrayofindexes.Length + 2)
                    {
                        Console.WriteLine("Bye");
                        Program.DefaultMenu(user,war,data,mythings,Mermaid,Dragon);
                    }
                    else
                    {

                        Console.WriteLine("Unknown option!");
                        
                    }
                }
            }
            else
            {
                while(true)
                {
                    Console.Clear();
                    Random rnd = new Random();
                    money = rnd.Next(100, 1000);
                    Console.WriteLine("Shopkeeper account value: " + money);
                    Console.WriteLine("Your account value: " + war.Money_player);
                    Console.WriteLine("Today I can offer you: ");
                    int[] value = new int[arrayofindexes.Length];
                    for (int i = 0; i < arrayofindexes.Length; i++)
                    {
                        value[i] = mythings.unit[arrayofindexes[i]].Generate_Value();
                        Console.WriteLine("{0}. {1}: {2}PLN", i, mythings.unit[arrayofindexes[i]].nameofitem, value[i]);
                    }
                    Console.WriteLine("{0}. I wanna sell you something", arrayofindexes.Length + 1);
                    Console.WriteLine("{0}. No, thank you", arrayofindexes.Length + 2);
                    Console.Write("Select: ");
                    char nb = Console.ReadKey().KeyChar;
                    int key = CharUnicodeInfo.GetDecimalDigitValue(nb);
                    Console.WriteLine();


                    if (key <= arrayofindexes.Length)
                    {
                        for (int i = 0; i < arrayofindexes.Length; i++)
                        {
                            if (key == i)
                            {
                                war.Pay(value[i], arrayofindexes[i], mythings,user,war);
                            }

                        }

                    }
                    else if (key == arrayofindexes.Length + 1)
                    {
                        bool toReturn2 = false;
                        do
                        {
                            equipment = war.Equipment();
                            Console.Clear();
                            Console.WriteLine("Shopkeeper account value: " + money);
                            Console.WriteLine("Your account value: " + war.Money_player);
                            Console.WriteLine("What do you wanna sell? ");
                            Console.WriteLine("Your equipment: ");
                            int[] value2 = new int[equipment.Length/2 ];
                            for (int i = 0; i < equipment.Length/2 ; i++)
                            {
                                if(equipment[i,0]!=0)
                                {
                                    value[i] = mythings.unit[equipment[i, 0]].Generate_Value();
                                    Console.WriteLine("{0}. {1}: {2}PLN", i, mythings.unit[equipment[i, 0]].nameofitem, value2[i]);
                                }
                               
                            }
                            Console.WriteLine("{0}. I resign", arrayofindexes.Length + 1);
                            Console.Write("Select: ");
                            char nb2 = Console.ReadKey().KeyChar;
                            int key2 = CharUnicodeInfo.GetDecimalDigitValue(nb2);


                            if (key2 <= arrayofindexes.Length)
                            {
                                for (int i = 0; i < arrayofindexes.Length; i++)
                                {
                                    if (key2 == i)
                                    {
                                        war.Sell(value[i], i);
                                    }

                                }
                                toReturn2 = true;

                            }
                            else if (key2 == arrayofindexes.Length + 1)
                            {
                                
                                toReturn2 = true;
                                break;
                            }

                            else
                            {

                                
                                Console.WriteLine("Unknown option!");
                                toReturn2 = true;
                              
                            }
                        } while (toReturn2 != false);

                    }

                    else if (key == arrayofindexes.Length + 2)
                    {
                        Console.WriteLine("Bye");

                        Program.DefaultMenu(user, war, data, mythings, Mermaid, Dragon);
                    }
                    else
                    {

                        Console.WriteLine("Unknown option!");
                       
                    }
                } 
            }

            Program.DefaultMenu(user,war,data,mythings,Mermaid,Dragon);
            
}
      public void GiveBeer_Sorcerer(Sorcerer user, Data data)
        {
            Warrior war = new Warrior("N",0,0,0,0,0);
            char decision;
            do
            {
                Console.Clear();
                Random rnd3 = new Random();
                int howmuch = rnd3.Next(50,150);
                Console.WriteLine("-Tough day, huh? Maybe wanna have some beer? Only {0} *Type \"y\" for yes or \"n\" for no*",howmuch);
                decision = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (decision == 'y')
                {
                    Console.Clear();
                    user.RemoveMoney(howmuch,user, war);
                    Console.WriteLine("Few hours later");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    actual_hp = basic_hp;
                    Random rnd = new Random();
                    int rand = rnd.Next(101);
                    if (rand <= 15)
                    {
                        Random rnd2 = new Random();
                        int cash = rnd2.Next(0, money_player);
                        user.RemoveMoney(cash, user, war);
                        Console.WriteLine("Unfortunatelly you offended someone in the tavern and lost {0} money",cash);
                        
                    }
                    else
                    {
                        Console.WriteLine("-See you again!");
                    }
                    break;

                }
                else if (decision == 'n')
                {
                    Console.WriteLine("Okay, See you then...");
                    Console.Clear();

                    break;
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                }


            } while (decision != 'y' && decision != 'n');
        }
        
      
      
        public void GiveBeer_Warrior(Enemy Human, Warrior user, Data data)
        {
            Sorcerer sor = new Sorcerer("N", 0, 0, 0, 0);
            char decision;
            do
            {
                Console.Clear();
                Random rnd3 = new Random();
                int howmuch = rnd3.Next(50, 150);
                Console.WriteLine("-Tough day, huh? Maybe wanna have some beer? Only {0} *Type \"y\" for yes or \"n\" for no*", howmuch);
                decision = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (decision == 'y')
                {
                    Console.Clear();
                    user.RemoveMoney(howmuch, sor, user);
                    Console.WriteLine("Few hours later");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    actual_hp = basic_hp;
                    Random rnd = new Random();
                    int rand = rnd.Next(101);
                    if(rand<=25)
                    {

                        
                         Console.WriteLine("*BURP* HO-HOW D-DID YOU CALL MY MOTHER?");
                        user.Fight(Human,data);                    }
                    else
                    {
                        Console.WriteLine("-See you again!");
                    }
                    break;

                }
                else if (decision == 'n')
                {
                    Console.WriteLine("Okay, See you then...");
                    Console.Clear();

                    break;
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                }


            } while (decision != 'y' && decision != 'n');

            
        }

    }
}
