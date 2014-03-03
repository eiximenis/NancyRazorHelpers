using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Razor.Helpers.Tag;
using Nancy.ViewEngines.Razor;

namespace Nancy.Razor.Helpers.Tests.Tag
{


    class BooleanModel
    {
        public bool BooleanProp { get; set; }

        [DefaultValue(true)]
        public bool BooleanPropWithDefValueTrue { get; set; }

        [DefaultValue("true")]
        public bool BooleanPropWithDefValueTrueAsString { get; set; }

        [DefaultValue(false)]
        public bool BooleanPropWithDefValueFalse { get; set; }

        [DefaultValue("false")]
        public bool BooleanPropWithDefValueFalseAsString { get; set; }
    }

    [TestClass]
    public class HtmlTagBuilderCheckboxesTests
    {
    

        [TestMethod]
        public void CheckboxForBoolPropertyIsNotChecked()
        {
            var html = new HtmlHelpers<BooleanModel>(null, null, null);
            var tag = HtmlTagBuilder.CreateCheckBoxFor(html, m => m.BooleanProp, null);
            Assert.IsNull(tag.Attribute("checked"));
        }

        [TestMethod]
        public void CheckboxForBoolPropertyWithDefValueTrueIsChecked()
        {
            var html = new HtmlHelpers<BooleanModel>(null, null, null);
            var tag = HtmlTagBuilder.CreateCheckBoxFor(html, m => m.BooleanPropWithDefValueTrue, null);
            Assert.IsTrue(tag.Attribute("checked").IsWithNoValue);
        }

        [TestMethod]
        public void CheckboxForBoolPropertyWithDefValueTrueAsStringIsChecked()
        {
            var html = new HtmlHelpers<BooleanModel>(null, null, null);
            var tag = HtmlTagBuilder.CreateCheckBoxFor(html, m => m.BooleanPropWithDefValueTrueAsString, null);
            Assert.IsTrue(tag.Attribute("checked").IsWithNoValue);
        }

        [TestMethod]
        public void CheckboxForBoolPropertyWithDefValueFalseIsNoChecked()
        {
            var html = new HtmlHelpers<BooleanModel>(null, null, null);
            var tag = HtmlTagBuilder.CreateCheckBoxFor(html, m => m.BooleanPropWithDefValueFalse, null);
            Assert.IsNull(tag.Attribute("checked"));
        }

        [TestMethod]
        public void CheckboxForBoolPropertyWithDefValueFalseAsStringIsNoChecked()
        {
            var html = new HtmlHelpers<BooleanModel>(null, null, null);
            var tag = HtmlTagBuilder.CreateCheckBoxFor(html, m => m.BooleanPropWithDefValueFalseAsString, null);
            Assert.IsNull(tag.Attribute("checked"));
        }

        [TestMethod]
        public void CheckboxForBoolPropertyIsCheckedIfPropertyIsTrue()
        {
            var model = new BooleanModel()
            {
                BooleanPropWithDefValueFalseAsString = true
            };
            var html = new HtmlHelpers<BooleanModel>(null, null, model);
            var tag = HtmlTagBuilder.CreateCheckBoxFor(html, m => m.BooleanPropWithDefValueFalseAsString, null);
            Assert.IsTrue(tag.Attribute("checked").IsWithNoValue);
        }


    }
}
