using EG2DCS.Engine.Globals;
using EG2DCS.Engine.Screen_Manager;
using Ludum_Dare_47.Engine.Worlds;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum_Dare_47.Engine.Entities
{
    [JsonConverter(typeof(CustomEntityConverter))]
    public class Entity
    {
        public virtual string EntId { get; } = "entity";

        public World World { get; set; }

        protected Vector2 velocity = new Vector2(0, 0);
        public Vector2 Velocity { get => velocity; }

        public Rectangle initPosition = new Rectangle(0, 0, 0, 0);
        public Rectangle position = new Rectangle(0, 0, 0, 0);
        public Rectangle Position { get => position; }

        protected bool inAir = true;

        public bool IsDead { get; set; } = false;

        public bool Gravity { get; set; } = true;

        public Entity(Rectangle position)
        {
            this.position = position;
            this.initPosition = new Rectangle(position.Location, position.Size);
        }

        public virtual void Setup(World World)
        {
            this.World = World;
        }

        public virtual void Reset()
        {
            this.position = new Rectangle(initPosition.Location, initPosition.Size);
            this.velocity = new Vector2(0, 0);
            inAir = false;
            IsDead = false;
        }

        public virtual void Update()
        {
            if (Gravity)
                velocity.Y += inAir || velocity.Y > 0 ? 0.5f : 0.1f;
        }

        public virtual void Draw(int offsetX, int offsetY)
        {
            Universal.SpriteBatch.Draw(Textures.Null, new Rectangle((int)Position.X + offsetX, (int)Position.Y + offsetY, Position.Width, Position.Height), Color.Pink);
        }

        public void MoveEntity(int xMov, int yMov)
        {
            position.X += xMov;
            position.Y += yMov;
        }

        public bool InAir()
        {
            return this.inAir;
        }

        public void SetOnGround()
        {
            if (inAir && velocity.Y >= 0)
                this.inAir = false;
            else if (velocity.Y <= 0)
                velocity.Y /= 2;
            else
                velocity.Y = 0;
        }

        public virtual bool OnCollide(Entity ent)
        {
            return true;
        }
    }
}
