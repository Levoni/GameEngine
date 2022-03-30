using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Components
{
   [Serializable]
   public class BaseComponentManager
   {
      virtual public void Destroy(int entityID)
      { }

      virtual public Dictionary<int, BaseComponent> GetEntityIComponentDictionary()
      { return default(Dictionary<int, BaseComponent>); }

   }
}
