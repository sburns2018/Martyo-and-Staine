using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MartyoAndStaine {
    class BattleWindow : GameWindow {
        private SpriteBatch sprite;
        private double turn, mHealth, sHealth;
        private MouseState mouse;
        private Boolean mAlive, sAlive, mAttacking, sAttacking, eAttacking1, eAttacking2, choosing, canShake;
        private int shakeFace, noCrack, whoAttac, attackFrames;
        private Rectangle mFace, sFace, mAttack, sAttack, eRecta1, eRecta2;
        private BattleEnemy enemy1, enemy2;

        public BattleWindow(Game game, SpriteBatch sb) : base(game) {
            sprite = sb; // Initializes the spritebatch used to draw stuff in the battle window
            mFace = new Rectangle(0, 500, 137, 80); // Sets space for Martyo's face
            sFace = new Rectangle(275, 500, 137, 80); // Sets space for Staine's face
            mAttack = new Rectangle(137, 500, 137, 40); // Sets space for Attack button of Martyo
            sAttack = new Rectangle(412, 500, 137, 40); // Sets space for Attack button of Staine
            eRecta1 = new Rectangle(750, 176, 50, 50); // Sets space to draw enemy 1
            eRecta2 = new Rectangle(950, 350, 50, 50); // Sets space to draw enemy 2
            turn = 1; // Sets to Martyo's turn (2 = Staine, 3 = Enemy 1, 4 = Enemy 2)
            canShake = true; // Sets whether or not enemy's face can shake when turn is active
            mAlive = true; // Sets life status of Martyo
            sAlive = true; // Sets life status of Staine
            mHealth = 100; // Sets Martyo's life to 100
            sHealth = 100; // Sets Staine's life to 100
            shakeFace = 20; // Sets shaking to only occur every 20 frames (?)
            mAttacking = false; // Sets if Martyo is attacking
            sAttacking = false; // Sets if Staine is attacking 
            eAttacking1 = false; // Sets if enemy 1 is attacking
            eAttacking2 = false; // Sets if enemy 2 is attacking
            choosing = false; // Sets if you are choosing to attack an enemy
        }

        public override void Update(GameTime gameTime) {
            mouse = Mouse.GetState(); // Mouse is obtained in order to control attacks
            Random rnd = new Random(); // Sets a random variable for chance situations (attacking, dodging, etc)
            if (mHealth <= 0) { // If Martyo is dead, make sure his health is zero and he is set to dead
                mHealth = 0; 
                mAlive = false;
            }
            if (sHealth <= 0) { // If Staine is dead, make sure his health is zero and he is set to dead
                sHealth = 0;
                sAlive = false;
            }
            if (enemy1.getHealth() <= 0) { // If the first enemy dies, make sure his health is zero and he is set to dead
                enemy1.setHealth(0);
                enemy1.setAlive(false);
            }
            if (enemy2.getHealth() <= 0) { // If the second enemy dies, make sure his health is zero and he is set to dead
                enemy2.setHealth(0);
                enemy2.setAlive(false);
            }
            if (turn == 1 && mAlive) { // If it's Martyo's turn and he's alive ...
                if (noCrack >= 15 && canShake) { // Sets face shaking (weird mechanic)
                    mFace.Y -= shakeFace;
                    shakeFace *= -1;
                    noCrack = 0;
                }
                if (!choosing && mouse.X >= mAttack.X && mouse.X <= mAttack.X + mAttack.Width && mouse.Y >= mAttack.Y && mouse.Y <= mAttack.Y + mAttack.Height && mouse.LeftButton == ButtonState.Pressed) {
                    mFace.Y = 500; // If the attack button is clicked, the shaking stops and choosing is enabled; he is ready to attack
                    shakeFace = 20;
                    canShake = false;
                    attackFrames = 0;
                    choosing = true;
                }
                if (choosing && mouse.X >= mAttack.X && mouse.X <= eRecta1.X + eRecta1.Width && mouse.Y >= eRecta1.Y && mouse.Y <= eRecta1.Y + eRecta1.Height && mouse.LeftButton == ButtonState.Pressed) {
                    enemy1.setHealth(enemy1.getHealth() - 50); // If the first enemy is clicked, it loses 50 health and it turns to Staine's turn
                    turn++;
                    canShake = true;
                    choosing = false;
                } else if (choosing && mouse.X >= mAttack.X && mouse.X <= eRecta2.X + eRecta2.Width && mouse.Y >= eRecta2.Y && mouse.Y <= eRecta2.Y + eRecta2.Height && mouse.LeftButton == ButtonState.Pressed) {
                    enemy2.setHealth(enemy2.getHealth() - 50); // Else, if the second enemy is clicked, it loses 50 health and it turns to Staine's turn
                    canShake = true;
                    turn++;
                    choosing = false;
                }
            } else if (turn == 1 && !mAlive) { // Else, if it turns to Martyo's turn and he's dead, go to Staine's turn
                turn++;
            } else if (turn == 2 & sAlive) { // Else, it's Staine's turn and he's alive ... 
                if (noCrack >= 15 && canShake) { // Sets face to shaking
                    sFace.Y -= shakeFace;
                    shakeFace *= -1;
                    noCrack = 0;
                }
                if (!choosing && mouse.X >= sAttack.X && mouse.X <= sAttack.X + sAttack.Width && mouse.Y >= sAttack.Y && mouse.Y <= sAttack.Y + sAttack.Height && mouse.RightButton == ButtonState.Pressed) {
                    sFace.Y = 500; // If the attack button is clicked, his face stops shaking and he can attack
                    shakeFace = 20;
                    canShake = false;
                    attackFrames = 0;
                    choosing = true;
                }
                if (choosing && mouse.X >= mAttack.X && mouse.X <= eRecta1.X + eRecta1.Width && mouse.Y >= eRecta1.Y && mouse.Y <= eRecta1.Y + eRecta1.Height && mouse.RightButton == ButtonState.Pressed) {
                    enemy1.setHealth(enemy1.getHealth() - 50); // If the first enemy is clicked on, it loses 50 health and it turns to the first enemy's turn
                    turn++;
                    canShake = true;
                    choosing = false;
                } else if (choosing && mouse.X >= mAttack.X && mouse.X <= eRecta2.X + eRecta2.Width && mouse.Y >= eRecta2.Y && mouse.Y <= eRecta2.Y + eRecta2.Height && mouse.RightButton == ButtonState.Pressed) {
                    enemy2.setHealth(enemy2.getHealth() - 50); // Else, if the second enemy is clicked on, it loses 50 health and it turns to the first enemy's turn
                    turn++;
                    canShake = true;
                    choosing = false;
                }
            } else if (turn == 2 && !sAlive) { // Else, if it's Staine's turn and he's dead, go to Enemy 1's turn
                turn++;
            } else if (turn == 3 & enemy1.getAlive()) { // Else, if it is the first enemy's turn and it's alive, ...
                whoAttac = rnd.Next(0, 2); // A random 1 or 2 is generated to determine which player the enemy will attack
                attackFrames = 0; // Tbd
                // If the random num is 0 and Martyo is alive, make Martyo lose the equivalent of the enemy's attack; else, if Staine is alive, he loses health
                if (whoAttac == 0) { if (mAlive) mHealth -= enemy1.getAttack(); else sHealth -= enemy1.getAttack(); } else { if (sAlive) sHealth -= enemy1.getAttack(); else mHealth -= enemy1.getAttack(); }
                if (!enemy2.getAlive()) turn = 1; else turn++; // If enemy 2 isn't alive, it's Martyo's turn; else it's enemy 2's turn
                eAttacking1 = true; // Tbd
            } else if (turn == 3 && !enemy1.getAlive()) { // Else, if it's enemy 1's turn and it's dead, it will be enemy 2's turn
                turn++;
            } else if (turn == 4 && enemy2.getAlive()) { // Else, if it is enemy 2's turn and he's alive, ...
                whoAttac = rnd.Next(0, 2); // A random 1 or 2 is generated again
                attackFrames = 0; // Tbd
                // Same attack function as enemy 1
                if (whoAttac == 0) { if (mAlive) mHealth -= enemy2.getAttack(); else sHealth -= enemy2.getAttack(); } else { if (sAlive) sHealth -= enemy2.getAttack(); else mHealth -= enemy2.getAttack(); }
                turn = 1; // It turns to Martyo's turn
                eAttacking2 = true; // Tbd
            }
            noCrack += 1; // Frame is incremented
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            sprite.Draw(Game.Content.Load<Texture2D>("fightBackground"), new Rectangle(0, 0, 1200, 600), Color.White); // Background is drawn
            sprite.Draw(Game.Content.Load<Texture2D>("Arena"), new Rectangle(350, 226, 150, 50), Color.White); // Arenas for players and enemies are drawn
            sprite.Draw(Game.Content.Load<Texture2D>("Arena"), new Rectangle(150, 400, 150, 50), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("Arena"), new Rectangle(700, 226, 150, 50), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("Arena"), new Rectangle(900, 400, 150, 50), Color.White);
            if (mAlive) sprite.Draw(Game.Content.Load<Texture2D>("martyo"), new Rectangle(400, 126, 50, 100), Color.White); // Players and enemies drawn if they're alive
            if (sAlive) sprite.Draw(Game.Content.Load<Texture2D>("staine"), new Rectangle(200, 300, 50, 100), Color.White); 
            if (enemy1.getAlive()) sprite.Draw(enemy1.getSprite(), eRecta1, Color.White);
            if (enemy2.getAlive()) sprite.Draw(enemy2.getSprite(), eRecta2, Color.White);
            if (enemy1.getAlive() && choosing) sprite.Draw(Game.Content.Load<Texture2D>("arrow"), new Rectangle(eRecta1.X + 13, eRecta1.Y - 35, 25, 25), Color.White); // If the enemy is alive and
            if (enemy2.getAlive() && choosing) sprite.Draw(Game.Content.Load<Texture2D>("arrow"), new Rectangle(eRecta2.X + 13, eRecta2.Y - 35, 25, 25), Color.White); // player's attacking, draw ^
            sprite.Draw(Game.Content.Load<Texture2D>("ground_grass_0"), new Rectangle(0, 500, 550, 100), Color.White); // Background for player's stats are drawn
            sprite.Draw(Game.Content.Load<Texture2D>("ground_grass_0"), new Rectangle(924, 500, 276, 100), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("martyoFace"), mFace, Color.White); // Players and enemies faces and health bars drawn
            sprite.Draw(Game.Content.Load<Texture2D>("mRect"), new Rectangle(0, 580, Convert.ToInt32(mHealth * 1.37), 20), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("staineFace"), sFace, Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("sRect"), new Rectangle(275, 580, Convert.ToInt32(sHealth * 1.37), 20), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("shrub_face"), new Rectangle(924, 500, 137, 80), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("shrub_face"), new Rectangle(1061, 500, 137, 80), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("eRect"), new Rectangle(924, 580, Convert.ToInt32(enemy1.getHealth() * 1.37), 20), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("eRect"), new Rectangle(1061, 580, Convert.ToInt32(enemy2.getHealth() * 1.37), 20), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("mAttac"), mAttack, Color.White); // Attack buttons are drawn for players
            sprite.Draw(Game.Content.Load<Texture2D>("sAttac"), sAttack, Color.White);
            base.Draw(gameTime);
        }

        public Boolean endBattle() { return enemy1.getAlive() || enemy2.getAlive(); } // Determines if the battle will end by the life status of enemy 1 and enemy 2

        public void setEnemy(BattleEnemy bE1, BattleEnemy bE2) {
            enemy1 = bE1; // Enemies are reset
            enemy2 = bE2;
        }

        public void reset() {
            turn = 1; // Turn is set to Martyo's 
            if (!mAlive) mHealth = 1; // If a player died, set his health to 1
            if (!sAlive) sHealth = 1; 
            mAlive = true; // Revive players and ememies
            sAlive = true;
            enemy1.setAlive(true);
            enemy2.setAlive(true);
            enemy1.setHealth(50); // Set enemy 1's player back, but not enemy 2's (could not be there)
            enemy2.setHealth(0);
            shakeFace = 20; // Resets shake counter
        }
    }
}