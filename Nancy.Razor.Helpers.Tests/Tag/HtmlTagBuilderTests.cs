using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Razor.Helpers.Tag;
using Nancy.ViewEngines.Razor;

namespace Nancy.Razor.Helpers.Tests.Tag
{

    public class Person
    {
        public int Age { get; set; }
        public string FirstName { get; set; }

        public int Grow()
        {
            this.Age++;
            return Age;
        }
    }

    [TestClass]
    public class HtmlTagBuilderTests
    {
        [TestMethod]
        public void CreateInputElementForNonPropertyReturnsNull()
        {
            var person = new Person();
            var helpers = new HtmlHelpers<Person>(null, null, person);
            var tag = HtmlTagBuilder.CreateInputElementFor(helpers, p => p.Grow(), HtmlInputType.Text, null);
            Assert.IsNull(tag);
        }

        [TestMethod]
        public void CreateInputElementDoesNotGenerateAttributeValueForNullModel()
        {
            var helpers = new HtmlHelpers<Person>(null, null, null);
            var tag = HtmlTagBuilder.CreateInputElementFor(helpers, p => p.Age, HtmlInputType.Text, null);
            Assert.IsNull(tag.Attribute("value"));
        }


        [TestMethod]
        public void CreateInputElementDoesNotGenerateAttributeValueForNullProperty()
        {
            var person = new Person() { FirstName = null };
            var helpers = new HtmlHelpers<Person>(null, null, person);
            var tag = HtmlTagBuilder.CreateInputElementFor(helpers, p => p.FirstName, HtmlInputType.Text, null);
            Assert.IsNull(tag.Attribute("value"));
        }

        [TestMethod]
        public void CreateInputElementUsesPropertyNameAsAttrName()
        {
            var person = new Person() { FirstName = "Alice" };
            var helpers = new HtmlHelpers<Person>(null, null, person);
            var tag = HtmlTagBuilder.CreateInputElementFor(helpers, p => p.FirstName, HtmlInputType.Text, null);
            Assert.AreEqual("firstname", tag.Attribute("name").Value, true);
        }

        [TestMethod]
        public void CreateInputElementStoresValueOfStringProperty()
        {
            var person = new Person() { FirstName = "Alice" };
            var helpers = new HtmlHelpers<Person>(null, null, person);
            var tag = HtmlTagBuilder.CreateInputElementFor(helpers, p => p.FirstName, HtmlInputType.Text, null);
            Assert.AreEqual("Alice", tag.Attribute("value").Value);
        }

        [TestMethod]
        public void CreateInputElementStoresValueOfNonStringProperty()
        {
            var person = new Person() { FirstName = "Alice", Age = 42};
            var helpers = new HtmlHelpers<Person>(null, null, person);
            var tag = HtmlTagBuilder.CreateInputElementFor(helpers, p => p.Age, HtmlInputType.Text, null);
            Assert.AreEqual(Convert.ToString(person.Age), tag.Attribute("value").Value);
        }

        [TestMethod]
        public void CreateInputPasswordElementNeverStoresPropertyValue()
        {
            var person = new Person() { FirstName = "Alice" };
            var helpers = new HtmlHelpers<Person>(null, null, person);
            var tag = HtmlTagBuilder.CreateInputElementFor(helpers, p => p.FirstName, HtmlInputType.Password, null);
            Assert.IsNull(tag.Attribute("value"));
        }

    }
}
