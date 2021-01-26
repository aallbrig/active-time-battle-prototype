using System.Collections.Generic;
using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IPlayerTargetsSelected
    {
        void NotifyPlayerTargetsSelected(List<FighterController> targets);
    }
}