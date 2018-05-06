using System;
using System.Collections.Generic;
using System.Linq;
using ProArch.CodingTest.Suppliers;

namespace ProArch.CodingTest.Invoices
{
    public static class InvoiceRepository
    {
        public static IQueryable<Invoice> Get(Supplier supplier)
        {
            return CreateInvoices(supplier).AsQueryable();
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
