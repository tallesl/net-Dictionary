namespace DictionaryLibrary.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Typical()
        {
            // Arrange
            var obj = new SomeData
            {
                Id = new Guid("8d995e6e-af88-4043-8c31-8ba04c6b4298"),
                Integer = 23,
                Text = "foo bar",
                Date = null
            };

            // Act
            var dict = DictionaryMaker.Make(obj);

            // Assert
            Assert.AreEqual(obj.Id, dict["Id"]);
            Assert.AreEqual(obj.Integer, dict["Integer"]);
            Assert.AreEqual(obj.Text, dict["Text"]);
            Assert.AreEqual(obj.Date, dict["Date"]);
        }

        [TestMethod]
        public void Immutable()
        {
            // Arrange
            var obj = new SomeImmutableData(new Guid("8d995e6e-af88-4043-8c31-8ba04c6b4298"), 23, "foo bar", null);

            // Act
            var dict = DictionaryMaker.Make(obj);

            // Assert
            Assert.AreEqual(obj.Id, dict["Id"]);
            Assert.AreEqual(obj.Integer, dict["Integer"]);
            Assert.AreEqual(obj.Text, dict["Text"]);
            Assert.AreEqual(obj.Date, dict["Date"]);
        }

        [TestMethod]
        public void Anonymous()
        {
            // Arrange
            var obj = new
            {
                Id = new Guid("8d995e6e-af88-4043-8c31-8ba04c6b4298"),
                Integer = 23,
                Text = "foo bar",
                Date = (DateTime?)null
            };

            // Act
            var dict = DictionaryMaker.Make(obj);

            // Assert
            Assert.AreEqual(obj.Id, dict["Id"]);
            Assert.AreEqual(obj.Integer, dict["Integer"]);
            Assert.AreEqual(obj.Text, dict["Text"]);
            Assert.AreEqual(obj.Date, dict["Date"]);
        }

        [TestMethod]
        public void Expando()
        {
            // Arrange
            dynamic obj = new ExpandoObject();
            obj.Id = new Guid("8d995e6e-af88-4043-8c31-8ba04c6b4298");
            obj.Integer = 23;
            obj.Text = "foo bar";
            obj.Date = (DateTime?)null;

            // Act
            // If I use "var dict" here the type of dict is an ExpandoObject...
            // What on earth is that?
            IDictionary<string, object> dict = DictionaryMaker.Make(obj);

            // Assert
            Assert.AreEqual(obj.Id, dict["Id"]);
            Assert.AreEqual(obj.Integer, dict["Integer"]);
            Assert.AreEqual(obj.Text, dict["Text"]);
            Assert.AreEqual(obj.Date, dict["Date"]);
        }

        [TestMethod]
        public void Dictionary()
        {
            // Arrange
            var obj = new Dictionary<string, object>
            {
                { "Id", new Guid("8d995e6e-af88-4043-8c31-8ba04c6b4298") },
                { "Integer", 23 },
                { "Text", "foo bar" },
                { "Date", (DateTime?)null }
            };

            // Act
            var dict = DictionaryMaker.Make(obj);

            // Assert
            Assert.AreEqual(obj["Id"], dict["Id"]);
            Assert.AreEqual(obj["Integer"], dict["Integer"]);
            Assert.AreEqual(obj["Text"], dict["Text"]);
            Assert.AreEqual(obj["Date"], dict["Date"]);
        }

        [TestMethod]
        public void AnonymousWithType()
        {
            // Arrange
            var obj = new
            {
                Id = new Guid("8d995e6e-af88-4043-8c31-8ba04c6b4298"),
                Integer = 23,
                Text = "foo bar",
                Date = (DateTime?)null
            };

            // Act
            var dict = DictionaryMaker.MakeWithType(obj);

            // Assert
            Assert.AreEqual(new Tuple<Type, object>(typeof(Guid), obj.Id), dict["Id"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(int), obj.Integer), dict["Integer"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(string), obj.Text), dict["Text"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(DateTime?), obj.Date), dict["Date"]);
        }

        [TestMethod]
        public void ExpandoWithType()
        {
            // Arrange
            dynamic obj = new ExpandoObject();
            obj.Id = new Guid("8d995e6e-af88-4043-8c31-8ba04c6b4298");
            obj.Integer = 23;
            obj.Text = "foo bar";
            obj.Date = (DateTime?)null;

            // Act
            IDictionary<string, Tuple<Type, object>> dict = DictionaryMaker.MakeWithType(obj);

            // Assert
            Assert.AreEqual(new Tuple<Type, object>(typeof(Guid), obj.Id), dict["Id"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(int), obj.Integer), dict["Integer"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(string), obj.Text), dict["Text"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(object), obj.Date), dict["Date"]);
        }

        [TestMethod]
        public void DictionaryType()
        {
            // Arrange
            var obj = new Dictionary<string, object>
            {
                { "Id", new Guid("8d995e6e-af88-4043-8c31-8ba04c6b4298") },
                { "Integer", 23 },
                { "Text", "foo bar" },
                { "Date", (DateTime?)null }
            };

            // Act
            var dict = DictionaryMaker.MakeWithType(obj);

            // Assert
            Assert.AreEqual(new Tuple<Type, object>(typeof(Guid), obj["Id"]), dict["Id"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(int), obj["Integer"]), dict["Integer"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(string), obj["Text"]), dict["Text"]);
            Assert.AreEqual(new Tuple<Type, object>(typeof(object), obj["Date"]), dict["Date"]);
        }
    }
}
