using Base.Events;
using Base.Utility;
using Base.Utility.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.UI.Mobile
{
   [Serializable]
   public class Textbox : Control
   {
      public static HashSet<char> defaultCharSet = new HashSet<char>()
         {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C',
            'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '9', '8', '7', '6', '5', '4',
            '3', '2', '1', ' ',
         };

      public HashSet<char> validChars;
      public int maxCharCount;
      private EHandler<KeyPressedEvent> keyPressHandler;
      bool isInShift;

      public Textbox() : base()
      {
         validChars = Textbox.defaultCharSet;
         maxCharCount = 10;
         isEditable = true;
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "textbox_default_none");
         imageReference.Add(Enums.cState.hover.ToString(), "textbox_default_none");
         imageReference.Add(Enums.cState.pressed.ToString(), "textbox_default_none");
         imageReference.Add(Enums.cState.released.ToString(), "textbox_default_none");
         keyPressHandler = new EHandler<KeyPressedEvent>(new Action<object, KeyPressedEvent>(HandleVirtualInput));
         isInShift = false;
         init();
      }

      public Textbox(string name, string value, EngineRectangle bounds, Color color, int maxCharCount, HashSet<char> validChars) : base(name, value, bounds, color)
      {
         this.validChars = validChars;
         this.maxCharCount = maxCharCount;
         this.isEditable = true;
         imageReference = new Serialization.SerializableDictionary<string, string>();
         imageReference.Add(Enums.cState.none.ToString(), "textbox_default_none");
         imageReference.Add(Enums.cState.hover.ToString(), "textbox_default_none");
         imageReference.Add(Enums.cState.pressed.ToString(), "textbox_default_none");
         imageReference.Add(Enums.cState.released.ToString(), "textbox_default_none");
         keyPressHandler = new EHandler<KeyPressedEvent>(new Action<object, KeyPressedEvent>(HandleVirtualInput));
         isInShift = false;
         init();
      }

      public override void Update(int dt)
      {
         if (isEnabled)
         {
            //bool previsEditing = isEditing;
            base.Update(dt);

         }
      }

      public override void Enter()
      {
         VirtualKeyboardService.ShowKeyboard();
         VirtualKeyboardService.AddKeyPressHandler(keyPressHandler);
         isInShift = false;
         value += '_';
      }

      public override void Exit()
      {
         base.Exit();
         VirtualKeyboardService.HideKeyboard();
         VirtualKeyboardService.RemoveKeyPressHandler(keyPressHandler);
         value = value.Trim('_');
      }

      //TODO: add support for '_' character, maybe use substring instead of trim end?
      //public override void Edit()
      //{
      //   //KeyboardState newKS = KeyboardService.currentState;
      //   //bool shiftModifier = KeyboardService.GetShiftStatus(newKS);
      //   //List<Keys> newKeyDowns = KeyboardService.GetNewKeyDownKeys(oldKS);
      //   List<char> pressedChars = KeyboardService.GetPressedCharacters(newKeyDowns, shiftModifier, newKS.CapsLock, newKS.NumLock);
      //   bool change = false;
      //   if (newKeyDowns.Contains(Keys.Back) && value.Length > 1)
      //   {
      //      value = value.TrimEnd('_');
      //      value = value.Substring(0, value.Length - 1);
      //      change = true;
      //      value += '_';
      //   }
      //   if (pressedChars.Count > 0)
      //   {
      //      value = value.TrimEnd('_');
      //      foreach (char c in pressedChars)
      //      {
      //         if (validChars.Contains(c) && value.Length < maxCharCount)
      //            value += c;
      //      }
      //      change = true;
      //      value += '_';
      //   }
      //   if (change)
      //      ValueChange();
      //}

      public void Edit(Keys keyPressed)
      {
          List<char> pressedChars = new List<char>();

         if (keyPressed == Keys.LeftShift)
         {
            isInShift = !isInShift;
         }
         if(keyPressed == Keys.Enter)
         {
            isFocused = false;
            Exit();
         }

         if((int)keyPressed >=48 && (int)keyPressed <= 57)
         {
            pressedChars.Add((((int)keyPressed) - 48).ToString()[0]);
         }

         if ((int)keyPressed >= 65 && (int)keyPressed <= 90)
         {
            if (isInShift)
            {
               pressedChars.Add(keyPressed.ToString()[0]);
            }
            else
            {
               pressedChars.Add(keyPressed.ToString().ToLower()[0]);
            }
            isInShift = false;
         }

         bool change = false;
         if (keyPressed == Keys.Delete && value.Length > 1)
         {
            value = value.TrimEnd('_');
            value = value.Substring(0, value.Length - 1);
            change = true;
            value += '_';
         }
         if (pressedChars.Count > 0)
         {
            value = value.TrimEnd('_');
            foreach (char c in pressedChars)
            {
               if (validChars.Contains(c) && value.Length < maxCharCount)
                  value += c;
            }
            change = true;
            value += '_';
         }
         if (change)
            ValueChange();


      }

      public override void Render(SpriteBatch sb)
      {
         if (isEnabled)
         {
            sb.Draw(images[clickState.ToString()], bounds.toRectangle(), Color.White);
            sb.DrawString(font, value, new Vector2(bounds.X, bounds.Y) + new Vector2(5, bounds.Height / 2), textColor, 0, new Vector2(0, font.MeasureString(value).Y / 2), 1, SpriteEffects.None, 0);
         }
      }

      public void HandleVirtualInput(object sender, KeyPressedEvent e)
      {
         Edit(e.key);
      }
   }
}
