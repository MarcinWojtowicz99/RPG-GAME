using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Enemy
    {
        int enemy_hp;
        public int HP { get { return enemy_hp; } }
        
        int enemy_score;
        public int SCORE { get { return enemy_score; } }
        string enemy_attack;
        public Enemy(int enemy_hp,int enemy_score,string enemy_attack)
        {

        }

    }
}
