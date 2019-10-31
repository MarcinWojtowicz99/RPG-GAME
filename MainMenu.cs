using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RPG_GAME
{
    class MainMenu
    {
        public void SaveGame(string fileName, Results results)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(results.ToString());
            writer.Close();
        }
        public void LoadGame(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            try
            {
                 // FileNotFound, Argument,  ... (use documentation)
                string descr = reader.ReadLine();
                int count = int.Parse(reader.ReadLine());
                double[] samples = new double[count];
                for (int i = 0; i < count; i++)
                {
                    samples[i] = double.Parse(reader.ReadLine());
                }
            }
            catch (FormatException e)
            {
                Exception bex = new Exception("The file is corrupt!", e);
                throw bex;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("You have no savegame :c");
            }
            finally
            {
                reader.Close();
            }

            
        }
        public void Options()
        {
            if()
            Console.WriteLine("Autosave is "+ );
        }
        public void NewGame(string filename)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.Close();
            Console.WriteLine("-Hello traveler, what's your name?");
            string name = Console.ReadLine();
            string classification;
            Console.WriteLine("Nice to meet you, {0}! I'm Neth and let me introduce you our kingdom of Omaghan. During your journey you can serve our queen as a soldier or as a warrior. By the way, could you tell me what's your actual speciality?");
            do
            {
                Console.WriteLine("(Type \"a\" to say you're a sorcerer or \"b\" to say that you're a sorcerer)");
                classification = Console.ReadLine();
            } while (classification == "b" || classification == "a");
            if (classification == "a")
                Warrior warrior = new Warrior(name);
            if (classification == "b")
                Sorcerer sorcerer = new Sorcerer(name);


        }
    }
}
