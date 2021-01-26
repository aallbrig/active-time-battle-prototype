using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IFighterDie
    {
        void NotifyFighterDie(FighterController fighter);
    }
}