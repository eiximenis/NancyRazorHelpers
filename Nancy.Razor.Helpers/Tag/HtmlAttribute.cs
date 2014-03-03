using System;
using System.Text;

namespace Nancy.Razor.Helpers.Tag
{
    public class HtmlAttribute
    {
        private readonly string _name;
        private readonly HtmlTag _owner;
        private string _value;


        public bool IsWithNoValue { get { return _value == null; } }

        public string Value
        {
            get { return _value; }
        }

        internal HtmlAttribute(string name, HtmlTag owner)
        {
            _name = SafeAttributeName(name);
            _owner = owner;
            _value = string.Empty;
        }

        private string SafeAttributeName(string name)
        {
            var sb = new StringBuilder(name);
            sb.Replace('_', '-');
            return sb.ToString().ToLowerInvariant();
        }

        public string Name { get { return _name; } }

        public HtmlTag WithValue(object value)
        {
            _value = value != null ? (string)Convert.ChangeType(value, typeof(string)) : string.Empty;
            return _owner;
        }

        public HtmlTag WithNovalue()
        {
            _value = null;
            return _owner;
        }

        public override string ToString()
        {
            if (_value == null) return _name;

            return string.IsNullOrEmpty(_value) ?
                string.Empty :
                string.Format("{0}=\"{1}\"", _name, _value);
        }
    }
}