using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using static Base.Utility.Enums;

namespace Base.Utility.Services
{
   static public class MouseService
   {
      private static MouseState curentMouseState = new MouseState();
      private static MouseState oldMouseState = new MouseState();

      public static void InitService()
      {
         oldMouseState = Mouse.GetState();
         curentMouseState = Mouse.GetState();
      }

      public static void UpdateMouseState()
      {
         oldMouseState = curentMouseState;
         curentMouseState = Mouse.GetState();
      }

      public static MouseState GetCurrentMouseState()
      {
         return curentMouseState;
      }

      public static EngineVector2 GetMousePosition()
      {
         return new EngineVector2(curentMouseState.X, curentMouseState.Y);
      }

      public static int GetMouseXPosition()
      {
         return curentMouseState.X;
      }

      public static int GetMouseYPosition()
      {
         return curentMouseState.Y;
      }

      public static ScrollWheelDirection GetScrollDirection()
      {
         int scrollValueDifferentce = curentMouseState.ScrollWheelValue - oldMouseState.ScrollWheelValue;
         if(scrollValueDifferentce == 0)
         {
            return ScrollWheelDirection.none;
         }
         else if(scrollValueDifferentce > 0)
         {
            return ScrollWheelDirection.up;
         }
         else
         {
            return ScrollWheelDirection.down;
         }
      }

      public static bool DidMouseMove()
      {
         return oldMouseState.Position != curentMouseState.Position;
      }

      public static bool DidMouseMove(MouseState old, MouseState cur)
      {
         return old.Position != cur.Position;
      }

      public static bool DidLeftClickOccur()
      {
         
         return (oldMouseState.LeftButton == ButtonState.Released && curentMouseState.LeftButton == ButtonState.Pressed);
      }

      public static bool DidLeftClickOccur(MouseState old, MouseState cur)
      {
         return (old.LeftButton == ButtonState.Released && cur.LeftButton == ButtonState.Pressed);
      }
      
      public static bool IsLeftButtonDown()
      {
         return curentMouseState.LeftButton == ButtonState.Pressed;
      }

      public static bool DidRightClickOccur()
      {
         return (oldMouseState.RightButton == ButtonState.Released && curentMouseState.RightButton == ButtonState.Pressed);
      }

      public static bool DidRightClickOccur(MouseState old, MouseState cur)
      {
         return (old.RightButton == ButtonState.Released && cur.RightButton == ButtonState.Pressed);
      }

      public static bool IsRightButtonDown()
      {
         return curentMouseState.RightButton == ButtonState.Pressed;
      }

      public static bool DidMiddleClickOccur()
      {
         return (oldMouseState.MiddleButton == ButtonState.Released && curentMouseState.MiddleButton == ButtonState.Pressed);
      }

      public static bool DidMiddleClickOccur(MouseState old, MouseState cur)
      {
         return (old.MiddleButton == ButtonState.Released && cur.MiddleButton == ButtonState.Pressed);
      }

      public static bool IsMiddleButtonDown()
      {
         return curentMouseState.MiddleButton == ButtonState.Pressed;
      }

      public static bool DidXOneClickOccur()
      {
         return (oldMouseState.XButton1 == ButtonState.Released && curentMouseState.XButton1 == ButtonState.Pressed);
      }

      public static bool DidXOneClickOccur(MouseState old, MouseState cur)
      {
         return (old.XButton1 == ButtonState.Released && cur.XButton1 == ButtonState.Pressed);
      }

      public static bool IsXOneButtonPresed()
      {
         return curentMouseState.XButton1 == ButtonState.Pressed;
      }

      public static bool DidXTwoClickOccur()
      {
         return (oldMouseState.XButton2 == ButtonState.Released && curentMouseState.XButton2 == ButtonState.Pressed);
      }

      public static bool DidXTwoClickOccur(MouseState old, MouseState cur)
      {
         return (old.XButton2 == ButtonState.Released && cur.XButton2 == ButtonState.Pressed);
      }

      public static bool IsXTwoButtonPresed()
      {
         return curentMouseState.XButton2 == ButtonState.Pressed;
      }
   }
}
