using System.Collections.Generic;
using Controllers;

namespace EventBroker
{
    public interface IPlayerTargetsSelected
    {
        void NotifyPlayerTargetsSelected(List<FighterController> targets);
    }
}