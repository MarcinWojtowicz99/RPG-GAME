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
            equipment = Equipment();
           
        }
        
        public void Teleport()
        {
            Console.WriteLine("Where would you like to teleport?");

        }
   
        public void ViewEquipment(Item mythings, Sorcerer user, Data data, Enemy Mermaid, Enemy Dragon)
        {
            bool endloop = true;
            do
            {
                Console.WriteLine("Your equipment: ");
                for (int i = 0; i < equipment.Length/2 ; i++)
                {

                    if (equipment[i, 0] != 0)
                    {
                        Console.Write("{0}. ", i);
                        int index = equipment[i, 0];
                        mythings.Item_Name(index, mythings);
                        Console.WriteLine("Uses: " + equipment[i, 1]);
                    }
                }
                bool toReturn = true;
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
                            user.Use(nb);
                            toReturn = false;
                            break;
                        case 'n':
                            Warrior war = new Warrior("FAKE", 0, 0, 0, 0,0);
                            Program.DefaultMenu(user,war, data, mythings, Mermaid, Dragon);
                            Console.Clear();
                            

                            break;


                        default:
                            toReturn = false;
                            endloop = false;
                            Console.WriteLine("Unknown option!");
                            break;
                    }
                } while (toReturn != true);

            } while (endloop != true);


        }


    }
}
