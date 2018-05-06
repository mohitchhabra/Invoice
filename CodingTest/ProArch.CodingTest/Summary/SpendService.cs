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
            ExternalInvoice[] externalInvs;
            List<Invoice> invoices = new List<Invoice>();

            if (sp.IsExternal)
            {  
            }

            else
            {
                invoices = InvoiceRepository.Get(sp).ToList();
            }

         return CreateSpendSummary(invoices, sp);

        }

        private SpendSummary CreateSpendSummary(List<Invoice> invoices, Supplier supplier)
        {
            //Calc total 
         var totalSpend =   invoices.GroupBy(t => t.InvoiceDate.Year)
                .Select(row => new SpendDetail
                {   Year = row.Select(c=> c.InvoiceDate.Year).FirstOrDefault(),
                   TotalSpend = row.Sum(a => a.Amount)
                }).ToList();
            return new SpendSummary
            {
                Name = supplier.Name,
                Years = totalSpend
            };
        }


        private ExternalInvoice[] GetExternalInvoices(int id)
        {
            return ExternalInvoiceService.GetInvoices(id.ToString());
        }
    }
}
