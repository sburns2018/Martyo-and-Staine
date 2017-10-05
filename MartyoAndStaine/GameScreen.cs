using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MartyoAndStaine {
    class GameScreen : GameWindow {
        private SpriteBatch sB;
        private Texture2D image;

        public GameScreen(Game game, SpriteBatch sB) : base(game) {
            this.sB = sB;
            image = new Texture2D(GraphicsDevice, 1, 1);
            image.SetData(new Color[] { Color.White });
        }

        public override void Update(GameTime gameTime) { base.Update(gameTime); }

        public override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}