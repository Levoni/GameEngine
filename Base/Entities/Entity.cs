using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entities
{
   [Serializable]
   public class Entity
   {
      public int id;

      public Entity() { }

      public Entity(int ID)
      {
         id = ID;
      }
   }

}
