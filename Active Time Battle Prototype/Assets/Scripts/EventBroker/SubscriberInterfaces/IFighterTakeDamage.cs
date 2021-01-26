using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IFighterTakeDamage
    {
        void NotifyFighterTakeDamage(FighterController fighter, float damage);
    }
}