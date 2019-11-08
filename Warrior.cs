using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Warrior:Character
    {
        int hpboost;
        public Warrior(string name, int basic_hp, int maxequipment, int hpboost, int basic_damage) : base(name, basic_hp, maxequipment, basic_damage)
        {
            this.hpboost = hpboost;
            basic_hp += hpboost;
        }
        

    }
}
