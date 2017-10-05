using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MartyoAndStaine {
    public class Entity : DrawableGameComponent {
        protected bool fRight;
        protected int holdX, holdY, xVelocity;
        protected Rectangle bounds;
        protected SpriteBatch spriteBatch;
        protected String spriteName;
        protected Texture2D sprite;

        public Entity(Game game, int xv, int startX, int startY, int width, int height, SpriteBatch sb, String sn) : base(game) {
            fRight = true; // Makes entity face right automatically
            holdX = startX; // Set starting position for entity
            holdY = startY;
            xVelocity = xv; // Set velocity for entity
            bounds = new Rectangle(startX, startY, width, height); // Sets the bounds for the entity
            spriteBatch = sb; // Loads sprite batch
            spriteName = sn; // Stores sprite name
            sprite = Game.Content.Load<Texture2D>(spriteName); // Sprite is created with file from spritename
        }

        public override void Update(GameTime gameTime) { if (Keyboard.GetState().IsKeyDown(Keys.Y)) Reset(); }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Draw(sprite, bounds, Color.White);
            base.Draw(gameTime);
        }

        public void Reset() {
            bounds.X = holdX;
            bounds.Y = holdY;
            this.Visible = true;
            this.Enabled = true;
        }

        public Rectangle getBounds() { return bounds; }

        public void Hide() {
            this.Visible = false;
            this.Enabled = false;
        }
    }
}