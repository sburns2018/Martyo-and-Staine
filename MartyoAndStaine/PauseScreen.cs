using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MartyoAndStaine {
    class PauseScreen : GameWindow {
        public PauseScreen(Game game, SpriteBatch sB) : base(game) { }

        public override void Update(GameTime gameTime) { base.Update(gameTime); }

        public override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Purple);
            base.Draw(gameTime);
        }
    }
}