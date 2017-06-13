using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace VerletIntegration.Simulation.Shapes
{
    class StickMan : Shape
    {
        Vertex Head, Chest, Hip, LeftHand, RightHand, LeftFoot, RightFoot;
        Stick Neck, Body;

        Vertex LeftElbow, RightElbow, LeftKnee, RightKnee;
        Stick LowerLeftArm, LowerRightArm, UpperLeftArm, UpperRightArm, LeftLeg, RightLeg, LeftThigh, RightThigh;

        Stick supportHead, supportLeftFoot, supportRightFoot;

        public bool AtRest = false;

        public StickMan(float x, float y)
            : base()
        {
            Initialize(x, y);
        }

        void Initialize(float x, float y)
        {
            Head = new Vertex(x, y);
            Head.SetRadius(12);

            Chest = new Vertex(x, y + 15);
            LeftElbow = new Vertex(x - 5, y + 30);
            RightElbow = new Vertex(x + 5, y + 30);
            LeftHand = new Vertex(x - 15, y + 45);
            RightHand = new Vertex(x + 15, y + 45);

            Hip = new Vertex(x, y + 40);
            LeftKnee = new Vertex(x - 10, y + 60);
            RightKnee = new Vertex(x + 10, y + 60);
            LeftFoot = new Vertex(x - 10, y + 80);
            RightFoot = new Vertex(x + 10, y + 80);


            Neck = new Stick(Head, Chest);
            Body = new Stick(Chest, Hip);
            UpperLeftArm = new Stick(Chest, LeftElbow);
            UpperRightArm = new Stick(Chest, RightElbow);
            LowerLeftArm = new Stick(LeftElbow, LeftHand);
            LowerRightArm = new Stick(RightElbow, RightHand);

            LeftThigh = new Stick(Hip, LeftKnee);
            RightThigh = new Stick(Hip, RightKnee);
            LeftLeg = new Stick(LeftKnee, LeftFoot);
            RightLeg = new Stick(RightKnee, RightFoot);

            supportHead = new Stick(Hip, Head);
            supportLeftFoot = new Stick(LeftFoot, Chest);
            supportRightFoot = new Stick(RightFoot, Chest);

            sticks.Add(supportHead);
            sticks.Add(supportLeftFoot);
            sticks.Add(supportRightFoot);
            supportHead.SetColor(Color.Red);
            supportLeftFoot.SetColor(Color.Red);
            supportRightFoot.SetColor(Color.Red);
            supportHead.Hidden = true;
            supportLeftFoot.Hidden = true;
            supportRightFoot.Hidden = true;

            vertcies.Add(Head);
            vertcies.Add(Chest);
            vertcies.Add(Hip);
            vertcies.Add(LeftElbow);
            vertcies.Add(LeftHand);
            vertcies.Add(LeftKnee);
            vertcies.Add(LeftFoot);
            vertcies.Add(RightElbow);
            vertcies.Add(RightHand);
            vertcies.Add(RightKnee);
            vertcies.Add(RightFoot);

            sticks.Add(Neck);
            sticks.Add(Body);
            sticks.Add(UpperLeftArm);
            sticks.Add(UpperRightArm);
            sticks.Add(LowerLeftArm);
            sticks.Add(LowerRightArm);
            sticks.Add(LeftThigh);
            sticks.Add(RightThigh);
            sticks.Add(LeftLeg);
            sticks.Add(RightLeg);

            foreach (Stick line in sticks)
            {
                line.SetStiffness(3);
            }
        }

        public override void Update(float time)
        {
            if (LeftFoot.Position().Y + LeftFoot.GetRadius() >= Game1.Height - LeftFoot.GetRadius() || LeftFoot.Position().Y + RightFoot.GetRadius() >= Game1.Height - RightFoot.GetRadius())
            {
                Head.AtRest = true;
                Hip.AtRest = true;
                LeftFoot.AtRest = true;
                RightFoot.AtRest = true;
                AtRest = true;
            }
            else
            {
                foreach (Vertex vert in vertcies)
                {
                    vert.AtRest = false;
                }
                AtRest = false;
            }

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void MoveX(float speed)
        {
            Head.SetVX(speed);
            Chest.SetVX(speed);
            Hip.SetVX(speed);
            if (speed > 0)
                RightKnee.SetVX(speed);
            else
                LeftKnee.SetVX(speed);

        }

        public void MoveY(float speed)
        {
            Head.SetVY(speed);
            Chest.SetVY(speed);
            Hip.SetVY(speed);
        }
    }
}
