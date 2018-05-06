using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProArch.CodingTest.Invoices;
using ProArch.CodingTest.Summary;
using ProArch.CodingTest.Suppliers;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        SpendService spend = new SpendService();

        [TestMethod]
        public void When_internal_Supplier_Should_Return_Success_Spend_Summary()
        {   
            
            var totalSpend =  spend.GetTotalSpend(1);
            Assert.IsInstanceOfType( totalSpend, typeof(SpendSummary));
            Assert.IsNotNull(totalSpend);
            Assert.IsNotNull(totalSpend.Name);
            Assert.IsNotNull(totalSpend.Years);
            Assert.IsTrue(totalSpend.Years.Count >0);

        }

        [TestMethod]
        public void When_external_Supplier_Should_Return_Success_Spend_Summary()
        {

            var totalSpend = spend.GetTotalSpend(1);
            Assert.IsInstanceOfType(totalSpend, typeof(SpendSummary));
            Assert.IsNotNull(totalSpend);
            Assert.IsNotNull(totalSpend.Name);
            Assert.IsNotNull(totalSpend.Years);
            Assert.IsTrue(totalSpend.Years.Count > 0);

        }

        private static List<Invoice> CreateInvoices(Supplier supplier)
        {
            return new List<Invoice>
            {
                new Invoice()
                {
                    Amount = 1000,
                    InvoiceDate = DateTime.Now.AddYears(-2),
                    SupplierId = supplier.Id
                },
                new Invoice()
                {
                    Amount = 1000,
                    InvoiceDate = DateTime.Now.AddYears(-2),
                    SupplierId = supplier.Id
                },
                new Invoice()
                {
                    Amount = 2000,
                    InvoiceDate = DateTime.Now.AddYears(-1),
                    SupplierId = supplier.Id
                },
                new Invoice()
                {
                    Amount = 2000,
                    InvoiceDate = DateTime.Now.AddYears(-1),
                    SupplierId = supplier.Id
                },
                new Invoice()
                {
                    Amount = 3000,
                    InvoiceDate = DateTime.Now,
                    SupplierId = supplier.Id
                }
            };
        }
    }
}
