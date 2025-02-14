using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PoliceProjectMVC.Custome_Helpers
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString GenerateField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string labelText, string type, string @readonly = null, string data_val = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string label = $"<label for='{metadata.PropertyName}'>{labelText}</label>";
            string Value = metadata.Model?.ToString();
            if (metadata.Model is DateTime mydatetime)
            {
                if (type == "date")
                {
                    Value = mydatetime.ToString("yyyy-MM-dd");
                }
                else if (type == "datetime-local")
                {
                    Value = mydatetime.ToString("yyyy-MM-ddThh:mm");
                }
            }
            string input;
            if (!String.IsNullOrEmpty(@readonly))
            {
                input = htmlHelper.TextBoxFor(expression, new { type, @class = "form-control form-control-sm", Value, @readonly }).ToHtmlString();
            }
            else if (!String.IsNullOrEmpty(data_val))
            {
                input = htmlHelper.TextBoxFor(expression, new { type, @class = "form-control form-control-sm", Value, data_val }).ToHtmlString();
            }
            else
            {
                input = htmlHelper.TextBoxFor(expression, new { type, @class = "form-control form-control-sm", Value }).ToHtmlString();
            }
            string validationMessage = htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" })?.ToHtmlString();
            string result = $"<div class='form-group'>{label}{input}{validationMessage}</div>";
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString GenerateDropDown<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string labelText, string oplabel, SelectList myList, string data_val = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string label = $"<label for='{metadata.PropertyName}'>{labelText}</label>";
            string input = "";
            if (!String.IsNullOrEmpty(data_val))
            {
                input = htmlHelper.DropDownListFor(expression, myList, oplabel, new { @class = "form-control select2 form-control-sm", data_val }).ToHtmlString();
            }
            else if (myList == null)
            {
                myList = MyDropdownsValue.EmptySelectList();
                input = htmlHelper.DropDownListFor(expression, myList, oplabel, new { @class = "form-control select2 form-control-sm" }).ToHtmlString();
            }
            else
            {
                input = htmlHelper.DropDownListFor(expression, myList, oplabel, new { @class = "form-control select2 form-control-sm" }).ToHtmlString();
            }
            string validationMessage = htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" })?.ToHtmlString();
            string result = $"<div class='form-group'>{label}{input}{validationMessage}</div>";
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString GenerateFileField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string labelText, string type, string accept, string imgid = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string label = $"<label for='{metadata.PropertyName}'>{labelText}</label>";
            string input;
            if (!String.IsNullOrEmpty(imgid))
            {
                input = htmlHelper.TextBoxFor(expression, new { type, @class = "form-control-file border border-dark p-1", accept, onchange = $"Show(event, '{imgid}')" }).ToHtmlString();
            }
            else
            {
                input = htmlHelper.TextBoxFor(expression, new { type, @class = "form-control-file border border-dark p-1", accept }).ToHtmlString();
            }
            string validationMessage = htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" })?.ToHtmlString();
            string result = $"<div class='form-group'>{label}{input}{validationMessage}</div>";
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString GenerateSimField(this HtmlHelper htmlHelper, string name, string labelText, string type, string value = "", string @readonly = null)
        {
            string label = $"<label for='{name}'>{labelText}</label>";
            string input = htmlHelper.TextBox(name, value, new { type, @class = "form-control form-control-sm" }).ToHtmlString();
            if (!String.IsNullOrEmpty(@readonly))
            {
                input = htmlHelper.TextBox(name, value, new { type, @class = "form-control form-control-sm", @readonly }).ToHtmlString();
            }
            string result = $"<div class='form-group'>{label}{input}</div>";
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString GenerateSimDropDown(this HtmlHelper htmlHelper, string name, string labelText, string oplabel, SelectList myList, bool required = false)
        {
            string label = $"<label for='{name}'>{labelText}</label>";
            string input = htmlHelper.DropDownList(name, myList, oplabel, new { @class = "form-control select2 form-control-sm", @id = name }).ToHtmlString();
            if (required)
            {
                input = input.Replace("<select", $"<select data-val='true' data-val-required='The {labelText} field is required.'");
            }
            string result = $"<div class='form-group'>{label}{input}{htmlHelper.ValidationMessage(name, new { @class = "text-danger" })}</div>";
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString GenerateCheckbox<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, string labelText)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string label = $"<label for='{metadata.PropertyName}'>{labelText}</label>";

            string checkbox;
            var isChecked = metadata.Model != null && (bool)metadata.Model;
            if (isChecked)
            {
                checkbox = htmlHelper.CheckBoxFor(expression, new { @class = "form-check-input", @checked = "" }).ToHtmlString();
            }
            else
            {
                checkbox = htmlHelper.CheckBoxFor(expression, new { @class = "form-check-input" }).ToHtmlString();
            }
            string validationMessage = htmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" })?.ToHtmlString();
            string result = $"<div class='form-check'>{checkbox}{label}{validationMessage}</div>";
            return MvcHtmlString.Create(result);
        }


    }
}