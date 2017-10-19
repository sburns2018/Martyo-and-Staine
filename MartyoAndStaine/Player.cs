using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MartyoAndStaine {
    public class Player : Entity {
        private bool doJump, up, down;
        private int curY, yVelocity, frameCount;
        private List<Rectangle> plats;
        private Rectangle curPlat;

        public Player(Game game, int xv, int yv, int sx, int sy, int width, int height, SpriteBatch sb, string sn, List<Rectangle> platforms) : base(game, xv, sx, sy, width, height, sb, sn) {
            doJump = false;
            up = false;
            down = false;
            curY = bounds.Y;
            yVelocity = yv;
            plats = platforms;
            curPlat = plats[0];
        }

        public override void Update(GameTime gameTime) {
            if (fRight) sprite = Game.Content.Load<Texture2D>(spriteName); else sprite = Game.Content.Load<Texture2D>(spriteName + "_reverse");
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !doJump && bounds.X >= curPlat.X && bounds.X <= curPlat.X + curPlat.Width) {
                doJump = true;
                up = true;
            }
            if (doJump) {
                if (up) bounds.Y -= 10; else bounds.Y += 10;
                if (bounds.Y + bounds.Height <= curY - 150) up = false;
                for (int i = 0; i < plats.Count; i++) {
                    if (bounds.Y + bounds.Height == plats[i].Y && bounds.X >= plats[i].X && bounds.X <= plats[i].X + plats[i].Width) {
                        curY = bounds.Y;
                        curPlat = plats[i];
                        doJump = false;
                    }
                }
                if (bounds.Y == curY) doJump = false;
            }
            if (bounds.X < curPlat.X || bounds.X > curPlat.X) {
                down = true;
                for (int i = 0; i < plats.Count; i++) {
                    if (bounds.Y + bounds.Height == plats[i].Y && bounds.X >= plats[i].X && bounds.X <= plats[i].X + plats[i].Width) {
                        curY = bounds.Y;
                        curPlat = plats[i];
                        down = false;
                    }
                }
                if (!doJump && down) bounds.Y += 10;
            }
            if (bounds.X <= -50) bounds.X = 1200; else if (bounds.X >= 1250) bounds.X = -40;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                bounds.X += xVelocity;
                fRight = true;
                if (frameCount <= 30) sprite = Game.Content.Load<Texture2D>(spriteName + "_walk1"); else sprite = Game.Content.Load<Texture2D>(spriteName + "_walk2");
            } else if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                bounds.X -= xVelocity;
                fRight = false;
                if (frameCount <= 30) sprite = Game.Content.Load<Texture2D>(spriteName + "_reverse_walk1"); else sprite = Game.Content.Load<Texture2D>(spriteName + "_reverse_walk2");
            }
            frameCount++;
            if (frameCount == 60) frameCount = 0;
            base.Update(gameTime);
        }

        public void DumbReset() {
            doJump = false;
            up = false;
            down = false;
            curPlat = plats[0];
        }
    }
}