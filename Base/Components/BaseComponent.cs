using Base.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Components
{
   [Serializable]
   public class BaseComponent: IOnDeserialization
   {
      public BaseComponent()
      { }

      public virtual int Family() { return -1; }
      public static int GetFamily() { return -1; }
      public virtual Type FamilyType() { return typeof(BaseComponent); }
      public static Type GetFamilyType() { return typeof(BaseComponent); }
      public virtual void Init() {}

      public virtual void onDeserialized() {}
   }
}
