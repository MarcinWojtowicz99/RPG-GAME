using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RPG_GAME
{
    class Plot
    {
     
        public static string name = Character.Nick();

        public static DateTime localDate = DateTime.Now;
        public static string loc1 = Plot.localDate.ToString();
        public static string loc = loc1.Replace(':', '_');
        public static string path = Directory.GetCurrentDirectory(); //universal way to save file and load
        public static string[] data;
        public static string[] gamedata { get { return data; } }
        public static string fileName = path + "\\" + name + loc + ".txt";
        public static string FILENAME { get { return fileName; } }
      
        
        public static void Prologue(bool autosave)
        {
            Console.Clear(); 
            Console.WriteLine("-Hello traveler, what's your name?");
            string name = Console.ReadLine();
            char classification;
            Console.WriteLine("Nice to meet you, {0}! I'm Neth and let me introduce you our" +
                " kingdom of Omaghan. During your journey you can serve our queen as a soldier or as a warrior. By the way, could you tell me what's your actual speciality?",name);
            Character[] user = new Character[1];
            do
            {
                Console.WriteLine("(Type \"a\" to say you're a sorcerer or \"b\" to say that you're a sorcerer)");

                classification = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (classification == 'a')
                {
                    Warrior soldier = new Warrior(name);
                }
                else if (classification == 'b')
                {
                    Sorcerer mag = new Sorcerer(name);
                }
                else
                {
                    Console.WriteLine("unknown command, try again...");
                }


            } while (classification != 'b' && classification != 'a');
            if (autosave == true)
            {
                Data.AddtoData(name,0);
                if(classification=='b')
                {
                    Data.AddtoData("Class:Sorcerer", 1);
                }
                
                else
                {
                    Data.AddtoData("Class:Warrior", 1);
                }
            }
        }
        public static void MainStory()
        {

        }
        //public void Choice(int dataindex, string question, string optionA, string optionB, string optionC, string optionD)
        //{
        //    char classification;
        //    do
        //    {
        //        Console.WriteLine(question);

        //        classification = Console.ReadKey().KeyChar;
        //        Console.WriteLine();
        //        if (classification == 'a')
        //        {
                    
        //        }
        //        else if (classification == 'b')
        //        {
                    
        //        }
        //        if (classification == 'c')
        //        {

        //        }
        //        else if (classification == 'd')
        //        {

        //        }
        //        else if (classification == 'e')
        //        {

        //        }
        //        else
        //        {
        //            Console.WriteLine("unknown command, try again...");
        //        }


        //    } while (classification != 'b' && classification != 'a' && classification != 'c' && classification != 'd' && classification != 'e' );
        //    if (autosave == true)
        //    {
        //        Data.AddtoData(classification.ToString(), dataindex);
        //    }
        //    static void Checkpoint()
        //    {
        //        if (autosave == true)
        //        {

        //            StreamWriter writer = new StreamWriter(fileName);
        //            for (int i = 0; i < gamedata.Length; i++)
        //            {
        //                writer.WriteLine(gamedata[i]);
        //            }

        //            writer.Close();


        //        }
        //    }
       // }
    }
}
