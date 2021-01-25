using System.Collections.Generic;
using ATBFighter;

namespace EventBroker
{
    public interface IPlayerFighterCreated
    {
        void NotifyPlayerFighterCreated(FighterController fighter);
    }

    public interface IEnemyFighterCreated
    {
        void NotifyEnemyFighterCreated(FighterController fighter);
    }

    public interface IPlayerActionSelected
    {
        void NotifyPlayerActionSelected(ATBFighterAction_SO action);
    }

    public interface IPlayerTargetsSelected
    {
        void NotifyPlayerTargetsSelected(List<FighterController> targets);
    }

    public interface IBattleMeterTick
    {
        void NotifyBattleMeterTick(FighterController fighter);
    }
}