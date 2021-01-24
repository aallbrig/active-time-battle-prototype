using System.Collections.Generic;

namespace EventBroker
{
    public interface IEventBroker<T>
    {
        List<T> Subscribers { get; }
        void Subscribe(T subscriber);
        void Unsubscribe(T subscriber);
    }
}