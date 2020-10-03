using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Animation
{
    public class Animator
    {
        private List<BaseAnimation> Animations { get; set; } = new List<BaseAnimation>();

        public Rectangle Rectangle;

        public Animator(Rectangle rectangle)
        {
            this.Rectangle = rectangle;
        }

        public virtual void Update()
        {
            for (int i = Animations.Count - 1; i >= 0; i--)
            {
                Animations[i].Update(this);
                if (Animations[i].Complete)
                {
                    Animations.RemoveAt(i);
                }
            }
        }

        public void AddAnimation(BaseAnimation animation)
        {
            Animations.Add(animation);
        }
    }
}
