using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Components
{
   public class ComponentCounter
   {
      public static int counter = 0;
   }

   [Serializable]
   public class Component<T>:BaseComponent
   {
      public static int family = -1;
      public Component()
      {
         Family();
      }

      public override int Family()
      {
         if(family == -1)
            family = ComponentCounter.counter++;
         return family;
      }

      public override Type FamilyType()
      {
         return typeof(T);
      }

      public static int GetFamilyTest()
      {
         return -1;
      }

      public new static int GetFamily()
      {
         Component < T > temp = new Component<T>();
         return temp.Family();
      }

      public new static Type GetFamilyType()
      {
         return typeof(T);
      }
   }
}
