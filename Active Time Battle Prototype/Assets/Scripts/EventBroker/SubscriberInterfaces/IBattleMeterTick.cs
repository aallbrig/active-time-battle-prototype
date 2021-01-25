using ATBFighter;

namespace EventBroker
{
    public interface IBattleMeterTick
    {
        void NotifyBattleMeterTick(FighterController fighter);
    }
}