using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Nancy.Razor.Helpers.Tag
{
    public class HtmlTag
    {
        private readonly HtmlTag _parent;
        private readonly string _name;
        private readonly IDictionary<string, HtmlAttribute> _attributes;
        private readonly List<HtmlTag> _childs;
        public bool AutoClose { get; set; }
        public string RawContent { get; set; }

        public HtmlAttribute Attribute(string name)
        {
            var attrName = name.ToLowerInvariant();
            HtmlAttribute attr;
            _attributes.TryGetValue(attrName, out attr);
            return attr;
        }


        public HtmlTag(string name)
        {
            _name = name;
            _attributes = new Dictionary<string, HtmlAttribute>();
            _childs = new List<HtmlTag>();
            AutoClose = true;
        }

        private HtmlTag(string name, HtmlTag parent)
            : this(name)
        {
            _parent = parent;
        }

        public bool IsRoot
        {
            get { return _parent == null; }
        }

        public IEnumerable<HtmlTag> Childs { get { return _childs; } }

        public HtmlTag WithAttributes(object attributes)
        {
            if (attributes == null) return this;
            var properties = attributes.GetType().GetProperties();

            foreach (var property in properties.Where(p => p.CanRead))
            {
                WithNonEmptyAttribute(property.Name, property.GetValue(attributes));
            }

            return this;
        }

        public HtmlTag WithChild(string name)
        {
            var child = new HtmlTag(name, this);
            _childs.Add(child);
            return child;
        }

        public HtmlTag ToRoot()
        {
            var currentTag = this;
            while (currentTag._parent != null)
            {
                currentTag = currentTag._parent;
            }
            return currentTag;
        }

        public HtmlTag ToParent()
        {
            return this._parent;
        }

        public HtmlTag ToParentIfAny()
        {
            return IsRoot ? this : this._parent;
        }

        public HtmlTag WithNonEmptyAttribute(string name, object value)
        {
            if (value != null && !HaveAttribute(name))
            {
                var attribute = new HtmlAttribute(name, this);
                attribute.WithValue(value);
                _attributes.Add(attribute.Name, attribute);
            }

            return this;
        }

        public HtmlTag WithEmptyAttribute(string name)
        {
            if (!HaveAttribute(name))
            {
                var attr = new HtmlAttribute(name, this);
                attr.WithNovalue();
                _attributes.Add(attr.Name, attr);
            }

            return this;
        }

        public HtmlTag WithEmptyAttributeIf(string name, bool add)
        {
            return add ? WithEmptyAttribute(name) : this;
        }

        private bool HaveAttribute(string name)
        {
            return _attributes.ContainsKey(name.ToLowerInvariant());
        }

        public HtmlTag RemoveAttribute(string name)
        {
            if (HaveAttribute(name))
            {
                _attributes.Remove(name.ToLowerInvariant());
            }

            return this;
        }

        public HtmlTag WithAttribute(string name, object value)
        {
            if (!HaveAttribute(name))
            {
                var attribute = new HtmlAttribute(name, this);
                if (value == null)
                {
                    attribute.WithNovalue();
                }
                else
                {
                    attribute.WithValue(value);
                    _attributes.Add(attribute.Name, attribute);
                }
            }
            return this;
        }

        public override string ToString()
        {
            var renderAutoClose = AutoClose && !_childs.Any() && string.IsNullOrEmpty(RawContent);
            var sb = new StringBuilder();
            sb.Append('<');
            sb.Append(_name.ToLowerInvariant());
            sb.Append(' ');
            foreach (var attribute in _attributes.Values)
            {
                var attrstring = attribute.ToString();
                sb.Append(attrstring);
                if (!string.IsNullOrEmpty(attrstring))
                {
                    sb.Append(' ');
                }
            }

            sb.Append(renderAutoClose ? "/>" : ">");

            if (_childs.Any())
            {
                foreach (var child in _childs)
                {
                    sb.Append(child.ToString());
                }
            }
            else if (!string.IsNullOrEmpty(RawContent))
            {
                sb.Append(RawContent);
            }

            if (!renderAutoClose)
            {
                sb.Append("</");
                sb.Append(_name.ToLowerInvariant());
                sb.Append('>');
            }

            sb.AppendLine();
            return sb.ToString();
        }
    }
}
