using CustomLogger.BL;
using System;
using System.Data;
using System.Reflection;
using ComvivaAPILibrary.Contracts.Repositories;


namespace ComvivaAPILibrary.Concretes.Repositories
{
    using CustomLogger.Constants;

    public class LogRepository : ILogRepository
    {
        private IDbConnection DbConnection;

        public LogRepository(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public bool Log(MethodBase methodBase, string objectDump, Enum.EventType eventType, Enum.EventPriority eventPriority, string message, DateTime tStamp)
        {
            try
            {
                return Manager.Instance.GetLoggerObject(DbConnection).Log(-1, methodBase, objectDump, eventType, eventPriority, message, tStamp);
            }
            catch
            {
                return false;
            }
        }
    }
}