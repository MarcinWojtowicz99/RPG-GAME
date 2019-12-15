﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    /// <summary>
    /// Derived class for warrior
    /// </summary>
    class Warrior : Character
    {
        int hpboost;
        double nbofuseOfEquipmentbeforeitcracks = 250.0;
        public double NbOfUseEQ {get{ return nbofuseOfEquipmentbeforeitcracks; } set{ nbofuseOfEquipmentbeforeitcracks = value; } }
        int[,] equipment;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="basic_hp"></param>
        /// <param name="maxequipment"></param>
        /// <param name="hpboost">Adds more health for warrior</param>
        /// <param name="basic_damage"></param>
        /// <param name="startmoney"></param>
        public Warrior(string name, int basic_hp, int maxequipment, int hpboost, int basic_damage, int startmoney) : base(name, basic_hp, maxequipment, basic_damage, startmoney)
        {
            this.hpboost = hpboost;
            equipment = Equipment;
            BoostMe();
        }
        /// <summary>
        /// Adds more health for warrior
        /// </summary>
        /// <returns></returns>
        public int BoostMe()
        {
            return actual_hp += hpboost;
        }
        /// <summary>
        /// Generate random number of use befor it breaks
        /// </summary>
        /// <returns></returns>
        public double GenerateNbOfUse()
        {
            Random rnd = new Random();
            int random = rnd.Next(5,10);
            double nb = Convert.ToDouble(random)*0.1;
            return NbOfUseEQ * nb;
        }
        /// <summary>
        /// Equips a sword
        /// </summary>
        /// <param name="index"></param>
        /// <param name="mythings"></param>
        /// <param name="warrior"></param>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Equip_Character(int index, Item mythings, Warrior warrior, Data data, Character user)
        {
            warrior.NbOfUseEQ = GenerateNbOfUse();
            warrior.UnEquip(warrior,mythings,data,user);
            warrior.Equip = index;
            data.gamedata[6] = "Equipped: "+index;
            Console.WriteLine("Equipped: "+index);
            System.Threading.Thread.Sleep(4000);
            return true;
        }
        /// <summary>
        /// Unequip a sword
        /// </summary>
        /// <param name="warrior"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UnEquip(Warrior warrior,Item mythings,Data data, Character user)
        {
            if (warrior.Equip != 0)
                            {
                                int index = warrior.Equip;
                                user.AddToEquipment(index,mythings,warrior,data);
                            }
            warrior.Equip = 0;
            data.gamedata[6] = "Equipped: No";
            
                return false;
        }
        /// <summary>
        /// Missed attack info
        /// </summary>
        /// <returns></returns>
        public static double Missed()
        {
            Console.WriteLine("YOU MISSED!");
            System.Threading.Thread.Sleep(2000);
            return 0.0;
        }
        /// <summary>
        /// Method for attacking an enemy
        /// </summary>
        /// <param name="attack">Value of attack</param>
        /// <param name="Strength">Type of attack</param>
        /// <param name="warrior"></param>
        /// <param name="mythings"></param>
        /// <returns>Returns value of attack</returns>
        public static double WarriorAttacks(double attack,int Strength,Warrior warrior,Item mythings)
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
                else attack = Missed();
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
                else attack = attack = Missed();
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
                else  attack = Missed();
            }
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            if (warrior.Equip == 0&&attack==0)
            {
                Console.WriteLine("Your hit value: " + attack);

            }
            else
            {
                attack+= mythings.unit[warrior.Equip].damageorhealvalueafteruse;
                Console.WriteLine("Your hit value: " + attack);
            }
            return attack;

        }
        /// <summary>
        /// Method for breaking a sword
        /// </summary>
        /// <param name="warrior"></param>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        public void IsSwordBroken(Warrior warrior, Data data, Character user, Item mythings)
        {
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
        }
        /// <summary>
        /// Continues fight as a warrior
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
        /// <param name="toBreak"></param>
        /// <returns></returns>
        public bool ContinueFight(Enemy enemy, Data data, Character user, Item mythings, Sorcerer sorcerer, Warrior warrior, Enemy mermaid, Enemy dragon, Enemy Human, bool toBreak)
        {

            int strength;
            double finalattack;
            do
            {
                warrior.AttacksAvailable(warrior,enemy);
                char choice = Console.ReadKey().KeyChar;

                Console.WriteLine();
                switch (choice)
                {
                    case '1':
                        strength = 1;
                        toBreak = true;
                        finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength,warrior,mythings);
                        enemy.Enemy_Actual_HP -= finalattack;
                        warrior.IsSwordBroken(warrior,data,user,mythings);
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case '2':
                        strength = 2;
                        toBreak = true;
                        finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength, warrior, mythings);
                        enemy.Enemy_Actual_HP -= finalattack;
                        warrior.IsSwordBroken(warrior, data, user, mythings);
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case '3':
                        strength = 3;
                        toBreak = true;
                        finalattack = Warrior.WarriorAttacks(user.Attack(mythings, user), strength, warrior, mythings);
                        enemy.Enemy_Actual_HP -= finalattack;
                        warrior.IsSwordBroken(warrior, data, user, mythings);
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    case '4':
                        user.ViewEquipment(mythings, user, sorcerer, warrior, data, mermaid, dragon, Human, true);
                        toBreak = false;
                        break;

                    default:
                        toBreak = UnknownOption();
                        break;
                }
            } while (toBreak != true);
            return true;
        }
        /// <summary>
        /// Displays info about available attacks
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enemy"></param>
        public void AttacksAvailable(Warrior user, Enemy enemy)
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
        }
        /// <summary>
        /// Method for teleportation to the final boss
        /// </summary>
        /// <param name="mythings"></param>
        /// <param name="user"></param>
        /// <param name="user_sorcerer"></param>
        /// <param name="user_warrior"></param>
        /// <param name="data"></param>
        /// <param name="Mermaid"></param>
        /// <param name="Dragon"></param>
        /// <param name="Human"></param>
        /// <returns></returns>
        public bool Teleporter(Item mythings, Character user, Sorcerer user_sorcerer, Warrior user_warrior, Data data, Enemy Mermaid, Enemy Dragon, Enemy Human)
           
        {
           while (true)
            {
                Console.WriteLine("Hello. I'm the teleporter! I'm here to send you to dragon's valley, but you need to pay me 20000 and show me your ring");
                Console.WriteLine("1. Okay");
                Console.WriteLine("2. No, thank you");
                char b = Console.ReadKey().KeyChar;
                bool ring = user.FindInEquipment(mythings, user, "Queen's ring");
                switch (b)
                {
                    case '1':
                        if(user.Money_player-20000>=0&&ring==true)
                        {
                            Console.Clear();
                            Console.WriteLine("Teleporting...");
                            System.Threading.Thread.Sleep(2000);
                            Console.Clear();
                            user.GameComplete = true;
                            user.Fight(Dragon, data,user,mythings,user_sorcerer,user_warrior,Mermaid,Dragon,Human);
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("I see, come back when you'll be ready");
                            Console.ReadKey();
                            Program.DefaultMenu(user, user_sorcerer, user_warrior, data, mythings, Mermaid, Dragon, Human);
                            return true;
                        }
                        break;
                    case '2':
                        Program.DefaultMenu(user,user_sorcerer,user_warrior,data,mythings,Mermaid,Dragon,Human);
                        return true;
                        break;
                    default:
                        Console.WriteLine("Incorrect sign");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        break;
                }
            }
            
        }
       
    }
}
