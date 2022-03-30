using Base.Events;
using Base.Utility;
using Base.Utility.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.UI.Mobile
{
   //TODO: add color variable for selected color
   [Serializable]
   public class ListBox : Control
   {
      public List<IListBoxItem> Items;
      public int startingIndex;
      public int selectedIndex;
      public bool DisplayButtons { get; set; }
      public int itemBaseHeight = 100;
      protected int actualItemHeight;
      protected int possibleCount;

      protected int swipeDistance;

      public Button upButton;
      public Button downButton;
      public Control bar;

      public ListBox(string name, string value, EngineRectangle Rectangle, Microsoft.Xna.Framework.Color color) : base(name, value, Rectangle, color)
      {
         Items = new List<IListBoxItem>();
         this.value = value;
         selectedIndex = 0;
         startingIndex = 0;
         isEditable = false;
         isDragable = true;
         DisplayButtons = false;
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
         int pCount = possibleCount = (int)((safeBounds.Height) / itemBaseHeight);
         int individualHeight = actualItemHeight = (int)(safeBounds.Height / pCount);


         for (int i = startingIndex; i < Items.Count && i < startingIndex + pCount; i++)
         {
            //TODO: find a better way to show which item is selected
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

            //Remove adding new EHnadler on every render, just initialize it once
            if (Items.Count > 1)
            {
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
      }

      public void AddItem(IListBoxItem item)
      {
         Items.Add(item);
      }

      public void RemoveItem(IListBoxItem item)
      {
         Items.Remove(item);
      }

      public object GetSelectedItem()
      {
         return Items[startingIndex + selectedIndex];
      }

      //public override void Edit()
      //{
      //   List<Keys> newKeyDowns = KeyboardService.GetNewKeyDownKeys(oldKS);
      //   int OldIndex = selectedIndex;
      //   if (newKeyDowns.Contains(Keys.Up))
      //   {
      //      if (startingIndex != 0 || selectedIndex != 0)
      //      {
      //         selectedIndex--;
      //         if (selectedIndex < 0)
      //         {
      //            startingIndex--;
      //            selectedIndex = 0;
      //         }
      //         ValueChange(OldIndex, selectedIndex);
      //      }
      //   }
      //   if (newKeyDowns.Contains(Keys.Down))
      //   {
      //      if (selectedIndex + startingIndex < Items.Count - 1)
      //      {
      //         selectedIndex++;
      //         if (selectedIndex >= possibleCount)
      //         {
      //            startingIndex++;
      //            selectedIndex--;
      //         }
      //         ValueChange(OldIndex, selectedIndex);
      //      }
      //   }
      //}

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
         if (oldTouchCollection.Count > 0)
         {
            TouchLocation TL = oldTouchCollection[0];
            EngineRectangle safeBounds = new EngineRectangle(bounds.X + padding[3], bounds.Y + padding[0], bounds.Width - (float)(padding[1] + padding[3]), bounds.Height - (float)(padding[0] + padding[2]));
            if (TL.Position.X > safeBounds.X && TL.Position.X < safeBounds.X + safeBounds.Width &&
               TL.Position.Y > safeBounds.Y && TL.Position.Y < safeBounds.Y + safeBounds.Height)
            {
               int itemClicked = startingIndex + (int)((TL.Position.Y - (safeBounds.Y)) / (float)actualItemHeight);

               if (itemClicked < Items.Count)
               {
                  selectedIndex = itemClicked - startingIndex;
                  ValueChange(oldValue, selectedIndex);
               }
               base.Click();
            }
         }
      }

      public override void Drag(TouchLocation oldTL, TouchLocation newTL)
      {
         float yMove = newTL.Position.Y - oldTL.Position.Y;
         if (yMove != 0)
         {
            swipeDistance += (int)yMove;
            if (swipeDistance > actualItemHeight)
            {
               swipeDistance -= actualItemHeight;
               if (startingIndex > 0)
               {
                  startingIndex--;
                  selectedIndex++;
               }
            }
            if (swipeDistance < actualItemHeight * -1)
            {
               swipeDistance += actualItemHeight;
               int lastIndex = startingIndex + possibleCount;
               if (lastIndex< Items.Count)
               {
                  startingIndex++;
                  selectedIndex--;
               }
            }
         }
      }
   }
}
