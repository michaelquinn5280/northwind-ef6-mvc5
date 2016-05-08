using System.IO;
using Northwind.Domain.Model;

namespace Northwind.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// DB Migrations
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<NorthwindContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NorthwindContext context)
        {

        }
    }

    /// <summary>
    /// Northwind DB Initializer
    /// </summary>
    public class NorthwindDbInitializer : CreateDatabaseIfNotExists<NorthwindContext>
    {
        /// <summary>
        /// Seeding just Category, Supplier, and Products for now
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(NorthwindContext context)
        {
            var baseDirectory = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "bin\\Migrations\\");
            context.Database.ExecuteSqlCommand(File.ReadAllText(string.Concat(baseDirectory, "SeedNorthwindSuppliers.sql")));
            context.Database.ExecuteSqlCommand(File.ReadAllText(string.Concat(baseDirectory, "SeedNorthwindCategories.sql")));
            context.Database.ExecuteSqlCommand(File.ReadAllText(string.Concat(baseDirectory, "SeedNorthwindProducts.sql")));
            base.Seed(context); 
        }
    }
}
