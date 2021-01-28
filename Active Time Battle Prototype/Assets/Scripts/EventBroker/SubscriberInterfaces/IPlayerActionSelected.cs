using Data;

namespace EventBroker.SubscriberInterfaces
{
    public interface IPlayerActionSelected
    {
        void NotifyPlayerActionSelected(FighterAction action);
    }
}