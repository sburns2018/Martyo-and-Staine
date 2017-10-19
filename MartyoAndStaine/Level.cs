using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MartyoAndStaine {
    public class Level : GameWindow {
        protected List<Rectangle> platforms;
        protected List<Enemy> deadEnemies;
        protected SpriteBatch sprite;
        protected Player martyo;
        protected double myLevel, eCount;
        protected bool enemiesGone, fight;
        protected int lIndex, decalX;

        public Level(Game game, SpriteBatch sb, double levelNum) : base(game) {
            fight = false;
            sprite = sb;
            platforms = new List<Rectangle>();
            platforms.Add(new Rectangle(-100, 570, 1400, 30));
            martyo = new Player(game, 8, 0, 50, 100, 30, 60, sprite, "martyo", platforms);
            components.Add(martyo);
            deadEnemies = new List<Enemy>();
            myLevel = levelNum;
            enemiesGone = false;
            eCount = 0;
            lIndex = 1;
            decalX = -10000;
        }

        public void addPlatform(int x, int y, int w, int h) {
            platforms.Add(new Rectangle(x, y, w, h));
            lIndex++;
        }

        public void deletePlatforms() {
            while (lIndex != 1) {
                //platforms[lIndex - 1] = new Rectangle(0, 0, 0, 0);
                //lIndex--;
                platforms.Remove(platforms[lIndex - 1]);
                lIndex--;
            }
        }

<<<<<<< HEAD
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

        public void addEnemy(Game game, int xv, int sx, int sy, int width, int height, string sn, int index) { components.Add(new Enemy(game, xv, sx, sy, width, height, sprite, sn, platforms, index)); }

        public override void Update(GameTime gameTime) {
            for (int i = 0; i < components.Count; i++) {
                if (components[i] is Enemy) {
                    if (((Enemy)components[i]).colP(martyo)) {
                        fight = true;
                        deadEnemies.Add((Enemy)components[i]);
                        components.Remove(components[i]);
                    }
                }
            }
            foreach (DrawableGameComponent e in components) if (e is Enemy && !enemiesGone) eCount++;
            enemiesGone = true;
            if (deadEnemies.Count == eCount && enemiesGone) {
                myLevel++;
                eCount = 0;
                enemiesGone = false;
                //for (int r = 0; r < deadEnemies.Count; r++) components.Add(deadEnemies[r]);
                deadEnemies.Clear();
                deletePlatforms();
                deleteEnemies();
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

        public void dedStuffReset() { deadEnemies.Clear(); }
    }
}