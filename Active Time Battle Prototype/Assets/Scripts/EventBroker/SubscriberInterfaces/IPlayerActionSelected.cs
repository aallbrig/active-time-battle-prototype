using ATBFighter;

namespace EventBroker
{
    public interface IPlayerActionSelected
    {
        void NotifyPlayerActionSelected(ATBFighterAction_SO action);
    }
}