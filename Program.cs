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

        static void Main(string[] args)
        {
             MainMenu.OpenMainMenu();
            Clear();
            Pause();

        }

       
        
        
    }
    }