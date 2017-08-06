using Planru.Domain.Core.Commands;
using Planru.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command;
        void RaiseEvent<T>(T theEvent) where T : Event;
    }
}
