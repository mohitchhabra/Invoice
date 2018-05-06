namespace ProArch.CodingTest.Suppliers
{
    public static class SupplierService
    {
        public static Supplier GetById(int id)
        {
            return  (id == 1 )? GetInternalSuppulier(id) : GetExternalSuppulier(id);
            
        }

        private static Supplier GetInternalSuppulier(int id)
        {
            return new Supplier
            {
                Id =id,
                IsExternal = false,
                Name = "Mohit"
            };
        }

        private static Supplier GetExternalSuppulier(int id)
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
