using EG2DCS.Engine.Animation;
using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework;
using System;

namespace EG2DCS.Engine.Toast
{
    public class BaseToast : Animator
    {
        private Color _color;

        private int _timer = 0;
        private bool _delay = false;

        private Vector2 _from;
        private Vector2 _to;

        private bool _complete = false;

        public BaseToast() : base(new Rectangle()) {}

        public virtual void Start()
        {
            _color = new Color(Universal.rnd.Next(256), Universal.rnd.Next(256), Universal.rnd.Next(256), 255);

            _from = new Vector2((Universal.GameSize.X / 2) - 250, Universal.GameSize.Y + 100);
            _to = new Vector2(_from.X, _from.Y - 300);

            Rectangle = new Rectangle((int)_from.X, (int)_from.Y, 500, 100);
            base.AddAnimation(new MoveAnimation(_to, _from, 120, () =>
            {
                _delay = true;
            }));
        }

        public override void Update()
        {
            base.Update();

            if (_delay)
            {
                if (_timer == 240)
                {
                    _delay = false;
                    base.AddAnimation(new MoveAnimation(_from, _to, 120, () =>
                    {
                        _complete = true;
                    }));
                }

                _timer++;
            }
        }

        public virtual void Draw()
        {
            if (_complete)
                return;

            Universal.SpriteBatch.Draw(Textures.Null, Rectangle, _color);
        }

        public virtual void Remove()
        {
            _complete = true;
        }

        public bool IsComplete()
        {
            return this._complete;
        }
    }
}
