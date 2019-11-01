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
        int counter=0;
         int index;
        int nbofuses;
        string nameofitem;
        public int Nb_Of_Items {get { return counter; }}
        
        string name;
        int numberofitems;
        public string Name { get { return name; } }
        double total;
        public int Numberofitems { get { return numberofitems; } }
        public double totalvalue { get { return total; } }
         Item[] thing;
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
            for (int i = 0; i < thing.Length; i++)
            {
                thing[i] = things.thing[i];
            }
        }
       
        
        
        public static void Use(int nbofuse,int indexnb)
        {
            if(nbofuse!=thing[indexnb].nbofuses)
            {

            }
        }
        
        public static void Show(int indexnb)
        {
            Console.WriteLine(thing[indexnb]);
        }
        public static int Cost()
        {
            return thing[]
        }
    }
}
