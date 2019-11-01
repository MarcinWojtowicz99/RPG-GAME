using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class NPC
    {
       static int money;
      
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
        public static void GiveBeer()
        {
            
            do
            {
                Console.Clear();
                Console.WriteLine("-Tough day, huh? Maybe wanna have some beer? *Type \"y\" for yes or \"n\" for no*");
                char decicion = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (decicion == 'y')
                {
                    actual_hp = basic_hp;
                    Random rnd = new Random();
                    int rand = rnd.Next(101);
                    if(rand<=25)
                    {
                        Character.Fight();
                    }
                    break;

                }
                else if (decicion == 'n')
                {
                    Console.WriteLine("Okay, See you then...");
                    Console.Clear();

                    break;
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                }


            } while (decicion != 'y' && decicion != 'n');

            
        }

    }
}
