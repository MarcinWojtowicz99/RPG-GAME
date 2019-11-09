using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Enemy
    {
        int enemy_hp;
        public int HP { get { return enemy_hp; } }
        public int enemy_actualhp;
        public int Enemy_Actual_HP { get { return enemy_actualhp; } }
        int counter;
        public int Nb_Of_Items { get { return counter; } }
        int enemy_score;
        public int SCORE { get { return enemy_score; } }
        

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
       // public Attack[] attacks;
        public void Stack(Attack attackss)
        {
            for (int i = 0; i < attack.Length; i++)
            {
                if (attack[i] == null)
                {
                    attack[i] = attackss;
                }
            }
        }
        public int actual_hp;
        public int Actual_hp { get { return actual_hp; } }
        public void Attack_Character()
        {
            Random rnd = new Random();
            int random = rnd.Next(attack.Length);
            int value = attack[random].attackvalue;
            actual_hp -= value;

        }
        public void Drop_Item(Character user, Item mythings)
        {
            Random rnd = new Random(counter);
            int ind = rnd.Next();
            user.AddToEquipment(ind,mythings);

        }
        public int WhatsMyActualHP()
        {
            return actual_hp;
        }
        public void TakeDamage(int value)
        {
            actual_hp= - value;
        }
        //public int attack_enemy()
        //{
        //    return attack_value;
        //}

    }
}
