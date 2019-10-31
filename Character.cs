using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Character
    {
        string name;
        int money;
        int hp;
        int score;
        private string Name{get {return name;} }

        public Character (string name, int hp=500)
        {
            this.name = name;
            hp=500;

        }
        public void checkyourequipment()
        {

        }
        

    }
}
