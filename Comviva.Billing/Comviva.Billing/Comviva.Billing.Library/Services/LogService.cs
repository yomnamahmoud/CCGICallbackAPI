using Comviva.Billing.Library.Repositories;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Comviva.Billing.Library.Services
{
    using CustomLogger.Constants;

    public class LogService
    {
        #region Singleton

        private static LogService instance;

        private static object lockObject = new object();

        public static LogService Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (lockObject)
                    {
                        instance = new LogService();
                    }
                }
                return instance;
            }
        }

        private LogService()
        {
        }

        #endregion

        public bool LogError(MethodBase methodBase, Exception exception)
        {
            try
            {
                return LogRepository.Instance.Log(methodBase, JsonConvert.SerializeObject(exception), Enum.EventType.Exception, Enum.EventPriority.High, string.Empty, DateTime.Now);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool LogError(MethodBase methodBase, Exception exception, string message)
        {
            try
            {
                return LogRepository.Instance.Log(methodBase, JsonConvert.SerializeObject(exception), Enum.EventType.Exception, Enum.EventPriority.High, message ?? string.Empty, DateTime.Now);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool LogError(MethodBase methodBase, Exception exception, dynamic objectSet)
        {
            try
            {
                return LogRepository.Instance.Log(methodBase, Stringify(exception, objectSet), Enum.EventType.Exception, Enum.EventPriority.High, string.Empty, DateTime.Now);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool LogError(MethodBase methodBase, Exception exception, string message, dynamic objectSet)
        {
            try
            {
                return LogRepository.Instance.Log(methodBase, Stringify(exception, objectSet), Enum.EventType.Exception, Enum.EventPriority.High, message ?? string.Empty, DateTime.Now);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool LogWarning(MethodBase methodBase, string message)
        {
            try
            {
                return LogRepository.Instance.Log(methodBase, string.Empty, Enum.EventType.Warrning, Enum.EventPriority.Medium, message ?? string.Empty, DateTime.Now);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool LogWarning(MethodBase methodBase, string message, dynamic objectSet)
        {
            try
            {
                return LogRepository.Instance.Log(methodBase, null == objectSet ? string.Empty : JsonConvert.SerializeObject(objectSet), Enum.EventType.Warrning, Enum.EventPriority.Medium, message ?? string.Empty, DateTime.Now);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool LogInformation(MethodBase methodBase, string message)
        {
            try
            {
                return LogRepository.Instance.Log(methodBase, string.Empty, Enum.EventType.Info, Enum.EventPriority.Low, message ?? string.Empty, DateTime.Now);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool LogInformation(MethodBase methodBase, string message, dynamic objectSet)
        {
            try
            {
                return LogRepository.Instance.Log(methodBase, null == objectSet ? string.Empty : JsonConvert.SerializeObject(objectSet), Enum.EventType.Info, Enum.EventPriority.Low, message ?? string.Empty, DateTime.Now);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private string Stringify(Exception exception, dynamic objectSet)
        {
            if (null != exception)
                return null == objectSet ? JsonConvert.SerializeObject(exception) : JsonConvert.SerializeObject(new { exception, objectSet });
            else if (null != objectSet)
                return JsonConvert.SerializeObject(objectSet);
            return string.Empty;
        }
    }
}