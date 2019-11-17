using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Unit
    {
       public string nameofitem; public int value_min; public int value_max; public int damageorhealvalueafteruse; public int nbofuses; public string type;

        public Unit( string nameofitem, int value_min, int value_max, int damageorhealvalueafteruse, int nbofuses, string type)
        {
            
            this.nbofuses = nbofuses;//usually for selling or receiving greater amount of items than 1
            this.nameofitem = nameofitem;//Name which can be displayed in equipment or shop
            this.value_min = value_min; //minimal possible item price
            this.value_max = value_max;//maximal possible item price
            this.damageorhealvalueafteruse = damageorhealvalueafteruse;//value of damage in case of swords and heal in case of potions
            this.type = type;//Defines item
        }
        public int Generate_Value()//Generates value of item for shopkeeper every time player goes to the shop
        {
            Random rnd = new Random();
            int random = rnd.Next(value_min, value_max);
            return random;
        }
    }
}
