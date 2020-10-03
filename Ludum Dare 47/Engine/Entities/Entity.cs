using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Entities
{
    class Entity
    {
        public Rectangle InitPosition;
        protected Vector2 _velocity = new Vector2(0, 0);
        public Vector2 Velocity => _velocity;
        protected Rectangle _position;
        public Rectangle Position => _position;
        protected bool InAir = true;
        public bool IsDead { get; set; }
        public bool Gravity { get; set; } = true;

        public Entity(Rectangle position)
        {
            _position = position;
            InitPosition = new Rectangle(position.Location, position.Size);
        }

        public virtual void Reset()
        {
            _position = new Rectangle(InitPosition.Location, InitPosition.Size);
            _velocity = new Vector2(0, 0);
            InAir = false;
            IsDead = false;
        }

        public virtual void Update()
        {
            if (Gravity)
                _velocity.Y += InAir ? 0.5f : 0.1f;
        }

        public virtual void Draw(int offsetX, int offsetY)
        {}

        public void MoveEntity(int xMov, int yMov)
        {
            _position.X += xMov;
            _position.Y += yMov;
        }

        public void SetOnGround()
        {
            if (InAir && _velocity.Y >= 0)
                InAir = false;
            _velocity.Y = 0;
        }

        public virtual bool OnCollide(Entity ent) => true;
    }
}
