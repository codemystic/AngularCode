using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularMVC.Controllers
{
    public class ExceptionController : Controller
    {
        public void LogException(string stackTrace, string exMessage)
        {
            using (MyTestDBContext contextObj = new MyTestDBContext())
            {
                ExceptionLog exLog = new ExceptionLog();
                exLog.StackTrace = stackTrace;
                exLog.Message = exMessage;
                exLog.DateStamp = DateTime.Now;
                contextObj.ExceptionLogs.Add(exLog);
                contextObj.SaveChanges();
            }
        }
    }
}