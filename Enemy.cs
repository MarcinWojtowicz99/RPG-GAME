using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Enemy
    {
        int enemy_hp;
        public int HP { get { return enemy_hp; } }
        int counter;
        public int Nb_Of_Items { get { return counter; } }
        int enemy_score;
        public int SCORE { get { return enemy_score; } }
        string enemy_attack;
        public Enemy(int enemy_hp,int enemy_score)
        {
            this.enemy_hp = enemy_hp;
            this.enemy_score = enemy_score;
        }
        public void Attack_Character()
        {

        }
        public void Drop_Item()
        {
            Random rnd = new Random(counter);
            int ind = rnd.Next();
            Item.Give(ind);

        }
        public int attack_enemy()
        {
            return attack_value;
        }

    }
}
