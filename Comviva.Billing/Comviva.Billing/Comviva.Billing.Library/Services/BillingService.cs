using Comviva.Billing.Library.Entities;
using Comviva.Billing.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Comviva.Billing.Library.Services
{
    public class BillingService
    {
        #region Singleton

        private static BillingService instance;

        private static object lockObject = new object();

        public static BillingService Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (lockObject)
                    {
                        instance = new BillingService();
                    }
                }
                return instance;
            }
        }

        private BillingService()
        {
        }

        #endregion

        public IEnumerable<CallbackCCG> QueryCallbackCCG()
        {
            try
            {
                return BillingRepository.Instance.QueryCallbackCCG();
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
                return BillingRepository.Instance.LogCallbackCCG(callbackCCG);
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
                return BillingRepository.Instance.DeleteCallbackCCG(id);
            }
            catch (Exception e)
            {
                LogService.Instance.LogError(MethodBase.GetCurrentMethod(), e, new { id });
                return false;
            }
        }
    }
}