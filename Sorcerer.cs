using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Sorcerer:Character
    {
        public Sorcerer(string name, int basic_hp, int maxequipment, int basic_damage) : base(name, basic_hp, maxequipment,basic_damage)
        {

           
        }
        public void Teleport()
        {
            Console.WriteLine("Where would you like to teleport?");

        }
        
        
    }
}
