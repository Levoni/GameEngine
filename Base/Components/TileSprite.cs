using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Components
{
   [Serializable]
   public class TileSprite:Component<TileSprite>
   {
      public int TileId { get; set; }
      public string TileSetName { get; set; }
      public int SourceStartX { get; set; }
      public int SourceStartY { get; set; }
      public int ImageWidth { get; set; }
      public int ImageHeight { get; set; }


   }
}
