using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    class Warrior:Character
    {
        int hpboost;
        int[,] equipment;
        public Warrior(string name, int basic_hp, int maxequipment, int hpboost, int basic_damage, int startmoney) : base(name, basic_hp, maxequipment, basic_damage, startmoney)
        {
            this.hpboost = hpboost;
            equipment = Equipment;
            BoostMe();
        }
        public int BoostMe()
        {
            return actual_hp += hpboost;
        }
        public bool Equip_Character(int index, Item mythings, Warrior warrior)
        {
            warrior.Equip = index;
            //weapondamage= mythings.unit[index].damageorhealvalueafteruse;
            return true;
        }
        public bool UnEquip(Warrior warrior)
        {
            warrior.Equip = 0;
            return false;
        }
    }
}
