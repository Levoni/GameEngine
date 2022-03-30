using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Events
{
   [Serializable]
   public class EHandler<EventType>:BaseEventHandler
   {
      public Action<object, EventType> function;

      public EHandler()
      {
      }

      public EHandler(Action<object, EventType> func)
      {
         function = func;
      }

      public void SetFunction(Action<object, EventType> func)
      {
         function = func;
      }

      public void Execute(object s, object e)
      {
         function(s, (EventType)e);
      }
   }
}
