using System;

namespace Nologo.Service.Contracts
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
