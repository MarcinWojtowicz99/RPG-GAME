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
            
            this.nbofuses = nbofuses;
            this.nameofitem = nameofitem;
            this.value_min = value_min;
            this.value_max = value_max;
            this.damageorhealvalueafteruse = damageorhealvalueafteruse;
            this.type = type;
        }
        public int Generate_Value()
        {
            Random rnd = new Random();
            int random = rnd.Next(value_min, value_max);
            return random;
        }
    }
}
