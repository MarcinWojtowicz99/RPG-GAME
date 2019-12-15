using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    /// <summary>
    /// Creates an attack
    /// </summary>
    class Attack
    {
       public string name; public int attackvalue; public string specialinfo;

        /// <summary>
        /// Attack constructor
        /// </summary>
        /// <param name="name">name of attack displayed on a screen during attack</param>
        /// <param name="attackvalue">basic attack value for monsters</param>
        /// <param name="specialinfo">stores special quote for attack</param>
        public Attack(string name, int attackvalue, string specialinfo)
        {
            this.name = name;
            this.attackvalue = attackvalue;
            this.specialinfo = specialinfo; 
        }
    }
}
