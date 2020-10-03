using EG2DCS.Engine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EG2DCS.Engine.Add_Ons
{
    class DrawLive
    {
        public static RenderTarget2D Screen { get; set; }
        public static GraphicsDeviceManager FreeDraw { get; set; }
        public static SpriteBatch LiveDraw { get; set; }
        public static Texture2D PermGfx { get; set; }

        public static void Send(Texture2D tex)
        {
            FreeDraw = Universal.Graphics;
            Screen = new RenderTarget2D(FreeDraw.GraphicsDevice, tex.Width, tex.Height);
            FreeDraw.GraphicsDevice.SetRenderTarget(Screen);
            LiveDraw = new SpriteBatch(FreeDraw.GraphicsDevice);
            LiveDraw.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            LiveDraw.Draw(tex, new Rectangle(0, 0, tex.Width, tex.Height), new Color(255, 255, 255));
            LiveDraw.End();
        }

        public static void Modify(Texture2D tex, Rectangle Drawto, Rectangle Drawfrom, Color col)
        {
            LiveDraw.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            LiveDraw.Draw(tex, Drawto, Drawfrom, col);
            LiveDraw.End();
        }
        public static Texture2D Retrieve()
        {
            FreeDraw.GraphicsDevice.SetRenderTarget(null);
            Color[] col = new Color[Screen.Width * Screen.Height - 1];
            Screen.GetData(col);
            PermGfx = new Texture2D(Universal.Graphics.GraphicsDevice, Screen.Width, Screen.Height);
            PermGfx.SetData(col);
            Screen.Dispose();
            LiveDraw.Dispose();
            return PermGfx;
        }
        public static Texture2D Transparency(Texture2D td2, Color col)
        {
            Color[] colo = new Color[Screen.Width * Screen.Height - 1];
            td2.GetData(colo);
            for (int q = 0; q < colo.Length - 1; q++)
            {
                if (colo[q] == col)
                {
                    colo[q] = Color.Transparent; 
                }
            }
            td2.SetData(colo);
            return td2;
        }
    }
}
