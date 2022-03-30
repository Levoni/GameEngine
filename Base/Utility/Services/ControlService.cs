using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Base.Utility.Enums;

namespace Base.Utility.Services
{
   public static class ControlService
   {
      public static Dictionary<ControlType, GameControl> controls;
      public static bool isInitialized = false;

      public static void Init()
      {
         isInitialized = true;
         if (DirectoryService.DoesFileExist("./controls.control"))
         {
            controls = (Dictionary < ControlType, GameControl >) Serialization.BSerializer.deserializeObject("controls", "control", "");
         }


         if (controls == null)
         {
            controls = new Dictionary<ControlType, GameControl>();
            controls.Add(ControlType.up, new GameControl(Keys.W));
            controls.Add(ControlType.down, new GameControl(Keys.S));
            controls.Add(ControlType.right, new GameControl(Keys.D));
            controls.Add(ControlType.left, new GameControl(Keys.A));
            controls.Add(ControlType.attack1, new GameControl(mouseButton.leftClick));
            controls.Add(ControlType.attack2, new GameControl(mouseButton.rightClick));
            controls.Add(ControlType.jump, new GameControl(Keys.Space));
            controls.Add(ControlType.modifier1, new GameControl(Keys.LeftShift));
            controls.Add(ControlType.modifier2, new GameControl(Keys.RightShift));
         }
      }

      public static void SaveControls()
      {
         if(controls != null)
         {
            Serialization.BSerializer.serializeObject("controls", "control", controls);
         }
      }

      public static void AddOrUpdateControl(ControlType controlType, GameControl gameControl)
      {
         controls[controlType] = gameControl;
      }

      public static void AddOrUpdateControl(ControlType controlType, Keys gameControl)
      {
         GameControl GC = new GameControl(gameControl);
         controls[controlType] = GC;
      }

      public static void AddOrUpdateControl(ControlType controlType, mouseButton gameControl)
      {
         GameControl GC = new GameControl(gameControl);
         controls[controlType] = GC;
      }

      public static void RemoveControl(ControlType controlType)
      {
         if(controls.ContainsKey(controlType))
         {
            controls.Remove(controlType);
         }
      }
   }
}
