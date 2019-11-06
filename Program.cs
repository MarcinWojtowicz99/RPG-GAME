using System;
using System.IO;

namespace RPG_GAME
{
    class Program
    {
        static void Pause()
        {
            Console.ReadKey();
        }
        static void Clear()
        {
            Console.Clear();
        }
        public static int basic_hp;
        public static int Basic_hp { get { return basic_hp; } }

        static void Main(string[] args)
        {
             MainMenu.OpenMainMenu();
            //load nemesis and their attacks
            Enemy Dragon = new Enemy(200, 1000, 3);
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Dragon.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Enemy Mermaid = new Enemy(100,350,3);
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Mermaid.Stack(new Attack("Dragon fire", 300, "THAT'S A LOT OF DAMAGE!"));
            Enemy Human = new Enemy(basic_hp, 100, 1);
            Human.Stack(new Attack("Drunk punch", 100, "I'll show you my real power!"));
            //listofnemesis


            //listofitems

            //below <20 non-weapons items
            //item index cannot equal zero

            //listofattacksforwarrior



            //listofattacksforsorcerer

        }




    }
    }