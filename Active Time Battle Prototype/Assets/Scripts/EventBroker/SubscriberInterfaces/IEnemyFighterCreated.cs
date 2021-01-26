using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IEnemyFighterCreated
    {
        void NotifyEnemyFighterCreated(FighterController fighter);
    }
}