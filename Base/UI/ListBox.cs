using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Base.Utility.Services;
using Base.Utility;
using static Base.Utility.Enums;
using Base.Events;

namespace Base.UI
{
   //TODO: add another variabe to set magin of text from left side
   [Serializable]
   public class ListBox : Control
   {
      public List<IListBoxItem> Items;
      public int startingIndex;
      public int selectedIndex;
      public bool DisplayButtons { get; set; }
      public int itemBaseHeight = 100;
      private int actualItemHeight;
      private int possibleCount;

      public Button upButton;
      public Button downButton;
      public Control bar;

      public ListBox(string name, string value, EngineRectangle Rectangle, Microsoft.Xna.Framework.Color color) : base(name, value, Rectangle, color)
      {
         Items = new List<IListBoxItem>();
         this.value = value;
         //testing
         //Items.Add("One");
         //Items.Add("Two");
         //Items.Add("Three");
         //Items.Add("Four");
         //Items.Add("Five");
         selectedIndex = 0;
         startingIndex = 0;
         isEditable = true;

         upButton = new Button("upButton", "", new EngineRectangle(Rectangle.X + Rectangle.Width - Rectangle.Width / 20, Rectangle.Y, Rectangle.Width / 20, Rectangle.Height / 2), Color.Black);
         upButton.setImageReferences("button_arrow_up_default_none", "button_arrow_up_default_hover", "button_arrow_up_default_pressed", "button_arrow_up_default_released");
         downButton = new Button("downButton", "", new EngineRectangle(Rectangle.X + Rectangle.Width - Rectangle.Width / 20, Rectangle.Y + Rectangle.Height / 2, Rectangle.Width / 20, Rectangle.Height / 2), Color.Black);
         downButton.setImageReferences("button_arrow_down_default_none", "button_arrow_down_default_hover", "button_arrow_down_default_pressed", "button_arrow_down_default_released");
         bar = new Control("bar", "", new EngineRectangle(Rectangle.X + Rectangle.Width - Rectangle.Width / 20, Rectangle.Y, Rectangle.Width / 20, Rectangle.Height), Color.Black);
         bar.setImageReferences("black", "black", "black", "black");
         bar.init();

         padding = new int[]
         {
            20,
            (int) (Rectangle.Width / 20),
            20,
            20
         };

         init();
      }

      public override void Update(int dt)
      {
         base.Update(dt);
         if (Items.Count > 1 && DisplayButtons)
         {
            upButton.Update(dt);
            downButton.Update(dt);
         }
      }


      public override void Render(SpriteBatch sb)
      {
         base.Render(sb);
         EngineRectangle safeBounds = new EngineRectangle(bounds.X + padding[3], bounds.Y + padding[0], bounds.Width - (float)(padding[1] + padding[3]), bounds.Height - (float)(padding[0] + padding[2]));
         int pCount = possibleCount = (int)(safeBounds.Height / itemBaseHeight);
         int individualHeight = actualItemHeight = (int)(safeBounds.Height / pCount);
         for (int i = startingIndex; i < Items.Count && i < startingIndex + pCount; i++)
         {
            if (selectedIndex == i - startingIndex)
            {
               Items[i].isSelected = true;
               Items[i].RenderToBounds(sb, new EngineRectangle(safeBounds.X, safeBounds.Y + ((i - startingIndex) * individualHeight), safeBounds.Width, individualHeight));
            }
            else
            {
               Items[i].isSelected = false;
               Items[i].RenderToBounds(sb, new EngineRectangle(safeBounds.X, safeBounds.Y + ((i - startingIndex) * individualHeight), safeBounds.Width, individualHeight));
            }
         }


         if (Items.Count > 1)
         {
            string test = Items[0].Value.ToString();
            if (DisplayButtons)
            {
               upButton.Render(sb);
               upButton.onClick = new EHandler<OnClick>(new Action<object, OnClick>(upButtonClickHandle));
               downButton.Render(sb);
               downButton.onClick = new EHandler<OnClick>(new Action<object, OnClick>(downButtonClickHandle));
            }

            //if (Items.Count <= possibleCount)
            //{
            //   bar.bounds.Y = safeBounds.Y;
            //   bar.bounds.Height = safeBounds.Height;
            //}
            if (Items.Count > possibleCount)
            {
               float startPercentage = (float)startingIndex / (float)Items.Count;
               bar.bounds.Y = safeBounds.Y + safeBounds.Height * startPercentage;
               float visiblePercentage = (float)possibleCount / (float)Items.Count;
               bar.bounds.Height = visiblePercentage * safeBounds.Height;
               bar.Render(sb);
            }
         }
      }

      public void AddItem(IListBoxItem item)
      {
         item.MinFontSize = minFontScale;
         item.MaxFontSize = maxFontScale;
         Items.Add(item);
      }

      public void RemoveItem(IListBoxItem item)
      {
         Items.Remove(item);
      }

      public IListBoxItem GetSelectedItem()
      {
         if (selectedIndex == -1 || selectedIndex >= Items.Count)
         {
            return null;
         }
         else
         {
            return Items[startingIndex + selectedIndex];
         }
      }

      public override void Edit()
      {
         //Key Checks
         List<Keys> newKeyDowns = KeyboardService.GetNewKeyDownKeys(oldKS);
         int OldIndex = selectedIndex;
         if (newKeyDowns.Contains(Keys.Up))
         {
            previousIndex();
         }
         if (newKeyDowns.Contains(Keys.Down))
         {
            nextIndex();
         }

         //Scroll
         ScrollWheelDirection direction = MouseService.GetScrollDirection();
         if (direction == ScrollWheelDirection.up)
         {
            if (startingIndex > 0)
            {
               startingIndex--;
               selectedIndex++;
            }
         }
         else if (direction == ScrollWheelDirection.down)
         {
            int lastIndex = startingIndex + possibleCount;
            if (lastIndex < Items.Count)
            {
               startingIndex++;
               selectedIndex--;
            }
         }
      }

      public void upButtonClickHandle(object sender, OnClick onClick)
      {
         previousIndex();
      }

      public void downButtonClickHandle(object sender, OnClick onClick)
      {
         nextIndex();
      }

      public void previousIndex()
      {
         int OldIndex = selectedIndex;
         if (startingIndex != 0 || selectedIndex != 0)
         {
            selectedIndex--;
            if (selectedIndex < 0)
            {
               startingIndex--;
               selectedIndex = 0;
            }
            ValueChange(OldIndex, selectedIndex);
         }
      }

      public void nextIndex()
      {
         int OldIndex = selectedIndex;
         if (selectedIndex + startingIndex < Items.Count - 1)
         {
            selectedIndex++;
            if (selectedIndex >= possibleCount)
            {
               startingIndex++;
               selectedIndex--;
            }
            ValueChange(OldIndex, selectedIndex);
         }
      }

      public override void Click()
      {
         object oldValue = selectedIndex;
         int yPositiion = MouseService.GetMouseYPosition();
         EngineRectangle safeBounds = new EngineRectangle(bounds.X + padding[3], bounds.Y + padding[0], bounds.Width - (float)(padding[1] + padding[3]), bounds.Height - (float)(padding[0] + padding[2]));
         int itemClicked = startingIndex + ((MouseService.GetMouseYPosition() - (int)safeBounds.Y) / actualItemHeight);

         if (itemClicked < Items.Count)
         {
            selectedIndex = itemClicked - startingIndex;
            ValueChange(oldValue, selectedIndex);
         }
         base.Click();
      }

      public override void onDeserialized()
      {
         base.onDeserialized();
         upButton.onDeserialized();
         downButton.onDeserialized();
         bar.onDeserialized();
      }
   }
}
