using Base.Utility;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.UI
{
   public interface IListBoxItem
   {
      object Value { get; set; }
      bool isSelected { get; set; }
      float MinFontSize { get; set; }
      float MaxFontSize { get; set; }
      void RenderToBounds(SpriteBatch sb, EngineRectangle renderBounds);
   }
}
