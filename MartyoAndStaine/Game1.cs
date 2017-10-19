using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MartyoAndStaine {
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D solidTexture;
        StartScreen sS;
        GameScreen gS;
        PauseScreen pS;
        BattlePause bP;
        GameWindow aS;
        BattleWindow bW;
        Level level;
        int countSwitch = 15;

        public Game1() {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 1200,
                PreferredBackBufferHeight = 600
            };
            graphics.ApplyChanges();
        }

        protected override void Initialize() { base.Initialize(); }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            solidTexture = new Texture2D(GraphicsDevice, 1, 1);
            solidTexture.SetData(new Color[] { Color.White });
            sS = new StartScreen(this, spriteBatch);
            sS.Hide();
            Components.Add(sS);
            bW = new BattleWindow(this, spriteBatch);
            bW.Hide();
            Components.Add(bW);
            pS = new PauseScreen(this, spriteBatch);
            pS.Hide();
            Components.Add(pS);
            bP = new BattlePause(this, spriteBatch);
            bP.Hide();
            Components.Add(bP);
            gS = new GameScreen(this, spriteBatch);
            gS.Hide();
            Components.Add(gS);
            level = new Level(this, spriteBatch, 1);
            level.Hide();
            Components.Add(level);
            aS = sS;
            aS.Show();
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime) {
            this.IsMouseVisible = true;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            if ((Keyboard.GetState().IsKeyDown(Keys.Space) && aS == sS) || (Keyboard.GetState().IsKeyDown(Keys.NumPad1))) {
                LevelToggler(level);
                level.addPlatform(33, 420, 200, 30);
                level.addPlatform(266, 270, 200, 30);
                level.addPlatform(499, 120, 200, 30);
                level.addPlatform(732, 270, 200, 30);
                level.addPlatform(965, 420, 200, 30);
                level.addEnemy(this, 1, 550, 520, 50, 50, "shrub", 0);
                level.addEnemy(this, 1, 599, 70, 50, 50, "shrub", 3); 
            }
            if (countSwitch >= 15) {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && aS == level) {
                    LevelToggler(pS);
                } else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && (aS == bW)) {
                    LevelToggler(bP);
                } else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && aS == pS) {
                    LevelToggler(level);
                } else if ((Keyboard.GetState().IsKeyDown(Keys.Q) && aS == level) || (Keyboard.GetState().IsKeyDown(Keys.Enter) && aS == bP)) {
                    LevelToggler(bW);
                } else if (aS == bW && !(bW.endBattle())) {
                    bW.reset();
                    LevelToggler(level);
                }
            }
            if (aS == level && level.getFight()) {
                LevelToggler(bW);
                bW.setEnemy(new Shrub(50, 5, true, this.Content.Load<Texture2D>("shrub_reverse")), new Shrub(50, 5, true, this.Content.Load<Texture2D>("shrub_reverse")));
                level.stopFight();
            }
            if (aS == level && level.getLevel() == 2 && !level.detectEnemies())
            {
                level.addPlatform(400, 300, 200, 30);
                level.addEnemy(this, 1, 450, 250, 50, 50, "shrub", 1);
                level.stopAddLevelLoop(2);
            }
            countSwitch++;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End();
        }

        protected void LevelToggler(GameWindow gw) {
            aS.Hide();
            aS = gw;
            aS.Show();
            countSwitch = 0;
        }
    }
}