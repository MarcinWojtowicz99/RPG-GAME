using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Warrior:Character
    {
        
        public Warrior(string name, int hp=500) :base(name,hp)
        {
           string weapon = "sword";
            hp = hp + 200;
        }
        public void Fight()
        {

        }
    }
}
