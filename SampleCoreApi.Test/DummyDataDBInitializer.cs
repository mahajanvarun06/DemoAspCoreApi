using SampleCore.Entities;
using System;
using System.Collections.Generic;

namespace SampleCoreApi.Test
{
    public static class DummyDataDBInitializer
    {
        #region SeedDB
        public static void InitializeDbForTests(SampleCoreDbContext db)
        {
            db.Products.AddRange(GetSeedingProducts());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(SampleCoreDbContext db)
        {
            db.Products.RemoveRange(db.Products);
            InitializeDbForTests(db);
        }

        public static List<Product> GetSeedingProducts()
        {
            return new List<Product>()
            {
              new Product() { Name = "Product1", Category = "Soap", Color ="Pink", UnitPrice= 20, AvailableQuantity = 10},
              new Product() { Name = "Product2", Category = "Shampoo", Color = "Black", UnitPrice = 100, AvailableQuantity = 10 }
            };
        }

        public static Product PostSeedingProduct()
        {
            Product prod = new Product();
            prod.Name = "Product3";
            prod.Category = "Water Bottle";
            prod.Color = "Blue";
            prod.UnitPrice = 50;
            prod.AvailableQuantity = 10;
            return prod;
        }
        #endregion

    }
}