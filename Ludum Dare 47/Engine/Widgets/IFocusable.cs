using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EG2DCS.Engine.Widgets
{
    public interface IFocusable
    {
        void OnFocus();

        void OnUnFocus();

        bool OnKeyPress(Keys key);

        bool OnKeyRelease(Keys key);
    }
}
