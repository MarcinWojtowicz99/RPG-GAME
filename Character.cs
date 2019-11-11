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
        public int actual_hp;

        public int Actual_hp
        {
            get { return actual_hp; }
            set { actual_hp = value; }
        }
        public int Basic_hp
        {
            get { return basic_hp; }
            set { basic_hp = value; }
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
              this.maxequipment = maxequipment;
            equipment = new int[maxequipment, 2];
            for (int i = 0; i < maxequipment; i++)
            {
                equipment[i, 0] = 0;
                equipment[i, 1] = 0;

            }
            equip = 0;

        }
        
        public int Pay(int value, int indexnb, Item mythings,Character user, int shopkeeperCash)
        {
           if(user.Money_player-value>=0)
            {
                user.Money_player -= value;
                AddToEquipment(indexnb, mythings, user);
                shopkeeperCash += value;
            }
            else
            {
                Console.WriteLine("You don't have enough money :c");
                Console.ReadKey();
            }
            

            return money_player;

        }
        public int Sell(int value, int indexEQ, Character user, int shopkeeperCash)
        {
            if(shopkeeperCash-value>=0)
            {
                user.Money_player += value;

                RemoveFromEquipment(indexEQ, user);
                shopkeeperCash -= value;
            }
            else
            {
                Console.WriteLine("I don't have enough money :c");
                Console.ReadKey();
            }
            return shopkeeperCash;
        }
        public int AddMoney(int cash, Character user)
        {
            user.Money_player += cash;
            return money_player += cash;
        }
        public int RemoveMoney(int cash, Character user)
        {
            if(user.Money_player - cash>=0)
            {
                user.Money_player -= cash;
            }
            else
            {
                user.Money_player = 0;
            }
            
            return money_player -= cash;
        }
        public void Use(int indexEQ, Character user, Sorcerer user_sorcerer,Warrior user_warrior, Item mythings, Data data)
        {
            if(data.gamedata[1]!="Class Sorcerer"&&mythings.unit[indexEQ].type=="Sword")
            {
                user_warrior.Equip_Character(indexEQ, mythings, user_warrior);
            }
            if (user.equipment[indexEQ, 1] > 0)
            {
                if (user.equipment[indexEQ, 0] < 20)
                {

                    user.equipment[indexEQ, 1] -= 1;
                    
                    if (user.equipment[indexEQ, 1] == 0)
                    {

                        RemoveFromEquipment(indexEQ, user);
                        if (user.equipment[indexEQ, 0] == user_warrior.Equip)
                        {
                            user_warrior.UnEquip(user_warrior);
                        }
                    }

                }
            }
        }
        int equip;
        public int Equip
        {
            get { return equip; }
            set { equip = value; }
        }
        public int[,] Equipment { get { return equipment; }set { equipment = value; } }
        // public int weapondamage;


       
        
        public double Attack(Item mythings, Character user)
        {
            double gen;
            if (user.equip != 0)
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

            return Convert.ToDouble(user.basic_damage)+ Convert.ToDouble(user.basic_damage) + gen;

        }
        public void Heal(int index, Item mythings, Data data, Character user)
        {
           
            user.Actual_hp += mythings.unit[index].damageorhealvalueafteruse;
            if (user.Actual_hp > user.Basic_hp)
            {
                user.Actual_hp= user.Basic_hp;
                
            }
            
            
        }
        public void ViewEquipment(Item mythings, Character user,Sorcerer user_sorcerer,Warrior user_warrior, Data data, Enemy Mermaid, Enemy Dragon)
        {
            bool endloop = true;
            do
            {
                Console.WriteLine("Your equipment: ");
                for (int i = 0; i < equipment.Length / 2; i++)
                {

                    if (equipment[i, 0] != 0)
                    {
                        Console.Write("{0}. ", i);
                        int index = equipment[i, 0];
                        mythings.Item_Name(index, mythings);
                        Console.WriteLine("Uses: " + equipment[i, 1]);
                    }
                }
                bool toReturn = true;
                do
                {

                    Console.WriteLine("Wanna use some equipment? y/n");


                    Console.Write("Select: ");
                    char choice = Console.ReadKey().KeyChar;
                    Console.WriteLine();


                    switch (choice)
                    {
                        case 'y':
                            Console.WriteLine("Type number of equipment to equip or use: ");
                            int nb = CharUnicodeInfo.GetDecimalDigitValue(Console.ReadKey().KeyChar);
                            int index = equipment[nb, 0];
                            if(data.gamedata[1]!="Class Sorcerer")
                            {
                                if (mythings.Type(index, mythings) == "Sword")
                                {
                                    user_warrior.Equip_Character(index, mythings, user_warrior);
                                }
                            }
                            
                            else
                            {
                                Warrior war2 = new Warrior("N", 0, 0, 0, 0, 0);
                                Heal(index, mythings, data, user);
                            }
                            user.Use(nb,user, user_sorcerer, user_warrior,mythings,data);
                            toReturn = false;
                            break;
                        case 'n':
                            Warrior war = new Warrior("FAKE", 0, 0, 0, 0, 0);
                            Program.DefaultMenu(user, user_sorcerer,user_warrior, data, mythings, Mermaid, Dragon);
                            Console.Clear();


                            break;


                        default:
                            toReturn = false;
                            endloop = false;
                            Console.WriteLine("Unknown option!");
                            break;
                    }
                } while (toReturn != true);

            } while (endloop != true);


        }
        public void Fight(Enemy enemy, Data data, Character user)
        {
            if (actual_hp > 0)
            {
                int enemy_hp = enemy.Enemy_Actual_HP;
                // int attack = Attack();
                //  Enemy.TakeDamage(attack);
                if (enemy_hp < 0)
                {
                    user.score += enemy.Enemyscore;
                    data.AddtoData(Convert.ToString(user.Actual_hp), 3);
                    data.AddtoData(Convert.ToString(user.Score), 4);
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
        public void AddToEquipment(int index, Item mythings, Character user)
        {
            for (int i = 0; i < user.Equipment.Length; i++)
            {
                if (user.Equipment[i, 0] == 0)
                {
                    user.Equipment[i, 0] = index;
                    nb = mythings.NB_Of_Uses(index, mythings);
                    user.Equipment[i, 1] = nb;
                    break;
                }
                    
                
            }

        }


        public void RemoveFromEquipment(int index,  Character user)
        {
            for (int i = 0; i < user.Equipment.Length; i++)
            {
                if (user.Equipment[i, 0] == index)
                {
                    user.Equipment[i, 0] = 0;
                    user.Equipment[i, 1] = 0;
                    break;
                }
            }
        }



        public void Walk(Character user,Data data, Sorcerer sorc, Warrior war, Item mythings,Enemy Mermaid, Enemy Dragon)
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
                NPC shopkeeper = new NPC(3, mythings);

                switch (choice)
                {
                    case '1':
                       
                        
                        
                        if (data.gamedata[1]=="Class Warrior")
                        {
                            Enemy Human = new Enemy(basic_hp, 100, 1);
                            Human.Stack(new Attack("Drunk punch", 100, "I'll show you my real power!"));
                            shopkeeper.GiveBeer_Warrior(Human,war,data);
                        }
                        else
                        {
                            shopkeeper.GiveBeer_Sorcerer(sorc,data);
                        }
                        
                        break;
                    case '2':
                        
                        break;
                    case '3':
                        shopkeeper.ShowItems(user,sorc,war,mythings,data,Mermaid,Dragon,shopkeeper);
                        break;
                    case '4':
                        Program.doPlot(data);
                        break;
                    case '5':
                        Program.DefaultMenu(user, sorc,war,data,mythings,Mermaid,Dragon);
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
        
        
    

