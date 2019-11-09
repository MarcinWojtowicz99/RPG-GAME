using System;
using System.Collections.Generic;
using System.Text;

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
        public static int Actual{get{return actual_hp; } }
        public static int Basic{get{return basic_hp; } }
        int counter;
        public int Nb_Of_Items { get { return counter; } }
        int[] arrayofindexes;
        public NPC(int nbofequipment, int[] arrayofindexes)
        {
            Random rnd = new Random();
            money = rnd.Next(100,1000);
            
                for(int i=0; i<nbofequipment;i++)
            {
                Random rnd2 = new Random();
                arrayofindexes[i] = rnd.Next(1,counter);
            }

        }
        public static void ShowItems()
        {
            Console.WriteLine("Shopkeeper account value: " + money);


        }
      public static void GiveBeer_Sorcerer()
        {
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
                    Character.RemoveMoney(howmuch);
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
                        Character.RemoveMoney(cash);
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
        
        Item mythings;
      
        public void GiveBeer_Warrior(Enemy Human, Character user, Data data)
        {
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
                    Character.RemoveMoney(howmuch);
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
