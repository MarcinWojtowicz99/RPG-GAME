using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using System.Text.RegularExpressions;

namespace RPG_GAME
{
    class Character
    {
        public string name;
        static string nick;
        int score;
        static int money_player;
        public int Money_player 
        { 
            get { return startmoney; }
            set { startmoney = value; } 
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public static int basic_hp;
        public static int Basic_hp { get { return basic_hp; } }
        public int actual_hp;

        public int Actual_hp
        {
            get { return actual_hp; }
            set { actual_hp = value; }
        }
        int[,] equipment;
    
        public virtual void UpdateHealth(Data data)
        {
            data.AddtoData(Convert.ToString(actual_hp), 3);
        }
        int maxequipment;

        static int enemy_score;

        public string Name { get { return name; } }
        int basic_damage;
        int startmoney;
        public Character(string name, int basic_hp, int maxequipment, int basic_damage, int startmoney)
        {
            this.startmoney = startmoney;
            score = 0;
            actual_hp = basic_hp;
            money_player= startmoney ;
            money_player = startmoney;
            this.name = name;
            this.basic_damage = basic_damage;
            nick = name;
            this.maxequipment = maxequipment;
            equipment = new int[maxequipment, 2];
            for (int i = 0; i < maxequipment; i++)
            {
                equipment[i, 0] = 0;
                equipment[i, 1] = 0;

            }
            equip = 0;

        }
        public int[,] Equipment()
        {
            return equipment;
        }
        public int Pay(int value, int indexnb, Item mythings,Sorcerer user1, Warrior user2)
        {
            money_player -= value;
            AddToEquipment(indexnb, mythings);
            return user1.Money_player;

        }
        public int Sell(int value, int indexEQ)
        {
            money_player += value;
            RemoveFromEquipment(indexEQ);
            return money_player;
        }
        public int AddMoney(int cash, Sorcerer user1, Warrior user2)
        {
           return money_player += cash;
        }
        public int RemoveMoney(int cash, Sorcerer user1, Warrior user2)
        {
            return money_player -= cash;
        }
        public void Use(int indexEQ)
        {
            if (equipment[indexEQ, 1] > 0)
            {
                if (equipment[indexEQ, 0] < 20)
                {

                    equipment[indexEQ, 1] -= 1;
                    if (equipment[indexEQ, 1] == 0)
                    {

                        RemoveFromEquipment(indexEQ);
                        if (equipment[indexEQ, 0] == equip)
                        {
                            UnEquip();
                        }
                    }

                }
            }
        }


        // public int weapondamage;


        int equip;
        public bool Equip_Character(int index, Item mythings)
        {
            equip = index;
            //weapondamage= mythings.unit[index].damageorhealvalueafteruse;
            return true;
        }
        public bool UnEquip()
        {
            equip = 0;
            return false;
        }
        public double Attack(Item mythings)
        {
            double gen;
            if (equip != 0)
            {
                Random rnd = new Random();
                double a = Convert.ToDouble(mythings.unit[equip].damageorhealvalueafteruse);
                double random = rnd.Next(5, 15);
                double b = Convert.ToDouble(random / 10);
                gen = b * a;
            }
            else
            {
                gen = 0.0;
            }

            return Convert.ToDouble(basic_damage) + gen;

        }
        public void Heal(int index, Item mythings, Data data)
        {

            actual_hp += mythings.unit[index].damageorhealvalueafteruse;
            if (actual_hp > basic_hp)
            {
                actual_hp = basic_hp;
            }
            data.AddtoData(Convert.ToString(actual_hp), 3);
        }

        public void Fight(Enemy enemy, Data data)
        {
            if (actual_hp > 0)
            {
                int enemy_hp = enemy.Enemy_Actual_HP;
                // int attack = Attack();
                //  Enemy.TakeDamage(attack);
                if (enemy_hp < 0)
                {
                    score += enemy.Enemyscore; ;
                    data.AddtoData(Convert.ToString(actual_hp), 3);
                    data.AddtoData(Convert.ToString(score), 4);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("YOU ARE DEAD!");
                Console.WriteLine("YOUR SCORE WAS: " + score);
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
            for (int i = 0; i < equipment.Length; i++)
            {
                if (equipment[i, 0] == 0)
                {
                    equipment[i, 0] = index;
                    nb = mythings.NB_Of_Uses(index, mythings);
                    equipment[i, 1] = nb;
                    break;
                }
                    
                
            }

        }


        public void RemoveFromEquipment(int index)
        {
            for (int i = 0; i < equipment.Length; i++)
            {
                if (equipment[i, 0] == index)
                {
                    equipment[i, 0] = 0;
                    equipment[i, 1] = 0;
                    break;
                }
            }
        }



        public void Walk(Data data, Sorcerer sorc, Warrior war, Item mythings,Enemy Mermaid, Enemy Dragon)
        {
            bool toReturn = true;
            do
            {
                Console.Clear();
                if (data.gamedata[1] == "Class Warrior")
                {
                    Console.WriteLine("Money: "+war.Money_player);
                    Console.WriteLine("HP: "+war.Actual_hp);
                    Console.WriteLine("Score: " + war.Score);
                }
                else
                {
                    Console.WriteLine("Money: " + sorc.Money_player);
                    Console.WriteLine("HP: " + sorc.Actual_hp);
                    Console.WriteLine("Score: " + sorc.Score);
                }
                Console.WriteLine("Where would you like to go?");


                Console.WriteLine("1. Tavern" );
                Console.WriteLine("2. Forest");
                Console.WriteLine("3. Shop");
                Console.WriteLine("4. Queen");
                Console.WriteLine("5. Back");
                Console.Write("Select: ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();


                switch (choice)
                {
                    case '1':
                        Item mythings2 = new Item(0);
                        NPC inkeeper = new NPC(1,mythings2);
                        
                        if (data.gamedata[1]=="Class Warrior")
                        {
                            Enemy Human = new Enemy(basic_hp, 100, 1);
                            Human.Stack(new Attack("Drunk punch", 100, "I'll show you my real power!"));
                            inkeeper.GiveBeer_Warrior(Human,war,data);
                        }
                        else
                        {
                            inkeeper.GiveBeer_Sorcerer(sorc,data);
                        }
                        
                        break;
                    case '2':
                        
                        break;
                    case '3':
                        NPC shopkeeper = new NPC(3,mythings);
                        shopkeeper.ShowItems(sorc,war,mythings,data,Mermaid,Dragon);
                        break;
                    case '4':
                        Program.doPlot(data);
                        break;
                    case '5':
                        Program.DefaultMenu(sorc,war,data,mythings,Mermaid,Dragon);
                        break;
                    default:
                        toReturn = false;
                        Console.WriteLine("Unknown option!");
                        break;
                }

            }
            while (toReturn != true);


        }

    }
}
        
        
    

