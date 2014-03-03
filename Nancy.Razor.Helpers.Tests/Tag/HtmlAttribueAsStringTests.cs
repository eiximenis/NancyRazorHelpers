using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Razor.Helpers.Tag;

namespace Nancy.Razor.Helpers.Tests
{
    [TestClass]
    public class HtmlAttribueAsStringTests
    {
        [TestMethod]
        public void TestTagWithOneValueAttribute()
        {
            var tag = new HtmlTag("a");
            tag.WithAttribute("href", "test");
            Assert.AreEqual(@"<a href=""test"" />" + Environment.NewLine, tag.ToString(), false);
        }

        [TestMethod]
        public void TestTagWithOneEmptyAttribute()
        {
            var tag = new HtmlTag("input");
            tag.WithEmptyAttribute("checked");
            Assert.AreEqual(@"<input checked />" + Environment.NewLine, tag.ToString(), false);
        }

        [TestMethod]
        public void TestTagWithMoreThanOneAttribute()
        {
            var tag = new HtmlTag("input");
            tag.WithAttributes(new {type = "checkbox", @checked = "checked"});
            Assert.AreEqual(@"<input type=""checkbox"" checked=""checked"" />" + Environment.NewLine, tag.ToString(), false);
        }

        [TestMethod]
        public void TestAttributesEmptyAreNotAdded()
        {
            var tag = new HtmlTag("input");
            tag.WithAttributes(new { type = "checkbox", @checked = "" });
            Assert.AreEqual(@"<input type=""checkbox"" />" + Environment.NewLine, tag.ToString(), false);
        }

        [TestMethod]
        public void TestAttributesNullAreNotAdded()
        {
            var tag = new HtmlTag("input");
            tag.WithAttributes(new { type = "checkbox", @checked = (string)null});
            Assert.AreEqual(@"<input type=""checkbox"" />" + Environment.NewLine, tag.ToString(), false);
        }


    }
}

