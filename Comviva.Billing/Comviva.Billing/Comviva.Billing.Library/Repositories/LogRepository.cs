using Comviva.Billing.Library.Shared;
using CustomLogger.BL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Comviva.Billing.Library.Repositories
{
    using CustomLogger.Constants;

    public class LogRepository
    {
        #region Singleton

        private static LogRepository instance;

        private static object lockObject = new object();

        public static LogRepository Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (lockObject)
                    {
                        instance = new LogRepository();
                    }
                }
                return instance;
            }
        }

        private LogRepository()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.DefaultConnectionString].ConnectionString);
        }

        #endregion

        private IDbConnection dbConnection;

        public bool Log(MethodBase methodBase, string objectDump, Enum.EventType eventType, Enum.EventPriority eventPriority, string message, DateTime tStamp)
        {
            try
            {
                return Manager.Instance.GetLoggerObject(dbConnection).Log(-1, methodBase, objectDump, eventType, eventPriority, message, tStamp);
            }
            catch
            {
                return false;
            }
        }
    }
}