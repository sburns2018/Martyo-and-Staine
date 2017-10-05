using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MartyoAndStaine {
    class BattlePause : GameWindow {
        private SpriteBatch sprite;

        public BattlePause(Game game, SpriteBatch sb) : base(game) { sprite = sb; }

        public override void Update(GameTime gameTime) { base.Update(gameTime); }

        public override void Draw(GameTime gameTime) {
            sprite.Draw(Game.Content.Load<Texture2D>("battlePause"), new Rectangle(0, 0, 1200, 600), Color.White);
            base.Draw(gameTime);
        }
    }
}