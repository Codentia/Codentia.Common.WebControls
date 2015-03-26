using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Codentia.Common.WebControls.Validators;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// Accessible Captcha - asks the user to answer a simple maths question
    /// </summary>
    public class AccessibleCaptcha : CECompositeControl
    {
        private int _firstNumber;
        private int _secondNumber;
        private string _validationGroup;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessibleCaptcha"/> class.
        /// </summary>
        public AccessibleCaptcha()
        {
            Random r = new Random();

            _firstNumber = r.Next(10, 20);
            _secondNumber = r.Next(1, 9);
        }

        /// <summary>
        /// Sets the validation group.
        /// </summary>
        /// <value>The validation group.</value>
        public string ValidationGroup
        {
            set
            {
                if (_validationGroup != value)
                {
                    _validationGroup = value;
                    this.CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Converts the numbers to words.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string - words</returns>
        public static string ConvertNumbersToWords(int value)
        {
            string[] ones = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            if (value > 0 && value < 10)
            {
                return ones[value - 1];
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            base.CreateChildControls();

            Label label = new Label();
            label.ID = "label";
            label.Text = string.Format("What is {0} - {1} (in digits)?", _firstNumber, AccessibleCaptcha.ConvertNumbersToWords(_secondNumber));
            this.Controls.Add(label);

            TextBox textbox = new TextBox();
            textbox.ID = "answer";
            label.AssociatedControlID = textbox.ID;
            this.Controls.Add(textbox);

            CECustomValidator captchaValidator = new CECustomValidator("validator", textbox.ID, "*", _validationGroup, true, new ServerValidateEventHandler(CaptchaValidator_Validate));
            captchaValidator.ErrorMessage = "Please check your maths (digits only, remember!)";
            captchaValidator.IsValid = false;
            this.Controls.Add(captchaValidator);
        }
        
        /// <summary>
        /// Handles the Validate event of the CaptchaValidator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void CaptchaValidator_Validate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = false;

            if (!string.IsNullOrEmpty(e.Value))
            {
                int value = 0;

                if (int.TryParse(this.GetChildControlValue<string>(typeof(TextBox), "answer"), out value))
                {
                    if (value == (_firstNumber - _secondNumber))
                    {
                        e.IsValid = true;
                    }
                }
            }
        }

        /// <summary>
        /// Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState"/> method was invoked.
        /// </summary>
        /// <returns>
        /// An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, null.
        /// </returns>
        protected override object SaveViewState()
        {
            return (object)(new int[] { _firstNumber, _secondNumber });
        }

        /// <summary>
        /// Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState"/> method.
        /// </summary>
        /// <param name="savedState">An object that represents the control state to restore.</param>
        protected override void LoadViewState(object savedState)
        {
            int[] numbers = (int[])savedState;

            if (numbers != null)
            {
                _firstNumber = numbers[0];
                _secondNumber = numbers[1];

                CreateChildControls();
            }
        }
    }
}