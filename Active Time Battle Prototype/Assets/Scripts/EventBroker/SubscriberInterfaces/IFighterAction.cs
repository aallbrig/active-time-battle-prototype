using System.Collections.Generic;
using Controllers;
using Data;

namespace EventBroker.SubscriberInterfaces
{
    public interface IFighterAction
    {
        void NotifyFighterAction(FighterController fighter, FighterAction action, List<FighterController> targets);
    }
}