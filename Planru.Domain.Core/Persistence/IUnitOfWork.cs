using Planru.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planru.Domain.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}
