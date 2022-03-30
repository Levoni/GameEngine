using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Events
{
   [Serializable]
   public class EventBus
   {
      
      Dictionary<Type, List<BaseEventHandler>> subscribers;

      public EventBus()
      {
         subscribers = new Dictionary<Type, List<BaseEventHandler>>();
      }

      public void Publish<T>(object sender, T e)
      {
         if (!subscribers.ContainsKey(e.GetType()))
            return;

         List<BaseEventHandler> tempList = subscribers[e.GetType()];
         for(int i = tempList.Count - 1; i >= 0; i--)
         {
            tempList[i].Execute(sender, e);
         }
      }

      public void Subscribe<T>(EHandler<T> eHandle)
      {
         if (eHandle != null)
         {
            if (!subscribers.ContainsKey(typeof(T)))
            {
               subscribers[typeof(T)] = new List<BaseEventHandler>();
            }
            List<BaseEventHandler> tempList = subscribers[typeof(T)];

            EHandler<T> eh = (EHandler<T>)tempList.Find((value) => { return (((EHandler<T>)value).function == eHandle.function); });
            if (eh == null)
            {
               tempList.Add(eHandle);
            }
         }
      }

      public void Unsubscribe<T>(EHandler<T> eHandle)
      {
         if(eHandle != null && subscribers.ContainsKey(typeof(T)))
         {
            List<BaseEventHandler> tempList = subscribers[typeof(T)];

            EHandler<T> eh = (EHandler<T>)tempList.Find((value) => { return (((EHandler<T>)value).function == eHandle.function); });

            if (eh != null)
            {
               tempList.Remove(eh);
               if (tempList.Count == 0)
                  subscribers.Remove(typeof(T));
            }
         }
      }
   }
}