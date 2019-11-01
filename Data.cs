using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Data
    {
        public static string[] data;
        public static string[] gamedata{get{return data;} }
        public static string[] AddtoData(string info,int order)
        {
            gamedata[order] = info;
            return gamedata;
        }
        //public static string[] ReadData(string info, int order)
        //{
        //    string name = gamedata[0];
        //    string classification=gamedata[1];

        //}
    }
}
