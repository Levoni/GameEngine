using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Events
{
   [Serializable]
   public class OnChange:Event
   {
      public object oldValue;
      public object newValue;
      public OnChange(object oldText, object newText)
      {
         oldValue = oldText;
         newValue = newText;
      }
   }
}
