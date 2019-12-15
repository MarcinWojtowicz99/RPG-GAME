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
        bool gamecomplete;
        public bool GameComplete { get { return gamecomplete; } set { gamecomplete = value; } }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        int basic_hp;
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
        public void UpdateEnemyKilled(Enemy enemy,Data data)
        {
            if(enemy.Enemy_Name=="Dragon")
            {
                data.gamedata[7]= "Dragons_killed: " + Convert.ToString(enemy.EnemyKilled);
            }
            else if(enemy.Enemy_Name == "Mermaid")
            {
                data.gamedata[8] = "Mermaids_killed: " + Convert.ToString(enemy.EnemyKilled);
            }
            else if (enemy.Enemy_Name == "Human")
            {
                data.gamedata[9] = "People_killed: " + Convert.ToString(enemy.EnemyKilled);
            }
            else
            {
                data.gamedata[10] = "Unknowns_killed: " + Convert.ToString(enemy.EnemyKilled);
            }
        }
        int[,] equipment;
    
        public void UpdateHealth(Character user,Data data)
        {
            data.gamedata[4]="Health_status: "+Convert.ToString(user.Actual_hp);
        }
        int maxequipment;

        public string Name { get { return name; } }
        int basic_damage;
        public int Basic_damage { get{ return basic_damage; }set { basic_damage = value; } }
        int startmoney;
        public Character(string name, int basic_hp, int maxequipment, int basic_damage, int startmoney)
        {
            this.startmoney = startmoney;
            score = 0;
            actual_hp = basic_hp;
            Actual_hp = actual_hp;
            money_player= startmoney ;
            money_player = startmoney;
            gamecomplete = false;
            this.name = name;
            this.basic_hp = basic_hp;
            Basic_hp = basic_hp;
            this.basic_damage = basic_damage;
            Basic_damage = basic_damage;
              this.maxequipment = maxequipment;
            equipment = new int[maxequipment, 2];
            for (int i = 0; i < maxequipment; i++)
            {
                equipment[i, 0] = 0;
                equipment[i, 1] = 0;

            }
            Equip= 0;

        }
        public bool UpdateMoney(Character user, Data data)
        {
            data.gamedata[2] = "AccountStatus: " +user.Money_player;
            return true;
        }
        public int Pay(int value, int indexnb, Item mythings,Character user, int shopkeeperCash, Data data)
        {
           if(user.Money_player-value>=0)
            {
                user.RemoveMoney(value,user,data);
                user.UpdateMoney(user, data);
                AddToEquipment(indexnb, mythings, user,data);
                shopkeeperCash += value;
            }
            else
            {
                Console.WriteLine("You don't have enough money :c");
                Console.ReadKey();
            }
            

            return shopkeeperCash;

        }
        public int Sell(int value, int indexEQ, Character user, int shopkeeperCash, Data data)
        {
            if(shopkeeperCash-value>=0)
            {
                user.AddMoney(value,user,data);
                user.UpdateMoney(user, data);
                user.RemoveFromEquipment(indexEQ, user, data);
                shopkeeperCash -= value;
            }
            else
            {
                Console.WriteLine("I don't have enough money :c");
                Console.ReadKey();
            }
            return shopkeeperCash;
        }
        public int AddMoney(int cash, Character user, Data data)
        {
            user.Money_player += cash;
            user.UpdateMoney(user, data);
            data.AutoSaveGame(data);
            return money_player += cash;
        }
        public int RemoveMoney(int cash, Character user, Data data)
        {
            if(user.Money_player - cash>=0)
            {
                user.Money_player -= cash;
                user.UpdateMoney(user, data);
            }
            else
            {
                user.Money_player = 0;
                user.UpdateMoney(user, data);
            }
            data.AutoSaveGame(data);
            return money_player -= cash;
        }
        int equip;
        public int Equip
        {
            get { return equip; }
            set { equip = value; }
        }
        public int[,] Equipment { get { return equipment; }set { equipment = value; } }
        public double Attack(Item mythings, Character user)
        {
            double gen;
            if (user.equip != 0)
            {
                Random rnd = new Random();
                int ba = mythings.unit[user.equip].damageorhealvalueafteruse;
                double a = Convert.ToDouble(ba);
                double random = rnd.Next(5, 15);
                double b = Convert.ToDouble(random / 10);
                gen = b * a;
                gen = Math.Abs(gen);
            }
            else
            {
                gen = 0.0;
            }

            return Convert.ToDouble(user.basic_damage)+ Convert.ToDouble(user.basic_damage) + gen;

        }
        public void Heal(int index, Item mythings, Data data, Character user, Warrior user_warrior)
        {
            user.Actual_hp += mythings.unit[index].damageorhealvalueafteruse;
            if (user.Actual_hp > user.Basic_hp)
            {
                user.Actual_hp= user.Basic_hp;
                if(data.Gamedata[1]==Program.Class['w'])
                {
                    user.Actual_hp = 0;
                    user.Actual_hp = user.Basic_hp;
                    user.Actual_hp = user_warrior.BoostMe();
                }
            }
        }
        public void ViewEquipment(Item mythings, Character user, Sorcerer user_sorcerer, Warrior user_warrior, Data data, Enemy Mermaid, Enemy Dragon, Enemy Human, bool duringfight)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Type number of equipment to equip or use *If you want to quit, hit ESC* ");
                Console.WriteLine("Your equipment: ");
                for (int i = 0; i < equipment.Length / 2; i++)
                {

                    if (user.Equipment[i, 0] != 0)
                    {
                        Console.Write("{0}. ", i + 1);
                        int index = equipment[i, 0];
                        mythings.Item_Name(index, mythings);
                        if (user.Equipment[i, 1] > 0)
                            Console.WriteLine(" [Uses: {0}]", equipment[i, 1]);
                        else
                            Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("{0}. [EMPTY]", i + 1);
                    }
                }
                    var key1 = Console.ReadKey();
                    char key = key1.KeyChar;
                    if (key1.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    int nb = CharUnicodeInfo.GetDecimalDigitValue(key) - 1;
                    Console.WriteLine();
                    try
                    {
                        int index = equipment[nb, 0];

                        if (index == 0)
                        {
                            Console.WriteLine("Item does not exist");
                        }
                        else if (mythings.Type(index, mythings) == "Sword")
                        {
                            if (data.gamedata[1] != Program.Class['s']&&user_warrior.Equip!=index)
                            {
                            user_warrior.Equip_Character(index, mythings, user_warrior, data, user);
                            user.Equipment[nb, 1] -= 1;
                        }
                            else
                            {
                                Console.WriteLine("You cannot use this item!");
                                System.Threading.Thread.Sleep(4000);

                            }
                        }
                        else if (mythings.Type(index, mythings) == "Potion")
                        {
                            user.Heal(index, mythings, data, user, user_warrior);
                            user.Equipment[nb, 1] -= 1;
                            if (user.Equipment[nb, 1] == 0)
                            {
                                user.RemoveFromEquipment(nb, user, data);
                            }
                        }
                        else if (mythings.Type(index, mythings) == "Rubbish")
                        {
                            Console.WriteLine("You cannot use this item!");
                            mythings.DoQuack(index, mythings);
                            System.Threading.Thread.Sleep(4000);
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Wrong key!");

                        System.Threading.Thread.Sleep(1000);
                    }
            } while (true);
            if (duringfight == false)
            {
                Program.DefaultMenu(user, user_sorcerer, user_warrior, data, mythings, Mermaid, Dragon, Human);
            }
            

        }
        public void UpdateScore(Character user,Data data)
        {
            data.gamedata[3] = "Score: "+Convert.ToString(user.Score);
        }
        public static bool UnknownOption()
        {
            Console.WriteLine("Unknown option!");
            System.Threading.Thread.Sleep(4000);
            return false;
        }
        public void FightWon(Enemy enemy, Data data, Character user, Item mythings, Sorcerer sorcerer, Warrior warrior, Enemy mermaid, Enemy dragon, Enemy Human)
        {
            Console.Clear();
            Console.WriteLine("You've slain an enemy!");
            System.Threading.Thread.Sleep(4000);
            user.score += enemy.Enemyscore;
            Console.WriteLine("+{0} points", user.Score);
            enemy.Drop_Item(user, mythings, data);
            enemy.EnemyKilled += 1;
            user.UpdateEnemyKilled(enemy, data);
            enemy.RestoreHealth(enemy);
            user.UpdateHealth(user, data);
            user.UpdateScore(user, data);
            System.Threading.Thread.Sleep(7000);
            if (user.GameComplete == true)
            {
                user.GameComplete = false;
                Program.ending_screen(user, data, mythings);
            }
            Program.DefaultMenu(user, sorcerer, warrior, data, mythings, mermaid, dragon, Human);
            
        }
        public bool GameOverScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("YOU ARE DEAD!");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("YOUR SCORE WAS: " + score);
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
                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);//start new process
                                                                                                  //Close the current process
                    Environment.Exit(0);
                    Data data = null;
                    Program.OpenMainMenu(data);
                    return true;

                }
                else
                {
                    Console.Clear();
                    UnknownOption();
                }
            }
            return true;
        }
        public bool Fight(Enemy enemy, Data data, Character user, Item mythings, Sorcerer sorcerer, Warrior warrior, Enemy mermaid, Enemy dragon, Enemy Human)
        {
            Console.Clear();
            data.AutoSaveGame(data);
            Console.WriteLine("Let's start the fight!");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            bool firedamageenabled = false;
            bool Exit = false;
            bool toBreak = false ;
            int counter = 0;
            bool sorcererResurection = false;
            bool Resurection_used = false;
            do
            {
                if (enemy.Enemy_Actual_HP <= 0)
                {
                    user.FightWon(enemy,data,user,mythings,sorcerer,warrior,mermaid,dragon,Human);
                    return true;
                }

                else
                {
                    
                    if (user.Actual_hp > 0 || sorcererResurection == true)
                    {
                        if (user.Actual_hp <= 0)
                        {
                            user.Actual_hp = Basic_hp;
                            sorcererResurection = false;
                        }

                        if (data.gamedata[1] != Program.Class['s'])
                        {
                            warrior.ContinueFight(enemy, data, user, mythings, sorcerer, warrior, mermaid, dragon, Human,toBreak);
                            enemy.Attack_Character(enemy,user);
                        }
                        else
                        {
                            bool used = Resurection_used;
                            sorcerer.ContinueFight(enemy, data, user, mythings, sorcerer, warrior, mermaid, dragon, Human, firedamageenabled,used,out Resurection_used,counter, toBreak, out sorcererResurection, out Exit);
                            if(Exit==true)
                            {
                                Program.DefaultMenu(user, sorcerer, warrior, data, mythings, mermaid, dragon, Human);
                                return true;
                            }
                            else
                            {
                                enemy.Attack_Character(enemy, user);
                            }
                            

                        }
                        
                    }
                        else   {  user.GameOverScreen(); }

                    }
            } while (true);
          
        }
        int nb;
        public bool FindInEquipment(Item mythings, Character user, string itemname)
        {
            bool isitem = false;
            for (int i = 0; i < user.Equipment.Length / 2; i++)
            {
                if (mythings.unit[user.Equipment[i, 0]].nameofitem == itemname)
                {

                    isitem = true;
                }
            }
            return isitem;
        }
        public bool AddToEquipment(int index, Item mythings, Character user, Data data)
        {
            bool a = false;
            for (int i = 0; i < user.Equipment.Length / 2; i++)
            {
                if (mythings.unit[user.Equipment[i, 0]].nameofitem == mythings.unit[index].nameofitem)
                {
                    user.Equipment[i, 1] += 1;
                    a = true;
                    data.gamedata[5] = "Items: ";
                    for (int j = 0; j < user.Equipment.Length / 2; j++)
                    {
                        data.gamedata[5] += user.Equipment[j, 0] + "-";
                        data.gamedata[5] += user.Equipment[j, 1] + ";";
                    }
                    return true;
                }

            }
            if (a == false)
            {
                for (int i = 0; i < user.Equipment.Length / 2; i++)
                {
                    if (user.Equipment[i, 0] == 0)
                    {
                        user.Equipment[i, 0] = index;
                        nb = mythings.NB_Of_Uses(index, mythings);
                        user.Equipment[i, 1] = nb;
                        data.gamedata[5] = "Items: ";
                        for (int j = 0; j < user.Equipment.Length / 2; j++)
                        {
                            data.gamedata[5] += user.Equipment[j, 0] + "-";
                            data.gamedata[5] += user.Equipment[j, 1] + ";";
                        }
                       
                        return true;
                    }
                    else if (i == (user.Equipment.Length / 2) - 1)
                    {
                        Console.WriteLine("You don't have enough space!");
                        System.Threading.Thread.Sleep(4000);
                        return false;
                    }
                }
               
            }
            return true;
            }
          
        public void RemoveFromEquipment(int indexEQ,Character user, Data data)
        {
                        user.Equipment[indexEQ, 0] = 0;
                        user.Equipment[indexEQ, 1] = 0;
            data.gamedata[5] = "Items: ";
            for (int j = 0; j < user.Equipment.Length / 2; j++)
            {
                data.gamedata[5] += user.Equipment[j, 0] + "-";
                data.gamedata[5] += user.Equipment[j, 1] + ";";
            }
        }

        public void Walk(Character user,Data data, Sorcerer sorc, Warrior war, Item mythings,Enemy Mermaid, Enemy Dragon, Enemy Human)
        {
            bool toReturn = true;
            do
            {
                Scenario.WhereToGoMenu(user);
                var a = Console.ReadKey();
                Console.WriteLine();
                NPC shopkeeper = new NPC(3, mythings,user);
                if (a.Key == ConsoleKey.Escape)
                {
                    Program.DefaultMenu(user, sorc, war, data, mythings, Mermaid, Dragon, Human);
                    break;
                }
                char choice =a.KeyChar;
                switch (choice)
                {
                    case '1':
                        if (data.gamedata[1]==Program.Class['w'])
                        {
                            war.UnEquip(war, mythings, data, user);
                            shopkeeper.GiveBeer_Warrior(user,war,data, mythings,Mermaid, Dragon,Human);
                        }
                        else
                        {
                            shopkeeper.GiveBeer_Sorcerer(user,sorc, mythings,data, Mermaid, Dragon,Human);
                        }
                        
                        break;
                    case '2':
                        
                            user.Fight(Mermaid,data,user,mythings,sorc,war,Mermaid,Dragon,Human);
                        
                        
                        break;
                    case '3':
                        shopkeeper.ShowItems(user,sorc,war,mythings,data,Mermaid,Dragon,shopkeeper,Human);
                        break;
                    case '4':
                        Program.PlotManager(data, user, Mermaid,sorc,war,mythings,Mermaid,Dragon,Human);
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
        
        
    

