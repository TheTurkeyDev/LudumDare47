using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Animation
{
    public abstract class BaseAnimation
    {
        protected bool _complete = false;

        public abstract void Update(Animator animator);

        public virtual void Draw(Animator animator) { }

        public virtual bool IsComplete() => _complete;

        public void SetComplete() => _complete = true;
    }
}
