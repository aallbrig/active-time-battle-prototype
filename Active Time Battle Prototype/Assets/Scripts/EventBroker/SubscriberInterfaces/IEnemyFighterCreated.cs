using Controllers;

namespace EventBroker
{
    public interface IEnemyFighterCreated
    {
        void NotifyEnemyFighterCreated(FighterController fighter);
    }
}