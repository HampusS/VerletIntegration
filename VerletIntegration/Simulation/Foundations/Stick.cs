using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerletIntegration.Simulation
{
    class Stick
    {
        float length;
        int thickness;

        public Vertex vert1, vert2;
        Color color;
        float angle;

        int stiffness = 1;

        float dx;
        float dy;
        float distance;
        float difference;
        float offsetX;
        float offsetY;


        public void SetStiffness(int i)
        {
            stiffness = i;
        }

        Rectangle HitBox()
        {
            float temp = Vector2.Distance(vert1.Position(), vert2.Position());

            return new Rectangle((int)vert1.Position().X, (int)vert1.Position().Y, (int)temp, thickness);
        }

        public void SetThickness(int index)
        {
            thickness = index;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public bool Hidden
        {
            get;
            set;
        }

        public Stick(Vertex vert1, Vertex vert2)
        {
            this.vert1 = vert1;
            this.vert2 = vert2;
            SetColor(Color.Black);
            SetThickness(1);

            length = Vector2.Distance(vert1.Position(), vert2.Position());

            angle = (float)Math.Atan2(vert2.Position().Y - vert1.Position().Y, vert2.Position().X - vert1.Position().X);
        }

        public void Update(float time)
        {
            for (int i = 0; i < stiffness; i++)
            {
                dx = vert2.Position().X - vert1.Position().X;
                dy = vert2.Position().Y - vert1.Position().Y;
                distance = (float)Math.Sqrt(dx * dx + dy * dy);
                difference = (length - distance) / distance;
                offsetX = dx * difference * 0.5f;
                offsetY = dy * difference * 0.5f;
                if (!vert1.Pinned)
                {
                    vert1.AccumulateX(-offsetX);
                    vert1.AccumulateY(-offsetY);
                }
                if (!vert2.Pinned)
                {
                    vert2.AccumulateX(offsetX);
                    vert2.AccumulateY(offsetY);
                }
                angle = (float)Math.Atan2(vert2.Position().Y - vert1.Position().Y, vert2.Position().X - vert1.Position().X);
                //vert1.Constrain();
                //vert2.Constrain();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Hidden)
                spriteBatch.Draw(Game1.pixel, HitBox(), null, color, angle, new Vector2(0, 1), SpriteEffects.None, 1);
        }
    }
}
