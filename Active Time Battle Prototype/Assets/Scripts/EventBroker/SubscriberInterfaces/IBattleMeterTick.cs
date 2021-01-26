using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IBattleMeterTick
    {
        void NotifyBattleMeterTick(FighterController fighter);
    }
}