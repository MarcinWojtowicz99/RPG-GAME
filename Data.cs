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
       
        public string[] Gamedata;
        public string[] gamedata { get { return Gamedata; }set { Gamedata = value; } }
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
            if (Program.autosave == true)
            {
                do
                {
                    string[] filePaths = Directory.GetFiles(path + @"\SaveGame", "*.txt");
                    if (filePaths.Length < 10)
                    {
                        DateTime localDate = DateTime.Now;
                        string date0 = localDate.ToString();
                        string date = date0.Replace(':', '.');
                        string finalpathforsave = path + "\\" + Gamedata[0] + date + ".txt";
                        string finalpathforsave3 = path + "\\SaveGame\\[AUTO]" + Gamedata[0] + date + ".txt";
                        StreamWriter writer = new StreamWriter(finalpathforsave);
                        for (int i = 0; i < Gamedata.Length; i++)
                        {

                            if (Gamedata[i] != null)
                            {
                                writer.WriteLine(Gamedata[i]);

                            }
                            else
                            {
                                break;
                            }

                        }
                        writer.Close();
                        EncryptFile(finalpathforsave, finalpathforsave3);
                        File.Delete(finalpathforsave);
                        break;
                    }
                    else
                    {
                        Console.Clear();

                        do
                        {
                            Console.Clear();
                            string[] filePathsUpdate = Directory.GetFiles(path + @"\SaveGame", "*.txt");
                            Console.WriteLine("Choose file or files to delete and press ESC to continue");
                            for (int i = 0; i < filePathsUpdate.Length; i++)
                            {
                                Console.WriteLine("{0}. {1}", i, filePathsUpdate[i]);
                            }
                            Console.Write("Select: ");
                            var A = Console.ReadKey();
                            if (A.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            int pather = CharUnicodeInfo.GetDecimalDigitValue(A.KeyChar);
                            if (pather <= filePathsUpdate.Length && pather >= 0)
                            {
                                try
                                {
                                    File.Delete(filePathsUpdate[pather]);
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("Unknown path!");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Unknown path!");
                                Console.ReadKey();
                            }
                        } while (true);

                    }
                } while (true);
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
                if (pather <= filePaths.Length && pather >= 0)
                {


                    try
                    {
                        DecryptFile(filePaths[pather], finalpathforsave3);
                        StreamReader reader = new StreamReader(finalpathforsave3);
                        for (int i = 0; i < Gamedata.Length; i++)
                        {

                            Gamedata[i] = reader.ReadLine();


                        }
                        reader.Close();
                    }
                    catch (FormatException e)
                    {
                        Exception bex = new Exception("The file is corrupted!", e);
                        throw bex;
                    }
                    catch(IndexOutOfRangeException)
                    {
                        Console.WriteLine("Wrong sign");
                        Console.ReadKey();
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);//start new process

                        //Close the current process
                        Environment.Exit(0);
                    }
                    File.Delete(finalpathforsave3);
                }
                else
                {
                    Console.WriteLine("Unknown path!");
                    Console.ReadKey();
                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);

                    //Close the current process
                    Environment.Exit(0);

                }
                }
                else if(filePaths.Length == 1)
                {
                    string finalpathforsave3 = path + "NEW.txt";
                    DecryptFile(filePaths[0], finalpathforsave3);
                    try
                    {
                        StreamReader reader = new StreamReader(finalpathforsave3);
                        for (int i = 0; i < Gamedata.Length; i++)
                        {

                        if (reader.ReadLine() != null)
                        {
                            Gamedata[i] = reader.ReadLine();
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
                Console.ReadKey();
                Console.Clear();
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);//start new process

                //Close the current process
                Environment.Exit(0);
            }

            
            
            
        }
        public void SaveGame(Data data)
        {
            
           
            Console.WriteLine("Type the name of your save: ");
            string date0 = Console.ReadLine();
            string date1 = date0.Replace("/", "[UNACCETABLE SIGN]");
            string date = date1.Replace(":", "[UNACCETABLE SIGN]");
            string date2 = date.Replace(@"\" , "[UNACCETABLE SIGN]");
            string date3 = date2.Replace("*", "[UNACCETABLE SIGN]");
            string date4 = date3.Replace("?", "[UNACCETABLE SIGN]");
            string date5 = date4.Replace(">", "[UNACCETABLE SIGN]");
            string date6 = date5.Replace("<", "[UNACCETABLE SIGN]");
            string date7 = date6.Replace("|", "[UNACCETABLE SIGN]");
            string nameoffile = date7.Replace('"', 'Q');

            
            string finalpathforsave = path +@"\"+ nameoffile + ".txt";
            string finalpathforsave2 = path + @"\SaveGame\" + nameoffile + ".txt";

            StreamWriter writer = new StreamWriter(finalpathforsave);
            for (int i = 0; i < Gamedata.Length; i++)
            {

                if (Gamedata[i] != null)
                {
                    writer.WriteLine(Gamedata[i]);
                    
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
            for (int i = 0; i < Gamedata.Length; i++)
            {
                if (Gamedata[i] == null)
                {
                    a = i;
                    break;
                }
                   
            }
            return a;
        }

        public Data(int nbofdata)
        {
            Gamedata = new String[nbofdata];
            gamedata = Gamedata;

        }
        //public void AddtoData(string info,int order)
        //{

        //    gamedata[order] = info;
        //}
        public string[] ReadDataSeries(int order1, int order2)
        {
            string[] toReturn = new String[order2-order1];
            for(int i=0; i<toReturn.Length;i++)
            {
                toReturn[i] = Gamedata[i];   
            }
            return toReturn;
        }
        public void ReadData(Data data, Character user, Warrior warrior, Item mythings)
        {
            string[] datas = new String[data.gamedata.Length];
            
            for (int i=2; i<data.gamedata.Length-2;i++)
            {
                    string[] reader = new String[2];
                    reader = data.gamedata[i].Split(' ');
                    datas[i] = reader[1];
            }
            

            user.Money_player = Convert.ToInt32(datas[2]);
            user.Score=Convert.ToInt32(datas[3]);
            user.Actual_hp = Convert.ToInt32(datas[4]);

            string[] eq = new string[user.Equipment.Length / 2];
            eq = datas[5].Split(';');
            for (int i=0; i<user.Equipment.Length/2;i++)
            {
                string[] eq2 = new String[2];
                eq2 = eq[i].Split('-');
                user.Equipment[i, 0] = Convert.ToInt32(eq2[0]);
                user.Equipment[i, 1] = Convert.ToInt32(eq2[1]);
            }
                if(datas[6] != "No")
            {
                warrior.Equip_Character(Convert.ToInt32(datas[6]),mythings,warrior,data);
            }
        }
    }
}
