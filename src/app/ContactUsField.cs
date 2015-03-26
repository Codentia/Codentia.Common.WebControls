using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Codentia.Common.Helper;
using Codentia.Common.Logging;
using Codentia.Common.Logging.BL;
using Codentia.Common.Net;
using Codentia.Common.WebControls.HtmlElements;
using Codentia.Common.WebControls.Validators;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// ContactUsField class
    /// </summary>
    public class ContactUsField
    {
        private ContactUsFieldType _type = ContactUsFieldType.TextBox;
        private string _label;
        private string _emailName;
        private bool _readOnly = false;
        private string _defaultValue = null;
        private string _cssClass = null;
        private ContactUsValidationType _validationType = ContactUsValidationType.None;
        private ContactUsEmailType _emailType = ContactUsEmailType.Custom;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsField"/> class.
        /// </summary>
        public ContactUsField()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsField"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="label">The label.</param>
        /// <param name="emailAs">The email as.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="validationType">Type of the validation.</param>
        /// <param name="emailAsType">Type of the email as.</param>
        public ContactUsField(ContactUsFieldType type, string label, string emailAs, bool readOnly, string defaultValue, ContactUsValidationType validationType, ContactUsEmailType emailAsType)
        {
            _type = type;
            _label = label;
            _emailName = emailAs;
            _readOnly = readOnly;
            _defaultValue = defaultValue;
            _validationType = validationType;
            _emailType = emailAsType;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ContactUsFieldType Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                _label = value;
            }
        }

        /// <summary>
        /// Gets or sets the email as.
        /// </summary>
        /// <value>
        /// The email as.
        /// </value>
        public string EmailAs
        {
            get
            {
                return _emailName;
            }

            set
            {
                _emailName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }

            set
            {
                _readOnly = value;
            }
        }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public string DefaultValue
        {
            get
            {
                return _defaultValue;
            }

            set
            {
                _defaultValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass
        {
            get
            {
                return _cssClass;
            }

            set
            {
                _cssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the validation.
        /// </summary>
        /// <value>
        /// The type of the validation.
        /// </value>
        public ContactUsValidationType ValidationType
        {
            get
            {
                return _validationType;
            }

            set
            {
                _validationType = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the email.
        /// </summary>
        /// <value>
        /// The type of the email.
        /// </value>
        public ContactUsEmailType EmailType
        {
            get
            {
                return _emailType;
            }

            set
            {
                _emailType = value;
            }
        }
    }
}