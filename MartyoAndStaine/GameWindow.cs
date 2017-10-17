using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MartyoAndStaine {
    public class GameWindow : DrawableGameComponent {
        public List<DrawableGameComponent> components;
        private static double health, level;

        public GameWindow(Game game) : base(game) {
            components = new List<DrawableGameComponent>();
            health = 100;
            level = 1;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            foreach (DrawableGameComponent x in components) x.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            foreach (DrawableGameComponent x in components) x.Draw(gameTime);
        }

        public void Show() {
            foreach (DrawableGameComponent w in components) {
                w.Visible = true;
                w.Enabled = true;
            }
            Visible = true;
            Enabled = true;
        }

        public void Hide() {
            foreach (DrawableGameComponent w in components) {
                w.Visible = false;
                w.Enabled = false;
            }
            Visible = false;
            Enabled = false;
        }

        public double getHealth() { return health; }

        public void loseLife(int val) { health = health - val; }

        public void resetLife() { health = 100; }

        public double getLevel() { return level; }
    }
}