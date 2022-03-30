using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility
{
   [Serializable]
   public class EngineRectangle
   {
      public float X, Y, Width, Height;
      public EngineVector2 Center
      {
         get
         {
            return new EngineVector2(X + (Width / 2),Y + (Height / 2));
         }
      }
      public EngineVector2 Location
      {
         get
         {
            return new EngineVector2(X, Y);
         }
      }
      public float Top
      {
         get
         {
            return Y;
         }
      }
      public float Right
      {
         get
         {
            return X + Width;
         }
      }
      public float Down
      {
         get
         {
            return Y + Height;
         }
      }
      public float Left
      {
         get
         {
            return X;
         }
      }
      public float Area
      { 
         get
         {
            return Width * Height;
         }
      }
      public bool Equals(EngineRectangle rect)
      {
         return X == rect.X 
            && Y == rect.Y 
            && Width == rect.Width 
            && Height == rect.Height;
      }
      public Rectangle toRectangle()
      {
         return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
      }

      public EngineRectangle()
      {
         X = Y = Width = Height = 0;
      }

     public EngineRectangle(float X, float Y, float Width, float Height)
      {
         this.X = X;
         this.Y = Y;
         this.Width = Width;
         this.Height = Height;
      }

      public EngineRectangle(Rectangle rect)
      {
         this.X = rect.X;
         this.Y = rect.Y;
         this.Width = rect.Width;
         this.Height = rect.Height;
      }
   }
}
