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
        private List<BaseAnimation> _animations = new List<BaseAnimation>();

        public Rectangle Rectangle;

        public Animator(Rectangle rectangle)
        {
            this.Rectangle = rectangle;
        }

        public virtual void Update()
        {
            for (int i = _animations.Count - 1; i >= 0; i--)
            {
                _animations[i].Update(this);
                if (_animations[i].IsComplete())
                {
                    _animations.RemoveAt(i);
                }
            }
        }

        public void AddAnimation(BaseAnimation animation) => _animations.Add(animation);
    }
}
