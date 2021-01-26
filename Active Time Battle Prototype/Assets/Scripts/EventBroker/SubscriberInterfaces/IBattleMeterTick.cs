using Controllers;

namespace EventBroker
{
    public interface IBattleMeterTick
    {
        void NotifyBattleMeterTick(FighterController fighter);
    }
}