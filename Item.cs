using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
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


        public Item(int nbofitems)
        {
            this.nbofitems = nbofitems;
            unit = new Unit[nbofitems];
        }
        public Item(Item things)
        {
            this.unit = new Unit[things.unit.Length];
            for (int i = 1; i < unit.Length + 1; i++)
            {
                unit[i] = things.unit[i];
            }
        }
        public void Stack(Unit units)
        {
            for (int i = 0; i < unit.Length; i++)
            {
                if (unit[i] == null)
                {
                    unit[i] = units;
                }
            }
        }

        public void Item_Name(int indexnb,Item mythings)
        {
            Console.Write(mythings.unit[indexnb].nameofitem);
        }

        public void Give(int indexnb, Character user, Item mythings)
        {
            user.AddToEquipment(indexnb, mythings,user);
            Console.Write("You received: ");
            Item_Name(indexnb, mythings);
            Console.WriteLine();
        }
        public int DamageOrHeal(int indexnb, Item mythings)
        {
            return mythings.unit[indexnb].damageorhealvalueafteruse;
        }
        

        public int NB_Of_Uses(int indexnb, Item mythings)
        {
            int nb = mythings.unit[indexnb].nbofuses;
            return nb;
        }
        public string Type(int indexnb, Item mythings)
        {
            string nb = mythings.unit[indexnb].type;
            return nb;
        }
        
        
    }
}
