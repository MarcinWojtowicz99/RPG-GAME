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
        int startmoney;
        public Character(string name, int basic_hp, int maxequipment, int basic_damage, int startmoney)
        {
            this.startmoney = startmoney;
            score = 0;
            actual_hp = basic_hp;
            Actual_hp = actual_hp;
            money_player= startmoney ;
            money_player = startmoney;
            this.name = name;
            this.basic_hp = basic_hp;
            Basic_hp = basic_hp;
            this.basic_damage = basic_damage;
              this.maxequipment = maxequipment;
            equipment = new int[maxequipment, 2];
            for (int i = 0; i < maxequipment; i++)
            {
                equipment[i, 0] = 0;
                equipment[i, 1] = 0;

            }
            Equip = 0;

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
        public void ViewEquipment(Item mythings, Character user,Sorcerer user_sorcerer,Warrior user_warrior, Data data, Enemy Mermaid, Enemy Dragon, Enemy Human, bool duringfight)
        {
            bool endloop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Your equipment: ");
                for (int i = 0; i < equipment.Length / 2; i++)
                {

                    if (equipment[i, 0] != 0)
                    {
                        Console.Write("{0}. ", i+1);
                        int index = equipment[i, 0];
                        mythings.Item_Name(index, mythings);
                        Console.WriteLine("Uses: " + equipment[i, 1]);
                    }
                    else
                    {
                        Console.WriteLine("{0}. [EMPTY]",i+1);
                    }
                }
                bool toReturn = true;
                

                    Console.WriteLine("Wanna use some equipment? y/n");


                    Console.Write("Select: ");
                    char choice = Console.ReadKey().KeyChar;
                    Console.WriteLine();


                    switch (choice)
                    {
                        case 'y':
                        do
                        {
                            
                            Console.Write("Type number of equipment to equip or use [If you want to quit, hit ESC] ");
                            var key1 = Console.ReadKey();
                            char key = key1.KeyChar;
                            if(key1.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            int nb = CharUnicodeInfo.GetDecimalDigitValue(key)-1;
                            Console.WriteLine();
                            try
                            {
                                int index = equipment[nb, 0];
                            
                            if (index == 0)
                            {
                                Console.WriteLine("Item does not exist");
                                }
                            else if(mythings.Type(index, mythings) == "Sword")
                            {
                                if (data.gamedata[1] != "Class Sorcerer")
                                {
                                   
                                        user_warrior.Equip_Character(index, mythings, user_warrior,data);
                                        user.Equipment[nb, 1] -= 1;
                                        if (user.Equipment[nb, 1] == 0)
                                        {
                                            Console.WriteLine("You broke your sword!");
                                            user.RemoveFromEquipment(nb, user, data);
                                            user_warrior.UnEquip(user_warrior, data);
                                        }

                                    } 
                                else
                                {
                                        Console.WriteLine("You cannot use this item!");
                                        System.Threading.Thread.Sleep(4000);
                                        toReturn = false;
                                        //Warrior war2 = new Warrior("N", 0, 0, 0, 0, 0);
                                        //user.Heal(index, mythings, data, user);

                                    }
                                //user.Use(nb, user, user_sorcerer, user_warrior, mythings, data);
                            }
                            else if (mythings.Type(index, mythings) == "Potion")
                                {
                                    user.Heal(index,mythings,data,user,user_warrior);
                                    user.Equipment[nb, 1] -= 1;
                                    if(user.Equipment[nb, 1]==0)
                                    {
                                        user.RemoveFromEquipment(nb,user,data);
                                    }
                                }
                            else if (mythings.Type(index, mythings) == "Rubbish")
                                {
                                    Console.WriteLine("You cannot use this item!");
                                    mythings.DoQuack(index,mythings);
                                    System.Threading.Thread.Sleep(4000);
                                    toReturn = false;
                                }
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                toReturn = false;
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("Wrong key!");

                                System.Threading.Thread.Sleep(1000);
                            }
                        } while (toReturn != true);

                        break;
                        case 'n':
                            Warrior war = new Warrior("FAKE", 0, 0, 0, 0, 0);
                       
                        Console.Clear();
                        if (duringfight == false)
                        {
                            Program.DefaultMenu(user, user_sorcerer, user_warrior, data, mythings, Mermaid, Dragon, Human);
                        }
                        endloop = true;
                       


                        break;


                        default:
                            endloop = false;
                            Console.WriteLine("Unknown option!");

                            System.Threading.Thread.Sleep(1000);
                            Console.Clear();
                            break;
                    }

               
            } while (endloop != true);
            if (duringfight == false)
            {
                Program.DefaultMenu(user, user_sorcerer, user_warrior, data, mythings, Mermaid, Dragon, Human);
            }
            

        }
        public void UpdateScore(Character user,Data data)
        {
            data.gamedata[3] = "Score: "+Convert.ToString(user.Score);
        }
        public void Fight(Enemy enemy, Data data, Character user,Item mythings,Sorcerer sorcerer, Warrior warrior, Enemy mermaid,Enemy dragon,Enemy Human)
        {
                Console.Clear();
                Console.WriteLine("Let's start the fight!");
                System.Threading.Thread.Sleep(2000);
                     Console.Clear();
            if (data.gamedata[1] == "Class Sorcerer")
            {
                bool firedamageenabled = false;
                bool toReturn = false;
                int counter = 0;
                do
                {
                    
                    
                   if (user.Actual_hp > 0)
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
                            Program.DefaultMenu(user, sorcerer, warrior, data, mythings, mermaid, dragon, Human);
                            break;

                        }
                        else
                        {
                            if (firedamageenabled == true)
                            {
                                counter++;
                                if (counter > 3)
                                {
                                    firedamageenabled = false;
                                    counter = 0;
                                }
                            }

                            Console.Clear();
                            double value = enemy.Attack_Character(enemy, user);
                            System.Threading.Thread.Sleep(4000);
                            bool toBreak = true;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Enemy HP: " + enemy.Enemy_Actual_HP);
                                Console.WriteLine("Your HP: " + user.Actual_hp);
                                Console.WriteLine("What are you going to do?");
                                Console.WriteLine("1. Use Confusion");
                                Console.WriteLine("2. Use Fireball");
                                Console.WriteLine("3. Use teleportation");
                                Console.WriteLine("4. Manage equipment");
                                Console.Write("Select: ");
                                // int toOverWrite = data.WhereIsNull(data);

                                char choice = Console.ReadKey().KeyChar;
                                double attack_sorc_value;
                                Console.WriteLine();
                                
                                switch (choice)
                                {
                                    case '1':

                                        attack_sorc_value = Sorcerer.Sorcerer_Spells(user.Attack(mythings, user), choice, enemy, sorcerer, mythings, data, firedamageenabled, mermaid, Human, dragon);
                                        enemy.Enemy_Actual_HP -= attack_sorc_value;
                                        System.Threading.Thread.Sleep(5000);
                                        break;
                                    case '2':
                                        attack_sorc_value = Sorcerer.Sorcerer_Spells(user.Attack(mythings, user), choice, enemy, sorcerer, mythings, data, firedamageenabled, mermaid, Human, dragon);
                                        firedamageenabled = true;
                                        enemy.Enemy_Actual_HP -= attack_sorc_value;
                                        System.Threading.Thread.Sleep(5000);
                                        break;
                                    case '3':
                                        enemy.RestoreHealth(enemy);
                                        user.UpdateHealth(user, data);
                                        Console.WriteLine("HOCUS POCUS!");
                                        System.Threading.Thread.Sleep(3000);
                                        Program.DefaultMenu(user, sorcerer, warrior, data, mythings, mermaid, dragon, Human);
                                        break;
                                    case '4':
                                        user.ViewEquipment(mythings, user, sorcerer, warrior, data, mermaid, dragon, Human, true);
                                        toReturn = true;
                                        toBreak = false;
                                        break;
                                    default:
                                        toReturn = true;
                                        Console.WriteLine("Unknown option!");
                                        System.Threading.Thread.Sleep(1000);
                                        toBreak = false;
                                        break;
                                }
                            } while (toBreak!=true);


                        } 
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("YOU ARE DEAD!");
                        System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("YOUR SCORE WAS: " + score);
                        Console.WriteLine("Press \"r\" to go back to main menu or \"ESC\" to quit");
                        var key = Console.ReadKey();
                        Console.WriteLine();
                        while (true)
                        {
                            if (key.Key == ConsoleKey.Escape)
                            {
                                Console.WriteLine("Bye!");
                                System.Threading.Thread.Sleep(5000);
                                Environment.Exit(0);//terminate console
                            }
                            else if (key.KeyChar == 'r')
                            {
                                Program.OpenMainMenu(data);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Unknown option!");
                            }
                        }
                    }
                }
                while (true);
                
            }
            else
            {//WARRIOR FIGHT IMPLEMENTATION

                int strength;
                double finalattack;
                bool toReturn = false;
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
                        Program.DefaultMenu(user, sorcerer, warrior, data, mythings, mermaid, dragon, Human);
                        break;

                    }
                    else if (user.Actual_hp > 0)
                    {
                        Console.Clear();
                        System.Threading.Thread.Sleep(2000);
                        double value = enemy.Attack_Character(enemy, user);
                        System.Threading.Thread.Sleep(4000);
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
                                finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength);
                                enemy.Enemy_Actual_HP -= finalattack;
                                if(finalattack!=0)
                                {
                                    Console.WriteLine("Hit value: " + finalattack);
                                }
                                
                                System.Threading.Thread.Sleep(2000);
                                if (user.Equip != 0)
                                {
                                    int EQNB = 0;
                                    for (int i = 0; i < Equipment.Length / 2; i++)
                                    {
                                        if (user.Equip == user.Equipment[i, 0])
                                        {
                                            EQNB = i;
                                            break;
                                        }
                                    }
                                    user.Equipment[EQNB, 1] -= 1;
                                    if (user.Equipment[EQNB, 1] == 0)
                                    {
                                        Console.WriteLine("You broke your sword!");
                                        user.RemoveFromEquipment(nb, user, data);
                                        warrior.UnEquip(warrior, data);
                                    }
                                }
                                break;
                            case '2':
                                strength = 2;
                                finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength);
                                enemy.Enemy_Actual_HP -= finalattack;
                                if (finalattack != 0)
                                {
                                    Console.WriteLine("Hit value: " + finalattack);
                                }
                                System.Threading.Thread.Sleep(2000);
                                if (user.Equip != 0)
                                {
                                    int EQNB=0;
                                    for(int i=0;i<Equipment.Length/2;i++)
                                    {
                                        if(user.Equip==user.Equipment[i,0])
                                        {
                                            EQNB = i;
                                            break;
                                        }
                                    }
                                    user.Equipment[EQNB, 1] -= 1;
                                    if (user.Equipment[EQNB, 1] == 0)
                                    {
                                        Console.WriteLine("You broke your sword!");
                                        user.RemoveFromEquipment(nb, user, data);
                                        warrior.UnEquip(warrior, data);
                                    }
                                }
                                break;
                            case '3':
                                strength = 3;
                                finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength);
                                enemy.Enemy_Actual_HP -= finalattack;
                                if (finalattack != 0)
                                {
                                    Console.WriteLine("Hit value: " + finalattack);
                                }
                                System.Threading.Thread.Sleep(2000);
                                if (user.Equip != 0)
                                {
                                    int EQNB = 0;
                                    for (int i = 0; i < Equipment.Length / 2; i++)
                                    {
                                        if (user.Equip == user.Equipment[i, 0])
                                        {
                                            EQNB = i;
                                            break;
                                        }
                                    }
                                    user.Equipment[EQNB, 1] -= 1;
                                    if (user.Equipment[EQNB, 1] == 0)
                                    {
                                        Console.WriteLine("You broke your sword!");
                                        user.RemoveFromEquipment(nb, user, data);
                                        warrior.UnEquip(warrior, data);
                                    }
                                }

                                break;
                            case '4':
                                user.ViewEquipment(mythings, user, sorcerer, warrior, data,mermaid,dragon,Human,true) ;
                                toReturn = true;
                                break;

                            default:
                                toReturn = true;
                                Console.WriteLine("Unknown option!");
                                System.Threading.Thread.Sleep(4000);
                                break;
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("YOU ARE DEAD!");
                            System.Threading.Thread.Sleep(2000);
                            Console.WriteLine("YOUR SCORE WAS: " + score);
                            Console.WriteLine("Press \"r\" to go back to main menu or \"ESC\" to quit");
                            var key = Console.ReadKey();
                            Console.WriteLine();

                            if (key.Key == ConsoleKey.Escape)
                            {
                                Console.WriteLine("Bye!");
                                System.Threading.Thread.Sleep(5000);
                                Environment.Exit(0);//terminate console
                            }
                            else if (key.KeyChar == 'r')
                            {
                                Program.OpenMainMenu(data);
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Unknown option!");
                                System.Threading.Thread.Sleep(5000);
                                break;
                            }



                        }


                    }

                } while (true);




            }    
            
        }
        int nb;
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
                Console.WriteLine("Where would you like to go?");
                Console.WriteLine("1. Tavern" );
                Console.WriteLine("2. Forest");
                Console.WriteLine("3. Shop");
                Console.WriteLine("4. Queen");
                Console.WriteLine("5. Back");
                Console.Write("Select: ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                NPC shopkeeper = new NPC(3, mythings,user);

                switch (choice)
                {
                    case '1':
                        if (data.gamedata[1]=="Class Warrior")
                        {
                            war.UnEquip(war,data);
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
                        Program.doPlot(data);
                        break;
                    case '5':
                        Program.DefaultMenu(user, sorc,war,data,mythings,Mermaid,Dragon,Human);
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
        
        
    

