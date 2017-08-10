using System;
using System.Reflection;

namespace ComvivaAPILibrary.Contracts.Services
{
    public interface ILogService
    {
        bool LogInformation(MethodBase methodBase, string objectDump, string message);

        bool LogWarning(MethodBase methodBase, string objectDump, string message);

        bool LogError(MethodBase methodBase, string objectDump, Exception exception);
    }
}