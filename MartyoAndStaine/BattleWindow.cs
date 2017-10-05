using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MartyoAndStaine {
    class BattleWindow : GameWindow {
        private SpriteBatch sprite;
        private double turn, mHealth, sHealth;
        private MouseState mouse;
        private Boolean mAlive, sAlive, mAttacking, sAttacking, eAttacking1, eAttacking2;
        private int shakeFace, noCrack, whoAttac, attackFrames;
        private Rectangle mFace, sFace, mAttack, sAttack;
        private SpriteFont font;
        private BattleEnemy enemy1, enemy2;

        public BattleWindow(Game game, SpriteBatch sb) : base(game) {
            sprite = sb;
            mFace = new Rectangle(0, 500, 137, 80);
            sFace = new Rectangle(275, 500, 137, 80);
            mAttack = new Rectangle(137, 500, 137, 40);
            sAttack = new Rectangle(412, 500, 137, 40);
            turn = 1;
            mAlive = true;
            sAlive = true;
            mHealth = 100;
            sHealth = 100;
            shakeFace = 20;
            mAttacking = false;
            sAttacking = false;
            eAttacking1 = false;
            eAttacking2 = false;
        }

        public override void Update(GameTime gameTime) {
            mouse = Mouse.GetState();
            Random rnd = new Random();
            if (mHealth <= 0) {
                mHealth = 0;
                mAlive = false;
            }
            if (sHealth <= 0) {
                sHealth = 0;
                sAlive = false;
            }
            if (enemy1.getHealth() <= 0) {
                enemy1.setHealth(0);
                enemy1.setAlive(false);
            }
            if (enemy2.getHealth() <= 0) {
                enemy2.setHealth(0);
                enemy2.setAlive(false);
            }
            if (turn == 1 && mAlive) {
                if (noCrack >= 15) {
                    mFace.Y -= shakeFace;
                    shakeFace *= -1;
                    noCrack = 0;
                }
                if (mouse.X >= mAttack.X && mouse.X <= mAttack.X + mAttack.Width && mouse.Y >= mAttack.Y && mouse.Y <= mAttack.Y + mAttack.Height && mouse.LeftButton == ButtonState.Pressed) {
                    mFace.Y = 500;
                    shakeFace = 20;
                    enemy1.setHealth(enemy1.getHealth() - 10);
                    turn++;
                    attackFrames = 0;
                    mAttacking = true;
                }
            } else if (turn == 1 && !mAlive) {
                turn++;
            } else if (turn == 2 & sAlive) {
                if (noCrack >= 15) {
                    sFace.Y -= shakeFace;
                    shakeFace *= -1;
                    noCrack = 0;
                }
                if (mouse.X >= sAttack.X && mouse.X <= sAttack.X + sAttack.Width && mouse.Y >= sAttack.Y && mouse.Y <= sAttack.Y + sAttack.Height && mouse.LeftButton == ButtonState.Pressed) {
                    sFace.Y = 500;
                    shakeFace = 20;
                    enemy1.setHealth(enemy1.getHealth() - 10);
                    turn++;
                    attackFrames = 0;
                    sAttacking = true;
                }
            } else if (turn == 2 && !sAlive) {
                turn++;
            } else if (turn == 3 & enemy1.getAlive()) {
                whoAttac = rnd.Next(0, 2);
                attackFrames = 0;
                if (whoAttac == 0) { if (mAlive) mHealth -= enemy1.getAttack(); else sHealth -= enemy1.getAttack(); } else { if (sAlive) sHealth -= enemy1.getAttack(); else mHealth -= enemy1.getAttack(); }
                if (!enemy2.getAlive()) turn = 1; else turn++;
                eAttacking1 = true;
            } else if (turn == 3 && !enemy1.getAlive()) {
                turn++;
            } else if (turn == 4 && enemy2.getAlive()) {
                whoAttac = rnd.Next(0, 2);
                attackFrames = 0;
                if (whoAttac == 0) { if (mAlive) mHealth -= enemy2.getAttack(); else sHealth -= enemy2.getAttack(); } else { if (sAlive) sHealth -= enemy2.getAttack(); else mHealth -= enemy2.getAttack(); }
                turn = 1;
                eAttacking2 = true;
            }
            //if (mAttacking)
            //{

            //} else if (sAttacking)
            //{

            //} else if (eAttacking1)
            //{
                
            //} else if (eAttacking2)
            //{

            //}
            noCrack += 1;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            sprite.Draw(Game.Content.Load<Texture2D>("fightBackground"), new Rectangle(0, 0, 1200, 600), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("Arena"), new Rectangle(350, 226, 150, 50), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("Arena"), new Rectangle(150, 400, 150, 50), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("Arena"), new Rectangle(700, 226, 150, 50), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("Arena"), new Rectangle(900, 400, 150, 50), Color.White);
            if (mAlive) sprite.Draw(Game.Content.Load<Texture2D>("martyo"), new Rectangle(400, 126, 50, 100), Color.White);
            if (sAlive) sprite.Draw(Game.Content.Load<Texture2D>("staine"), new Rectangle(200, 300, 50, 100), Color.White);
            if (enemy1.getAlive()) sprite.Draw(enemy1.getSprite(), new Rectangle(750, 176, 50, 50), Color.White);
            if (enemy2.getAlive()) sprite.Draw(enemy2.getSprite(), new Rectangle(950, 350, 50, 50), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("ground_grass_0"), new Rectangle(0, 500, 550, 100), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("ground_grass_0"), new Rectangle(924, 500, 276, 100), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("martyoFace"), mFace, Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("mRect"), new Rectangle(0, 580, Convert.ToInt32(mHealth * 1.37), 20), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("staineFace"), sFace, Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("sRect"), new Rectangle(275, 580, Convert.ToInt32(sHealth * 1.37), 20), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("shrub_face"), new Rectangle(924, 500, 137, 80), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("shrub_face"), new Rectangle(1061, 500, 137, 80), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("eRect"), new Rectangle(924, 580, Convert.ToInt32(enemy1.getHealth() * 1.37), 20), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("eRect"), new Rectangle(1061, 580, Convert.ToInt32(enemy2.getHealth() * 1.37), 20), Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("mAttac"), mAttack, Color.White);
            sprite.Draw(Game.Content.Load<Texture2D>("sAttac"), sAttack, Color.White);
            base.Draw(gameTime);
        }

        public Boolean endBattle() { return enemy1.getAlive() || enemy2.getAlive(); }

        public void setEnemy(BattleEnemy bE1, BattleEnemy bE2)
        {
            enemy1 = bE1;
            enemy2 = bE2;
        }

        public void reset() {
            turn = 1;
            if (!mAlive) mHealth = 1;
            if (!sAlive) sHealth = 1;
            mAlive = true;
            sAlive = true;
            enemy1.setAlive(true);
            enemy2.setAlive(true);
            enemy1.setHealth(50);
            enemy2.setHealth(0);
            shakeFace = 20;
        }
    }
}