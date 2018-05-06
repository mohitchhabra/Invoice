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
            //Arrange
            var supplier= GetInternalSupplier(1);
            CreateInvoices(supplier);

            //Act
            var totalSpend =  spend.GetTotalSpend(1);

            //Assert
            Assert.IsInstanceOfType( totalSpend, typeof(SpendSummary));
            Assert.IsNotNull(totalSpend);
            Assert.IsNotNull(totalSpend.Name);
            Assert.IsTrue(totalSpend.Years.Count >0);

        }

        
        [TestMethod]
        public void When_external_Supplier_Should_Return_Success_Spend_Summary()
        {
            //Arrange
            var supplier = GetExternalSupplier(2);
            CreateInvoices(supplier);

            //Act
            var totalSpend = spend.GetTotalSpend(2);

            //Assert
            Assert.IsInstanceOfType(totalSpend, typeof(SpendSummary));
            Assert.IsNotNull(totalSpend);
            Assert.IsNotNull(totalSpend.Name);
            Assert.IsTrue(totalSpend.Years.Count > 0);

        }

        [TestMethod]
        public void When_external_Supplier_Fails_Should_Return_Success_With_Failover_Service_Spend_Summary()
        {
            //Arrange
            var supplier = GetExternalSupplier(3);
            CreateInvoices(supplier);

            //Act
            var totalSpend = spend.GetTotalSpend(3);

            //Assert
            Assert.IsInstanceOfType(totalSpend, typeof(SpendSummary));
            Assert.IsNotNull(totalSpend);
            Assert.IsNotNull(totalSpend.Name);
            Assert.IsTrue(totalSpend.Years.Count > 0);

        }


        [TestMethod]
        [ExpectedException(typeof(DataNotSyncedException), "Data is older than 1 Month")]
        public void When_Failover_Service_Timestamp_older_than_a_Month_should_Throw_Exception()
        {
            //Arrange
            var supplier = GetExternalSupplier(3);
            CreateInvoices(supplier);

            //Act
            var totalSpend = spend.GetTotalSpend(3);

            //Assert
            try
            {
               Assert.Fail("No Exception Throwed");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is DataNotSyncedException);
                throw;
            }

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

        private static Supplier GetInternalSupplier(int id)
        {
            return new Supplier
            {
                Id = id,
                IsExternal = false,
                Name = "Mohit"
            };
        }

        private static Supplier GetExternalSupplier(int id)
        {
            return new Supplier
            {
                Id = id,
                IsExternal = true,
                Name = "John"
            };
        }



    }
}
