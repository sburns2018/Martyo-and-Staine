using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MartyoAndStaine
{
    class BattleEnemy
    {
        private int health, attack;
        private bool alive;
        private Texture2D sprite;

        public BattleEnemy(int healthus, int attackus, bool aliveus, Texture2D spriteus)
        {
            health = healthus;
            attack = attackus;
            alive = aliveus;
            sprite = spriteus;
        }

        public int getHealth()
        {
            return health;
        }

        public void setHealth(int val)
        {
            health = val;
        }

        public int getAttack()
        {
            return attack;
        }
        
        public bool getAlive()
        {
            return alive;
        }
        
        public void setAlive(bool val)
        {
            alive = val;
        }

        public Texture2D getSprite()
        {
            return sprite;
        }

        public void setSprite(Texture2D spriteus)
        {
            sprite = spriteus;
        }
    }
}
