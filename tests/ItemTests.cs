using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Field.Dragon.Domain.Catalog;   // Item & Rating live here

// Use the SAME namespace line that you used in RatingTests.cs.
// If your RatingTests says "namespace Project.Field.Dragon.Domain.RatingTests",
// copy that exactly here.
namespace Project.Field.Dragon.Domain.Tests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void Can_Create_New_Item()
        {
            var item = new Item("Name", "Description", "Brand", 10.00m);

            Assert.AreEqual("Name", item.Name);
            Assert.AreEqual("Description", item.Description);
            Assert.AreEqual("Brand", item.Brand);
            Assert.AreEqual(10.00m, item.Price);
        }

        [TestMethod]
        public void Can_Create_Add_Rating()
        {
            // Arrange
            var item = new Item("Name", "Description", "Brand", 10.00m);
            var rating = new Rating(5, "Name", "Review");

            // Act
            item.AddRating(rating);

            // Assert
            Assert.AreEqual(rating, item.Ratings[0]);
        }
    }
}
