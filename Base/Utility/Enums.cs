using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility
{
   [Serializable]
   public class Enums
   {
      //UI control State
      [Serializable]
      public enum cState
      {
         none,
         hover,
         pressed,
         released,
         draging
      }

      [Serializable]
      public enum gameControlState
      {
         keyDown,
         keyPressed,
         keyUp,
         keyNotPressed
      }

      [Serializable]
      public enum keyType
      {
         NONE,
         CHARACTER,
         COMMAND,
         MODIFIYER,
      }

      [Serializable]
      public enum commandType
      {
         DELETE_BACK,
         DELETE_FOWARD,
         ESCAPE,
      }

      [Serializable]
      //Collision Enums
      public enum colliderShape
      {
         Box,
         Line,
         Circle,
         Polygon
      }

      [Serializable]
      public enum mouseButton
      {
         leftClick,
         middleClick,
         rightClick,
         x1Click,
         x2Click,
         none
      }

      [Serializable]
      public enum TextAchorLocation
      {
         topLeft,
         centerLeft,
         center,
      }

      [Serializable]
      public enum ScrollWheelDirection
      {
         none,
         up,
         down
      }

      [Serializable]
      public enum ControlType
      {
         up,
         down,
         right,
         left,
         attack1,
         attack2,
         modifier1,
         modifier2,
         jump,
         ability1,
         ability2,
      }
   }
}
