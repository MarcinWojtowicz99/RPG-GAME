using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    /// <summary>
    /// An arrayed object which stores attacks of given enemy
    /// </summary>
    class Enemy
    {
        double enemy_hp;
        int counter;
        public int EnemyKilled { get { return counter; }set { counter=value; } }
        public int Nb_Of_EnemyAttacks { get { return totalnbofattacks; } set { totalnbofattacks = value; } }
        string name;
        public string Enemy_Name { get { return name; } }
        int enemy_score;
        public int Enemyscore { get { return enemy_score; }set { enemy_score = value; } }
        public double enemy_actualhp;
        public double Enemy_Actual_HP { get { return enemy_actualhp; } set { enemy_actualhp = value; } }

        public Attack[] attack;
        int totalnbofattacks;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enemy_hp"></param>
        /// <param name="enemy_score"></param>
        /// <param name="totalnbofattacks"></param>
        public Enemy(string name,double enemy_hp,int enemy_score, int totalnbofattacks)
        {
            this.name = name;
            this.totalnbofattacks = totalnbofattacks;
            Nb_Of_EnemyAttacks = totalnbofattacks;
            counter = 0;
            this.enemy_hp = enemy_hp;
            Enemy_Actual_HP = enemy_hp;
            this.enemy_score = enemy_score;
            attack = new Attack[totalnbofattacks];
        }
        /// <summary>
        /// Restores enemy's health bar after loosing a fight
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public double RestoreHealth(Enemy enemy)
        {
          return  enemy.enemy_actualhp = enemy.enemy_hp;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemy"></param>
        public Enemy(Enemy enemy)
        {
            this.attack = new Attack[enemy.attack.Length];
            this.enemy_hp = enemy.enemy_hp;
            this.enemy_score = enemy.enemy_score;
            for (int i = 0; i < attack.Length; i++)
            {
                attack[i] = enemy.attack[i];

            }

        }/// <summary>
        /// Stacks attacks
        /// </summary>
        /// <param name="attackss"></param>
        public void Stack(Attack attackss)//stores all attacks of one enemy-makes generating value easier
        {
            for (int i = 0; i < attack.Length; i++)
            {
                if (attack[i] == null)
                {
                    attack[i] = attackss;
                    break;
                }
            }
            
        }/// <summary>
        /// Method for attacking character
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="user"></param>
        /// <returns>Return attack value</returns>
        public int Attack_Character(Enemy enemy, Character user)
        {
            Console.Clear();
            Random random = new Random();
                            int rand = random.Next(100);
                            double value = 0;
                            if (rand <= 80)
                            {
                Random rnd = new Random();
                int random2 = rnd.Next(enemy.Nb_Of_EnemyAttacks);
                value = enemy.attack[random2].attackvalue;
                user.Actual_hp -= Convert.ToInt32(value);
                Console.WriteLine("{0}...used {1} and you lost {2}hp", enemy.name, enemy.attack[random2].name, enemy.attack[random2].attackvalue);
                Console.WriteLine(enemy.attack[random2].specialinfo);
                System.Threading.Thread.Sleep(4000);
                return user.Actual_hp;
            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Enemy missed!");
                                Console.ForegroundColor = ConsoleColor.White;
                System.Threading.Thread.Sleep(4000);
                return 0;

            }
                           
        }/// <summary>
        /// Drops random item after death
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mythings"></param>
        /// <param name="data"></param>
        public void Drop_Item(Character user, Item mythings, Data data)
        {
            Random rnd = new Random();
            int ind = rnd.Next(1,mythings.Nb_Of_Items-2);
            bool got=user.AddToEquipment(ind,mythings,user,data);
            if(got==true)
            {
                Console.WriteLine("+ {0}", mythings.unit[ind].nameofitem);
            }

        }
        /// <summary>
        /// Special dragon's death screen
        /// </summary>
        public static void DragonDeath()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dragon: AAAAAAAAAAAAAAAAAAARRRRRGH!");
            System.Threading.Thread.Sleep(1400);
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < 100; i++)
                Console.WriteLine();
            System.Threading.Thread.Sleep(400);
            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 0; i < 100; i++)
                Console.WriteLine();
            System.Threading.Thread.Sleep(400);
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < 100; i++)
                Console.WriteLine();
            System.Threading.Thread.Sleep(400);
            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 0; i < 100; i++)
                Console.WriteLine();
            System.Threading.Thread.Sleep(400);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            System.Threading.Thread.Sleep(4000);
            Console.ForegroundColor = ConsoleColor.Blue;
        }
       

    }
}
