using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MartyoAndStaine {
    class BattlePause : GameWindow {
        private SpriteBatch sprite;

        public BattlePause(Game game, SpriteBatch sb) : base(game) { sprite = sb; } // Initializes the level with one sprite for the background

        public override void Update(GameTime gameTime) { base.Update(gameTime); }

        public override void Draw(GameTime gameTime) {
            sprite.Draw(Game.Content.Load<Texture2D>("battlePause"), new Rectangle(0, 0, 1200, 600), Color.White); // Creates the background
            base.Draw(gameTime);
        }
    }
}