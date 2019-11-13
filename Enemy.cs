using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Enemy
    {
        double enemy_hp;
        int counter;
        public int EnemyKilled { get { return counter; }set { counter=value; } }

        string name;
        public string Enemy_Name { get { return name; } }
        int enemy_score;
        public int Enemyscore { get { return enemy_score; }set { enemy_score = value; } }
        public double enemy_actualhp;
        public double Enemy_Actual_HP { get { return enemy_actualhp; } set { enemy_actualhp = value; } }

        public Attack[] attack;
        int totalnbofattacks;
        public Enemy(string name,double enemy_hp,int enemy_score, int totalnbofattacks)
        {
            this.name = name;
            counter = 0;
            this.enemy_hp = enemy_hp;
            MyHp();
            this.enemy_score = enemy_score;
            attack = new Attack[totalnbofattacks];
        }
        public double MyHp()
        {
            return enemy_actualhp = enemy_hp;
        }
        public double RestoreHealth(Enemy enemy)
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
        
        public int Attack_Character(Enemy enemy, Character user)
        {
            Random rnd = new Random();
            int random = rnd.Next(enemy.attack.Length);
            int value = enemy.attack[random].attackvalue;
            user.Actual_hp -= value;
            Console.WriteLine("{0}...used {1} and you lost {2}hp",enemy.name, enemy.attack[random].name, enemy.attack[random].attackvalue);
            Console.WriteLine(enemy.attack[random].specialinfo);
            return value;

        }
        public void Drop_Item(Character user, Item mythings, Data data)
        {
            Random rnd = new Random();
            int ind = rnd.Next(1,mythings.Nb_Of_Items);
            bool got=user.AddToEquipment(ind,mythings,user,data);
            if(got=true)
            {
                Console.WriteLine("+ {0}", mythings.unit[ind].nameofitem);
            }

        }

    }
}
