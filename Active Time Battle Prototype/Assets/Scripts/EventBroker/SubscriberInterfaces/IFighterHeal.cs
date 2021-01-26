using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IFighterHeal
    {
        void NotifyFighterHeal(FighterController fighter, float heal);
    }
}