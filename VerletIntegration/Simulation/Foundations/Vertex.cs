using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerletIntegration.Simulation
{
    class Vertex
    {
        float x, y;
        float oldX, oldY;
        float ax = 0, ay = 98f;
        float radius;

        Vector2 origin;
        float scale;

        float bounce = 0.99f;
        float friction = 0.99f;
        float mass = 1;
        float gravity = 98f;

        float top = 0, bottom = Game1.Height, left = 0, right = Game1.Width;

        public bool AtRest
        {
            get;
            set;
        }

        public bool Collision(float xin, float yin)
        {
            if (xin > x - radius && xin < x + radius &&
                yin > y - radius && yin < y + radius)
                return true;
            return false;
        }

        public void RunLeftClick()
        {
            bool m = Game1.mouse.LeftButton == ButtonState.Released, old = Game1.oldmouse.LeftButton == ButtonState.Pressed;

            if (m && old && Pinned)
                Pinned = false;
            else if (m && old && !Pinned)
                Pinned = true;
        }

        void RunRightClick()
        {
            bool m = Game1.mouse.RightButton == ButtonState.Released && Game1.oldmouse.RightButton == ButtonState.Pressed;

            if (m && !Locked)
                Locked = true;
            else if (!m && Locked)
                Locked = false;
        }

        void Test()
        {
            float xin = Game1.mouse.X, yin = Game1.mouse.Y;
            if (xin + 3 > x - radius && xin - 3 < x + radius &&
               yin + 3 > y - radius && yin - 3 < y + radius)
            {
                bool O = Game1.mouse.RightButton == ButtonState.Pressed && Game1.oldmouse.RightButton == ButtonState.Pressed;

                if (O)
                {
                    x = Game1.mouse.Position.X;
                    oldX = Game1.oldmouse.Position.X;
                    y = Game1.mouse.Position.Y;
                    oldY = Game1.oldmouse.Position.Y;
                }
            }
        }

        bool Locked;

        void LockOn()
        {
            x = Game1.mouse.Position.X;
            y = Game1.mouse.Position.Y;
        }

        public Vertex(float x, float y)
        {
            this.x = x;
            this.y = y;
            AtRest = false;
            radius = 2;
            oldX = x;
            oldY = y;
            scale = Math.Min(radius * 2, Game1.vertext.Width) / Math.Max(radius * 2, Game1.vertext.Width);
            origin = new Vector2(Game1.vertext.Width / 2, Game1.vertext.Height / 2);
        }

        public Vertex()
        {
            radius = 2;
            oldX = 0;
            oldY = 0;
            scale = Math.Min(radius * 2, Game1.vertext.Width) / Math.Max(radius * 2, Game1.vertext.Width);
            origin = new Vector2(Game1.vertext.Width / 2, Game1.vertext.Height / 2);
        }

        public void Update(float time)
        {
            if (Collision(Game1.mouse.X, Game1.mouse.Y))
            {
                RunLeftClick();
                RunRightClick();
            }

            if (!Pinned)
            {
                if (!AtRest)
                    AccumulateForces(0, gravity);
                else
                {
                    ax = 0;
                    ay = 0;
                }
                Test();
                Constrain();
                Inertia(time);
            }

            if (Locked)
                LockOn();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Hidden)
                spriteBatch.Draw(Game1.vertext, new Vector2(x, y), null, Color.Black, 0, origin, scale, SpriteEffects.None, 1);
        }

        public Vector2 Position()
        {
            return new Vector2(x, y);
        }

        public bool Hidden
        {
            get;
            set;
        }

        public bool Pinned
        {
            get;
            set;
        }

        public void SetRadius(float i)
        {
            radius = i;
            scale = Math.Min(radius * 2, Game1.vertext.Width) / Math.Max(radius * 2, Game1.vertext.Width);
        }

        public float GetRadius()
        {
            return radius;
        }

        public void SetVX(float i)
        {
            oldX = x - i;
        }

        public void SetVY(float i)
        {
            oldY = y - i;
        }

        public void AccumulateX(float x)
        {
            this.x += x;
        }

        public void AccumulateY(float y)
        {
            this.y += y;
        }

        public void SetX(float x)
        {
            this.x = x;
        }

        public void SetY(float y)
        {
            this.y = y;
        }

        public void AccumulateForces(float x, float y)
        {
            ax += x;
            ay += y;
        }

        public void Inertia(float time)
        {
            float tempx = x, tempy = y;
            x = x + (x - oldX) * friction + ax * time * time;
            y = y + (y - oldY) * friction + ay * time * time;
            oldX = tempx;
            oldY = tempy;
            ax = 0;
            ay = 0;
        }

        public void Constrain()
        {
            if (x + radius > right)
            {
                oldX = x;
                x = right - radius;
            }
            else if (x - radius < left)
            {
                oldX = x;
                x = radius + left;
            }

            if (y + radius > bottom)
            {
                oldY = y;
                y = bottom - radius;
            }
            else if (y - radius < top)
            {
                oldY = y;
                y = top + radius;
            }
        }
    }
}
