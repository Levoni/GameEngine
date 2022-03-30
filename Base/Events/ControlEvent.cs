using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Base.Utility.Enums;

namespace Base.Events
{
   [Serializable]
   public class ControlEvent
   {
      public Entity e { get; set; }
      public ControlType controlType { get; set; }
      public gameControlState state { get; set; }

      public ControlEvent()
      {
         e = null;
      }

      public ControlEvent(Entity entity, ControlType control, gameControlState state)
      {
         this.e = entity;
         this.controlType = control;
         this.state = state;
      }
   }
}
