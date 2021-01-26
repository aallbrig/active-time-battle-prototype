using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IActivePlayerFighterSet
    {
        void NotifyActivePlayerFighterSet(FighterController fighter);
    }
}