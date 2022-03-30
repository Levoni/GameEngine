using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utility;
using Microsoft.Xna.Framework;

namespace Base.UI
{
   [Serializable]
   public class SaveInfo : UI.Form
   {
      public List<Label> saveLabels;

      public SaveInfo() : base()
      {
         //saveLabels = new List<Label>();
         //string[] tempSaveArray = GetSavePaths();
         //for (int i = 0; i < tempSaveArray.Length; i++)
         //{
         //   Label templabel;
         //   Serialization.SaveFile save = Serialization.Serializer<Serialization.SaveFile>.LoaGame(tempSaveArray[i]);
         //   templabel = new Label("lblSave" + save.FileName, save.FileName, new Rectangle(bounds.X + 50, bounds.Y + (bounds.Height / tempSaveArray.Length) * i, bounds.Width, bounds.Height / tempSaveArray.Length), textColor);
         //   saveLabels.Add(templabel);
         //}
         //foreach (Control c in saveLabels)
         //   AddControl(c);
      }

      public SaveInfo(string name, string value, EngineRectangle bounds, Color color, List<Control> controls):base(name,value,bounds,color,controls)
      {
         //saveLabels = new List<Label>();
         //string[] tempSaveArray = GetSavePaths();
         //for (int i = 0; i < tempSaveArray.Length; i++)
         //{
         //   Label templabel;
         //   Serialization.SaveFile save = Serialization.Serializer<Serialization.SaveFile>.LoaGame(tempSaveArray[i]);

         //   templabel = new Label("lblSave" + save.FileName, save.FileName, new Rectangle(bounds.X + 50, bounds.Y + (bounds.Height / tempSaveArray.Length) * i, bounds.Width, bounds.Height / tempSaveArray.Length), textColor);
         //   saveLabels.Add(templabel);
         //}
         //foreach (Control c in saveLabels)
         //   AddControl(c);
      }

      private string[] GetSavePaths()
      {
         string[] fullPaths = global::System.IO.Directory.GetFiles("./Save", "*.save");
         List<string> fileNames = new List<string>();
         foreach(string s in fullPaths)
         {
            fileNames.Add(s.Split('\\').Last());
         }
         return fileNames.ToArray();
      }


   }
}
