using System;
using System.Reflection;
using ComvivaAPILibrary.Contracts.Repositories;
using ComvivaAPILibrary.Contracts.Services;

namespace ComvivaAPILibrary.Concretes.Services
{
    
    using CustomLogger.Constants;

    public class LogService : ILogService
    {
        private ILogRepository LogRepository;

        public LogService(ILogRepository logRepository)
        {
            LogRepository = logRepository;
        }

        public bool LogInformation(MethodBase methodBase, string objectDump, string message)
        {
            return LogRepository.Log(methodBase, objectDump, Enum.EventType.Info, Enum.EventPriority.High, message, DateTime.Now);
        }

        public bool LogWarning(MethodBase methodBase, string objectDump, string message)
        {
            return LogRepository.Log(methodBase, objectDump, Enum.EventType.Warrning, Enum.EventPriority.High, message, DateTime.Now);
        }

        public bool LogError(MethodBase methodBase, string objectDump, Exception exception)
        {
            return LogRepository.Log(methodBase, objectDump, Enum.EventType.Exception, Enum.EventPriority.High, exception.Message, DateTime.Now);
        }
    }
}