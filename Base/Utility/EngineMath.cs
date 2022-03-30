using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility
{
   public static class EngineMath
   {
      public static float CelingAwayFromZero(float number)
      {
         if (number > 0)
            return (float)Math.Ceiling(number);
         if (number < 0)
            return (float)Math.Floor(number);
         return number;
      }

      public static float ClosestToZero(float number, float numberTwo)
      {
         if (Math.Abs(number) < Math.Abs(numberTwo))
            return number;
         else
            return numberTwo;
      }
   }
}
