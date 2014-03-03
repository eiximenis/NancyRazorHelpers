using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Nancy.Razor.Helpers.Extensions;

namespace Nancy.Razor.Helpers
{
    public class SelectList : IEnumerable<SelectListItem>
    {
        private readonly List<SelectListItem> _items;

        public SelectList(IEnumerable items, string value, string text)
            : this(items, value, text, null) { }

        public SelectList(IEnumerable items, string value, string text, object selectedValue)
            : this()
        {
            foreach (var item in items)
            {
                var valueProperty = item.GetType().GetProperty(value);
                var textProperty = item.GetType().GetProperty(text);
                var listItem = AddToSelectList(this, item, valueProperty, textProperty);
                var valueAsObj = valueProperty.GetValue(item);
                if (selectedValue != null)
                {
                    if (selectedValue.Equals(valueAsObj))
                    {
                        listItem.Selected = true;
                    }
                }
            }
        }

        private SelectList()
        {
            _items = new List<SelectListItem>();
        }


        public static SelectList CreateFrom<TS, TV, TT>(IEnumerable<TS> items, Expression<Func<TS, TV>> value,
            Expression<Func<TS, TT>> text)
        {
            var valueProperty = value.AsPropertyInfo();
            var textProperty = text.AsPropertyInfo();
            var selectList = new SelectList();

            foreach (var item in items)
            {
                AddToSelectList(selectList, item, valueProperty, textProperty);
            }

            return selectList;
        }

        private static SelectListItem AddToSelectList(SelectList @this, object item, PropertyInfo valueProperty, PropertyInfo textProperty)
        {
            var listItem = new SelectListItem
            {
                Value = valueProperty != null ? valueProperty.GetValueAsString(item) : string.Empty,
                Text = textProperty != null ? textProperty.GetValueAsString(item) : string.Empty
            };
            @this._items.Add(listItem);
            return listItem;
        }

        public IEnumerator<SelectListItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}