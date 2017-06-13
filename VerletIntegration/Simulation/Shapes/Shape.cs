using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerletIntegration.Simulation.Shapes
{
    class Shape
    {
        protected List<Vertex> vertcies;
        protected List<Stick> sticks;

        public bool HideVertcies
        {
            get;
            set;
        }

        public bool HideLines
        {
            get;
            set;
        }

        public Shape()
        {
            vertcies = new List<Vertex>();
            sticks = new List<Stick>();
        }

        public virtual void Update(float time)
        {
            foreach (Vertex vertex in vertcies)
            {
                vertex.Update(time);
            }

            foreach (Stick stick in sticks)
            {
                stick.Update(time);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!HideVertcies)
            {
                foreach (Vertex vertex in vertcies)
                {
                    vertex.Draw(spriteBatch);
                }
            }

            if (!HideLines)
            {
                foreach (Stick stick in sticks)
                {
                    stick.Draw(spriteBatch);
                }
            }
        }
    }
}
