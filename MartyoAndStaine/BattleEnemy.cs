using Microsoft.Xna.Framework.Graphics;

namespace MartyoAndStaine {
    class BattleEnemy {
        private int health, attack; 
        private bool alive;
        private Texture2D sprite;

        public BattleEnemy(int healthus, int attackus, bool aliveus, Texture2D spriteus) {
            health = healthus; // Sets enemy's health to custom variable 
            attack = attackus; // Sets enemy's damage to custom variable 
            alive = aliveus; // Sets if the enemy is alive or not (since two enemy's are created during a battle and sometimes only one is needed like a boss)
            sprite = spriteus; // Sets sprite of enemy
        }

        public int getHealth() { return health; } // Returns enemy health

        public void setHealth(int val) { health = val; } // Sets health to a custom variable (if attacked or used to ensure enemy is dead)

        public int getAttack() { return attack; } // Returns enemy damage
        
        public bool getAlive() { return alive; } // Returns if enemy is alive or not
        
        public void setAlive(bool val) { alive = val; } // Sets enemy to dead or alive

        public Texture2D getSprite() { return sprite; } // Return's enemy's sprite

        public void setSprite(Texture2D spriteus) { sprite = spriteus; } // Changes enemy's sprite
    }
}