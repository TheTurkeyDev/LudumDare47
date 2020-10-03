using EG2DCS.Engine.Widgets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Animation
{
    public class ButtonHighlightAnimation : BaseAnimation
    {
        private float _step;
        private int _timer;
        private int _time;

        public ButtonHighlightAnimation(Animator animator, int time)
        {
            _step = animator.Rectangle.Width / (float)time;
            this._time = time;
        }

        public override void Update(Animator animator)
        {
            if (animator.GetType() != typeof(Button) || _complete)
                return;

            Button button = (Button)animator;

            button.HighlightWidth = Math.Min(button.HighlightWidth + _step, button.Rectangle.Width);

            this._timer++;

            if (_timer == _time)
            {
                _complete = true;
            }
        }
    }
}
