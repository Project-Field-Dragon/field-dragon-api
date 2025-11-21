using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Field.Dragon.Domain.Catalog;

namespace Project.Field.Dragon.Domain.RatingTests   // or .Tests – either is fine
{
    [TestClass]
    public class RatingTests
    {
        [TestMethod]
        public void Can_Create_New_Rating()
        {
            var rating = new Rating(1, "Mike", "Great fit!");

            Assert.AreEqual(1, rating.Stars);
            Assert.AreEqual("Mike", rating.UserName);
            Assert.AreEqual("Great fit!", rating.Review);
        }

        [TestMethod]
        public void Constructor_WithStarsLessThan1_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Rating(0, "user", "too low"));
        }

        [TestMethod]
        public void Constructor_WithStarsGreaterThan5_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Rating(6, "user", "too high"));
        }

        [TestMethod]
        public void Constructor_WithEmptyUserName_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Rating(5, "", "no username"));
        }
    }
}
