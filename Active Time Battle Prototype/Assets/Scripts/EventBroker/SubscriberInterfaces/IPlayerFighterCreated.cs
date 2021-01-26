using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IPlayerFighterCreated
    {
        void NotifyPlayerFighterCreated(FighterController fighter);
    }
}