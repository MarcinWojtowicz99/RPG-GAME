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

        int counter = 0;
        int index;
        int nbofuses;
        string nameofitem;
        public int Nb_Of_Items { get { return counter; } }

        string name;
        int numberofitems;
        public string Name { get { return name; } }
        double total;
        public int Numberofitems { get { return numberofitems; } }
      
        public double totalvalue { get { return total; } }
        public Item[] thing;
        

        public Item(string name, int numberofitems, string nameofitem, int value_min, int value_max, int damageorhealvalueafteruse, int nbofuses)
        {
            this.numberofitems = numberofitems;
            this.nbofuses = nbofuses;
            this.nameofitem = nameofitem;
            this.value_min = value_min;
            this.value_max = value_max;
            this.damageorhealvalueafteruse = damageorhealvalueafteruse;
            counter++;
            this.name = name;
            thing = new Item[numberofitems];

        }
        public Item(Item things)
        {
            this.name = things.name;
            this.nbofuses = things.nbofuses;
            this.nameofitem = things.nameofitem;
            this.value_min = things.value_min;
            this.value_max = things.value_max;
            this.damageorhealvalueafteruse = things.damageorhealvalueafteruse;

            this.thing = new Item[things.thing.Length];
            for (int i = 1; i < thing.Length+1; i++)
            {
                thing[i] = things.thing[i];
            }
        }

        public void Item_Name(int indexnb)
            {
            Console.Write(thing[indexnb].name);
            }
        
        public void Give(int indexnb, Character user)
        {
            user.AddToEquipment(indexnb);
            Console.WriteLine("You received ");
            Item_Name(indexnb);
        }
        public int DamageOrHeal(int indexnb)
        {
            return thing[indexnb].damageorhealvalueafteruse;
        }
        public int Generate_Value(int indexnb)
        {
            Random rnd = new Random();
            int random = rnd.Next(thing[indexnb].value_min, thing[indexnb].value_max);
            return random;
        }
       
        public int NB_Of_Uses(int indexnb)
        {
           int nb = thing[indexnb].nbofuses;
            return nb;
        }
        
    }
}
