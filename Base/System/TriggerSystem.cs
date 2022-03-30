using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Scenes;
using Base.Components;
using Base.Entities;

namespace Base.System
{
   [Serializable]
   class TriggerSystem:EngineSystem
   {

      public TriggerSystem(Scene s)
      {
         systemSignature = (uint)(1 << ColliderTwoD.GetFamily() | 1 << Trigger.GetFamily());
         registeredEntities = new List<Entity>();
         Init(s);
      }

      public TriggerSystem()
      {
         systemSignature = (uint)(1 << ColliderTwoD.GetFamily() | 1 << Trigger.GetFamily());
         registeredEntities = new List<Entity>();
      }

      public override void Init(Scene s)
      {
         RegisterScene(s);
      }
   }
}
