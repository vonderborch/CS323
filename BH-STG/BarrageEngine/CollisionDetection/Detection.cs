/*
 * Component: Collision Detection System
 * Version: 1.1.2
 * Last Updated: March 28th, 2014
 * Created By: Christian
 * Last Updated: April 14th, 2014
 * Last Updated By: Christian
*/

using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace BH_STG.BarrageEngine.CollisionDetection
{
    public class Detection
    {
        public bool Rectangle(Rectangle Box1, Rectangle Box2)
        {
            if (Box1.Intersects(Box2))
                return true;

            return false;
        }

        public bool RectangleCircle(Rectangle Box, Vector2 Circle, int Radius)
        {
            if (Circle.X >= Box.X && Circle.X <= Box.X && Circle.Y >= Box.Y && Circle.Y <= Box.Y)
                return true;

            float closestX = MathHelper.Clamp(Circle.X, Box.Left, Box.Right),
                  closestY = MathHelper.Clamp(Circle.Y, Box.Top, Box.Bottom),
                  dx = Circle.X - closestX,
                  dy = Circle.Y - closestY,
                  dt = dx * dx + dy * dy,
                  rt = Radius * Radius;

            if (dt < Radius)
                return true;

            return false;
        }

 

        public bool Circle(Vector2 Circle1, int Radius1, Vector2 Circle2, int Radius2)
        {
            Vector2 Distance = Circle1 - Circle2;

            if (Distance.Length() < (Radius1 + Radius2))
                return true;

            return false;
        }

        public bool Triangle(List<Vector2> Triangle1, List<Vector2> Triangle2)
        {
            for (int i = 0; i < 3; i++)
                if (isInTriangle(Triangle1, Triangle2[i]))
                    return true;
            for (int i = 0; i < 3; i++)
                if (isInTriangle(Triangle2, Triangle1[i]))
                    return true;

            return false;
        }

        private bool isInTriangle(List<Vector2> Points, Vector2 point)
        {
            Vector2 Check1 = point - Points[0],
                    Check2 = Points[1] - Points[0],
                    Check3 = Points[2] - Points[0];

            if (Check2.X == 0)
            {
                if (Check3.X == 0)
                    return false;
                if (Check2.Y == 0)
                    return false;
                float i = Check1.X / Check3.X,
                      j = (Check1.Y - Check3.Y * i) / Check2.Y;
                if (i < 0 || i > 1)
                    return false;
                if (j < 0)
                    return false;
            }
            else
            {
                float i = (Check1.Y * Check2.X - Check1.X * Check2.Y), 
                      j = (Check1.X - Check3.X * i) / Check2.X, 
                      k = Check3.Y * Check1.X - Check3.X * Check2.Y;

                if (k == 0)
                    return false;
                if (i < 0 || i > 1)
                    return false;
                if (j < 0)
                    return false;
                if ((i + j) > 1)
                    return false;
            }

            return true;
        }
    }
}
