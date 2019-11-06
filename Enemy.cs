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

        public Attack[] attack;
        int totalnbofattacks;
        public Enemy(int enemy_hp,int enemy_score, int totalnbofattacks)
        {
            this.enemy_hp = enemy_hp;
            this.enemy_score = enemy_score;
            attack = new Attack[totalnbofattacks];
        }
        public Enemy(Enemy enemy)
        {
            this.attack = new Attack[enemy.attack.Length];
            this.enemy_hp = enemy.enemy_hp;
            this.enemy_score = enemy.enemy_score;
            for (int i = 0; i < attack.Length; i++)
            {
                attack[i] = enemy.attack[i];

            }

        }
        public Attack[] attacks;
        public void Stack(Attack attack)
        {
            for (int i = 0; i < attacks.Length; i++)
            {
                if (attacks[i] == null)
                {
                    attacks[i] = attack;
                }
            }
        }
        public void Attack_Character()
        {
            Random rnd = new Random();
            int random = rnd.Next(attacks.Length);
            int value = attacks[random].attackvalue;

        }
        public void Drop_Item()
        {
            Random rnd = new Random(counter);
            int ind = rnd.Next();
            Item.Give(ind);

        }
        //public int attack_enemy()
        //{
        //    return attack_value;
        //}

    }
}
