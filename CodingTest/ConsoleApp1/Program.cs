using ProArch.CodingTest.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SpendService spend = new SpendService();
            //for internal
            // var totalSpend =  spend.GetTotalSpend(1);

                //for ext Scen positive
           // var totalSpend = spend.GetTotalSpend(2);

            //for Failover (Ext scn negative)
           // var totalSpend = spend.GetTotalSpend(2);

            // EXT scb failover posiitve
            var totalSpend = spend.GetTotalSpend(3);

        }
    }
}
