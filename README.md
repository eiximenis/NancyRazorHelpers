ASP.NET MVC-Like Razor Helpers for NancyFx
=========
The goal of the project is provide a implementation for some of the Razor Helpers that come in ASP.NET MVC for use in NancyFx (with Razor view engine).  
Not all helpers will be implemented and not of them work exactly as their asp.net mvc counterparts do.

Current list of helpers is:

- Html.TextBoxFor: Creates a input type="text" for a property
- Html.PasswordFor: Creates a input type="password" for a property
- Html.HiddenFor: Creates a input type="hidden" for a property
- Html.CheckBoxFor: Creates a input type="checkbox" for a property
- Html.InputElementFor: Creates a input for a property. Type is inferred. If model property is decorated with [DataType] this is honoured. Otherwise type is inferred from property type.
- Html.LabelFor: Creates a label for a property
- Html.DropDownListFor: Creates a select for a property. The property stores the value of the selected item. Source for the list is passed using a parameter.
- Html.ValidationMessageFor: Creates a validation message for a property that has a binding error
- Html.ValidationSummaryFor: Creates a set of validation messages for all the properties of the model that have validation errors.

This is just a preliminary version. Use at your own risks. Comments, code reviews and pull requests are welcome.

Thanks.
Edu - http://twitter.com/eiximenis
