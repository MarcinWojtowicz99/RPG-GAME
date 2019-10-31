using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Sorcerer:Character
    {
        public Sorcerer(string weapon, string name, int hp) : base(name, hp)
        {
            weapon = "wand";
            
        }
        public void Teleport()
        {

        }
    }
}
