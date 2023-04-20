using System;
using System.Collections.Generic;

namespace ECGameplay
{
    public sealed class ActionPoint
    {
        public List<Action<Entity>> Listeners { get; set; } = new List<Action<Entity>>();


        public void AddListener(Action<Entity> action)
        {
            Listeners.Add(action);
        }

        public void RemoveListener(Action<Entity> action)
        {
            Listeners.Remove(action);
        }

        public void TriggerAllActions(Entity actionExecution)
        {
            if (Listeners.Count == 0)
            {
                return;
            }
            for (int i = Listeners.Count - 1; i >= 0; i--)
            {
                var item = Listeners[i];
                item.Invoke(actionExecution);
            }
        }
    }
}