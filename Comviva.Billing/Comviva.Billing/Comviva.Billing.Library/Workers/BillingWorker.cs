using Comviva.Billing.Library.Entities;
using System.Linq;
using Comviva.Billing.Library.Services;
using Comviva.Billing.Library.Shared;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System;
using System.Reflection;
using System.Net.Http;
using System.Text;

namespace Comviva.Billing.Library.Workers
{
    public class BillingWorker
    {
        #region Singleton

        private static BillingWorker instance;

        private static object lockObject = new object();

        public static BillingWorker Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (lockObject)
                    {
                        instance = new BillingWorker();
                    }
                }
                return instance;
            }
        }

        private BillingWorker()
        {
        }

        #endregion

        private string payloadTemplate;

        public void Start()
        {
            try
            {
                int waitHandle = 0;
                int.TryParse(ConfigurationManager.AppSettings[Constants.WaitHandle], out waitHandle);
                if (0 > waitHandle)
                    waitHandle = 0;
                payloadTemplate = IOService.Instance.Read(ConfigurationManager.AppSettings[Constants.PayloadTemplatePathKey]);
                if (!string.IsNullOrWhiteSpace(payloadTemplate))
                {
                    while (true)
                    {
                        Console.Clear();
                        IEnumerable<CallbackCCG> requests = BillingService.Instance.QueryCallbackCCG();
                        if (null != requests && 0 != requests.Count())
                        {
                            Console.WriteLine("{0} requests were found", requests.Count());
                            for (int i = 0; i < requests.Count(); i++)
                                ProcessRequest(requests.ElementAt(i));
                        }
                        else
                        {
                            Console.WriteLine("No requests were found");
                        }
                        if (0 != waitHandle)
                            Thread.Sleep(waitHandle);
                    }
                }
                else
                {
                    LogService.Instance.LogWarning(MethodBase.GetCurrentMethod(), "The payload template is not valid");
                    return;
                }
            }
            catch (Exception e)
            {
                LogService.Instance.LogError(MethodBase.GetCurrentMethod(), e);
            }
        }

        private void ProcessRequest(CallbackCCG callbackCCG)
        {
            try
            {
                Console.WriteLine("Processing request #{0}", callbackCCG.ID);
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = httpClient.PostAsync(Constants.Uri, new StringContent(string.Format(payloadTemplate, callbackCCG.TPCGID, callbackCCG.MSISDN), Encoding.UTF8, "text/xml")).Result;
                if (null != response)
                {
                    string responseText = response.Content.ReadAsStringAsync().Result;
                    if (responseText.Contains("<result>DBILL:Ok, Accepted</result>"))
                    {
                        BillingService.Instance.LogCallbackCCG(callbackCCG);
                        BillingService.Instance.DeleteCallbackCCG(callbackCCG.ID);
                    }
                    else
                    {
                        LogService.Instance.LogWarning(MethodBase.GetCurrentMethod(), "The response indicated a failure", new { callbackCCG, responseText });
                    }
                }
                else
                {
                    LogService.Instance.LogWarning(MethodBase.GetCurrentMethod(), "The response turned out null", new { callbackCCG });
                }
            }
            catch (Exception e)
            {
                LogService.Instance.LogError(MethodBase.GetCurrentMethod(), e);
            }
        }
    }
}