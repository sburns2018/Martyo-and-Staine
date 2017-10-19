using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MartyoAndStaine {
    public class Level : GameWindow {
        protected List<Rectangle> platforms;
        protected SpriteBatch sprite;
        protected Player martyo;
        protected double myLevel, stopLevelLoop;
        protected bool enemiesGone, fight;
        protected int lIndex, decalX;

        public Level(Game game, SpriteBatch sb, double levelNum) : base(game) {
            fight = false;
            sprite = sb;
            platforms = new List<Rectangle>();
            platforms.Add(new Rectangle(-100, 570, 1400, 30));
            martyo = new Player(game, 8, 0, 50, 100, 30, 60, sprite, "martyo", platforms);
            components.Add(martyo);
            myLevel = levelNum;
            enemiesGone = false;
            lIndex = 1;
            decalX = -10000;
            stopLevelLoop = 2;
        }

        public void addPlatform(int x, int y, int w, int h) {
            platforms.Add(new Rectangle(x, y, w, h));
            lIndex++;
        }

        public double getLevel()
        {
            return myLevel;
        }

        public void stopAddLevelLoop(double stopus)
        {
            stopLevelLoop = stopus;
        }

        public void deletePlatforms() {
            while (lIndex != 1) {
                platforms.Remove(platforms[lIndex - 1]);
                lIndex--;
            }
        }

        public void deleteEnemies()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is Enemy)
                {
                    components.Remove(components[i]);
                    i--;
                }
            }
        }
<<<<<<< HEAD

        public bool detectEnemies()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is Enemy)
                {
                    return true;
                }
            }
            return false;
        }
=======
>>>>>>> c96dff0e25dfce9b139f305db204d011ff8bf256

        public void addEnemy(Game game, int xv, int sx, int sy, int width, int height, string sn, int index) { components.Add(new Enemy(game, xv, sx, sy, width, height, sprite, sn, platforms, index)); }

        public override void Update(GameTime gameTime) {
            for (int i = 0; i < components.Count; i++) {
                if (components[i] is Enemy) {
                    if (((Enemy)components[i]).colP(martyo)) {
                        fight = true;
                        components.Remove(components[i]);
                    }
                }
            }
            enemiesGone = true;
            if (!detectEnemies() && myLevel < stopLevelLoop) {
                myLevel++;
                enemiesGone = false;
                deletePlatforms();
                martyo.Reset();
            }
            if (decalX >= 1400) { decalX = -10000; }
            decalX += 50;
            base.Update(gameTime);
        }

        public bool getFight() { return fight; }

        public void stopFight() { fight = false; }

        public override void Draw(GameTime gameTime) {
            if (myLevel == 2) {
                sprite.Draw(Game.Content.Load<Texture2D>("stars"), new Rectangle(0, 0, 1200, 600), Color.White);
                sprite.Draw(Game.Content.Load<Texture2D>("decal1"), new Rectangle(decalX, 80, 100, 200), Color.White);
            }
            sprite.Draw(Game.Content.Load<Texture2D>("level" + myLevel.ToString()), new Rectangle(0, 0, 1200, 600), Color.White);
            if (myLevel == 1) { sprite.Draw(Game.Content.Load<Texture2D>("ground_grass_0"), platforms[0], Color.White); }
            for (int i = 1; i < platforms.Count; i++) sprite.Draw(Game.Content.Load<Texture2D>("clod"), platforms[i], Color.White);
            base.Draw(gameTime);
        }
    }
}