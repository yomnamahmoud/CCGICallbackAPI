using CallBackCCGAPILibrary.Concretes.Entities;
using CallBackCCGAPILibrary.Contracts.Entities;
using CallBackCCGAPILibrary.Contracts.Services;
using ObjectDumper;
using SMSManager.DAL;
using SMSManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace CallBackCCGAPI.Controllers
{
    public class CallBackController : ApiController
    {
        private ILogService LogService;
        private ICallBackCCGService CallBackCCGService;
        public CallBackController(ILogService logService, ICallBackCCGService callBackCCGService)
        {
            LogService = logService;
            CallBackCCGService = callBackCCGService;
        }

       [HttpGet]
        public IHttpActionResult CallBackCCG(string msisdn="", string Result="", string Reason="", string productId="", string transID="", string TPCGID="", string Songname="")
        {
            try
            {
                if (!string.IsNullOrEmpty(msisdn) /*&& Regex.IsMatch(msisdn, "(^[2][0-9]{11}$)")*/)
                {
                    ICallBackCCGEntity CallBackCCG = new CallBackCCGEntity
                    {
                        MSISDN = msisdn,
                        Result = Result,
                        Reason=Reason,
                        productId=productId,
                    transID=transID,
                    TPCGID=TPCGID,
                    Songname=Songname

                    };
                    if (CallBackCCGService.InsertCallBackCCG(CallBackCCG))
                    {
                       string [] args =new string[10];
                        Comviva.Billing.Engine.Program.Main(args);
                        return Ok("Success"); 
                    }
                    else
                    {
                        LogService.LogWarning(MethodBase.GetCurrentMethod(), ObjectDumperExtensions.DumpToString<ICallBackCCGEntity>(CallBackCCG, "SMS"), "Failed to insert a message into the received queue");
                        return Content(HttpStatusCode.InternalServerError, "Failed to insert the callbackCCG");
                      
                    }
                }
                else
                {
                    LogService.LogWarning(MethodBase.GetCurrentMethod(), string.Format("MSISDN: {0}", msisdn), "Invalid MSISDN)");
                    return Content(HttpStatusCode.BadRequest, "Invalid MSISDN");
                }
            }
            catch (Exception e)
            {
                LogService.LogError(MethodBase.GetCurrentMethod(), string.Format("MSISDN: {0}", msisdn), e);
                return InternalServerError(e);
            }
        }
   
    }
}