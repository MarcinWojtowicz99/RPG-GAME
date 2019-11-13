using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    class Warrior:Character
    {
        int hpboost;
       
        
        int[,] equipment;
        public Warrior(string name, int basic_hp, int maxequipment, int hpboost, int basic_damage, int startmoney) : base(name, basic_hp, maxequipment, basic_damage, startmoney)
        {
            this.hpboost = hpboost;
            equipment = Equipment;
            BoostMe();
        }
        public int BoostMe()
        {
            return actual_hp += hpboost;
        }
     
        public bool Equip_Character(int index, Item mythings, Warrior warrior, Data data)
        {

            warrior.Equip = index;
            data.gamedata[6] = "Equipped: "+index;
            return true;
        }
        public bool UnEquip(Warrior warrior,Data data)
        {
            warrior.Equip = 0;
            data.gamedata[6] = "Equipped: No";
            return false;
        }
        public static double WarriorAttacks(double attack,int Strength)
        {
            Random rnd = new Random();
            int random = rnd.Next(100);
            Random rnd2 = new Random();
            int critical_hit = rnd.Next(100);
            
            if (Strength == 1)
            {
                if (random<90)
                {
                    attack *= 0.85;
                    if (critical_hit <= 35)
                    {
                        Console.WriteLine("Critical hit!");
                        System.Threading.Thread.Sleep(2000);
                        attack *= 3.6;
                    }
                }
                else
                {
                    attack = 0;
                    Console.WriteLine("YOU MISSED!");
                    System.Threading.Thread.Sleep(2000);
                }
               
            }
            else if (Strength == 2)
            {
                if (random<65)
                {
                    attack *= 1;
                    if (critical_hit <= 5)
                    {
                        Console.WriteLine("Critical hit!");
                        System.Threading.Thread.Sleep(2000);
                        attack *= 3.6;
                    }
                }
                else
                {
                    attack = 0;
                    Console.WriteLine("YOU MISSED!");
                    System.Threading.Thread.Sleep(2000);
                }
                
            }
            else
            {

                
                if (random<25)
                {
                    attack *= 1.25;
                    if (critical_hit <= 3)
                    {
                        Console.WriteLine("Critical hit!");
                        System.Threading.Thread.Sleep(2000);
                        attack *= 3.6;
                    }
                }
                else
                {
                    attack = 0;
                    Console.WriteLine("YOU MISSED!");
                    System.Threading.Thread.Sleep(2000);
                }
            }
            return attack;

        }
        public void Teleporter(Item mythings, Character user, Sorcerer user_sorcerer, Warrior user_warrior, Data data, Enemy Mermaid, Enemy Dragon, Enemy Human)
        {
           while (true)
            {
                Console.WriteLine("Hello. I'm the teleporter! I'm here to send you to dragon's valley, but you need to pay me 20000 and show me your ring");
                Console.WriteLine("1. Okay");
                Console.WriteLine("2. No, thank you");
                char b = Console.ReadKey().KeyChar;
                switch (b)
                {
                    case '1':
                        if(user.Money_player-20000>=0)
                        {

                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money, come here when you'll be ready");
                        }
                        break;
                    case '2':
                        Program.DefaultMenu(user,user_sorcerer,user_warrior,data,mythings,Mermaid,Dragon,Human);
                        break;
                    default:
                        Console.WriteLine("Incorrect sign");
                        break;
                }
            }
            
        }
    }
}
