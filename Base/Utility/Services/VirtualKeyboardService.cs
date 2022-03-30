using Android.Views;
using Android.Views.InputMethods;
using Base.Events;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Utility.Services
{
   public static class VirtualKeyboardService
   {
      public static View view;
      public static InputMethodManager inputMethodManager;
      public static EventBus bus;

      public static void initialize(View View, InputMethodManager Manager)
      {
         view = View;
         inputMethodManager = Manager;
         view.KeyPress += OnKeyPress;
         bus = new EventBus();
      }

      public static void ShowKeyboard()
      {
         inputMethodManager.ShowSoftInput(view, ShowFlags.Forced);
         inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
      }

      public static void HideKeyboard()
      {
         inputMethodManager.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
      }

      public static void AddKeyPressHandler(EHandler<KeyPressedEvent> eHandler)
      {
         bus.Subscribe(eHandler);
      }

      public static void RemoveKeyPressHandler(EHandler<KeyPressedEvent> eHandler)
      {
         bus.Unsubscribe(eHandler);
      }
      //del, 
      private static void OnKeyPress(object sender, View.KeyEventArgs e)
      {
         // do stuff with the input
         if (e.Event.Action == KeyEventActions.Up)
         {
            //if(e.Event.MetaState == MetaKeyStates.)

            string s = e.KeyCode.ToString();
            Keys cAsKey = Keys.None;
            if (s == "Del")
            {
               cAsKey = Keys.Delete;
            }
            else if(s == "ShiftLeft")
            {
               cAsKey = Keys.LeftShift;
            }
            else if(s == "Enter")
            {
               cAsKey = Keys.Enter;
            }
            else if(s.Length==4 && s.Substring(0,3) == "Num")
            {
               cAsKey = (Keys)((int)(char.ToUpper(s[3]))); ;
            }
            else
            {
               cAsKey = (Keys)((int)(char.ToUpper(s[0])));
            }
            bus.Publish(null, new KeyPressedEvent(cAsKey, Enums.gameControlState.keyUp));
         }
      }
   }
}
