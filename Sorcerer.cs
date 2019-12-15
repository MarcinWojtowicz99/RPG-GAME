using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    /// <summary>
    /// Derived class for sorcerer
    /// </summary>
    class Sorcerer:Character
    {
        int[,] equipment;
        int demonskilled;
        public int DemonsKilled { get {return demonskilled; } set { demonskilled = value; } }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="basic_hp"></param>
        /// <param name="maxequipment"></param>
        /// <param name="basic_damage"></param>
        /// <param name="startmoney"></param>
        public Sorcerer(string name, int basic_hp, int maxequipment, int basic_damage, int startmoney) : base(name, basic_hp, maxequipment,basic_damage,startmoney)
        {
            equipment = Equipment;
            activskil = false;
            demonskilled = 0;
        }
       bool activskil;
       public bool Activeskill { get { return activskil; }set { activskil = value; } }
        /// <summary>
        /// Teleport feature. Sorcerer may teleport anywhere he/she wants
        /// </summary>
        /// <param name="mythings"></param>
        /// <param name="user"></param>
        /// <param name="user_Sorcerer"></param>
        /// <param name="user_Warrior"></param>
        /// <param name="data"></param>
        /// <param name="mermaid"></param>
        /// <param name="dragon"></param>
        /// <param name="Human"></param>
        public void Teleport(Item mythings,Character user,Sorcerer user_Sorcerer,Warrior user_Warrior,Data data,Enemy mermaid,Enemy dragon,Enemy Human)
        {
            bool toReturn = false;
            do
            {
                Console.WriteLine("Where would you like to teleport? (Press [ESC] to go back)");
                Console.WriteLine("1. Gyouku Mountain");//May find jewels
                Console.WriteLine("2. Schu-chan Temple");//basic damage increase temporarly 
                user.GameComplete = true;
                bool ring = user.FindInEquipment(mythings, user, "Queen's ring");
                if (ring == true)
                {
                    Console.WriteLine("3. Dragon Valley");
                }
                var a = Console.ReadKey();
                if (a.Key == ConsoleKey.Escape)
                {
                    Program.DefaultMenu(user, user_Sorcerer, user_Warrior, data, mythings, mermaid, dragon, Human);
                    break;
                }
                char choice = a.KeyChar;
                Console.Clear();
                switch (choice)
                {
                    case '1':
                        Random rnd = new Random();
                        int ind = rnd.Next(100);
                        if (ind < 2)
                        {
                            bool got = user.AddToEquipment(2, mythings, user, data);
                            Console.Clear();
                            if (got == true)
                            {
                                Console.WriteLine("You have found jewels!");
                                Console.WriteLine("+200 points!");
                                user.Score += 200;
                                user.UpdateScore(user, data);
                            }
                            else  Console.WriteLine("Your storage is full :c");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Unfortunately you didn't find jewels :(");
                        }
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case '2':

                        user_Sorcerer.Basic_damage *= 2;
                        user_Sorcerer.Activeskill = true;
                        Console.Clear();
                        Console.WriteLine("Your mana increased!");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case '3':
                        if (ring == true)
                        {
                            user.GameComplete = true;
                            user.Fight(dragon, data, user, mythings, user_Sorcerer, user_Warrior, mermaid, dragon, Human);
                            toReturn = true;
                        }
                        else  toReturn = Character.UnknownOption();
                        break;
                    default:
                        toReturn = Character.UnknownOption();
                        break;
                }
                Console.Clear();
            } while (toReturn != true);
        }
        /// <summary>
        /// Random event-spawning a goldfish
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        /// <param name="Mermaid"></param>
        /// <param name="Human"></param>
        /// <param name="Dragon"></param>
        public static void GoldFish(Enemy enemy, Sorcerer user, Item mythings, Data data, Enemy Mermaid, Enemy Human, Enemy Dragon)
        {
            bool ToReturn = true;
            while (ToReturn != true)
            {
                Scenario.FishyQuotes();
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.Clear();
                switch (key)
                {
                    case '1':
                        Random rnd2 = new Random();
                        int random2 = rnd2.Next(100);

                        if (random2 < 50)
                        {
                            user.Score += 200;
                            enemy.RestoreHealth(enemy);
                            Warrior war = null;
                            Console.Clear();
                            Console.WriteLine("Monster has been killed");
                            Console.WriteLine("+200 points");
                            enemy.Drop_Item(user, mythings, data);
                            enemy.EnemyKilled += 1;
                            user.UpdateEnemyKilled(enemy, data);
                            System.Threading.Thread.Sleep(4000);
                            user.UpdateScore(user, data);
                            Program.DefaultMenu(user, user, war, data, mythings, Mermaid, Dragon, Human);

                        }
                        else
                        {
                            Console.WriteLine("Oops.. Something went wrong!");
                            enemy.RestoreHealth(enemy);
                            Console.WriteLine("Enemy HP restored!");
                            System.Threading.Thread.Sleep(1500);
                        }

                        break;
                    case '2':
                        user.Money_player += 2000;
                        Console.WriteLine("+2000");
                        user.UpdateMoney(user, data);
                        System.Threading.Thread.Sleep(1500);
                        break;
                    case '3':
                        Console.WriteLine("-Okay...");
                        System.Threading.Thread.Sleep(1500);
                        break;
                    case '4':

                        Random rnd3 = new Random();
                        int random3 = rnd3.Next(100);
                        bool isholywater = false;
                        for (int i = 0; i < user.Equipment.Length / 2; i++) if (mythings.unit[user.Equipment[i, 0]].nameofitem == "Holy water") isholywater = true;
                        if (random3 < 50 || isholywater == true)
                        {
                            Console.WriteLine("Congratulations! You used secret spell and defeated the demon! Unfortunately you lost half of you HP in this battle and your main enemy HP has been restored...");
                            user.Money_player += 6000;
                            Console.WriteLine("+" + user.Money_player);
                            enemy.RestoreHealth(enemy);
                            user.Score += 5000;
                            Console.WriteLine("+{0} points", user.Money_player);
                            user.Actual_hp /= 2;
                            user.UpdateEnemyKilled(enemy, data);
                            user.UpdateHealth(user, data);
                            user.UpdateMoney(user, data);
                            user.UpdateScore(user, data);
                            Console.ReadKey();
                            user.DemonsKilled++;
                            data.gamedata[10] = "Unknowns: " + user.DemonsKilled;
                        }
                        else
                        {
                            Console.WriteLine("Unfortunately goldfish has changed into the demon who took you soul");
                            Console.WriteLine("GAME OVER!");
                            user.Actual_hp = 0;
                            Console.ReadKey();
                        }
                        break;
                    default:
                        Console.Clear();
                        ToReturn = Character.UnknownOption();
                        break;
                }
            } 
        }
        /// <summary>
        /// Basic spells
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="enemy"></param>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        /// <param name="firedamageenabled">Boolean if character use fire spells</param>
        /// <param name="Mermaid"></param>
        /// <param name="Human"></param>
        /// <param name="Dragon"></param>
        /// <returns></returns>
        public static double BasicSorcerer_Spells(double value,int type, Enemy enemy, Sorcerer user, Item mythings, Data data,bool firedamageenabled,Enemy Mermaid, Enemy Human, Enemy Dragon)
        {
            value = user.Basic_damage;
            Random rnd = new Random();
            int random = rnd.Next(100);
            if(random<=5)
            {
                    GoldFish(enemy,user,mythings,data,Mermaid,Human,Dragon);
            }
            if (firedamageenabled == true)
            {
                Console.WriteLine("+FLAMING DAMAGE");
                Console.Clear();
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Your attack value: " + (value + (0.15 * enemy.Enemy_Actual_HP)));
                System.Threading.Thread.Sleep(2000);
                value += (0.15 * enemy.Enemy_Actual_HP);
            }
            if (type == '1')
            {

                Console.Clear();
                Console.Write("You used confusion... Your enemy is falling asleep");
                Program.WaitDots();
                value *= 2;
            }
            else if (type == '2')
            {

                Console.Clear();
                Console.WriteLine("You used FIREBALL... Flaming damage enabled!");
                System.Threading.Thread.Sleep(1500);
                firedamageenabled = true;
                
               

            }
            Random rnd4 = new Random();
                    int random4 = rnd4.Next(100);
            if (random4 < 15)
            {
                Console.WriteLine("Your spell ricocheted!");
                System.Threading.Thread.Sleep(3000);
                user.Actual_hp -=Convert.ToInt32(value);
                return 0.0;

            }
           
           
                return value;
           
        }
        /// <summary>
        /// Spells to unlock
        /// </summary>
        /// <param name="type"></param>
        /// <param name="enemy"></param>
        /// <param name="user"></param>
        public static void Advanced_Sorcerer_Spells(int type,Enemy enemy, Sorcerer user)
        {
            if(type==6)
            {
                
                    Console.Clear();
                    enemy.Enemy_Actual_HP -= enemy.Enemy_Actual_HP * 0.25;
                    double shield = user.Score * 0.1;
                    user.Actual_hp += Convert.ToInt32(shield);
                    Console.WriteLine("You earned demon armor +" + Convert.ToInt32(shield));
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    enemy.Enemy_Actual_HP -= enemy.Enemy_Actual_HP * 0.02;
                    Console.WriteLine("Demon attack: " + enemy.Enemy_Actual_HP * 0.02);
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                
            }
            else if(type==7)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("You used Interfectorem draconis");
                Console.WriteLine("Attack value: " + enemy.Enemy_Actual_HP * 0.15);
                enemy.Enemy_Actual_HP -= enemy.Enemy_Actual_HP * 0.15;
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }
        /// <summary>
        /// Displays info about available attacks
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enemy"></param>
        /// <param name="Resurection_used"></param>
        public void AttacksAvailable(Sorcerer user, Enemy enemy, bool Resurection_used)
        {
            Console.Clear();
            Console.WriteLine("Enemy HP: " + enemy.Enemy_Actual_HP);
            Console.WriteLine("Your HP: " + user.Actual_hp);
            Console.WriteLine("What are you going to do?");
            Console.WriteLine("1. Use Confusion");
            Console.WriteLine("2. Use Fireball");
            Console.WriteLine("3. Use teleportation");
            Console.WriteLine("4. Manage equipment");
            if (user.Score >= 3000 && Resurection_used == false)
            {
                Console.WriteLine("5. Resurrection");
            }
            if (user.DemonsKilled > 2)
            {
                Console.WriteLine("6. Little helper (Summons little demon)");
            }
            if (user.Score >= 6000)
            {
                Console.WriteLine("7. Interfectorem draconis");
            }

            Console.Write("Select: ");
        }
        /// <summary>
        /// Turns on/off the fire damage
        /// </summary>
        /// <param name="firedamageenabled"></param>
        /// <param name="counter"></param>
        public static void FireOnOff (bool firedamageenabled, int counter)
        {
            if (firedamageenabled == true)
            {
                if (counter > 3)
                {
                    counter = 0;
                    firedamageenabled = false;
                }
                else
                {
                    counter++;
                }
            }
        }
        /// <summary>
        /// Continues fight as a sorcerer
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        /// <param name="sorcerer"></param>
        /// <param name="warrior"></param>
        /// <param name="mermaid"></param>
        /// <param name="dragon"></param>
        /// <param name="Human"></param>
        /// <param name="firedamageenabled">Boolean if firedamage is enabled</param>
        /// <param name="used"></param>
        /// <param name="Resurection_used">Boolean if resurrection feature was used</param>
        /// <param name="counter">Counts number of fire attacks</param>
        /// <param name="toBreak"></param>
        /// <param name="sorcererResurection">Boolean if resurrection is possible</param>
        /// <param name="Exit">Quits fight</param>
        /// <returns></returns>
        public bool ContinueFight(Enemy enemy, Data data, Character user, Item mythings, Sorcerer sorcerer, Warrior warrior, Enemy mermaid, Enemy dragon, Enemy Human, bool firedamageenabled, bool used,out bool Resurection_used,int counter, bool toBreak, out bool sorcererResurection,out bool Exit)
        {
            Exit = false;
            sorcererResurection = false;
            Resurection_used = used;
            do
            {
                if (sorcerer.Activeskill == true)
                {
                    sorcerer.Basic_damage /= 2;
                }
                FireOnOff(firedamageenabled, counter);
                sorcerer.AttacksAvailable(sorcerer,enemy, Resurection_used);

                char choice = Console.ReadKey().KeyChar;
                double attack_sorc_value;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        toBreak = true;
                        attack_sorc_value = Sorcerer.BasicSorcerer_Spells(user.Attack(mythings, user), choice, enemy, sorcerer, mythings, data, firedamageenabled, mermaid, Human, dragon);
                        enemy.Enemy_Actual_HP -= attack_sorc_value;
                        System.Threading.Thread.Sleep(5000);
                        break;
                    case '2':
                        toBreak = true;
                        attack_sorc_value = Sorcerer.BasicSorcerer_Spells(user.Attack(mythings, user), choice, enemy, sorcerer, mythings, data, firedamageenabled, mermaid, Human, dragon);
                        firedamageenabled = true;
                        enemy.Enemy_Actual_HP -= attack_sorc_value;
                        System.Threading.Thread.Sleep(5000);
                        break;
                    case '3':
                        toBreak = true;
                        Exit = true;
                        enemy.RestoreHealth(enemy);
                        user.UpdateHealth(user, data);
                        Console.WriteLine("HOCUS POCUS!");
                        System.Threading.Thread.Sleep(3000);
                        break;
                    case '4':
                        user.ViewEquipment(mythings, user, sorcerer, warrior, data, mermaid, dragon, Human, true);
                        toBreak = false;
                        break;
                    case '5':
                        if (user.Score >= 3000 && Resurection_used == false)
                        {
                            sorcererResurection = true;
                            Resurection_used = true;
                            toBreak = true;
                            Console.Clear();
                            Console.WriteLine("You used resurrection. You are temporary immortal");
                            System.Threading.Thread.Sleep(1000);
                            Console.Clear();
                        }
                        else
                        {
                            toBreak = UnknownOption();
                            break;
                        }
                        break;
                    case '6':
                    if (sorcerer.DemonsKilled > 2)
                        {
                            toBreak = true;
                            Advanced_Sorcerer_Spells(6, enemy, sorcerer);
                        }
                        else
                        {
                            toBreak = UnknownOption();
                            break;
                        }
                        break;
                    case '7':
                        if (user.Score >= 6000)
                        {
                            toBreak = true;
                            Advanced_Sorcerer_Spells(7, enemy, sorcerer);
                        }
                        else
                        {
                            toBreak = UnknownOption();
                            break;
                        }
                        break;
                    default:
                        toBreak = UnknownOption();
                        break;
                }

            } while (toBreak != true);
            return true;
        }

    }
}
