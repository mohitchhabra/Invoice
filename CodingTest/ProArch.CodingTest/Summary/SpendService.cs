using System;
using ProArch.CodingTest.External;
using ProArch.CodingTest.Invoices;
using ProArch.CodingTest.Suppliers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProArch.CodingTest.Helper;

namespace ProArch.CodingTest.Summary
{
    public class SpendService
    {
        public SpendSummary GetTotalSpend(int supplierId)
        {
            Supplier sp = SupplierService.GetById(supplierId);
            List<ExternalInvoice> externalInvs;
            List<Invoice> invoices = new List<Invoice>();

            if (sp.IsExternal)
            {
                //GetExternalInvoiceShouldruninretry
                RetryLogic(sp,3);
                externalInvs = GetExternalInvoices(sp).ToList(); //This Should be executed as part of retry
                // externalInvs = createDummyExternalInvoices(); // Todo Remove Dummy Code

                //Failover 


               
            }

            else
            {
                invoices = InvoiceRepository.Get(sp).ToList();
            }

            return CreateSpendSummary(invoices, sp);

        }

        private FailoverInvoiceCollection CreateInvoiceFromFailoverInvoice(Supplier supplier)
        {
            var failoverInvoices = FailoverInvoiceService.GetInvoices(supplier.Id);

            return failoverInvoices;
        }

        private FailoverInvoiceCollection CreateDummyFailoverInvoices(Supplier supplier)
        {
            return new FailoverInvoiceCollection
            {
                Timestamp = (supplier.Id == 2) ? DateTime.Now.AddMonths(-2) : DateTime.Now,
                Invoices = createDummyExternalInvoices().ToArray()
            };
        }

        private List<ExternalInvoice> createDummyExternalInvoices()
        {
            return new List<ExternalInvoice>
            {
                new ExternalInvoice()
                {
                    Year = DateTime.Now.AddYears(-2).Year,
                    TotalAmount = 4000
                },
                new ExternalInvoice()
                {
                    Year = DateTime.Now.AddYears(-1).Year,
                    TotalAmount = 5000
                },
                new ExternalInvoice()
                {
                    Year = DateTime.Now.AddYears(-2).Year,
                    TotalAmount = 3000
                },
                new ExternalInvoice()
                {
                    Year = DateTime.Now.AddYears(-1).Year,
                    TotalAmount = 2000
                },
                new ExternalInvoice()
                {
                    Year = DateTime.Now.Year,
                    TotalAmount = 4000
                }
            };

        }

        private SpendSummary CreateSpendSummary(List<Invoice> invoices, Supplier supplier)
        {
            //Calc total 
            var totalSpend = invoices.GroupBy(t => t.InvoiceDate.Year)
                .Select(row => new SpendDetail
                {
                    Year = row.Select(c => c.InvoiceDate.Year).FirstOrDefault(),
                    TotalSpend = row.Sum(a => a.Amount)
                }).ToList();
            return new SpendSummary
            {
                Name = supplier.Name,
                Years = totalSpend
            };
        }


        private List<Invoice> CreateInvoiceFromExternalInvoice(List<ExternalInvoice> invoices, Supplier supplier)
        {
            var output = invoices.Select(r => new Invoice
                {
                    SupplierId = supplier.Id,
                    InvoiceDate = new DateTime(r.Year, 1, 1),
                    Amount = r.TotalAmount
                }

            ).ToList();
            return output;
        }

        private ExternalInvoice[] GetExternalInvoices(Supplier supplier)
        {
            return ExternalInvoiceService.GetInvoices(supplier.Id.ToString());
        }

        private void RetryLogic( Supplier supplier, int maxAttemptCount = 3)
        {
            DateTime latestFailureTimeStamp = DateTime.MinValue;
            List<ExternalInvoice> externalInvs;
            var exceptions = new List<Exception>();
            bool maxRetryReached = false;
            var tries = 3;
            while (tries > 0 && latestFailureTimeStamp > DateTime.Now.AddMinutes(-1))
            {
                try
                {
                    externalInvs = GetExternalInvoices(supplier).ToList();
                    break; // success!
                }
                catch
                {
                    if (--tries == 0)
                    {   

                        CreateFailoverInvoices(supplier);
                    }

                }
            }
        }

        private void CreateFailoverInvoices(Supplier supplier)
        {
            List<ExternalInvoice> externalInvs;
            List<Invoice> invoices = new List<Invoice>();
            var failoverInvoices =
                CreateInvoiceFromFailoverInvoice(supplier); //Should be executed in failure block of retry
            failoverInvoices = CreateDummyFailoverInvoices(supplier); //To Do Remove this dummy code
            if (failoverInvoices.Timestamp < DateTime.Now.AddDays(-30))
            {
                throw new Exception("Data is older than 1 Month");
            }

            externalInvs = failoverInvoices.Invoices.ToList();

            invoices = CreateInvoiceFromExternalInvoice(externalInvs, supplier);
        }
    }
}