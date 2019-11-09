using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Character
    {
        public string name;
        static string nick;
         int score;
        static int money_player;
        public static int moneyPlayer { get { return money_player; } }
       public static int basic_hp;
        public static int Basic_hp { get { return basic_hp; } }
        public int actual_hp;
        public int Actual_hp { get { return actual_hp; } }
        int[,] equipment;
       
        int maxequipment;
     
        static int enemy_score;
        public static int SCORE { get { return enemy_score; } }

        public string Name{get {return name;} }
        int basic_damage;
        public Character (string name, int basic_hp, int maxequipment,int basic_damage)
        {
            this.name = name;
            this.basic_damage=basic_damage;
            nick = name;
            this.maxequipment = maxequipment;
            equipment = new int[maxequipment, 2];
            for(int i=0; i<maxequipment;i++)
            {
                equipment[i,0] = 0;
                equipment[i, 1] = 0;

            }

        }
        public void Pay(int value, int indexnb, Item mythings)
        {
            money_player -= value;
            AddToEquipment(indexnb, mythings);

        }
        public void Sell(int value, int indexEQ)
        {
            money_player += value;
            RemoveFromEquipment(indexEQ);
        }
       public void AddMoney(int cash)
        {
            money_player += cash;
        }
        public static void RemoveMoney(int cash)
        {
            money_player -= cash;
        }
        public void Use(int nbofuse, int indexEQ)
        {
            if ( equipment[indexEQ,1]>0)
            {
                if (equipment[indexEQ,0] < 20)
                {

                    equipment[indexEQ,1] -= 1;
                    if(equipment[indexEQ, 1]==0)
                    {
                        RemoveFromEquipment(indexEQ);
                    }
                    
                }
            }
        }
        
        public void checkyourequipment()
        {

        }
        
      
        public int enemy_actualhp;
        public int Enemy_Actual_HP { get { return enemy_actualhp; } }
        int equip;
        public bool Equip_Character(int index)
        {
            equip = index;
            return true;
        }
        public bool UnEquip()
        {
            equip = 0;
            return false;
        }
        public int Attack(Item mythings)
        {
            int gen;
            if(equip!=0)
            {
                gen = mythings.Generate_Value(equip);
            }
            else
            {
                gen = 0;
            }
            
            return basic_damage+gen;

        }
        public void Fight(Enemy enemy,Data data)
        {
            if(actual_hp>0)
            {
                int enemy_hp = enemy.WhatsMyActualHP();
               // int attack = Attack();
              //  Enemy.TakeDamage(attack);
                if (enemy_hp < 0)
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
                        Program.OpenMainMenu(data);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Unknown option!");
                    }
                } while (actual_hp > 0);
            }
         }
        int nb;
        public void AddToEquipment(int index, Item mythings)
        {
            for(int i=0; i<equipment.Length;i++)
            {
                if(equipment[i,0]!=0)
                equipment[i,0] = index;
                nb = mythings.NB_Of_Uses(index);
                equipment[i, 1] = nb;
            }
            
        }
        
        public void RemoveFromEquipment(int index)
        {
            for(int i=0; i<equipment.Length;i++)
            {
                if(equipment[i,0]==index)
                {
                    equipment[i,0] = 0;
                    equipment[i, 1] = 0;
                    break;
                }
            }
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
        public void ViewEquipment()
        {

        }
        
        
    }
}
