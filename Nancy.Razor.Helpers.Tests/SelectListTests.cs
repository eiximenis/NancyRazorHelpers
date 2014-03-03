using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Razor.Helpers.Tests.TestModels;

namespace Nancy.Razor.Helpers.Tests
{
    [TestClass]
    public class SelectListTests
    {
        [TestMethod]
        public void CreateFromCreatesSameNumberOfElementsThanSource()
        {
            var model = new SelectModelTest();
            var list = SelectList.CreateFrom(model.Countries, cm => cm.Id, cm => cm.Name);
            Assert.AreEqual(model.Countries.Count(), list.Count());
        }

        [TestMethod]
        public void CreateFromDoesNotSelectAnyElementByDefault()
        {
            var model = new SelectModelTest();
            var list = SelectList.CreateFrom(model.Countries, cm => cm.Id, cm => cm.Name);
            Assert.IsFalse(list.Any(item => item.Selected));
        }

        [TestMethod]
        public void CreateFromCreatesElementsUsingValuePropertyCorrecly()
        {
            var model = new SelectModelTest();
            var list = SelectList.CreateFrom(model.Countries, cm => cm.Id, cm => cm.Name);
            Assert.AreEqual(model.Countries.Skip(1).First().Id.ToString(), list.Skip(1).First().Value);
        }

        [TestMethod]
        public void CreateFromCreatesElementsUsingTextPropertyCorrecly()
        {
            var model = new SelectModelTest();
            var list = SelectList.CreateFrom(model.Countries, cm => cm.Id, cm => cm.Name);
            Assert.AreEqual(model.Countries.Skip(1).First().Name, list.Skip(1).First().Text);
        }

        [TestMethod]
        public void CtorCreatesSameNumberOfElementsThanSource()
        {
            var model = new SelectModelTest();
            var list = new SelectList(model.Countries, "Id", "Name");
            Assert.AreEqual(model.Countries.Count(), list.Count());
        }


        [TestMethod]
        public void CtorDoesNotSelectAnyElementByDefault()
        {
            var model = new SelectModelTest();
            var list = new SelectList(model.Countries, "Id", "Name");
            Assert.IsFalse(list.Any(item => item.Selected));
        }

        [TestMethod]
        public void CtorCreatesElementsUsingValuePropertyCorrecly()
        {
            var model = new SelectModelTest();
            var list = new SelectList(model.Countries, "Id", "Name");
            Assert.AreEqual(model.Countries.Skip(1).First().Id.ToString(), list.Skip(1).First().Value);
        }

        [TestMethod]
        public void CtorCreatesElementsUsingTextPropertyCorrecly()
        {
            var model = new SelectModelTest();
            var list = new SelectList(model.Countries, "Id", "Name");
            Assert.AreEqual(model.Countries.Skip(1).First().Name, list.Skip(1).First().Text);
        }

        [TestMethod]
        public void CtorSelectsElementIfSelectedValuePassed()
        {
            var model = new SelectModelTest();
            var list = new SelectList(model.Countries, "Id", "Name", 2);
            Assert.IsTrue(list.Single(item=>item.Selected).Value == 2.ToString());
        }

        [TestMethod]
        public void CtorDoesNotSelectsAnyElementIfSelectedValueIsNotFound()
        {
            var model = new SelectModelTest();
            var list = new SelectList(model.Countries, "Id", "Name", 100);
            Assert.IsFalse(list.Any(item => item.Selected));
        }

    }
}
