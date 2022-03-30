using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Base.Camera;

namespace Base.Utility.Services
{
   public static class CameraService
   {
      public static TwoDCamera camera;

      public static void InitSystem()
      {
         camera = new TwoDCamera();
      }
   }
}
