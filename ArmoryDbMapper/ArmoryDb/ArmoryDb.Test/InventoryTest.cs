using System;
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ArmoryDb.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArmoryDb.DataAccess.Entities;

namespace ArmoryDb.Test
{
    [TestClass]
    public class InventoryTest
    {
        /*
        [TestMethod]
        public void Add_writes_to_database()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ArmoryDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ArmoryDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new ArmoryDbContext(options))
                {
                    var service = new BlogService(context);
                    service.Add("http://sample.com");
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ArmoryDbContext(options))
                {
                    Assert.AreEqual(1, context.Blogs.Count());
                    Assert.AreEqual("http://sample.com", context.Blogs.Single().Url);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void Find_searches_url()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ArmoryDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ArmoryDbContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new ArmoryDbContext(options))
                {
                    context.Blogs.Add(new Blog { Url = "http://sample.com/cats" });
                    context.Blogs.Add(new Blog { Url = "http://sample.com/catfish" });
                    context.Blogs.Add(new Blog { Url = "http://sample.com/dogs" });
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new ArmoryDbContext(options))
                {
                    var service = new BlogService(context);
                    var result = service.Find("cat");
                    Assert.AreEqual(2, result.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }
        */
    }
}
