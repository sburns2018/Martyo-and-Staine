using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace MartyoAndStaine {
    public class Enemy : Entity {
        protected Rectangle plat;

        public Enemy(Game game, int xv, int sx, int sy, int width, int height, SpriteBatch sb, String sn, Rectangle[] platforms, int index) : base(game, xv, sx, sy, width, height, sb, sn) { plat = platforms[index]; }

        public override void Update(GameTime gameTime) {
            if (fRight) sprite = Game.Content.Load<Texture2D>(spriteName); else sprite = Game.Content.Load<Texture2D>(spriteName + "_reverse");
            if (bounds.X >= plat.X + plat.Width - bounds.Width) {
                xVelocity *= -1;
                fRight = false;
            } else if (bounds.X <= plat.X) {
                xVelocity *= -1;
                fRight = true;
            }
            bounds.X += xVelocity;
            base.Update(gameTime);
        }

        public bool colP(Player pl) {
            if (bounds.Intersects(pl.getBounds())) return true;
            return false;
        }
    }
}