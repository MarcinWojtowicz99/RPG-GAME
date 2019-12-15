using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    /// <summary>
    /// All NPCs available in the game
    /// </summary>
    class NPC
    {
       int money;
        static int money_player;
        public static int moneyPlayer { get { return money_player; } }
        int nbofequipment;
        public int Nb { get { return nbofequipment; }set { nbofequipment = value; } }
        static int actual_hp;
        static int basic_hp;
        public static int Actual { get { return actual_hp; } }
        public static int Basic { get { return basic_hp; } }
        
        int[] arrayofindexes;
        public int[] ReturnToArray { get { return arrayofindexes; } set { arrayofindexes = value; } }
        int[] Value;
        public int[] ValueOfItem { get { return Value; } set { Value = value; } }
        public int[] value2;
        public int[] ValueOfMYItem { get { return value2; } set { value2 = value; } }
        public int ShopKeeperMoney { get { return money; } set { money = value; } }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nbofequipment">Total number of equipment</param>
        /// <param name="mythings">Item list to draw </param>
        /// <param name="user">User object</param>
        public NPC(int nbofequipment, Item mythings, Character user)
        {
            this.nbofequipment = nbofequipment;
            int[] arrayofindexes = new int[nbofequipment];
            int[] Value = new int[nbofequipment];
            int[] value2 = new int[user.Equipment.Length / 2];
            Random rnd1 = new Random();
            money = rnd1.Next(100, 1000);
            for (int i = 0; i < nbofequipment; i++)
            {
                Random rnd = new Random();
                arrayofindexes[i] = rnd.Next(1, mythings.Nb_Of_Items-2);
                Value[i]= mythings.unit[arrayofindexes[i]].Generate_Value();
                value2[i]= mythings.unit[user.Equipment[i,0]].Generate_Value();

            }
            ReturnToArray = arrayofindexes;
            ValueOfItem = Value;
            ValueOfMYItem = value2;
            ShopKeeperMoney = money;

        }
        /// <summary>
        /// Method for buying items from shopkeeper
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="shopkeeper">Shopkeeper</param>
        /// <param name="mythings">Items</param>
        /// <param name="data">User's data</param>
        public void SellItemsToShopkeeper(Character user, NPC shopkeeper, Item mythings, Data data)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Shopkeeper account value: " + shopkeeper.ShopKeeperMoney);
                Console.WriteLine("Your account value: " + user.Money_player);
                Console.WriteLine("What do you wanna sell? ");
                Console.WriteLine("Your equipment: ");
                for (int i = 0; i < user.Equipment.Length / 2; i++)
                {
                    if (user.Equipment[i, 0] != 0)
                    {
                        if (shopkeeper.value2[i] == 0)
                        {
                            shopkeeper.value2[i] = mythings.unit[user.Equipment[i, 0]].Generate_Value();
                            if (user.Equipment[i, 1] > 1)   Console.WriteLine("{0}. {1} x{2} Cost: {3} Dragon Coins [Total: {4}]", i, mythings.unit[user.Equipment[i, 0]].nameofitem, user.Equipment[i, 1], shopkeeper.value2[i], shopkeeper.value2[i] * user.Equipment[i, 1]);
                           else   Console.WriteLine("{0}. {1}  Cost: {2} Dragon Coins", i, mythings.unit[user.Equipment[i, 0]].nameofitem, shopkeeper.value2[i]);
                            

                        }
                        else
                        {
                            if (user.Equipment[i, 1] > 1)  Console.WriteLine("{0}. {1} x{2} Cost: {3} Dragon Coins [Total: {4}]", i, mythings.unit[user.Equipment[i, 0]].nameofitem, user.Equipment[i, 1], shopkeeper.value2[i], shopkeeper.value2[i] * user.Equipment[i, 1]);
                           else   Console.WriteLine("{0}. {1}  Cost: {2} Dragon Coins", i, mythings.unit[user.Equipment[i, 0]].nameofitem, shopkeeper.value2[i]);
                            
                        }

                    }
                    else   Console.WriteLine("{0}. [Empty slot]", i);
                }
                Console.WriteLine("{0}. I resign", user.Equipment.Length / 2);
                Console.Write("Select: ");
                char nb2 = Console.ReadKey().KeyChar;
                int key2 = CharUnicodeInfo.GetDecimalDigitValue(nb2);
                Console.WriteLine();

                try
                {
                    if (key2 < user.Equipment.Length / 2 && user.Equipment[key2, 0] != 0)
                    {
                        Console.WriteLine(user.Equipment[key2, 1]);
                        if (user.Equipment[key2, 1] > 1)
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Please, write the number of items to sell: ");
                                char nb = Console.ReadKey().KeyChar;
                                int key3 = CharUnicodeInfo.GetDecimalDigitValue(nb);
                                if (key3 < user.Equipment[key2, 1] && nb > 0)
                                {
                                    if (shopkeeper.money >= shopkeeper.value2[key2] * key3)
                                    {
                                        user.Equipment[key2, 1] -= key3;
                                        user.AddMoney(shopkeeper.value2[key2] * key3, user, data);
                                        shopkeeper.money -= shopkeeper.value2[key2] * key3;
                                        break;
                                    }
                                    else Console.WriteLine("I don't have enopugh money!"); System.Threading.Thread.Sleep(2000);

                                }
                                else Console.WriteLine("Wrong key!"); System.Threading.Thread.Sleep(2000);
                            } while (true);

                        }
                        else shopkeeper.ShopKeeperMoney = user.Sell(shopkeeper.value2[key2] * user.Equipment[key2, 1], key2, user, shopkeeper.ShopKeeperMoney, data);
                    }
                    else if (key2 < user.Equipment.Length / 2 && user.Equipment[key2, 0] == 0)
                    {
                        Console.WriteLine("Item does not exist");   System.Threading.Thread.Sleep(4000);
                    }
                    else if (key2 == user.Equipment.Length / 2) break;
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Unknown sign!");
                    System.Threading.Thread.Sleep(2000);  Console.Clear();
                }
            }
        }
        /// <summary>
        /// Method for showing items
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="user_sorcerer">User object</param>
        /// <param name="user_warrior">User object</param>
        /// <param name="mythings">Items</param>
        /// <param name="data">User's data</param>
        /// <param name="Mermaid"></param>
        /// <param name="Dragon"></param>
        /// <param name="shopkeeper">Shopkeeper</param>
        /// <param name="Human"></param>
        public void ShowItems(Character user, Sorcerer user_sorcerer, Warrior user_warrior, Item mythings, Data data, Enemy Mermaid, Enemy Dragon, NPC shopkeeper, Enemy Human)
        {
            Console.Clear();
            int additionalprize = Scenario.ShopkeeperQuotes(data);


            while (true)
            {
               
                Console.Clear();
                Console.WriteLine("Shopkeeper account value: " + shopkeeper.ShopKeeperMoney);
                Console.WriteLine("Your account value: " + user.Money_player);
                Console.WriteLine("Today I can offer you: ");
                for (int i = 0; i < shopkeeper.ReturnToArray.Length; i++)
                {
                    Console.WriteLine("{0}. {1}: {2}Dragon Coins", i, mythings.unit[shopkeeper.ReturnToArray[i]].nameofitem, shopkeeper.Value[i]+ additionalprize);
                }
                Console.WriteLine("{0}. I wanna sell you something", shopkeeper.ReturnToArray.Length + 1);
                Console.WriteLine("{0}. No, thank you", shopkeeper.ReturnToArray.Length + 2);
                Console.Write("Select: ");
                int key = CharUnicodeInfo.GetDecimalDigitValue(Console.ReadKey().KeyChar);
                Console.WriteLine();
                Console.WriteLine(key);
                if (key <= shopkeeper.ReturnToArray.Length)
                {
                    for (int i = 0; i < shopkeeper.ReturnToArray.Length; i++)
                    {
                        if (key == i)
                        {
                            shopkeeper.ShopKeeperMoney=user.Pay(shopkeeper.Value[i]+ additionalprize, shopkeeper.ReturnToArray[i], mythings, user, shopkeeper.ShopKeeperMoney+ additionalprize, data);
                        }
                    }
                }
                else if (key == shopkeeper.ReturnToArray.Length + 1)
                {
                    shopkeeper.SellItemsToShopkeeper(user,shopkeeper,mythings,data);
                }

                else if (key == shopkeeper.ReturnToArray.Length + 2)
                {
                    Console.WriteLine("Bye");
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown option!");
                }
            }
            Program.DefaultMenu(user, user_sorcerer, user_warrior, data, mythings, Mermaid, Dragon, Human);
        }
        /// <summary>
        /// Bartender for sorcerer
        /// </summary>
        /// <param name="user"></param>
        /// <param name="user_sorcerer"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        /// <param name="Mermaid"></param>
        /// <param name="Dragon"></param>
        /// <param name="Human"></param>
      public void GiveBeer_Sorcerer(Character user, Sorcerer user_sorcerer, Item mythings, Data data, Enemy Mermaid, Enemy Dragon, Enemy Human)
        {
           
            char decision;
            do
            {
                int howmuch = Scenario.BartenderQuotes(data);
                decision = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (decision == 'y')
                {
                    Console.Clear();
                    user.RemoveMoney(howmuch,user,data);
                    user.Actual_hp = user.Basic_hp;
                    Program.Print("Few hours later", 'w');
                    System.Threading.Thread.Sleep(1000);
                    Program.WaitDots();
                    actual_hp = basic_hp;
                    Random rnd = new Random();
                    int rand = rnd.Next(101);
                    if (rand <= 15)
                    {
                        Random rnd2 = new Random();
                        int cash = rnd2.Next(0, money_player);
                        user.RemoveMoney(cash, user,data);
                        Console.Clear();
                        Program.Print("Unfortunatelly you offended someone in the tavern and lost "+Convert.ToString(cash)+" money",'w');
                        System.Threading.Thread.Sleep(5000);

                    }
                    else
                    {
                        Program.Print("-See you again!",'y');
                        System.Threading.Thread.Sleep(5000);
                        Console.Clear();
                    }
                    break;

                }
                else if (decision == 'n')
                {
                    Program.Print("Okay, See you then...", 'y');
                    System.Threading.Thread.Sleep(5000);
                    Console.Clear();

                    break;
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                    System.Threading.Thread.Sleep(5000);
                    Console.Clear();
                }


            } while (decision != 'y' && decision != 'n');

            Warrior user_warrior = null;
            Program.DefaultMenu(user,user_sorcerer,user_warrior,data,mythings,Mermaid,Dragon,Human);
        }
        /// <summary>
        /// Bartender for warrior
        /// </summary>
        /// <param name="user"></param>
        /// <param name="user_warrior"></param>
        /// <param name="data"></param>
        /// <param name="mythings"></param>
        /// <param name="mermaid"></param>
        /// <param name="dragon"></param>
        /// <param name="Human"></param>
        public void GiveBeer_Warrior(Character user, Warrior user_warrior, Data data, Item mythings, Enemy mermaid, Enemy dragon, Enemy Human)
        {
            Sorcerer sor = null;
            char decision;
            do
            {
                int howmuch = Scenario.BartenderQuotes(data);
                user.Actual_hp = 0;
                user.Actual_hp = user.Basic_hp;
                user.Actual_hp = user_warrior.BoostMe();
                decision = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (decision == 'y')
                {
                    Console.Clear();
                    user.RemoveMoney(howmuch,user,data);
                   
                    Console.Write("Few hours later");
                    Program.WaitDots();
                   
                    Random rnd = new Random();
                    int rand = rnd.Next(101);
                    if(rand<=25)
                    {
                        Console.Clear();
                        Sorcerer sor2 = new Sorcerer("N", 0,  0, 0,0);
                        Program.Print("*BURP* HO-HOW D-DID YOU CALL MY MOTHER?",'r');
                        System.Threading.Thread.Sleep(4000);
                        Console.ForegroundColor = ConsoleColor.White;
                        user.Fight(Human,data,user,mythings,sor2, user_warrior, mermaid, dragon, Human);                    }
                    else
                    {
                        Program.Print("-See you again!", 'y');
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                    }
                    break;

                }
                else if (decision == 'n')
                {
                    Program.Print("Okay, See you then...", 'y');
                    Console.Clear();

                    break;
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                }


            } while (decision != 'y' && decision != 'n');
            Program.DefaultMenu(user, sor, user_warrior, data, mythings, mermaid, dragon, Human);
        }

    }
}
