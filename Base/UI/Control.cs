using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base.Utility;
using Base.Utility.Services;
using Base.Events;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Base.Serialization;
using Base.Serialization.Interfaces;

namespace Base.UI
{
   // TODO: add support for resizing of text based on string render size and box size
   //       Fix constructors for the control and children classes
   //       Add Load/Unload content methods
   [Serializable]
   public class Control : IOnDeserialization
   {
      public bool isEnabled;
      public bool isFocused;
      public bool isEditable;
      public bool isEditing;
      public bool isDragable;
      public string Name;
      public string value;
      //Padding: top,right,bottom,left
      public int[] padding;
      public bool isMultiLine;
      public float minFontScale;
      public float maxFontScale;
      public Enums.TextAchorLocation textAnchor;
      public uint textColorUint;
      [NonSerialized]
      public Color textColor;
      public uint drawColorUint;
      [NonSerialized]
      public Color drawColor;
      public int DragDelayInMS;
      public int dragTime;
      public EngineRectangle bounds { get; set; }
      //TODO: make this someething that can be XML serialized
      public SerializableDictionary<string, string> imageReference;
      [NonSerialized]
      protected Dictionary<string, Texture2D> images;
      public string fontName;
      [NonSerialized]
      protected SpriteFont font;
      public Enums.cState prevClickState;
      public Enums.cState clickState;
      [NonSerialized]
      public MouseState oldMS;
      [NonSerialized]
      public KeyboardState oldKS;

      public EHandler<OnClick> onClick;
      public EHandler<Event> onMouseDown;
      public EHandler<Event> onMouseUp;
      public EHandler<OnChange> onValueChange;
      public EHandler<Event> onEnter;
      public EHandler<Event> onExit;
      public EHandler<Event> onDoubleClick;

      public Control()
      {
         SetDefaults();
      }

      public Control(string name, string value, EngineRectangle bounds, Color textColor)
      {
         SetDefaults();
         Name = name;
         this.value = value;
         this.bounds = bounds;
         textColorUint = textColor.PackedValue;
         this.textColor = textColor;
         drawColor = Color.White;
         drawColorUint = Color.White.PackedValue;
      }

      public void SetDefaults()
      {
         isEnabled = true;
         isFocused = false;
         isEditable = false;
         isEditing = false;
         isDragable = false;
         Name = string.Empty;
         value = string.Empty;
         textColor = Color.Black;
         textColorUint = Color.Black.PackedValue;
         drawColor = Color.White;
         drawColorUint = Color.White.PackedValue;
         fontName = "defaultFont";
         isMultiLine = false;
         minFontScale = 1;
         maxFontScale = 1;
         DragDelayInMS = 0;
         dragTime = 0;
         padding = new int[] { 0, 0, 0, 0 };
         bounds = new EngineRectangle(0, 0, 50, 50);
         images = new Dictionary<string, Texture2D>();
         clickState = Enums.cState.none;
         prevClickState = Enums.cState.none;
         imageReference = new SerializableDictionary<string, string>();
         images = new Dictionary<string, Texture2D>();
         textAnchor = Enums.TextAchorLocation.topLeft;
         imageReference.Add(Enums.cState.none.ToString(), "defaultTexture");
         imageReference.Add(Enums.cState.hover.ToString(), "defaultTexture");
         imageReference.Add(Enums.cState.pressed.ToString(), "defaultTexture");
         imageReference.Add(Enums.cState.released.ToString(), "defaultTexture");
      }

      public virtual void init()
      {
         textColor = new Color(textColorUint);
         drawColor = new Color(drawColorUint);
         font = ContentService.GetSpriteFont(fontName);
         images = new Dictionary<string, Texture2D>();
         foreach (KeyValuePair<string, string> keyValuePair in imageReference.ToDictionary())
         {
            images.Add(keyValuePair.Key, ContentService.Get2DTexture(keyValuePair.Value));
         }
      }

      public void setImageReferences(string none, string hover, string pressed, string released)
      {
         imageReference = new SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), none);
         imageReference.Add(Enums.cState.hover.ToString(), hover);
         imageReference.Add(Enums.cState.pressed.ToString(), pressed);
         imageReference.Add(Enums.cState.released.ToString(), released);
         images.Clear();
         foreach (KeyValuePair<string, string> keyValuePair in imageReference.ToDictionary())
         {
            images.Add(keyValuePair.Key, ContentService.Get2DTexture(keyValuePair.Value));
         }
      }

      public void setImageReferences(string all)
      {
         imageReference = new SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), all);
         imageReference.Add(Enums.cState.hover.ToString(), all);
         imageReference.Add(Enums.cState.pressed.ToString(), all);
         imageReference.Add(Enums.cState.released.ToString(), all);
         images.Clear();
         foreach (KeyValuePair<string, string> keyValuePair in imageReference.ToDictionary())
         {
            images.Add(keyValuePair.Key, ContentService.Get2DTexture(keyValuePair.Value));
         }
      }

      public virtual void Update(int dt)
      {
         if (isEnabled)
         {
            MouseState newMS = Mouse.GetState();
            if (clickState == Enums.cState.draging)
            {
               Drag(new EngineVector2(oldMS.X,oldMS.Y), new EngineVector2(newMS.X, newMS.Y));
            }
            else if (newMS.X > bounds.X && newMS.X < bounds.X + bounds.Width &&
                newMS.Y > bounds.Y && newMS.Y < bounds.Y + bounds.Height)
            {
               if (!isFocused)
               {
                  Enter();
                  isFocused = true;
               }
               if (newMS.LeftButton == ButtonState.Pressed)
               {
                  if (isEditable)
                  {
                     isEditing = true;
                  }
                  if (prevClickState != Enums.cState.pressed)
                  {
                     MouseDown();
                     dragTime = 0;
                  }
                  if(isDragable)
                  {
                     dragTime += dt;
                  }
                  if (DragDelayInMS < dragTime)
                  {
                     StartDrag();
                  }
                  else
                  {
                     clickState = Enums.cState.pressed;
                  }
               }
               else
               {
                  if (clickState == Enums.cState.pressed)
                  {
                     clickState = Enums.cState.released;
                     Click();
                     MouseUp();
                  }
                  else if (clickState == Enums.cState.draging)
                  {
                     StopDrag();
                  }
                  else
                     clickState = Enums.cState.hover;
               }
            }
            else
            {
               if (isFocused)
               {
                  Exit();
                  isFocused = false;
               }
               if (newMS.LeftButton == ButtonState.Pressed)
                  isEditing = false;
               clickState = Enums.cState.none;
            }

            if (isEditing)
            {
               Edit();
            }
            if (clickState != prevClickState)
            {
               //selectedImage = clickState.ToString();
               prevClickState = clickState;
            }
            oldMS = newMS;
            oldKS = KeyboardService.currentState;
         }

      }

      public virtual void Edit() { }

      //public float GetText

      public virtual void Render(SpriteBatch sb)
      {
         if (isEnabled)
         {
            EngineRectangle textBounds = new EngineRectangle(bounds.X + padding[3], bounds.Y + padding[0], bounds.Width - (padding[1] + padding[3]), bounds.Height - (padding[0] + padding[2]));
            Vector2 textAnchorVector2 = new Vector2();
            EngineVector2 textOrigin = new EngineVector2();
            // Determine anchor Location
            if (textAnchor == Enums.TextAchorLocation.topLeft)
            {
               textAnchorVector2 = new Vector2(textBounds.X, textBounds.Y);
               textOrigin = new EngineVector2(0, 0);
            }
            else if (textAnchor == Enums.TextAchorLocation.center)
            {
               textAnchorVector2 = new Vector2(textBounds.X + textBounds.Width / 2, textBounds.Y + textBounds.Height / 2);
               textOrigin = new EngineVector2(.5f, .5f);
            }
            else if (textAnchor == Enums.TextAchorLocation.centerLeft)
            {
               textAnchorVector2 = new Vector2(textBounds.X, textBounds.Y + textBounds.Height / 2);
               textOrigin = new EngineVector2(0, .5f);
            }

            if (images.ContainsKey(clickState.ToString()))
            {
               sb.Draw(images[clickState.ToString()], bounds.toRectangle(), null, drawColor, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            }

            EngineVector2 textScale = RenderUtil.CalculateFontScaling(value, textBounds, font);
            EngineVector2 fontSize = new EngineVector2(font.MeasureString(value).X, font.MeasureString(value).Y);
            var minimumScale = Math.Min(textScale.X, textScale.Y);
            if (minimumScale < minFontScale)
               minimumScale = minFontScale;
            if (minimumScale > maxFontScale)
               minimumScale = maxFontScale;
            sb.DrawString(font, value, textAnchorVector2, textColor, 0, new Vector2(fontSize.X * textOrigin.X, fontSize.Y * textOrigin.Y), minimumScale, SpriteEffects.None, 0);
         }
      }

      public virtual void Click()
      {
         if (onClick != null)
            onClick.Execute(this, new OnClick());
      }

      public virtual void MouseDown()
      {
         if (onMouseDown != null)
            onMouseDown.Execute(this, new Event());
      }

      public virtual void MouseUp()
      {
         if (onMouseUp != null)
            onMouseDown.Execute(this, new Event());
      }

      public virtual void ValueChange()
      {
         if (onValueChange != null)
            onValueChange.Execute(this, new OnChange("", value));
      }

      public virtual void ValueChange(object oldValue, object newValue)
      {
         if (onValueChange != null)
            onValueChange.Execute(this, new OnChange(oldValue, newValue));
      }

      public virtual void Enter()
      {
         if (onEnter != null)
            onEnter.Execute(this, new Event());
      }

      public virtual void Exit()
      {
         if (onExit != null)
            onExit.Execute(this, new Event());

      }

      public virtual void DoubleClick()
      {
         if (onDoubleClick != null)
            onDoubleClick.Execute(this, new Event());
      }

      public virtual void StartDrag()
      {
         clickState = Enums.cState.draging;
      }

      public virtual void Drag(EngineVector2 oldMouseLocation, EngineVector2 newMouseLocation)
      {
         float xMove = newMouseLocation.X - oldMouseLocation.X;
         float yMove = newMouseLocation.Y - oldMouseLocation.Y;
         if (xMove != 0 || yMove != 0)
         {
            bounds.X += newMouseLocation.X - oldMouseLocation.X;
            bounds.Y += newMouseLocation.Y - oldMouseLocation.Y;
            clickState = Enums.cState.draging;
         }
      }

      public virtual void StopDrag()
      {
         clickState = Enums.cState.released;
      }

      #region deserialization
      public virtual void onDeserialized()
      {
         init();
      }
      #endregion
   }
}
