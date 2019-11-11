using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Enemy
    {
        int enemy_hp;
        
       
       
        int enemy_score;
        public int Enemyscore { get { return enemy_score; }set { enemy_score = value; } }
        public int enemy_actualhp;
        public int Enemy_Actual_HP { get { return enemy_actualhp; } set { enemy_actualhp = value; } }

        public Attack[] attack;
        int totalnbofattacks;
        public Enemy(int enemy_hp,int enemy_score, int totalnbofattacks)
        {
            this.enemy_hp = enemy_hp;
            enemy_actualhp = enemy_hp;
            this.enemy_score = enemy_score;
            attack = new Attack[totalnbofattacks];
        }
        public int RestoreHealth(Enemy enemy)
        {
          return  enemy.enemy_actualhp = enemy.enemy_hp;
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
        
        public void Attack_Character(Enemy enemy, Character user)
        {
            Random rnd = new Random();
            int random = rnd.Next(enemy.attack.Length);
            int value = enemy.attack[random].attackvalue;
            user.Actual_hp -= value;

        }
        public void Drop_Item(Character user, Item mythings)
        {
            Random rnd = new Random(mythings.Nb_Of_Items);
            int ind = rnd.Next();
            user.AddToEquipment(ind,mythings,user);

        }
        
        public void TakeDamage(int value, Enemy enemy)
        {
            enemy.Enemy_Actual_HP -= value;
        }
        

    }
}
