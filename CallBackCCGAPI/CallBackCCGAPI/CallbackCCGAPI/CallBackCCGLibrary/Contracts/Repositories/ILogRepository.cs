using System;
using System.Reflection;

namespace ComvivaAPILibrary.Contracts.Repositories
{
    using CustomLogger.Constants;

    public interface ILogRepository
    {
        bool Log(MethodBase methodBase, string objectDump, Enum.EventType eventType, Enum.EventPriority eventPriority, string message, DateTime tStamp);
    }
}