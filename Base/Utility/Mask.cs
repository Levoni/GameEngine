using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility
{
   [Serializable]
   public class Mask
   {
      uint value;

      public Mask()
      {
         this.value = 0;
      }

      public Mask(uint maskValue)
      {
         this.value = maskValue;
      }

      public void SetBit(int index)
      {
         value |= (uint)(1 << index);
      }

      public void ClearBit(int index)
      {
         value &= ~(uint)(1 << index);
      }

      public void SetMask()
      {
         value = uint.MaxValue;
      }

      public void ClearMask()
      {
         value = 0;
      }

      public void SetMaskValue(uint mask)
      {
         value = mask;
      }

      public void SetMaskValue(Mask mask)
      {
         value = mask.value;
      }

      public int GetBit(int index)
      {
         return (value & (1 << index)) > 0 ? 1 : 0; 
      }

      public uint GetMask()
      {
         return value;
      }

      public bool isBitSet(int index)
      {
         return (value & (1 << index)) > 0;
      }

      public bool Contains(Mask mask)
      {
         return (value & mask.value) == mask.value;
      }

      public bool Contains(uint mask)
      {
         return (value & mask) == mask;
      }
   }
}
