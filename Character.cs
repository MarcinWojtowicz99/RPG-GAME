using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Character
    {
        public string name;
        static string nick;
        static int score;
        int money_player;
       public static int basic_hp=500;
        public static int actual_hp;
       static int[] equipment;
        static int enemy_hp;
        public static int HP { get { return enemy_hp; } }
        static int enemy_score;
        public static int SCORE { get { return enemy_score; } }

        public string Name{get {return name;} }

        public Character (string name, int hp=500)
        {
            this.name = name;
            nick = name;

        }
        public void Pay(int value, int indexnb)
        {
            money_player -= value;
            AddToEquipment(indexnb);

        }
        public void Sell(int value, int indexEQ)
        {
            money_player += value;
            RemoveFromEquipment(indexEQ);
        }
        public void UseItem(int indexEQ, int indexnb)
        {
            Item.Use(indexnb);
            if(indexnb<20)
            {
                RemoveFromEquipment(indexEQ);
            }

            
        }
        public void checkyourequipment()
        {

        }
        public virtual static void Fight()
        {
            if(actual_hp>0)
            {
                if(enemy_hp<0)
                {
                    score += enemy_score;
                    
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("YOU ARE DEAD!");
                Console.WriteLine("YOUR SCORE WAS: "+score);
                Console.WriteLine("Press \"r\" to go back to main menu or \"ESC\" to quit");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                do
                {
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Bye!");
                        System.Threading.Thread.Sleep(5000);
                        Environment.Exit(0);//terminate console
                    }
                    else if (key == 'r')
                    {
                        MainMenu.OpenMainMenu();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Unknown option!");
                    }
                } while (actual_hp > 0);
            }
         }
        public static void AddToEquipment(int index)
        {
            for(int i=0; i<equipment.Length;i++)
            {
                if(equipment[i]!=null)
                equipment[i] = index;
            }
            
        }
        
        public static void RemoveFromEquipment(int index)
        {

        }

        
        public static string Nick()
        {
            return nick;
        }
        public void Walk()
        {
            Console.WriteLine("Where would you like to go?");
            Console.WriteLine("a) Tavern");
            Console.WriteLine("b) Forest");
            Console.WriteLine("c) Shop");
            Console.WriteLine("d) Queen");
        }
    }
}
