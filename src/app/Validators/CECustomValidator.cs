using System.Web.UI.WebControls;
using Codentia.Common.Helper;

namespace Codentia.Common.WebControls.Validators
{
    /// <summary>
    /// CECustomValidator for creating a CustomValidator control with a standard constructor
    /// </summary>
    public class CECustomValidator : CustomValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CECustomValidator"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="controlToValidate">The control to validate.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="validationGroup">The validation group.</param>
        /// <param name="validateEmptyText">if set to <c>true</c> [validate empty text].</param>
        /// <param name="serverValidateEventHandler">The server validate event handler.</param>
        public CECustomValidator(string id, string controlToValidate, string errorMessage, string validationGroup, bool validateEmptyText, ServerValidateEventHandler serverValidateEventHandler)
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
            this.ValidateEmptyText = validateEmptyText;
            this.ServerValidate += serverValidateEventHandler;
            this.EnableClientScript = false;
        }
    }
}