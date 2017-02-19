using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Web.Mvc;

namespace BaseAspNetMvc.Structure.Utilities
{
    public class BooleanRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (value.GetType() != typeof(bool))
                throw new InvalidOperationException("can only be used on boolean properties.");

            return (bool)value == true;
        }

        public override string FormatErrorMessage(string name)
        {
            var errorMessage = "The " + name + " field must be checked in order to continue.";
            if (String.IsNullOrWhiteSpace(ErrorMessage))
            {
                // Check if they supplied an error message resource
                if (ErrorMessageResourceType != null && !String.IsNullOrWhiteSpace(ErrorMessageResourceName))
                {
                    var resMan = new ResourceManager(ErrorMessageResourceType.FullName, ErrorMessageResourceType.Assembly);
                    errorMessage = resMan.GetString(ErrorMessageResourceName);
                }
            }
            else
            {
                errorMessage = ErrorMessage;
            }
            return errorMessage;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var errorMessage = string.Empty;
            if (String.IsNullOrWhiteSpace(ErrorMessage))
            {
                // Check if they supplied an error message resource
                if (ErrorMessageResourceType != null && !String.IsNullOrWhiteSpace(ErrorMessageResourceName))
                {
                    var resMan = new ResourceManager(ErrorMessageResourceType.FullName, ErrorMessageResourceType.Assembly);
                    errorMessage = resMan.GetString(ErrorMessageResourceName);
                }
            }
            else
            {
                errorMessage = ErrorMessage;
            }
            yield return new ModelClientValidationRule
            {
                ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
                ValidationType = "booleanrequired"
            };
        }
    }
}
