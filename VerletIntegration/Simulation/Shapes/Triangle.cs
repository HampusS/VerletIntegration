using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace VerletIntegration.Simulation.Shapes
{
    class Triangle : Shape
    {
        public Vertex v_top, v_left, v_right;
        public Stick s_left, s_bottom, s_right;

        public Triangle(float x, float y, float height, float width)
            : base()
        {
            v_top = new Vertex(x, y);
            vertcies.Add(v_top);
            v_left = new Vertex(x - (width / 2), y + height);
            vertcies.Add(v_left);
            v_right = new Vertex(x + (width / 2), y + height);
            vertcies.Add(v_right);

            s_left = new Stick(v_top, v_left);
            sticks.Add(s_left);
            s_bottom = new Stick(v_left, v_right);
            sticks.Add(s_bottom);
            s_right = new Stick(v_right, v_top);
            sticks.Add(s_right);
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
