using Planru.Domain.Core.Bus;
using System;
using Planru.Domain.Core.Commands;
using Planru.Domain.Core.Events;

namespace Planru.CrossCutting.Bus
{
    public class InMemoryBus : IBus
    {
        public static Func<IServiceProvider> ContainerAccessor { get; set; }
        private static IServiceProvider Container => ContainerAccessor();

        private readonly IEventStore _eventStore;

        public void RaiseEvent<T>(T theEvent) where T : Event
        {
            throw new NotImplementedException();
        }

        public void SendCommand<T>(T theCommand) where T : Command
        {
            throw new NotImplementedException();
        }
    }
}
