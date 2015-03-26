using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Codentia.Common.Helper;

namespace Codentia.Common.WebControls.Validators
{
    /// <summary>
    /// CERequiredFieldValidator for creating a RequiredFieldValidator control with a standard constructor
    /// </summary>
    public class CERequiredFieldValidator : RequiredFieldValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CERequiredFieldValidator"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="controlToValidate">The control to validate.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="validationGroup">The validation group.</param>
        public CERequiredFieldValidator(string id, string controlToValidate, string errorMessage, string validationGroup)
        {
            ParameterCheckHelper.CheckIsValidString(id, "id", false);
            ParameterCheckHelper.CheckIsValidString(controlToValidate, "controlToValidate", false);

            this.ID = id;
            this.ControlToValidate = controlToValidate;
            this.ErrorMessage = errorMessage;

            if (!string.IsNullOrEmpty(validationGroup))
            {
                this.ValidationGroup = validationGroup;
            }

            this.Text = "*";
            this.EnableClientScript = false;
        }
    }
}