using System;
using System.IO;
using System.Reflection;

namespace Comviva.Billing.Library.Services
{
    public class IOService
    {
        #region Singleton

        private static IOService instance;

        private static object lockObject = new object();

        public static IOService Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (lockObject)
                    {
                        instance = new IOService();
                    }
                }
                return instance;
            }
        }

        private IOService()
        {
        }

        #endregion

        public string Read(string filePath)
        {
            StreamReader sr = null;
            try
            {
                if (File.Exists(filePath))
                {
                    sr = new StreamReader(filePath);
                    return sr.ReadToEnd();
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                LogService.Instance.LogError(MethodBase.GetCurrentMethod(), e, new { filePath });
                return string.Empty;
            }
            finally
            {
                if (null != sr)
                    sr.Close();
            }
        }
    }
}