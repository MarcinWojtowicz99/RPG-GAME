using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    class Sorcerer:Character
    {
        int[,] equipment;
        public Sorcerer(string name, int basic_hp, int maxequipment, int basic_damage, int startmoney) : base(name, basic_hp, maxequipment,basic_damage,startmoney)
        {
            equipment = Equipment;
        }
        public void Teleport()
        {
            Console.WriteLine("Where would you like to teleport?");
        }
        public static double Sorcerer_Spells(double value,int type)
        {
            if (type == 1)
            {
               

            }
            else if (type == 2)
            {
                

            }
            else
            {


               
            }


            return value;
        }
    }
}
