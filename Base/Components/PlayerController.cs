using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Base.Utility;
using static Base.Utility.Enums;

namespace Base.Components
{
   [Serializable]
   public class PlayerController:Component<PlayerController>
   {
      public Dictionary<ControlType, GameControl> controls;
 
      public float Speed { get; set; }


      public PlayerController()
      {
         controls = new Dictionary<ControlType, GameControl>();
         Speed = 500;
      }

      public PlayerController(float speed, Dictionary<ControlType, GameControl> controls)
      {
         this.controls = controls;
         this.Speed = speed;
      }
   }
}
