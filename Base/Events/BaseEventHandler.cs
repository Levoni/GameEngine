using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Events
{
   public interface BaseEventHandler
   {
      void Execute(object sender, object Event);
   }
}
