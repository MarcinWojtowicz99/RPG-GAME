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
            equipment = Equipment();
            BoostMe();
        }

        public int BoostMe()
        {
            return actual_hp += hpboost;
        }
       
        public void ViewEquipment(Item mythings, Warrior user_Warrior, Data data, Enemy Mermaid, Enemy Dragon)
        {
            bool endloop = true;
            do
            {
                Console.WriteLine("Your equipment: ");
                for (int i = 0; i < equipment.Length / 2; i++)
                {

                    if (equipment[i, 0] != 0)
                    {
                        Console.Write("{0}. ", i);
                        int index = equipment[i, 0];
                        mythings.Item_Name(index, mythings);
                        Console.WriteLine("Uses: " + equipment[i, 1]);
                    }
                }
                bool toReturn = false;
                do
                {

                    Console.WriteLine("Wanna use some equipment? y/n");


                    Console.Write("Select: ");
                    char choice = Console.ReadKey().KeyChar;
                    Console.WriteLine();


                    switch (choice)
                    {
                        case 'y':
                            Console.WriteLine("Type number of equipment to equip or use: ");
                            int nb = CharUnicodeInfo.GetDecimalDigitValue(Console.ReadKey().KeyChar);
                            int index = equipment[nb, 0];
                            if (mythings.Type(index, mythings) == "Sword")
                            {
                                Equip_Character(index, mythings);
                            }
                            else
                            {
                                Heal(index, mythings,data);
                            }
                            user_Warrior.Use(nb);

                            break;
                        case 'n':
                            Sorcerer sorc = new Sorcerer("FAKE",0,0,0,0);
                            Program.DefaultMenu(sorc,user_Warrior, data, mythings,Mermaid, Dragon);
                            Console.Clear();


                            break;


                        default:
                            toReturn = true;
                            Console.WriteLine("Unknown option!");
                            break;
                    }
                } while (toReturn != true);

            } while (endloop == true);


        }

    }
}
