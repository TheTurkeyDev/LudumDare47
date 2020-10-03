using EG2DCS.Engine.Screen_Manager;
using Microsoft.Xna.Framework.Graphics;
using EG2DCS.Engine.Globals;

namespace EG2DCS.Engine.Blanks
{
    public class Default_Screen : BaseScreen
    {
        private double _aniTime;

        public Default_Screen()
        {
            Name = "DefaultScreen";
            State = ScreenState.Active;
        }

        public override void Update()
        {
            _aniTime += Universal.GameTime.ElapsedGameTime.TotalMilliseconds;
            if (_aniTime > 2)
            {
                _aniTime = 0;
            }
        }
        public override void Draw()
        {
            Universal.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Universal.SpriteBatch.End();
        }
    }
}
