using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace Base.Utility.Services
{
   public class KeyboardService
   {
      public static KeyboardState currentState { get; set; }
      public static KeyboardState oldState { get; set; }
      public static Stack<KeyboardState> history;
      public static Stack<KeyboardState> newHistory;

      public static void InitService()
      {
         oldState = new KeyboardState();
         currentState = new KeyboardState();
      }

      public static void UpdateKeyboardState()
      {
         oldState = currentState;
         currentState = Keyboard.GetState();
      }

      public static KeyboardState GetNewestKeyboardState()
      {
         return Keyboard.GetState();
      }

      public static bool GetShiftStatus()
      {
         return currentState.IsKeyDown(Keys.LeftShift) || currentState.IsKeyDown(Keys.RightShift);
      }

      public static bool GetShiftStatus(KeyboardState k)
      {
         return k.IsKeyDown(Keys.LeftShift) || k.IsKeyDown(Keys.RightShift);
      }

      public static bool GetShiftStatus(bool leftShift, bool rightShift)
      {
         return leftShift || rightShift;
      }

      public static bool GetCapLockStatus()
      {
         return currentState.CapsLock;
      }

      public static bool GetCapLockStatus(KeyboardState k)
      {
         return k.CapsLock;
      }

      public static bool GetCapitalizationStatus()
      {
         return GetShiftStatus(currentState) ^ currentState.CapsLock;
      }

      public static bool GetCapitalizationStatus(KeyboardState k)
      {
         return GetShiftStatus(k) ^ k.CapsLock;
      }

      public static bool GetCapitalizationStatus(bool Shift, bool CapLock)
      {
         return Shift ^ CapLock;
      }

      public static bool GetNumLockStatus()
      {
         return currentState.NumLock;
      }

      public static bool GetNumLockStatus(KeyboardState k)
      {
         return k.NumLock;
      }

      static public List<Keys> GetNewKeyDownKeys()
      {
         Keys[] OldPressedKeys = oldState.GetPressedKeys();
         Keys[] NewPressedKeys = currentState.GetPressedKeys();
         // Setup local variables
         HashSet<Keys> prevPressedKeys = new HashSet<Keys>();
         List<Keys> newKeyPresses = new List<Keys>();
         foreach (Keys key in OldPressedKeys)
         {
            prevPressedKeys.Add(key);
         }
         // Compare old pressed keys with new pressed keys
         foreach (Keys k in NewPressedKeys)
         {
            if (!OldPressedKeys.Contains(k))
            {
               newKeyPresses.Add(k);
            }
         }
         return newKeyPresses;
      }

      static public List<Keys> GetNewKeyDownKeys(KeyboardState oldState)
      {
         Keys[] OldPressedKeys = oldState.GetPressedKeys();
         Keys[] NewPressedKeys = currentState.GetPressedKeys();
         // Setup local variables
         HashSet<Keys> prevPressedKeys = new HashSet<Keys>();
         List<Keys> newKeyPresses = new List<Keys>();
         foreach (Keys key in OldPressedKeys)
         {
            prevPressedKeys.Add(key);
         }
         // Compare old pressed keys with new pressed keys
         foreach (Keys k in NewPressedKeys)
         {
            if (!OldPressedKeys.Contains(k))
            {
               newKeyPresses.Add(k);
            }
         }
         return newKeyPresses;
      }

      static public List<Keys> GetNewKeyDownKeys(Keys[] NewPressedKeys, Keys[] OldPressedKeys)
      {
         // Setup local variables
         HashSet<Keys> prevPressedKeys = new HashSet<Keys>();
         List<Keys> newKeyPresses = new List<Keys>();
         foreach (Keys key in OldPressedKeys)
         {
            prevPressedKeys.Add(key);
         }
         // Compare old pressed keys with new pressed keys
         foreach (Keys k in NewPressedKeys)
         {
            if (!OldPressedKeys.Contains(k))
            {
               newKeyPresses.Add(k);
            }
         }
         return newKeyPresses;
      }

      static public List<Keys> GetNewKeyUpKeys()
      {
         return GetNewKeyDownKeys(oldState.GetPressedKeys(), currentState.GetPressedKeys());
      }

      static public List<Keys> GetNewKeyUpKeys(KeyboardState oldState)
      {
         return GetNewKeyDownKeys(oldState.GetPressedKeys(), currentState.GetPressedKeys());
      }

      static public List<Keys> GetNewKeyUpKeys(Keys[] NewPressedKeys, Keys[] OldPressedKeys)
      {
         return GetNewKeyDownKeys(OldPressedKeys, NewPressedKeys);
      }

      //TODO: Add function for getting non-New pressed keys

      public static List<char> GetPressedCharacters()
      {
         bool shiftModifier = GetShiftStatus();
         bool caps = GetCapitalizationStatus();
         List<Keys> pressedKeys = GetNewKeyDownKeys();
         List<char> characters = new List<char>();
         foreach (Keys key in pressedKeys)
         {

            //Numbers
            if ((int)key >= 48 && (int)key <= 57)
            {
               if (!shiftModifier)
                  characters.Add((((int)key) - 48).ToString()[0]);
               else if ((int)key == 48)
                  characters.Add(')');
               else if ((int)key == 49)
                  characters.Add('!');
               else if ((int)key == 50)
                  characters.Add('@');
               else if ((int)key == 51)
                  characters.Add('#');
               else if ((int)key == 52)
                  characters.Add('$');
               else if ((int)key == 53)
                  characters.Add('%');
               else if ((int)key == 54)
                  characters.Add('^');
               else if ((int)key == 55)
                  characters.Add('&');
               else if ((int)key == 56)
                  characters.Add('*');
               else if ((int)key == 57)
                  characters.Add('(');
            }
            //Letters
            else if ((int)key >= 65 && (int)key <= 90)
            {
               if (caps)
               {
                  characters.Add(key.ToString()[0]);
               }
               else
               {
                  characters.Add(key.ToString().ToLower()[0]);
               }
            }
            //Dpad Numbers
            else if ((int)key >= 96 && (int)key <= 105)
            {
               if (!shiftModifier)
                  characters.Add((((int)key) - 96).ToString()[0]);
            }
            //Math Characters
            else if ((int)key >= 106 && (int)key <= 111)
            {
               if ((int)key == 106)
                  characters.Add('*');
               else if ((int)key == 107)
                  characters.Add('+');
               else if ((int)key == 108)
                  characters.Add(' ');
               else if ((int)key == 109)
                  characters.Add('-');
               else if ((int)key == 110)
                  characters.Add('.');
               else if ((int)key == 111)
                  characters.Add('/');
            }
            // Misc Characters
            else if ((int)key >= 186 && (int)key <= 226)
            {
               if (!shiftModifier)
               {
                  if ((int)key == 186)
                     characters.Add(';');
                  else if ((int)key == 187)
                     characters.Add('=');
                  else if ((int)key == 188)
                     characters.Add(',');
                  else if ((int)key == 189)
                     characters.Add('-');
                  else if ((int)key == 190)
                     characters.Add('.');
                  else if ((int)key == 191)
                     characters.Add('/');
                  else if ((int)key == 192)
                     characters.Add('`');
                  else if ((int)key == 219)
                     characters.Add('[');
                  else if ((int)key == 220)
                     characters.Add('\\');
                  else if ((int)key == 221)
                     characters.Add(']');
                  else if ((int)key == 222)
                     characters.Add('\'');
                  else if ((int)key == 223)
                     characters.Add('/');
                  else if ((int)key == 226)
                     characters.Add('\\');
               }
               else
               {
                  if ((int)key == 186)
                     characters.Add(':');
                  else if ((int)key == 187)
                     characters.Add('+');
                  else if ((int)key == 188)
                     characters.Add('<');
                  else if ((int)key == 189)
                     characters.Add('_');
                  else if ((int)key == 190)
                     characters.Add('>');
                  else if ((int)key == 191)
                     characters.Add('?');
                  else if ((int)key == 192)
                     characters.Add('"');
                  else if ((int)key == 219)
                     characters.Add('{');
                  else if ((int)key == 220)
                     characters.Add('|');
                  else if ((int)key == 221)
                     characters.Add('}');
                  else if ((int)key == 222)
                     characters.Add('"');
                  else if ((int)key == 223)
                     characters.Add('?');
                  else if ((int)key == 226)
                     characters.Add('|');
               }
            }
            //space
            else if (key == Keys.Space)
               characters.Add(' ');
         }
         return characters;
      }

      public static List<char> GetPressedCharacters(List<Keys> pressedKeys, bool shiftModifier, bool capsLock, bool numLock)
      {
         bool caps = GetCapitalizationStatus(shiftModifier, capsLock);
         List<char> characters = new List<char>();
         foreach (Keys key in pressedKeys)
         {

            //Numbers
            if ((int)key >= 48 && (int)key <= 57)
            {
               if (!shiftModifier)
                  characters.Add((((int)key) - 48).ToString()[0]);
               else if ((int)key == 48)
                  characters.Add(')');
               else if ((int)key == 49)
                  characters.Add('!');
               else if ((int)key == 50)
                  characters.Add('@');
               else if ((int)key == 51)
                  characters.Add('#');
               else if ((int)key == 52)
                  characters.Add('$');
               else if ((int)key == 53)
                  characters.Add('%');
               else if ((int)key == 54)
                  characters.Add('^');
               else if ((int)key == 55)
                  characters.Add('&');
               else if ((int)key == 56)
                  characters.Add('*');
               else if ((int)key == 57)
                  characters.Add('(');
            }
            //Letters
            else if ((int)key >= 65 && (int)key <= 90)
            {
               if (caps)
               {
                  characters.Add(key.ToString()[0]);
               }
               else
               {
                  characters.Add(key.ToString().ToLower()[0]);
               }
            }
            //Dpad Numbers
            else if ((int)key >= 96 && (int)key <= 105)
            {
               if (!shiftModifier)
                  characters.Add((((int)key) - 96).ToString()[0]);
            }
            //Math Characters
            else if ((int)key >= 106 && (int)key <= 111)
            {
               if ((int)key == 106)
                  characters.Add('*');
               else if ((int)key == 107)
                  characters.Add('+');
               else if ((int)key == 108)
                  characters.Add(' ');
               else if ((int)key == 109)
                  characters.Add('-');
               else if ((int)key == 110)
                  characters.Add('.');
               else if ((int)key == 111)
                  characters.Add('/');
            }
            // Misc Characters
            else if((int)key >= 186 && (int)key <= 226)
            {
               if (!shiftModifier)
               {
                  if ((int)key == 186)
                     characters.Add(';');
                  else if ((int)key == 187)
                     characters.Add('=');
                  else if ((int)key == 188)
                     characters.Add(',');
                  else if ((int)key == 189)
                     characters.Add('-');
                  else if ((int)key == 190)
                     characters.Add('.');
                  else if ((int)key == 191)
                     characters.Add('/');
                  else if ((int)key == 192)
                     characters.Add('`');
                  else if ((int)key == 219)
                     characters.Add('[');
                  else if ((int)key == 220)
                     characters.Add('\\');
                  else if ((int)key == 221)
                     characters.Add(']');
                  else if ((int)key == 222)
                     characters.Add('\'');
                  else if ((int)key == 223)
                     characters.Add('/');
                  else if ((int)key == 226)
                     characters.Add('\\');
               }
               else
               {
                  if ((int)key == 186)
                     characters.Add(':');
                  else if ((int)key == 187)
                     characters.Add('+');
                  else if ((int)key == 188)
                     characters.Add('<');
                  else if ((int)key == 189)
                     characters.Add('_');
                  else if ((int)key == 190)
                     characters.Add('>');
                  else if ((int)key == 191)
                     characters.Add('?');
                  else if ((int)key == 192)
                     characters.Add('"');
                  else if ((int)key == 219)
                     characters.Add('{');
                  else if ((int)key == 220)
                     characters.Add('|');
                  else if ((int)key == 221)
                     characters.Add('}');
                  else if ((int)key == 222)
                     characters.Add('"');
                  else if ((int)key == 223)
                     characters.Add('?');
                  else if ((int)key == 226)
                     characters.Add('|');
               }
            }
            //space
            else if (key == Keys.Space)
               characters.Add(' ');
         }
         return characters;
      }

      //TODO: Add checking for other types besides character
      public static Enums.keyType getkeyType(Keys key)
      {
         Enums.keyType kt = Enums.keyType.NONE;
         if (((int)key >= 48 && (int)key <= 90)
              || ((int)key >= 96 && (int)key <= 111))
            kt = Enums.keyType.CHARACTER;
         return kt;
      }
   }
}
