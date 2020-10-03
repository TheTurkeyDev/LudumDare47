using System;
using Microsoft.Xna.Framework;

namespace EG2DCS.Engine.Add_Ons
{
    class Maths
    {
        public static Random Genf { get; set; } = new Random();

        public static int RNGfree(int low, int high)
        {
            Random gen = new Random();
            return gen.Next(low, high + 1);
        }

        public static int RNGfixed(int low, int high, int seed, bool reset)
        {
            if (reset)
            {
                Genf = new Random(seed);
            }
            return Genf.Next(low, high + 1);
        }
        public static bool Collision(Rectangle a, Rectangle b)
        {
            return a.X < b.X + b.Width && a.X + a.Width > b.X && a.Y < b.Y + b.Height && a.Y + a.Height > b.Y;
        }
        public static bool Collisioninside(Rectangle a, Rectangle b)
        {
            return a.X > b.X && a.X + a.Width < b.X + b.Width && a.Y > b.Y && a.Y + a.Height < b.Y + b.Height;
        }
        public static bool Collisionoutside(Rectangle a, Rectangle b)
        {
            return a.X < b.X && a.X + a.Width > b.X + b.Width && a.Y < b.Y && a.Y + a.Height > b.Y + b.Height;
        }
        public static bool Collisioncircle(Vector2 a, float ar, Vector2 b, float br)
        {
            return Distance(a, b) > ar && Distance(a, b) < br;
        }
        public static float Distance(Vector2 a, Vector2 b)
        {
            return (float)Math.Abs(Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2)));
        }
    }
}
