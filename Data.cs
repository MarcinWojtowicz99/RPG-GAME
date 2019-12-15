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
    /// <summary>
    /// Stores and manages data
    /// </summary>
    class Data
    {

        public string[] Gamedata;
        public string[] gamedata { get { return Gamedata; } set { Gamedata = value; } }
        public static string path = Directory.GetCurrentDirectory(); //universal way to save file and load
        /// <summary>
        /// Encrypts file
        /// </summary>
        /// <param name="inputFile">Path of file to encrypt</param>
        /// <param name="outputFile">Path to save the encrypted file</param>
        private static void EncryptFile(string inputFile, string outputFile)
        {

            using (RijndaelManaged aes = new RijndaelManaged())//new class of encryption
            {
                byte[] key = ASCIIEncoding.UTF8.GetBytes("1234512345678976");//encryption keycode

                byte[] IV = ASCIIEncoding.UTF8.GetBytes("1234512345678976");//used to achieve better security. This key everytime generates another value

                using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))//locating the output file and file to create new filestream
                {
                    using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))//creates particular cipher encryptor using our keys
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))//creates new class for cryptpgraphic transformation of bytes
                        {
                            using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))//set input filestream to read
                            {
                                int data;
                                while ((data = fsIn.ReadByte()) != -1)//set readposition for particular byte
                                {
                                    cs.WriteByte((byte)data);//write byte to ouput
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Decrypts file
        /// </summary>
        /// <param name="inputFile">Path of file to encrypt</param>
        /// <param name="outputFile">Path to save the encrypted file</param>
        private static void DecryptFile(string inputFile, string outputFile)
        {

            using (RijndaelManaged aes = new RijndaelManaged())//new class of encryption
            {
                byte[] key = ASCIIEncoding.UTF8.GetBytes("1234512345678976");//encryption keycode
                byte[] IV = ASCIIEncoding.UTF8.GetBytes("1234512345678976");//used to achieve better security. This key everytime generates another value

                using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))//load input file
                {
                    using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))//load output file
                    {
                        using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))//create decryptor based on our keys
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))//read data
                            {
                                int data;
                                while ((data = cs.ReadByte()) != -1)//load readposition of byte based on key
                                {
                                    fsOut.WriteByte((byte)data);//write byte to output file
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Deletes file
        /// </summary>
        /// <param name="filePaths">Path to delete the file</param>
        public static void ToDelete(string[] filePaths)
        {
            while (true)
            {
                Console.Clear();
                string[] filePathsUpdate = Directory.GetFiles(path + @"\SaveGame", "*.txt");
                Console.WriteLine("Choose file or files to delete and press ENTER to continue");
                for (int i = 0; i < filePathsUpdate.Length; i++)
                {
                    string[] filePathsToShow = filePaths[i].Split(@"\");
                    Console.WriteLine("{0}. {1}", i, filePathsToShow[filePathsToShow.Length - 1]);
                }
                Console.Write("Select: ");
                var A = Console.ReadKey();
                if (A.Key == ConsoleKey.Enter)
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
            }
        }
        /// <summary>
        /// Autosave feature
        /// </summary>
        /// <param name="data">Save data stored in object</param>
        public void AutoSaveGame(Data data)
        {
            
            if (Program.autosave == true)
            {
                
                    string[] filePaths = Directory.GetFiles(path + @"\SaveGame", "*.txt");
                   
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
            }
        }
        /// <summary>
        /// Loads savegame
        /// </summary>
        /// <param name="pather">Filepath to read</param>
        /// <param name="finalpathforsave3">Filepath to decode and read</param>
        public void LoadSave(string pather,string finalpathforsave3)
        {
            try
            {
                DecryptFile(pather, finalpathforsave3);
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
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Wrong sign");
                Console.ReadKey();
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);//start new process

                //Close the current process
                Environment.Exit(0);
            }
            File.Delete(finalpathforsave3);
        }
        /// <summary>
        /// Loads savegame. If there is more than 10 savegames user has to delete files and then choose the right ones.
        /// </summary>
        /// <param name="data">Save data stored in object</param>
        public void LoadGame(Data data)
        {
            string[] filePaths = Directory.GetFiles(path + @"\SaveGame", "*.txt");
            bool ToReturn = true;
            do
            {
                filePaths = Directory.GetFiles(path + @"\SaveGame", "*.txt");
                if (filePaths.Length > 10)
                {
                        ToDelete(filePaths);
                }
                } while (filePaths.Length > 10);
            Console.Clear();
                    if (filePaths.Length > 1)
                    {

                        Console.WriteLine("Which save would you like to read?");
                        for (int i = 0; i < filePaths.Length; i++)
                        {
                            string[] filePathsToShow = filePaths[i].Split(@"\");
                            Console.WriteLine("{0}. {1}", i, filePathsToShow[filePathsToShow.Length - 1]);
                        }
                        Console.Write("Select: ");
                        char A = Console.ReadKey().KeyChar;
                        int pather = CharUnicodeInfo.GetDecimalDigitValue(A);//convert key character nb to integer value
                        string finalpathforsave3 = path + @"\NEW.txt";
                        Console.WriteLine();
                        if (pather <= filePaths.Length && pather >= 0)
                        {
                    try { data.LoadSave(filePaths[pather], finalpathforsave3); }
                    catch(IndexOutOfRangeException)
                    {
                        Console.WriteLine("Unknown path!");
                    }
                    
                            
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

                    else if (filePaths.Length == 1)
                    {
                        string finalpathforsave3 = path + "NEW.txt";
                data.LoadSave(filePaths[0], finalpathforsave3);
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
        /// <summary>
        /// Saves game
        /// </summary>
        /// <param name="data">Temporary data to save</param>
        public void SaveGame(Data data)
        {
            do { 
                string[] filePaths = Directory.GetFiles(path + @"\SaveGame", "*.txt");
            if (filePaths.Length < 10)
            {

                Console.WriteLine("Type the name of your save: ");
                string date0 = Console.ReadLine();
                string date1 = date0.Replace("/", "[UNACCETABLE SIGN]");
                string date = date1.Replace(":", "[UNACCETABLE SIGN]");
                string date2 = date.Replace(@"\", "[UNACCETABLE SIGN]");
                string date3 = date2.Replace("*", "[UNACCETABLE SIGN]");
                string date4 = date3.Replace("?", "[UNACCETABLE SIGN]");
                string date5 = date4.Replace(">", "[UNACCETABLE SIGN]");
                string date6 = date5.Replace("<", "[UNACCETABLE SIGN]");
                string date7 = date6.Replace("|", "[UNACCETABLE SIGN]");
                string nameoffile = date7.Replace('"', 'Q');


                string finalpathforsave = path + @"\" + nameoffile + ".txt";
                string finalpathforsave2 = path + @"\SaveGame\" + nameoffile + ".txt";

                StreamWriter writer = new StreamWriter(finalpathforsave);
                for (int i = 0; i < Gamedata.Length; i++)
                {

                    if (Gamedata[i] != null) writer.WriteLine(Gamedata[i]);
                    
                    else break;
                    
                }

                writer.Close();
                EncryptFile(finalpathforsave, finalpathforsave2);
                File.Delete(finalpathforsave);
                    break;
            }

            else ToDelete(filePaths);
                    

                
        } while (true);

        }
        /// <summary>
        /// Data constructor
        /// </summary>
        /// <param name="nbofdata">Number of data to be stored</param>
        public Data(int nbofdata)
        {
            Gamedata = new String[nbofdata];
            gamedata = Gamedata;
        }
        /// <summary>
        /// Reads data and saves to object
        /// </summary>
        /// <param name="data">Temporary data object</param>
        /// <param name="user">Object used for assigning data from savefile</param>
        /// <param name="warrior">Object used for assigning data from savefile</param>
        /// <param name="mythings">Used for equipping the warrior</param>
        /// <param name="user_Sorcerer">Object used for assigning data from savefile</param>
        /// <param name="dragon">For assigning number of dragons killed</param>
        /// <param name="human">For assigning number of people killed</param>
        /// <param name="mermaid">For assigning number of mermaids killed</param>
        public void ReadData(Data data, Character user, Warrior warrior, Item mythings, Sorcerer user_Sorcerer, Enemy dragon, Enemy human, Enemy mermaid)
        {
            string[] datas = new String[11];
            
            for (int i=2; i<11;i++)
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
                warrior.Equip_Character(Convert.ToInt32(datas[6]),mythings,warrior,data, user);
            }
                if(warrior== null)
            {
                user_Sorcerer.DemonsKilled=Convert.ToInt32(datas[10]);
            }
                mermaid.EnemyKilled= Convert.ToInt32(datas[8]);
            dragon.EnemyKilled = Convert.ToInt32(datas[7]);
            human.EnemyKilled = Convert.ToInt32(datas[9]);
        }
    }
}
