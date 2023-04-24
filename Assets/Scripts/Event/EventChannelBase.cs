using UnityEngine;
using UnityEngine.Events;

namespace Event
{
    public abstract class EventChannelBase<T> : ScriptableObject
    {
        public UnityAction<T> OnEventRaised;
        
        public virtual void RaiseEvent(T value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}