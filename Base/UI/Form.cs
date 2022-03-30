using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Base.UI
{
   [Serializable]
   public class Form:Control
   {
      List<Control> controls;

      public Form():base()
      {
         controls = new List<Control>();
      }

      public Form(string name, string value, EngineRectangle bounds, Color color, List<Control> controls):base(name,value,bounds,color)
      {
         this.controls = controls;
      }

      public override void Update(int dt)
      {
         base.Update(dt);
         foreach(Control c in controls)
         {
            c.Update(dt);
         }
      }

      public override void Render(SpriteBatch sb)
      {
         base.Render(sb);
         foreach(Control c in controls)
         {
            c.Render(sb);
         }
      }

      public void AddControl(Control c)
      {
         controls.Add(c);
      }

      public void RemoveControl(Control c)
      {
         controls.Remove(c);
      }

      public void ClearControls()
      {
         controls.Clear();
      }

      public void RemoveControl(string controlName)
      {
         controls.Remove(FindControl(controlName));
      }

      public Control FindControl(Control c)
      {
         return controls.Find((value) => { return c == value; });
      }

      public Control FindControl(string controlName)
      {
         return controls.Find((value) => { return value.Name == controlName; });
      }
   }
}
