using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    class Sorcerer:Character
    {
        int[,] equipment;
        int demonskilled;
        public int DemonsKilled { get {return demonskilled; } set { demonskilled = value; } }
        public Sorcerer(string name, int basic_hp, int maxequipment, int basic_damage, int startmoney) : base(name, basic_hp, maxequipment,basic_damage,startmoney)
        {
            equipment = Equipment;
            activskil = false;
            demonskilled = 0;
        }
       bool activskil;
       public bool Activeskill { get { return activskil; }set { activskil = value; } }
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
                            if (got == true)
                            {
                                Console.Clear();
                                Console.WriteLine("You have found jewels!");
                                Console.WriteLine("+200 points!");
                                user.Score += 200;
                                user.UpdateScore(user, data);
                                System.Threading.Thread.Sleep(2000);
                                Console.Clear();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Your storage is full :c");
                                System.Threading.Thread.Sleep(2000);
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Unfortunately you didn't find jewels :(");
                            System.Threading.Thread.Sleep(2000);
                            Console.Clear();
                        }


                        break;
                    case '2':

                        Basic_damage *= 2;
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
                        else
                        {
                            Console.WriteLine("Wrong key!");
                            System.Threading.Thread.Sleep(2000);
                            Console.Clear();
                            toReturn = false;
                        }
                        break;
                    default:

                        Console.WriteLine("Wrong key!");
                        toReturn = false;
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        break;
                }
                Console.Clear();

            } while (toReturn != true);
        }
        public static double Sorcerer_Spells(double value,int type, Enemy enemy, Sorcerer user, Item mythings, Data data,bool firedamageenabled,Enemy Mermaid, Enemy Human, Enemy Dragon)
        {
            Random rnd = new Random();
            int random = rnd.Next(100);
            if(random<=5)
            {
                bool ToReturn = true;
                do {
                    
                    Console.Clear();
                Console.WriteLine("You summoned the goldfish! Now, make your wish...");
                Console.WriteLine("1. Kill the monster");
                Console.WriteLine("2. I want money");
                Console.WriteLine("3. Leave me alone");
                Console.WriteLine("4. I'll finish you!");
                Console.Write("Select: ");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.Clear();
                switch (key)
                {
                    case '1':
                            Random rnd2 = new Random();
                            int random2 = rnd2.Next(100);

                            if(random2<50)
                            {
                                user.Score += 200;
                                enemy.RestoreHealth(enemy);
                                Warrior war = new Warrior("FAKE",0,0,0,0,0);
                                Console.Clear();
                                Console.WriteLine("Monster has been killed");
                                Console.WriteLine("+200 points");
                                enemy.Drop_Item(user, mythings, data);
                                enemy.EnemyKilled += 1;
                                user.UpdateEnemyKilled(enemy, data);
                                System.Threading.Thread.Sleep(4000);
                                user.UpdateScore(user, data);
                                Program.DefaultMenu(user,user,war,data,mythings,Mermaid,Dragon,Human);
                                
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
                            bool isholywater = false ;
                            for(int i=0;i<user.Equipment.Length/2;i++)
                            {
                                if(mythings.unit[user.Equipment[i,0]].nameofitem=="Holy water")
                                {
                                    isholywater = true;
                                }
                            }
                            if (random3 < 50||isholywater==true)
                            {
                                Console.WriteLine("Congratulations! You used secret spell and defeated the demon! Unfortunately you lost half of you HP in this battle and your main enemy HP has been restored...");
                                user.Money_player += 6000;
                                Console.WriteLine("+"+user.Money_player);
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
                        Console.WriteLine("Unknown option!");
                            System.Threading.Thread.Sleep(1500);
                            Console.Clear();
                            ToReturn = false;
                        break;
                }
                    
            }
            while (ToReturn != true) ; 
            }
            if (type == '1')
            {

                Console.Clear();
                Console.Write("You used confusion... Your enemy is falling asleep");
                Program.WaitDots();
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
            if (firedamageenabled == true)
            {
                Console.WriteLine("+FLAMING DAMAGE");
                Console.Clear();
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Your attack value: " + (value + (0.15 * enemy.Enemy_Actual_HP)));
                System.Threading.Thread.Sleep(2000);
                return value+(0.15*enemy.Enemy_Actual_HP);
            }
            else
            {
                return value;
            }


           
        }
    }
}
