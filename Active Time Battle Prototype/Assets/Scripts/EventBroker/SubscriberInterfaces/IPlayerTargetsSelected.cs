using System.Collections.Generic;
using ATBFighter;

namespace EventBroker
{
    public interface IPlayerTargetsSelected
    {
        void NotifyPlayerTargetsSelected(List<FighterController> targets);
    }
}