using Comviva.Billing.Library.Services;
using Comviva.Billing.Library.Workers;
using System;
using System.Reflection;

namespace Comviva.Billing.Engine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                BillingWorker.Instance.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred, Please refer to the logs for details");
                LogService.Instance.LogError(MethodBase.GetCurrentMethod(), e);
            }
        }
    }
}