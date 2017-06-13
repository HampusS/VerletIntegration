using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerletIntegration.Simulation.Shapes;

namespace VerletIntegration.Simulation
{
    class Box : Shape
    {
        public Vertex TopLeft, BottomLeft, BottomRight, TopRight;
        public Stick Left, Bottom, Right, Top, SupportBeam1, SupportBeam2;
       

        public Box(int x, int y, int width, int height)
            : base()
        {
            InitializeBox(x, y, width, height);
        }

        public void InitializeBox(int x, int y, int width, int height)
        {
            Random rnd = new Random();
            TopLeft = new Vertex(x, y);
            TopLeft.SetVX(rnd.Next(10, 100));
            BottomLeft = new Vertex(x, y + height);
            BottomRight = new Vertex(x + width, y + height);
            TopRight = new Vertex(x + width, y);
            vertcies.Add(TopLeft);
            vertcies.Add(BottomLeft);
            vertcies.Add(BottomRight);
            vertcies.Add(TopRight);

            Left = new Stick(TopLeft, BottomLeft);
            Bottom = new Stick(BottomLeft, BottomRight);
            Right = new Stick(BottomRight, TopRight);
            Top = new Stick(TopRight, TopLeft);
            SupportBeam1 = new Stick(TopLeft, BottomRight);
            SupportBeam2 = new Stick(BottomLeft, TopRight);
            Top.SetColor(Color.Red);
            Top.SetThickness(3);
            sticks.Add(Left);
            sticks.Add(Bottom);
            sticks.Add(Right);
            sticks.Add(Top);
            sticks.Add(SupportBeam1);

            foreach (Stick line in sticks)
            {
                line.SetStiffness(20);
            }
        }

        public void PinAll()
        {
            foreach (Vertex vertex in vertcies)
            {
                vertex.Pinned = true;
            }
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
