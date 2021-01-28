using System;
using System.Collections.Generic;
using Commands;
using Controllers;

namespace EventBroker.SubscriberInterfaces
{
    public interface IFighterActionEnqueueRequest
    {
        void NotifyFighterCommand(ICommand fighterCommand);
    }
}