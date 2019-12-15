using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    /// <summary>
    /// Creates an item
    /// </summary>
    class Unit
    {
       public string nameofitem; public int value_min; public int value_max; public int damageorhealvalueafteruse; public int nbofuses; public string type;
        /// <summary>
        /// Item constructor
        /// </summary>
        /// <param name="nameofitem">Name which can be displayed in equipment or shop</param>
        /// <param name="value_min">minimal possible item price</param>
        /// <param name="value_max">maximal possible item price</param>
        /// <param name="damageorhealvalueafteruse">value of damage in case of swords and heal in case of potions</param>
        /// <param name="nbofuses">usually for selling or receiving greater amount of items than 1</param>
        /// <param name="type">Defines item</param>
        public Unit( string nameofitem, int value_min, int value_max, int damageorhealvalueafteruse, int nbofuses, string type)
        {
            
            this.nbofuses = nbofuses;
            this.nameofitem = nameofitem;
            this.value_min = value_min; 
            this.value_max = value_max;
            this.damageorhealvalueafteruse = damageorhealvalueafteruse;
            this.type = type;
        }
        /// <summary>
        /// Generates value of item for shopkeeper every time player goes to the shop
        /// </summary>
        /// <returns>Returns random cost of item in the given limitations</returns>
        public int Generate_Value()
        {
            Random rnd = new Random();
            int random = rnd.Next(value_min, value_max);
            return random;
        }
    }
}
