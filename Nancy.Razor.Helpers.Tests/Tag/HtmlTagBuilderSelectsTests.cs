using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Razor.Helpers.Tag;
using Nancy.Razor.Helpers.Tests.TestModels;

namespace Nancy.Razor.Helpers.Tests.Tag
{
    [TestClass]
    public class HtmlTagBuilderSelectsTests
    {
        [TestMethod]
        public void CreateSelectForDoesNotSelectAnythingByDefault()
        {
            var model = new SelectModelTest();
            var items = SelectList.CreateFrom(model.Countries, cm => cm.Id, cm => cm.Name);
            var tag = HtmlTagBuilder.CreateSelectFor(model, m => m.SelectedCountryId, items);
            Assert.IsFalse(tag.Childs.Any(child => child.Attribute("selected") != null));
        }

        [TestMethod]
        public void CreateSelectForSelectsItemBasedOnModelValue()
        {
            var model = new SelectModelTest();
            model.SelectedCountryId = 1;
            var items = SelectList.CreateFrom(model.Countries, cm => cm.Id, cm => cm.Name);
            var tag = HtmlTagBuilder.CreateSelectFor(model, m => m.SelectedCountryId, items);
            var child = tag.Childs.Single(c => c.Attribute("selected") != null);
            Assert.AreEqual(model.SelectedCountryId.ToString(), child.Attribute("value").Value);
        }

        [TestMethod]
        public void CreateSelectForSelectsItemBasedOnSelectList()
        {
            var model = new SelectModelTest();
            var items = SelectList.CreateFrom(model.Countries, cm => cm.Id, cm => cm.Name);
            items.Last().Selected = true;
            var tag = HtmlTagBuilder.CreateSelectFor(model, m => m.SelectedCountryId, items);
            var child = tag.Childs.Single(c => c.Attribute("selected") != null);
            Assert.AreEqual(items.Last().Value, child.Attribute("value").Value);
        }


        [TestMethod]
        public void CreateSelectForWithNoModelDoesNotSelectAnything()
        {
            var model = new SelectModelTest();
            var items = new SelectList(model.Countries, "Id", "Name");
            var tag = HtmlTagBuilder.CreateSelectFor<SelectModelTest, int>(m => m.SelectedCountryId, items);
            Assert.IsFalse(tag.Childs.Any(child => child.Attribute("selected") != null));
        }


        [TestMethod]
        public void CreateSelectForWithNoModelSelectsItemBasedOnSelectList()
        {
            var model = new SelectModelTest();
            var items = new SelectList(model.Countries, "Id", "Name", 2);
            var tag = HtmlTagBuilder.CreateSelectFor<SelectModelTest, int>(m => m.SelectedCountryId, items);
            Assert.AreEqual(2.ToString(), tag.Childs.Single(child => 
                child.Attribute("selected") != null).Attribute("value").Value);
        }
    }
}
