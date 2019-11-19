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
                user.Money_player -= value;
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
                user.Money_player += value;
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
                if(data.Gamedata[1]=="Class Warrior")
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
                Console.WriteLine("Type number of equipment to equip or use [If you want to quit, hit ESC] ");
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
                            if (data.gamedata[1] != "Class Sorcerer"&&user_warrior.Equip!=index)
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
        public void Fight(Enemy enemy, Data data, Character user, Item mythings, Sorcerer sorcerer, Warrior warrior, Enemy mermaid, Enemy dragon, Enemy Human)
        {
            Console.Clear();
            data.AutoSaveGame(data);
            Console.WriteLine("Let's start the fight!");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            int strength;
            double finalattack;
            bool firedamageenabled = false;
            bool toBreak = false ;
            int counter = 0;
            bool sorcererResurection = false;
            bool Resurection_used = false;
            do
            {
                if (enemy.Enemy_Actual_HP <= 0)
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
                    if(sorcerer.Activeskill==true)
                    {
                        sorcerer.Basic_damage /= 2;
                    }
                    break;

                }

                else
                {
                    if (user.Actual_hp > 0 || sorcererResurection == true)
                    {
                        if(user.Actual_hp<=0)
                        {
                            user.Actual_hp = Basic_hp;
                            sorcererResurection = false;
                        }
                        
                        if (data.gamedata[1] != "Class Sorcerer")
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Enemy HP: " + enemy.Enemy_Actual_HP);
                                Console.WriteLine("Your HP: " + user.Actual_hp);
                                Console.WriteLine("What are you going to do?");
                                Console.WriteLine("1. Use quick attack");
                                Console.WriteLine("2. Use normal attack");
                                Console.WriteLine("3. Use powerful attack");
                                Console.WriteLine("4. Manage Equipment");
                                Console.Write("Select: ");
                                char choice = Console.ReadKey().KeyChar;

                                Console.WriteLine();
                                switch (choice)
                                {
                                    case '1':
                                        strength = 1;
                                     toBreak = true;
                                        finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength);
                                        enemy.Enemy_Actual_HP -= finalattack;
                                        if (finalattack != 0)
                                        {
                                            if(warrior.Equip!=0)
                                            {
                                                enemy.Enemy_Actual_HP += mythings.unit[warrior.Equip].damageorhealvalueafteruse;
                                            }
                                            Console.Clear();
                                            Console.WriteLine("Your hit value: " + finalattack+ mythings.unit[warrior.Equip].damageorhealvalueafteruse);
                                            System.Threading.Thread.Sleep(2000);
                                        }

                                        if (warrior.Equip != 0)
                                        {
                                            
                                            warrior.NbOfUseEQ -= 1;
                                            if (warrior.NbOfUseEQ <= 0)
                                            {
                                                Console.WriteLine("You broke your sword!");
                                                System.Threading.Thread.Sleep(2000);
                                                warrior.UnEquip(warrior,mythings, data, user);
                                            }
                                        }
                                        break;
                                    case '2':
                                        strength = 2;
                                    toBreak = true;
                                        finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength);
                                        enemy.Enemy_Actual_HP -= finalattack;
                                        if (finalattack != 0)
                                        {
                                            if (warrior.Equip != 0)
                                            {
                                                enemy.Enemy_Actual_HP += mythings.unit[warrior.Equip].damageorhealvalueafteruse;
                                            }
                                            Console.Clear();
                                            Console.WriteLine("Your hit value: " + finalattack + mythings.unit[warrior.Equip].damageorhealvalueafteruse);
                                            System.Threading.Thread.Sleep(2000);
                                        }
                                        if (warrior.Equip != 0)
                                        {
                                            
                                            warrior.NbOfUseEQ -= 1;
                                            if (warrior.NbOfUseEQ <= 0)
                                            {
                                                Console.WriteLine("You broke your sword!");
                                                System.Threading.Thread.Sleep(2000);
                                                warrior.UnEquip(warrior, mythings, data, user);
                                            }
                                        }
                                        break;
                                    case '3':
                                        strength = 3;
                                      toBreak = true;
                                        finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength);
                                        enemy.Enemy_Actual_HP -= finalattack;
                                        if (finalattack != 0)
                                        {
                                            if (warrior.Equip != 0)
                                            {
                                                enemy.Enemy_Actual_HP += mythings.unit[warrior.Equip].damageorhealvalueafteruse;
                                            }
                                            Console.Clear();
                                            Console.WriteLine("Your hit value: " + finalattack + mythings.unit[warrior.Equip].damageorhealvalueafteruse);
                                            System.Threading.Thread.Sleep(2000);
                                        }
                                        if (warrior.Equip != 0)
                                        {
                                            
                                            warrior.NbOfUseEQ -= 1;
                                            if (warrior.NbOfUseEQ <= 0)
                                            {
                                                Console.WriteLine("You broke your sword!");
                                                System.Threading.Thread.Sleep(2000);
                                                warrior.UnEquip(warrior, mythings, data, user);
                                            }
                                            
                                        }

                                        break;
                                    case '4':
                                        user.ViewEquipment(mythings, user, sorcerer, warrior, data, mermaid, dragon, Human, true);
                                        toBreak = false;
                                        break;

                                    default:
                                        toBreak = false;
                                        Console.WriteLine("Unknown option!");
                                        System.Threading.Thread.Sleep(4000);
                                        break;
                                }
                            } while (toBreak != true);
                        }
                        else
                        {
                            do
                            {
                                if(firedamageenabled==true)
                                {
                                    if(counter>3)
                                    {
                                        counter = 0;
                                        firedamageenabled = false;
                                    }
                                    else
                                    {
                                        counter++;
                                    }
                                }
                                
                                Console.Clear();
                                Console.WriteLine("Enemy HP: " + enemy.Enemy_Actual_HP);
                                Console.WriteLine("Your HP: " + user.Actual_hp);
                                Console.WriteLine("What are you going to do?");
                                Console.WriteLine("1. Use Confusion");
                                Console.WriteLine("2. Use Fireball");
                                Console.WriteLine("3. Use teleportation");
                                Console.WriteLine("4. Manage equipment");
                                if(user.Score >= 3000 && Resurection_used == false)
                                {
                                    Console.WriteLine("5. Resurrection");
                                }
                                if(sorcerer.DemonsKilled > 2)
                                {
                                    Console.WriteLine("6. Little helper (Summons little demon)");
                                }
                                if(user.Score >= 6000)
                                {
                                    Console.WriteLine("7. Interfectorem draconis");
                                }
                               
                                Console.Write("Select: ");

                                char choice = Console.ReadKey().KeyChar;
                                double attack_sorc_value;
                                Console.WriteLine();

                                switch (choice)
                                {
                                    case '1':
                                        toBreak = true;
                                        attack_sorc_value = Sorcerer.Sorcerer_Spells(user.Attack(mythings, user), choice, enemy, sorcerer, mythings, data, firedamageenabled, mermaid, Human, dragon);
                                        enemy.Enemy_Actual_HP -= attack_sorc_value;
                                        System.Threading.Thread.Sleep(5000);
                                        break;
                                    case '2':
                                        toBreak = true;
                                        attack_sorc_value = Sorcerer.Sorcerer_Spells(user.Attack(mythings, user), choice, enemy, sorcerer, mythings, data, firedamageenabled, mermaid, Human, dragon);
                                        firedamageenabled = true;
                                        enemy.Enemy_Actual_HP -= attack_sorc_value;
                                        System.Threading.Thread.Sleep(5000);
                                        break;
                                    case '3':
                                        toBreak = true;
                                        enemy.RestoreHealth(enemy);
                                        user.UpdateHealth(user, data);
                                        Console.WriteLine("HOCUS POCUS!");
                                        System.Threading.Thread.Sleep(3000);
                                        Program.DefaultMenu(user, sorcerer, warrior, data, mythings, mermaid, dragon, Human);
                                        break;
                                    case '4':
                                        user.ViewEquipment(mythings, user, sorcerer, warrior, data, mermaid, dragon, Human, true);
                                        toBreak = false;
                                        break;
                                    case '5':
                                        if(user.Score>=3000&& Resurection_used == false)
                                        {
                                            sorcererResurection = true;
                                            Resurection_used = true;
                                            Console.Clear();
                                            Console.WriteLine("You used resurrection. You are temporary immortal");
                                            System.Threading.Thread.Sleep(1000);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            toBreak = false;
                                            Console.WriteLine("Unknown option!");
                                            System.Threading.Thread.Sleep(1000);
                                            break;
                                        }
                                        break;
                                    case '6':
                                        if(sorcerer.DemonsKilled>2)
                                        {
                                            Console.Clear();
                                            enemy.Enemy_Actual_HP -= enemy.Enemy_Actual_HP * 0.25;
                                            double shield= user.Score*0.1;
                                            user.Actual_hp += Convert.ToInt32(shield);
                                            Console.WriteLine("You earned demon armor +"+ Convert.ToInt32(shield));
                                            System.Threading.Thread.Sleep(1000);
                                            Console.Clear();
                                            enemy.Enemy_Actual_HP -= enemy.Enemy_Actual_HP * 0.02;
                                            Console.WriteLine("Demon attack: "+ enemy.Enemy_Actual_HP * 0.02);
                                            System.Threading.Thread.Sleep(1000);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            toBreak = false;
                                            Console.WriteLine("Unknown option!");
                                            System.Threading.Thread.Sleep(1000);
                                            break;
                                        }
                                        break;
                                    case '7':
                                        if (user.Score >= 6000)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.WriteLine("You used Interfectorem draconis");
                                            Console.WriteLine("Attack value: "+ enemy.Enemy_Actual_HP * 0.15);
                                            enemy.Enemy_Actual_HP -= enemy.Enemy_Actual_HP * 0.15;
                                            System.Threading.Thread.Sleep(1000);
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                        else
                                        {
                                            toBreak = false;
                                            Console.WriteLine("Unknown option!");
                                            System.Threading.Thread.Sleep(1000);
                                            break;
                                        }
                                        break;
                                    default:
                                        toBreak = false;
                                        Console.WriteLine("Unknown option!");
                                        System.Threading.Thread.Sleep(1000);
                                        break;
                                }
                                
                            } while (toBreak != true);

                        }
                        if (enemy.Enemy_Actual_HP >= 0)
                        {
                            Console.Clear();
                            Random random = new Random();
                            int rand = random.Next(100);
                            double value = 0;
                            if (rand <= 80)
                            {
                                value = enemy.Attack_Character(enemy, user);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Enemy missed!");
                                Console.ForegroundColor = ConsoleColor.White;

                            }
                            System.Threading.Thread.Sleep(4000);
                        }


                    }
                        else
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
                                Program.OpenMainMenu(data);
                                    break;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Unknown option!");
                                    System.Threading.Thread.Sleep(5000);
                                }



                            }
                        }

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
                Console.WriteLine("Where would you like to go? (Press [ESC] to go back)");
                Console.WriteLine("1. Royal tavern" );
                Console.WriteLine("2. Lake of truth");
                Console.WriteLine("3. Local shop");
                Console.WriteLine("4. Queen Ziva");
                Console.Write("Select: ");
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
                        if (data.gamedata[1]=="Class Warrior")
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
                        Program.doPlot(data, user, Mermaid,sorc,war,mythings,Mermaid,Dragon,Human);
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
        
        
    

