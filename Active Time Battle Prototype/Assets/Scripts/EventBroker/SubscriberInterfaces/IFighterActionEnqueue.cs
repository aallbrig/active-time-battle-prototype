using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.Actions;

namespace EventBroker.SubscriberInterfaces
{
    public interface IFighterActionEnqueueRequest
    {
        void NotifyFighterCommand(ICommand fighterCommand);
    }
}