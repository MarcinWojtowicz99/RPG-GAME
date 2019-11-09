using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RPG_GAME
{
    class Data
    {
       
        public string[] gamedata;
        public static bool autosave = true;
        public static bool Autosave { get { return autosave; } }
        public static string path = Directory.GetCurrentDirectory(); //universal way to save file and load
       

        public void AutoSaveGame(Data data)
        {
            if (autosave == true)
            {
                DateTime localDate = DateTime.Now;
                string date0 = localDate.ToString();
                string date1 = date0.Replace(' ', '_');
                string date = date1.Replace(':', '.');
                string finalpathforsave = path + "\\" + gamedata[0] + date + ".txt";
                StreamWriter writer = new StreamWriter(finalpathforsave);
                for (int i = 0; i < gamedata.Length; i++)
                {
                    writer.WriteLine(gamedata[i]);
                }

                writer.Close();
            }
        }
        public void LoadGame(Data data)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(path, "*.txt");
                if (filePaths.Length > 1)
                {
                    Console.WriteLine("Which save would you like to read? Type only the name of text file .txt");
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        Console.WriteLine(filePaths[i]);
                    }
                    string pather = Console.ReadLine();
                    string finalpath = path + "\\" + pather + ".txt";
                    StreamReader reader = new StreamReader(finalpath);
                    for (int i = 0; i < gamedata.Length; i++)
                    {
                        gamedata[i] = reader.ReadLine();
                    }
                    reader.Close();

                }
                else
                {
                    StreamReader reader = new StreamReader(filePaths[0]);
                    for (int i = 0; i < gamedata.Length; i++)
                    {
                        gamedata[i] = reader.ReadLine();
                    }
                    reader.Close();

                    //string descr = reader.ReadLine();
                    //int count = int.Parse(reader.ReadLine());
                    //double[] samples = new double[count];
                    //for (int i = 0; i < count; i++)
                    //{
                    //    samples[i] = double.Parse(reader.ReadLine());
                    //}
                }
            }
            catch (FormatException e)
            {
                Exception bex = new Exception("The file is corrupted!", e);
                throw bex;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("You have no savegame :c");
            }
        }
        public void SaveGame(Data data)
        {

            Console.WriteLine("Type the name of your save: ");
            string nameoffile = Console.ReadLine();
            string finalpathforsave = path + "\\" + nameoffile + ".txt";
            StreamWriter writer = new StreamWriter(finalpathforsave);
            for (int i = 0; i < gamedata.Length; i++)
            {
                writer.WriteLine(gamedata[i]);
            }

            writer.Close();

        }
        public Data(int nbofdata)
        {
            gamedata = new String[nbofdata];
        }
        public void AddtoData(string info,int order)
        {
            gamedata[order] = info;
            //return gamedata;
        }
        public string[] ReadDataSeries(int order1, int order2)
        {
            string[] toReturn = new String[order2-order1];
            for(int i=0; i<toReturn.Length;i++)
            {
                toReturn[i] = gamedata[i];   
            }
            return toReturn;
        }
        public string ReadData(int order)
        {
            return gamedata[order];
        }
    }
}
