using System.Collections.Generic;

namespace Nancy.Razor.Helpers.Tests.TestModels
{
    public class SelectModelTest
    {
        private readonly List<CountryModel> _countries;

        public IEnumerable<CountryModel> Countries
        {
            get { return _countries; }
        }

        public SelectModelTest()
        {
            _countries = new List<CountryModel>
            {
                new CountryModel {Id = 1, Name = "Spain"},
                new CountryModel {Id = 2, Name = "US"},
                new CountryModel {Id = 3, Name = "UK"}
            };
        }

        public int SelectedCountryId { get; set; }
    }
}