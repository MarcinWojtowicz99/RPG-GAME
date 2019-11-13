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
            this.name = name;
            this.attackvalue = attackvalue;
            this.specialinfo = specialinfo;        }
    }
}
