using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_GAME
{
    class Attack
    {
        string name; string user; int attackvalue; string specialinfo;
        public Attack[] attack;
        int totalnbofattacks;
        public Attack(string user,int totalnbofattacks)
        {
            this.user = user;
            attack = new Attack[totalnbofattacks];
        }
        public Attack(Attack attacks)
        {
            this.name = attacks.name;
            this.user = attacks.user;
            this.attackvalue = attacks.attackvalue;
            this.specialinfo = attacks.specialinfo;

            this.attack = new Attack[attacks.attack.Length];
            for (int i = 0; i < attack.Length; i++)
            {
                attack[i] = attacks.attack[i];
            }
        }
        public void Stack(Enemy attackss)
        {
            
            
                for (int i = 0; i < attack.Length; i++)
                {
                    if (attack[i] == null)
                    {
                        attack[i] = attackss;
                        
                    }
                }
            
            

        }

    }
}
