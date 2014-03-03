using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nancy.Razor.Helpers
{
    public class SelectListItem
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public bool Selected { get; set; }
    }
}
