using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.ViewEngines.Razor;

namespace Nancy.Razor.Helpers.Tests.Tag
{

    class TestModel
    {
        public int Number { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        public int TextNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string Uri { get; set; }

        public string Text { get; set; }

        public bool IsAnything { get; set; }

    }

    [TestClass]
    public class HtmlExtensionsInputElementForTests
    {
        [TestMethod]
        public void InputForPhoneNumberGeneratesInputTypeTel()
        {
            var helper = new HtmlHelpers<TestModel>(null, null, null);
            var html = helper.InputElementFor(m => m.Phone);
            Assert.IsTrue(html.ToHtmlString().Contains("type=\"tel\""));
        }

        [TestMethod]
        public void InputForTextGeneratesInputTypeText()
        {
            var helper = new HtmlHelpers<TestModel>(null, null, null);
            var html = helper.InputElementFor(m => m.TextNumber);
            Assert.IsTrue(html.ToHtmlString().Contains("type=\"text\""));
        }   

        [TestMethod]
        public void InputForPasswordGeneratesInputTypePassword()
        {
            var helper = new HtmlHelpers<TestModel>(null, null, null);
            var html = helper.InputElementFor(m => m.Password);
            Assert.IsTrue(html.ToHtmlString().Contains("type=\"password\""));
        }

        [TestMethod]
        public void InputForEmailAddressGeneratesInputTypeUrl()
        {
            var helper = new HtmlHelpers<TestModel>(null, null, null);
            var html = helper.InputElementFor(m => m.Uri);
            Assert.IsTrue(html.ToHtmlString().Contains("type=\"url\""));
        }

        [TestMethod]
        public void InputForUrlGeneratesInputTypeEmail()
        {
            var helper = new HtmlHelpers<TestModel>(null, null, null);
            var html = helper.InputElementFor(m => m.Email);
            Assert.IsTrue(html.ToHtmlString().Contains("type=\"email\""));
        }

        [TestMethod]
        public void InputForNumericPropertyGeneratesInputTypeNumberIfNotDataType()
        {
            var helper = new HtmlHelpers<TestModel>(null, null, null);
            var html = helper.InputElementFor(m => m.Number);
            Assert.IsTrue(html.ToHtmlString().Contains("type=\"number\""));
        }

        [TestMethod]
        public void InputForStringPropertyGeneratesInputTypeTextIfNotDataType()
        {
            var helper = new HtmlHelpers<TestModel>(null, null, null);
            var html = helper.InputElementFor(m => m.Text);
            Assert.IsTrue(html.ToHtmlString().Contains("type=\"text\""));
        }

        [TestMethod]
        public void InputForBoolPropertyGeneratesInputTypeCheckboxIfNotDataType()
        {
            var helper = new HtmlHelpers<TestModel>(null, null, null);
            var html = helper.InputElementFor(m => m.IsAnything);
            Assert.IsTrue(html.ToHtmlString().Contains("type=\"checkbox\""));
        }


    }
}
