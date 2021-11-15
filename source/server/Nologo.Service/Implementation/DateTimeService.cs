using System;
using Nologo.Service.Contracts;

namespace Nologo.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}