using Data.Actions;

namespace EventBroker
{
    public interface IPlayerActionSelected
    {
        void NotifyPlayerActionSelected(FighterAction action);
    }
}