using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility.Services
{
   static class MaskCollectionService
   {
      static Dictionary<string, MaskCollection> layerMasks;

      static void Init()
      {
         layerMasks = new Dictionary<string, MaskCollection>();
      }

      static bool LayerExists(string maskType)
      {
         return layerMasks.ContainsKey(maskType);
      }

      static MaskCollection GetMask(string maskType)
      {
         if (!layerMasks.ContainsKey(maskType))
         {
            layerMasks.Add(maskType, new MaskCollection());
         }
         return layerMasks[maskType];
      }

      static void CreateMask(string maskType)
      {
         if (!layerMasks.ContainsKey(maskType))
         {
            layerMasks.Add(maskType, new MaskCollection());
         }
      }

      static void SetMask(string maskType, int maskIndex, uint mask)
      {
         if (!layerMasks.ContainsKey(maskType))
         {
            layerMasks.Add(maskType, new MaskCollection());
         }
         layerMasks[maskType].SetMaskValue(maskIndex, mask);
      }

      static void SetBitInMask(string maskType, int maskIndex, int bitIndex)
      {
         if (!layerMasks.ContainsKey(maskType))
         {
            layerMasks.Add(maskType, new MaskCollection());
         }
         layerMasks[maskType].SetBitInMask(maskIndex, bitIndex);
      }

      static void ClearBitInMask(string maskType, int maskIndex, int bitIndex)
      {
         if (!layerMasks.ContainsKey(maskType))
         {
            layerMasks.Add(maskType, new MaskCollection());
         }
         layerMasks[maskType].ClearBitInMask(maskIndex, bitIndex);
      }

      static void ClearMask(string maskType, int maskIndex)
      {
         if (layerMasks.ContainsKey(maskType))
         {
            layerMasks[maskType].ClearMask(maskIndex);
         }
      }

      static void SetAllMask(string maskType, int maskIndex)
      {
         if (layerMasks.ContainsKey(maskType))
         {
            layerMasks[maskType].SetMask(maskIndex);
         }
      }
   }
}
