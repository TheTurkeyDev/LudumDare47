using EG2DCS.Engine.Widgets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Animation
{
    public class MoveAnimation : BaseAnimation
    {
        private Vector2 _step;
        private int _timer;
        private int _time;

        private Action _onComplete;

        public MoveAnimation(Vector2 from, Vector2 to, int time, Action onComplete)
        {
            _step = (from - to) / time;
            this._time = time;
            this._onComplete = onComplete;
        }

        public override void Update(Animator animator)
        {
            animator.Rectangle.X += (int)_step.X;
            animator.Rectangle.Y += (int)_step.Y;

            this._timer++;

            if (_timer == _time)
            {
                _complete = true;
                _onComplete.Invoke();
            }
        }
    }
}
