using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ProArch.CodingTest.External;
using ProArch.CodingTest.Invoices;

namespace ProArch.CodingTest.Helper
{
    public static class Retry
    {

    //    public static DateTime Retry (  
    //        TimeSpan retryInterval, int supplierId,
    //        int maxAttemptCount = 3)
    //    {
    //        DateTime latestFailureTimeStamp = DateTime.MinValue;
    //        var exceptions = new List<Exception>();
    //        bool maxRetryReached = false;
    //        for (int attempted = 0; attempted < maxAttemptCount; attempted++)
    //        {
    //            try
    //            {
    //                if (attempted < maxAttemptCount && latestFailureTimeStamp > DateTime.Now.AddMinutes(-1))
    //                {
    //                   var externalInvoices = GetExternalInvoices(supplierId);

    //                }
    //                else
    //                {
    //                    //Call failover service
    //                    var failoverInvoice = FailoverInvoiceService.GetInvoices(supplierId);
    //                    if ((failoverInvoice.Timestamp - DateTime.Now).TotalDays > 30)
    ////                    {
    ////                        throw new Exception("Data is older than 1 Month");
    ////                    }
    ////                    latestFailureTimeStamp = DateTime.Now;
    ////                }
    ////            }
    ////            catch (Exception ex)
    ////            {
    ////                exceptions.Add(ex);
    ////            }
    ////        }
    ////        throw new AggregateException(exceptions);
    ////    }

    ////    private static ExternalInvoice[] GetExternalInvoices(int id)
    ////    {
    ////        return ExternalInvoiceService.GetInvoices(id.ToString());
    ////    }
    }
}
