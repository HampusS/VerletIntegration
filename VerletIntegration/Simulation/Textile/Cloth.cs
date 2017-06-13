using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using VerletIntegration.Simulation.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VerletIntegration.Simulation.Textile
{
    class Cloth : Shape
    {

        int width, height;
        float restLength;

        Vertex[,] net;

        public Cloth(float x, float y, int width, int height, float distance)
            : base()
        {
            restLength = distance;
            this.width = width;
            this.height = height;
            net = new Vertex[width, height];

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Vertex temp = new Vertex(x + i * restLength, y + j * restLength);
                    if (j == 0)
                        //if (i == 0 || i == width - 1)
                            temp.Pinned = true;
                    net[i, j] = temp;
                }
            }

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (i + 1 < width)
                        sticks.Add(new Stick(net[i, j], net[i + 1, j]));
                    if (j + 1 < height)
                        sticks.Add(new Stick(net[i, j], net[i, j + 1]));
                    if (i + 1 < width && j + 1 < height)
                    {
                        //sticks.Add(new Stick(net[i, j], net[i + 1, j + 1]));
                        //sticks.Add(new Stick(net[i, j + 1], net[i + 1, j]));
                    }
                }
            }
            foreach (Stick stick in sticks)
            {
                stick.SetStiffness(1);
            }
            foreach (Vertex vert in net)
            {
                vert.Hidden = true;
                vertcies.Add(vert);
            }
        }

        public override void Update(float time)
        {
            base.Update(time);
            foreach (Vertex vert in vertcies)
            {
                Random rnd = new Random();
                vert.AccumulateForces(rnd.Next(-100, 100), 0);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
