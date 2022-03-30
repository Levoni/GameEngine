using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utility;
using Microsoft.Xna.Framework;

namespace Base.Camera
{
   [Serializable]
   //TODO: switch all vector2 to EngineVector2
   //Default position 0,0 is in the center of the screen
   public class TwoDCamera
   {
      public Vector2 Position;
      public float Zoom;
      public float MaxZoom;
      public float MinZoom;
      public float Rotation;
      public int ViewportWidth;
      public int ViewportHeight;
      public EngineRectangle ClampBounds;

      public Vector2 ViewportCenter
      {
         get
         {
            return new Vector2(ViewportWidth * .5f, ViewportHeight * .5f);
         }
      }
      public Matrix translationMatrix
      {
         get
         {
            return Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0)
               * Matrix.CreateRotationZ(Rotation)
               * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))
               * Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
         }
      }

      public TwoDCamera()
      {
         Zoom = 1;
         MaxZoom = 1;
         MinZoom = .1f;
         Rotation = 0;
         Position = new Vector2(0, 0);
         ViewportWidth = 1920;
         ViewportHeight = 1080;
         ClampBounds = new EngineRectangle(float.MinValue / 2,float.MinValue / 2,float.MaxValue,float.MaxValue);
      }

      public void AdjuctZoom(float amount)
      {
         Zoom += amount;
         if (Zoom < MinZoom)
         {
            Zoom = MinZoom;
         }
         else if (Zoom > MaxZoom)
         {
            Zoom = MaxZoom;
         }
      }

      public void MoveCamera(Vector2 cameraMovement, bool clampToMap = false)
      {
         Vector2 newPosition = Position + cameraMovement;

         if (clampToMap)
         {
            Position = ClampToBounds(newPosition);
         }
         else
         {
            Position = newPosition;
         }
      }

      public Rectangle ViewportWorldBoundry()
      {
         Vector2 viewPortCorner = ScreenToWorld(new Vector2(0, 0));
         Vector2 viewPortBottomCorner = ScreenToWorld(new Vector2(ViewportWidth, ViewportHeight));
         return new Rectangle((int)viewPortCorner.X,
            (int)viewPortCorner.Y,
            (int)viewPortCorner.X - (int)viewPortBottomCorner.X,
            (int)viewPortCorner.Y - (int)viewPortBottomCorner.Y);
      }

      public void CenterOn(Vector2 position)
      {
         Position = position;
      }

      public Vector2 WordToScreen(Vector2 worldPosition)
      {
         return Vector2.Transform(worldPosition, translationMatrix);
      }

      public Vector2 ScreenToWorld(Vector2 screenPosition)
      {
         return Vector2.Transform(screenPosition, Matrix.Invert(translationMatrix));
      }

      public Vector2 ClampToBounds(Vector2 position)
      {
         if (ClampBounds != null)
         {
            var cameraMax = new Vector2(ClampBounds.X + ClampBounds.Width - (ViewportWidth / Zoom / 2),
                                        ClampBounds.Y + ClampBounds.Height - (ViewportHeight / Zoom / 2));
            var cameraMin = new Vector2(ClampBounds.X + (ViewportWidth / Zoom / 2),
                                        ClampBounds.Y + (ViewportHeight / Zoom / 2));
            return Vector2.Clamp(position, cameraMin, cameraMax);
         }
         return position;
      }
   }
}
