using Comviva.Billing.Library.Entities;
using Comviva.Billing.Library.Services;
using Comviva.Billing.Library.Shared;
using SQLObjectMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Comviva.Billing.Library.Repositories
{
    public class BillingRepository
    {
        #region Singleton

        private static BillingRepository instance;

        private static object lockObject = new object();

        public static BillingRepository Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (lockObject)
                    {
                        instance = new BillingRepository();
                    }
                }
                return instance;
            }
        }

        private BillingRepository()
        {
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.DefaultConnectionString].ConnectionString);
        }

        #endregion

        private IDbConnection dbConnection;

        public IEnumerable<CallbackCCG> QueryCallbackCCG()
        {
            try
            {
                return dbConnection.Query<CallbackCCG>("usp_CallbackCCG_Query", commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                LogService.Instance.LogError(MethodBase.GetCurrentMethod(), e);
                return null;
            }
        }

        public bool LogCallbackCCG(CallbackCCG callbackCCG)
        {
            try
            {
                return 0 != dbConnection.Execute("usp_CallbackCCGLogs_Insert", new { callbackCCG.MSISDN, callbackCCG.Result, callbackCCG.Reason, callbackCCG.ProductId, callbackCCG.TransID, callbackCCG.TPCGID, callbackCCG.Songname }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                LogService.Instance.LogError(MethodBase.GetCurrentMethod(), e, new { callbackCCG });
                return false;
            }
        }

        public bool DeleteCallbackCCG(int id)
        {
            try
            {
                return 0 != dbConnection.Execute("usp_CallbackCCG_Delete", new { id }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                LogService.Instance.LogError(MethodBase.GetCurrentMethod(), e, new { id });
                return false;
            }
        }
    }
}