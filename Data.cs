using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
namespace RPG_GAME
{
    class Data
    {
       
        public string[] gamedata;
        public static bool autosave = true;
        public static bool Autosave { get { return autosave; } }
        public static string path = Directory.GetCurrentDirectory(); //universal way to save file and load



        private static void EncryptFile(string inputFile, string outputFile)
        {

            using (RijndaelManaged aes = new RijndaelManaged())
            {
                byte[] key = ASCIIEncoding.UTF8.GetBytes("1234512345678976");

                /* This is for demostrating purposes only. 
                 * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                byte[] IV = ASCIIEncoding.UTF8.GetBytes("1234512345678976");

                using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                {
                    using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                            {
                                int data;
                                while ((data = fsIn.ReadByte()) != -1)
                                {
                                    cs.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void DecryptFile(string inputFile, string outputFile)
        {
            
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes("1234512345678976");

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes("1234512345678976");

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
          }

        public void AutoSaveGame(Data data)
        {
            if (autosave == true)
            {
                DateTime localDate = DateTime.Now;
                string date0 = localDate.ToString();
                string date1 = date0.Replace(' ', '_');
                string date = date1.Replace(':', '.');
                string finalpathforsave = path + "\\" + gamedata[0] + date + ".txt";
                string finalpathforsave3 = path + "\\SaveGame\\" + gamedata[0] + date + ".txt";
                StreamWriter writer = new StreamWriter(finalpathforsave);
                for (int i = 0; i < gamedata.Length; i++)
                {

                    if (gamedata[i] != null)
                    {
                        writer.WriteLine(gamedata[i]);

                    }
                    else
                    {
                        break;
                    }

                }
                writer.Close();
                EncryptFile(finalpathforsave, finalpathforsave3);
                File.Delete(finalpathforsave);
            }
        }
        public void LoadGame(Data data) 
        {
            
                string[] filePaths = Directory.GetFiles(path + @"\SaveGame", "*.txt");         
                if (filePaths.Length > 1)
                {
                    Console.WriteLine("Which save would you like to read? Type only the name of text file .txt");
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        Console.WriteLine("{0}. {1}",i,filePaths[i]);
                    }
                // int pather = Convert.ToInt32(Console.ReadLine());
                Console.Write("Select: ");
                char A = Console.ReadKey().KeyChar;
                int pather=CharUnicodeInfo.GetDecimalDigitValue(A);//convert key character nb to integer value
                string finalpathforsave3 = path + @"\NEW.txt";
                Console.WriteLine();
                   
                try
                {
                    DecryptFile(filePaths[pather], finalpathforsave3);
                    StreamReader reader = new StreamReader(finalpathforsave3);
                    for (int i = 0; i < gamedata.Length; i++)
                    {
                        
                            gamedata[i] = reader.ReadLine();
                        
                          
                    }
                    reader.Close();
                }
                catch (FormatException e)
                {
                    Exception bex = new Exception("The file is corrupted!", e);
                    throw bex;
                }
                File.Delete(finalpathforsave3);
                    
                }
                else if(filePaths.Length == 1)
                {
                    string finalpathforsave3 = path + "NEW.txt";
                    DecryptFile(filePaths[0], finalpathforsave3);
                    try
                    {
                        StreamReader reader = new StreamReader(finalpathforsave3);
                        for (int i = 0; i < gamedata.Length; i++)
                        {

                        if (reader.ReadLine() != null)
                        {
                            gamedata[i] = reader.ReadLine();
                        }
                        else
                        {
                            break;
                        }

                    }
                        reader.Close();
                        File.Delete(finalpathforsave3);
                    }
                    catch(FormatException e)
                    {
                        Exception bex = new Exception("The file is corrupted!", e);
                        throw bex;
                    }

                   
                }
                else
                {
                    Console.WriteLine("You have no savefile :c");
                }

            
            
            
        }
        public void SaveGame(Data data)
        {
            
           
            Console.WriteLine("Type the name of your save: ");
            string nameoffile = Console.ReadLine();
            
            string finalpathforsave = path +@"\"+ nameoffile + ".txt";
            string finalpathforsave2 = path + @"\SaveGame\" + nameoffile + ".txt";

            StreamWriter writer = new StreamWriter(finalpathforsave);
            for (int i = 0; i < gamedata.Length; i++)
            {

                if (gamedata[i] != null)
                {
                    writer.WriteLine(gamedata[i]);
                    
                }
                else
                {
                    break;
                }
                

                
            }

            writer.Close();
            EncryptFile(finalpathforsave, finalpathforsave2);
            File.Delete(finalpathforsave);

        }
        public int WhereIsNull(Data data)
        {
            int a=0;
            for (int i = 0; i < gamedata.Length; i++)
            {
                if (gamedata[i] == null)
                {
                    a = i;
                    break;
                }
                   
            }
            return a;
        }

        public Data(int nbofdata)
        {
            gamedata = new String[nbofdata];
        }
        public void AddtoData(string info,int order)
        {

            gamedata[order] = info;
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
