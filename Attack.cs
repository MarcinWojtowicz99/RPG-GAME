using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Attack
    {
       public string name; public int attackvalue; public string specialinfo;
       
        
        public Attack(string name, int attackvalue, string specialinfo)
        {
            this.name = name;//name of attack displayed on a screen during attack
            this.attackvalue = attackvalue;//basic attack value for monsters
            this.specialinfo = specialinfo;    //stores special quote for attack
        }
    }
}
