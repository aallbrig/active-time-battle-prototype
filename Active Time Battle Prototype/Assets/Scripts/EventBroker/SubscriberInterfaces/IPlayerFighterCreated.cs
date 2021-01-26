using Controllers;

namespace EventBroker
{
    public interface IPlayerFighterCreated
    {
        void NotifyPlayerFighterCreated(FighterController fighter);
    }
}