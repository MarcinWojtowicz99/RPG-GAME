using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Sorcerer:Character
    {
        public Sorcerer(string name, int basic_hp, int maxequipment) : base(name, basic_hp, maxequipment)
        {

           
        }
        public static void Teleport()
        {
            Console.WriteLine("Where would you like to teleport?");

        }
        
        
    }
}
