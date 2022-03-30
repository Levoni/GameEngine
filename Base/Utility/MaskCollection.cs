using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility
{
   //Contains 32 32bit masks
   //Mask 0 is defaulted ot 
   //TODO: think about a better name for the mask stuff
   [Serializable]
   public class MaskCollection
   {
      Mask[] masks;

      public MaskCollection()
      {
         masks = new Mask[32];
         for(int i = 0; i < 32; i++)
         {
            masks[i] = new Mask(uint.MaxValue);
         }
      }

      public MaskCollection(Mask defaultMask)
      {
         masks = new Mask[32];
         for (int i = 0; i < 32; i++)
         {
            masks[i] = new Mask(uint.MaxValue);
         }
      }

      public MaskCollection(uint defaultMask)
      {
         masks = new Mask[32];
         for (int i = 0; i < 32; i++)
         {
            masks[i] = new Mask(uint.MaxValue);
         }
      }

      public Mask GetMask(int maskIndex)
      {
         return masks[maskIndex];
      }

      public void SetMaskValue(int maskIndex, Mask mask)
      {
         masks[maskIndex] = mask;
      }

      public void SetMaskValue(int maskIndex, uint mask)
      {
         masks[maskIndex] = new Mask(mask);
      }

      public void SetMask(int maskIndex)
      {
         masks[maskIndex].SetMask();
      }

      public void ClearMask(int maskIndex)
      {
         masks[maskIndex].ClearMask();
      }

      public void SetBitInMask(int maskIndex, int bitIndex)
      {
         masks[maskIndex].SetBit(bitIndex);
      }

      public void ClearBitInMask(int maskIndex, int bitIndex)
      {
         masks[maskIndex].ClearBit(bitIndex);
      }

      public bool isIdInMask(int maskIndex, int Id)
      {
         return masks[maskIndex].isBitSet(Id);
      }
   }
}
