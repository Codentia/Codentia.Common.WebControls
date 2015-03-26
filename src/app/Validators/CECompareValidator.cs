using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Codentia.Common.Helper;

namespace Codentia.Common.WebControls.Validators
{
    /// <summary>
    /// CECompareValidator for creating a CompareValidator control with a standard constructor
    /// </summary>
    public class CECompareValidator : CompareValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CECompareValidator"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="controlToValidate">The control to validate.</param>
        /// <param name="controlToCompare">The control to compare.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="validationGroup">The validation group.</param>
        /// <param name="validationDataType">Type of the validation data.</param>
        /// <param name="validationCompareOperator">The validation compare operator.</param>
        public CECompareValidator(string id, string controlToValidate, string controlToCompare, string errorMessage, string validationGroup, ValidationDataType validationDataType, ValidationCompareOperator validationCompareOperator)
        {
            ParameterCheckHelper.CheckIsValidString(id, "id", false);
            ParameterCheckHelper.CheckIsValidString(controlToValidate, "controlToValidate", false);
            ParameterCheckHelper.CheckIsValidString(controlToCompare, "controlToCompare", false);

            this.ID = id;
            this.ControlToValidate = controlToValidate;
            this.ControlToCompare = controlToCompare;
            this.ErrorMessage = errorMessage;

            if (!string.IsNullOrEmpty(validationGroup))
            {
                this.ValidationGroup = validationGroup;
            }

            this.Text = "*";
            this.Type = validationDataType;
            this.Operator = validationCompareOperator;
            this.EnableClientScript = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CECompareValidator"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="controlToValidate">The control to validate.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="validationGroup">The validation group.</param>
        /// <param name="validationDataType">Type of the validation data.</param>
        /// <param name="validationCompareOperator">The validation compare operator.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        public CECompareValidator(string id, string controlToValidate, string errorMessage, string validationGroup, ValidationDataType validationDataType, ValidationCompareOperator validationCompareOperator, string valueToCompare)
        {
            ParameterCheckHelper.CheckIsValidString(id, "id", false);
            ParameterCheckHelper.CheckIsValidString(controlToValidate, "controlToValidate", false);
            ParameterCheckHelper.CheckIsValidString(valueToCompare, "valueToCompare", false);

            this.ID = id;
            this.ControlToValidate = controlToValidate;
            this.ErrorMessage = errorMessage;

            if (!string.IsNullOrEmpty(validationGroup))
            {
                this.ValidationGroup = validationGroup;
            }

            this.Text = "*";
            this.Type = validationDataType;
            this.Operator = validationCompareOperator;
            this.ValueToCompare = valueToCompare;
            this.EnableClientScript = false;
        }
    }
}