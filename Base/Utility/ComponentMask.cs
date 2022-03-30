using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Components;

namespace Base.Utility
{
   [Serializable]
   public class ComponentMask
   {
      Mask mask;
      Mask oldMask;

      public ComponentMask()
      {
         mask =  new Mask();
         oldMask = new Mask();
      }

      public void SetBit(int shift)
      {
         oldMask.SetMaskValue(mask);
         mask.SetBit(shift);
      }

      public void AddComponent<componentType>() //where T:BaseComponent
      {

         oldMask.SetMaskValue(mask);
         mask.SetBit(Component<componentType>.GetFamily());
      }

      public void AddComponent(int componentFamily) //where T:BaseComponent
      {

         oldMask.SetMaskValue(mask);
         mask.SetBit(componentFamily);
      }

      public void RemoveComponent<componentType>()
      {
         oldMask.SetMaskValue(mask);
         mask.ClearBit(Component<componentType>.GetFamily());
      }

      public void RemoveComponent(int componentFamily)
      {
         oldMask.SetMaskValue(mask);
         mask.ClearBit(componentFamily);
      }

      /// <summary>
      /// MaskOne contains MaskTwo
      /// </summary>
      /// <param name="maskOne">Containing mask</param>
      /// <param name="maskTwo">Mask to be contained</param>
      /// <returns></returns>
      private bool Contains(Mask maskOne, Mask maskTwo)
      {
         return maskOne.Contains(maskTwo);
      }
      
      public bool IsNewMatch(uint system_mask)
      {
         return mask.Contains(system_mask) && !oldMask.Contains(system_mask);
      }

      public bool noLongerMatch(uint system_mask)
      {
         return !mask.Contains(system_mask) && oldMask.Contains(system_mask);
      }

      // Getters
      public Mask GetCurrentMask()
      {
         return mask;
      }
   }
}
