using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    /// <summary>
    /// Object keeps all items together in an array
    /// </summary>
    class Item
    {
        int value_min;
        int value_max;
        int damageorhealvalueafteruse;
        int index;
        int nbofuses;
        string nameofitem;
        int nbofitems;

        public int Nb_Of_Items { get { return nbofitems; } set { nbofitems = value; } }

        string name;
        public string Name { get { return name; } }
        double total;
       

        public double totalvalue { get { return total; } }
        public Unit[] unit;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nbofitems">Number of items</param>
        public Item(int nbofitems)
        {
            this.nbofitems = nbofitems;
            unit = new Unit[nbofitems];
        }
        /// <summary>
        /// Copy
        /// </summary>
        /// <param name="things"></param>
        public Item(Item things)
        {
            this.unit = new Unit[things.unit.Length];
            for (int i = 1; i < unit.Length + 1; i++)
            {
                unit[i] = things.unit[i];
            }
        }
        /// <summary>
        /// Method for stacking items
        /// </summary>
        /// <param name="units">item to stack</param>
        /// <returns></returns>
        public bool Stack(Unit units)
        {
            for (int i = 0; i < unit.Length; i++)
            {
                if (unit[i] == null)
                {
                    unit[i] = units;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Calls item's name
        /// </summary>
        /// <param name="indexnb"></param>
        /// <param name="mythings"></param>
        public void Item_Name(int indexnb,Item mythings)
        {
            Console.Write(mythings.unit[indexnb].nameofitem);
        }
        /// <summary>
        /// Give's an item for player
        /// </summary>
        /// <param name="indexnb"></param>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        public void Give(int indexnb, Character user, Item mythings, Data data)
        {
            user.AddToEquipment(indexnb, mythings,user,data);
            Console.Write("You received: ");
            Item_Name(indexnb, mythings);
            Console.WriteLine();
        }
        /// <summary>
        /// Returns value for healing or damage
        /// </summary>
        /// <param name="indexnb"></param>
        /// <param name="mythings"></param>
        /// <returns></returns>
        public int DamageOrHeal(int indexnb, Item mythings)
        {
            return mythings.unit[indexnb].damageorhealvalueafteruse;
        }
        /// <summary>
        /// Number of use of item till it breakes
        /// </summary>
        /// <param name="indexnb"></param>
        /// <param name="mythings"></param>
        /// <returns></returns>

        public int NB_Of_Uses(int indexnb, Item mythings)
        {
            int nb = mythings.unit[indexnb].nbofuses;
            return nb;
        }
        /// <summary>
        /// Returns type of item
        /// </summary>
        /// <param name="indexnb"></param>
        /// <param name="mythings"></param>
        /// <returns></returns>
        public string Type(int indexnb, Item mythings)
        {
            string nb = mythings.unit[indexnb].type;
            return nb;
        }
        /// <summary>
        /// Easter egg. Rubber duck does "Quack"
        /// </summary>
        /// <param name="indexnb"></param>
        /// <param name="mythings"></param>
        public void DoQuack(int indexnb, Item mythings)//easter egg
        {
            string nb = mythings.unit[indexnb].nameofitem;
            if(nb== "Rubber Duck")
            {
                Console.WriteLine("QUACK!");
                System.Threading.Thread.Sleep(2000);
            }
           

        }

    }
}
