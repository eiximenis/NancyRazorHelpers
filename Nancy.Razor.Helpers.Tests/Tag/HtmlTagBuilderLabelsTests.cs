using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Razor.Helpers.Tag;
using Nancy.ViewEngines.Razor;

namespace Nancy.Razor.Helpers.Tests
{

    class LabelModel
    {
        public string SomeProperty { get; set; }

        [DisplayName("Another Property")]
        public string AnotherProperty { get; set; }
    }

    [TestClass]
    public class HtmlTagBuilderLabelsTests
    {
        [TestMethod]
        public void LabelForUsesPropertyNameByDefault()
        {
            var tag = HtmlTagBuilder.CreateLabelFor<LabelModel, string>(lm => lm.SomeProperty, null);
            Assert.AreEqual("SomeProperty", tag.RawContent);
        }

        [TestMethod]
        public void LabelForUsesDisplayNameIfFound()
        {
            var tag = HtmlTagBuilder.CreateLabelFor<LabelModel, string>(lm => lm.AnotherProperty, null);
            Assert.AreEqual("Another Property", tag.RawContent);
        }
    }
}
