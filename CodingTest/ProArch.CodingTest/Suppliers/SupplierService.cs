namespace ProArch.CodingTest.Suppliers
{
    public static class SupplierService
    {
        public static Supplier GetById(int id)
        {
            return  (id == 1 )? GetInternalSuppulier() : GetExternalSuppulier();


        }

        private static Supplier GetInternalSuppulier()
        {
            return new Supplier
            {
                Id = 1,
                IsExternal = false,
                Name = "Mohit"
            };
        }

        private static Supplier GetExternalSuppulier()
        {
            return new Supplier
            {
                Id = 2,
                IsExternal = true,
                Name = "John"
            };
        }
    }
}
