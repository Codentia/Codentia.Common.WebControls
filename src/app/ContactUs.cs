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
using Codentia.Common.Net.BotPlug;
using Codentia.Common.WebControls.HtmlElements;
using Codentia.Common.WebControls.Validators;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// ContactUsEmailType enumerated type
    /// </summary>
    public enum ContactUsEmailType
    {
        /// <summary>
        /// Name value
        /// </summary>
        Name,

        /// <summary>
        /// Email value
        /// </summary>
        Email,

        /// <summary>
        /// Subject value
        /// </summary>
        Subject,

        /// <summary>
        /// Message value
        /// </summary>
        Message,

        /// <summary>
        /// Custom value
        /// </summary>
        Custom
    }

    /// <summary>
    /// ContactUsFieldType enum
    /// </summary>
    public enum ContactUsFieldType
    {
        /// <summary>
        /// Hidden value
        /// </summary>
        Hidden,

        /// <summary>
        /// TextBox value
        /// </summary>
        TextBox,

        /// <summary>
        /// MultiLineTextBox value
        /// </summary>
        MultiLineTextBox,

        /// <summary>
        /// DateTime value
        /// </summary>
        DateTime,

        /// <summary>
        /// CheckBox value
        /// </summary>
        CheckBox
    }

    /// <summary>
    /// ContactUsValidationType enum
    /// </summary>
    public enum ContactUsValidationType
    {
        /// <summary>
        /// None value
        /// </summary>
        None,

        /// <summary>
        /// Required value
        /// </summary>
        Required,

        /// <summary>
        /// EmailAddress value
        /// </summary>
        EmailAddress,

        /// <summary>
        /// PhoneNumber value
        /// </summary>
        PhoneNumber
    }

    /// <summary>
    /// Basic ContactUs area encapsulated in a single control
    /// </summary>
    [ToolboxData("<{0}:ContactUs runat=server></{0}:ContactUs>")]
    public class ContactUs : CECompositeControl
    {
        private ContactUsCollection _items = new ContactUsCollection();

        private ITemplate _footerTemplate;

        private string _defaultSubject = string.Empty;
        private string _toEmailAddress = ConfigurationManager.AppSettings["SystemTargetEmailAddress"];
        private string _fromEmailAddress = ConfigurationManager.AppSettings["SystemSourceEmailAddress"];
        private bool _allowEnterSubject = true;
        private bool _sendUserReceipt = false;

        /// <summary>
        /// ContactUsEventHandler event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Codentia.Common.WebControls.ContactUsEventArgs"/> instance containing the event data.</param>
        public delegate void ContactUsEventHandler(ContactUs sender, ContactUsEventArgs e);

        /// <summary>
        /// Occurs when [contact message sent].
        /// </summary>
        public event ContactUsEventHandler ContactMessageSent;

        /// <summary>
        /// Sets the email to.
        /// </summary>
        /// <value>
        /// The email to.
        /// </value>
        public string EmailTo
        {
            set
            {
                _toEmailAddress = value;
            }
        }

        /// <summary>
        /// Sets the email from.
        /// </summary>
        /// <value>
        /// The email from.
        /// </value>
        public string EmailFrom
        {
            set
            {
                _fromEmailAddress = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether [send user receipt].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [send user receipt]; otherwise, <c>false</c>.
        /// </value>
        public bool SendUserReceipt
        {
            set
            {
                _sendUserReceipt = value;
            }
        }

        /// <summary>
        /// Gets or sets the contact fields.
        /// </summary>
        /// <value>
        /// The contact fields.
        /// </value>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ContactUsCollection ContactFields
        {
            get
            {
                return _items;
            }

            set
            {
                _items = value;
            }
        }

        /// <summary>
        /// Gets or sets the footer template.
        /// </summary>
        /// <value>
        /// The footer template.
        /// </value>
        [TemplateContainer(typeof(RegionTemplate))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public virtual ITemplate FooterTemplate
        {
            get
            {
                return _footerTemplate;
            }

            set
            {
                _footerTemplate = value;
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        public virtual void SendEmail(string name, string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(_fromEmailAddress))
            {
                _fromEmailAddress = "noreply@mattchedit.com";
            }

            if (string.IsNullOrEmpty(_toEmailAddress))
            {
                _toEmailAddress = "noreply@mattchedit.com";
            }

            try
            {
                EmailManager.GetInstance(HttpContext.Current).SendEmail(_fromEmailAddress, string.IsNullOrEmpty(email) ? _fromEmailAddress : email, _toEmailAddress, string.Format("Customer Contact: {0}", subject), string.Format("From: {0} - {1}\r\nSubject: {2}\r\n\r\n{3}", name, email, subject, message), false);
            }
            catch (Exception ex)
            {
                LogManager.Instance.AddToLog(LogMessageType.Information, this.ToString(), string.Format("Failed message: {0} {1} {2} {3}", name, email, subject, message));
                LogManager.Instance.AddToLog(ex, this.ToString());
            }

            // send user receipt if required
            if (_sendUserReceipt)
            {
                try
                {
                    EmailManager.GetInstance(HttpContext.Current).SendEmail(_fromEmailAddress, email, _toEmailAddress, string.Format("Your message to {0}: {1}", _toEmailAddress, subject), string.Format("From: {0} - {1}\r\nSubject: {2}\r\n\r\n{3}", name, email, subject, message), false);
                }
                catch (Exception ex)
                {
                    LogManager.Instance.AddToLog(LogMessageType.Information, this.ToString(), string.Format("Failed message receipt: {0} {1} {2} {3}", name, email, subject, message));
                    LogManager.Instance.AddToLog(ex, this.ToString());
                }
            }
        }

        /// <summary>
        /// Validate Email Addresses entered within this control
        /// </summary>
        /// <param name="source">Validation Source</param>
        /// <param name="args">The Arguments</param>
        protected void emailValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string value = this.MakeInputStringSafe(args.Value);

            args.IsValid = false;

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    ParameterCheckHelper.CheckIsValidEmailAddress(value, "emailAddress");
                    args.IsValid = true;
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Handles the ServerValidate event of the phoneValidator control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void phoneValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string value = this.MakeInputStringSafe(args.Value);

            args.IsValid = false;

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    value = value.Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
                    ParameterCheckHelper.CheckStringIsValidPhoneNumber(value, "phoneNumber");
                    args.IsValid = true;
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the sendButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void sendButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                TextBox botDetector = (TextBox)this.FindChildControl("botDetector");
                if (!string.IsNullOrEmpty(botDetector.Text))
                {
                    LogManager.Instance.AddToLog(LogMessageType.Information, "Codentia.Common.WebControls.ContactUs", string.Format("botDetector={0}, not sending message - blocking request", botDetector.Text));
                    BotPlugManager.BlockCurrentRequest();
                    return;
                }

                string email = string.Empty;
                string subject = "Not Specified";
                string name = "Not Specified";
                string message = "Not Specified";

                List<string> custom = new List<string>();
                Dictionary<string, string> argsDictionary = new Dictionary<string, string>();

                // process items
                for (int i = 0; i < _items.Count; i++)
                {
                    string value = null;

                    if (string.IsNullOrEmpty(_items[i].DefaultValue))
                    {
                        switch (_items[i].Type)
                        {
                            case ContactUsFieldType.Hidden:
                            case ContactUsFieldType.MultiLineTextBox:
                            case ContactUsFieldType.TextBox:
                                value = this.GetChildControlValue<string>(typeof(TextBox), _items[i].EmailAs);
                                break;
                            case ContactUsFieldType.CheckBox:
                                value = Convert.ToString(this.GetChildControlValue<bool>(typeof(CheckBox), _items[i].EmailAs));

                                if (value == true.ToString())
                                {
                                    value = "y";
                                }
                                else
                                {
                                    value = "n";
                                }

                                break;
                            default:
                                throw new System.NotImplementedException();
                        }
                    }
                    else
                    {
                        value = _items[i].DefaultValue;
                    }

                    switch (_items[i].EmailType)
                    {
                        case ContactUsEmailType.Name:
                            name = value;
                            break;
                        case ContactUsEmailType.Email:
                            email = value;
                            break;
                        case ContactUsEmailType.Subject:
                            subject = value;
                            break;
                        case ContactUsEmailType.Message:
                            message = value;
                            break;
                        default:
                            custom.Add(string.Format("{0} = {1}", _items[i].EmailAs, value));
                            break;
                    }

                    argsDictionary.Add(_items[i].EmailAs, value);
                }

                if (custom.Count > 0)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.Append(message);

                    for (int i = 0; i < custom.Count; i++)
                    {
                        messageBuilder.AppendFormat("\r\n\r\n{0}", custom[i]);
                    }

                    message = messageBuilder.ToString();
                }

                this.SendEmail(name, email, subject, message);

                if (ContactMessageSent != null)
                {
                    ContactUsEventArgs args = new ContactUsEventArgs();
                    args.Values = argsDictionary;

                    ContactMessageSent(this, args);
                }

                this.FindChildControl("thanksPanel").Visible = true;
                this.FindChildControl("controlPanel").Visible = false;
            }
        }

        /// <summary>
        /// Create child controls
        /// </summary>
        protected override void CreateChildControls()
        {
            Panel controlPanel = new Panel();
            controlPanel.ID = "controlPanel";
            controlPanel.CssClass = "ContactUsControls";

            // default configuration
            if (_items.Count == 0)
            {
                _items.Add(new ContactUsField(ContactUsFieldType.TextBox, "Your Name:", "name", false, null, ContactUsValidationType.Required, ContactUsEmailType.Name));
                _items.Add(new ContactUsField(ContactUsFieldType.TextBox, "Your E-Mail:", "email", false, null, ContactUsValidationType.EmailAddress, ContactUsEmailType.Email));

                if (_allowEnterSubject)
                {
                    _items.Add(new ContactUsField(ContactUsFieldType.TextBox, "Subject:", "subject", false, null, ContactUsValidationType.Required, ContactUsEmailType.Subject));
                }
                else
                {
                    _items.Add(new ContactUsField(ContactUsFieldType.TextBox, "Subject:", "subject", true, _defaultSubject, ContactUsValidationType.None, ContactUsEmailType.Subject));
                }

                _items.Add(new ContactUsField(ContactUsFieldType.MultiLineTextBox, "Message:", "message", false, null, ContactUsValidationType.Required, ContactUsEmailType.Message));
            }

            // ensure items are valid (email address required)
            bool itemsAreValid = false;

            for (int i = 0; i < _items.Count && !itemsAreValid; i++)
            {
                if (_items[i].EmailType == ContactUsEmailType.Email)
                {
                    itemsAreValid = true;
                }
            }

            if (!itemsAreValid)
            {
                throw new Exception("At least one ContactUs Field must relate to email address");
            }

            // build display controls
            LabelledControlList contactList = new LabelledControlList(_items.Count);

            for (int i = 0; i < _items.Count; i++)
            {
                Control entryControl = null;
                switch (_items[i].Type)
                {
                    case ContactUsFieldType.Hidden:
                        continue;
                    case ContactUsFieldType.TextBox:
                        TextBox textField = new TextBox();
                        textField.TextMode = TextBoxMode.SingleLine;
                        textField.ReadOnly = _items[i].ReadOnly;
                        textField.Text = _items[i].DefaultValue;
                        entryControl = textField;
                        break;
                    case ContactUsFieldType.MultiLineTextBox:
                        textField = new TextBox();
                        textField.TextMode = TextBoxMode.MultiLine;
                        textField.ReadOnly = _items[i].ReadOnly;
                        textField.Text = _items[i].DefaultValue;
                        entryControl = textField;
                        break;

                    /*
                    case ContactUsFieldType.DateTime:
                        CEC*lendar calendar = new CEC*lendar();
                        calendar.EnteredDate = (string.IsNullOrEmpty(_items[i].DefaultValue)?DateTime.Now.Date:Convert.ToDateTime(_items[i].DefaultValue));

                        // read only not supported by mitc*lendar
                        if (_items[i].ReadOnly)
                        {
                            throw new System.NotImplementedException();
                        }

                        entryControl = calendar;
                        break;
                     */
                    case ContactUsFieldType.CheckBox:
                        CheckBox checkbox = new CheckBox();
                        checkbox.Checked = Convert.ToBoolean(_items[i].DefaultValue);
                        entryControl = (Control)checkbox;
                        break;
                }

                entryControl.ID = _items[i].EmailAs;
                ((WebControl)entryControl).CssClass = _items[i].CssClass;

                IValidator validator = null;

                switch (_items[i].ValidationType)
                {
                    case ContactUsValidationType.None:
                        break;
                    case ContactUsValidationType.Required:
                        RequiredFieldValidator requiredValidator = new RequiredFieldValidator();
                        requiredValidator.ControlToValidate = entryControl.ID;
                        requiredValidator.ErrorMessage = string.Format("You must provide '{0}'", _items[i].Label);
                        requiredValidator.Text = "*";
                        requiredValidator.Display = ValidatorDisplay.Dynamic;
                        requiredValidator.ValidationGroup = this.ID;
                        requiredValidator.IsValid = false;
                        requiredValidator.EnableClientScript = false;

                        validator = (IValidator)requiredValidator;
                        break;
                    case ContactUsValidationType.EmailAddress:
                        CustomValidator emailValidator = new CustomValidator();
                        emailValidator.ControlToValidate = entryControl.ID;
                        emailValidator.ErrorMessage = "Please enter a valid email address.";
                        emailValidator.Text = "*";
                        emailValidator.Display = ValidatorDisplay.Dynamic;
                        emailValidator.ValidationGroup = this.ID;
                        emailValidator.ValidateEmptyText = true;
                        emailValidator.IsValid = false;
                        emailValidator.EnableClientScript = false;

                        emailValidator.ServerValidate += new ServerValidateEventHandler(emailValidator_ServerValidate);

                        validator = (IValidator)emailValidator;
                        break;
                    case ContactUsValidationType.PhoneNumber:
                        CustomValidator phoneValidator = new CustomValidator();
                        phoneValidator.ControlToValidate = entryControl.ID;
                        phoneValidator.ErrorMessage = "Please enter a valid phone number (without spaces or brackets).";
                        phoneValidator.Text = "*";
                        phoneValidator.Display = ValidatorDisplay.Dynamic;
                        phoneValidator.ValidationGroup = this.ID;
                        phoneValidator.IsValid = false;
                        phoneValidator.EnableClientScript = false;
                        phoneValidator.ServerValidate += new ServerValidateEventHandler(phoneValidator_ServerValidate);

                        validator = (IValidator)phoneValidator;
                        break;
                }

                contactList.Items.Add(new LabelledControlListItem(_items[i].Label, entryControl, validator));
            }

            // buttons (clear, send, etc)
            P buttonContainer = new P();
            if (_footerTemplate != null)
            {
                RegionTemplate f = new RegionTemplate();
                f.ID = "footerTemplate";
                _footerTemplate.InstantiateIn(f);
                buttonContainer.Controls.Add(f);
            }
            else
            {
                IdempotentLinkButton sendButton = new IdempotentLinkButton();
                sendButton.ID = "sendButton";
                sendButton.Text = "Send";
                sendButton.Click += new EventHandler(sendButton_Click);
                sendButton.CausesValidation = true;
                sendButton.ValidationGroup = this.ID;
                buttonContainer.Controls.Add(sendButton);
            }

            // bot-detector
            TextBox botDetector = new TextBox();
            botDetector.ID = "botDetector";
            botDetector.Text = string.Empty;
            botDetector.Style.Add(HtmlTextWriterStyle.Display, "none");
            botDetector.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");

            // normal controls
            controlPanel.Controls.Add(contactList);
            controlPanel.Controls.Add(botDetector);
            controlPanel.Controls.Add(buttonContainer);
            this.Controls.Add(controlPanel);

            Panel validationPanel = new Panel();
            validationPanel.CssClass = "ContactUsValidation";
            CEValidationSummary validationSummary = new CEValidationSummary();
            validationSummary.ValidationGroup = this.ID;
            validationSummary.DisplayMode = ValidationSummaryDisplayMode.List;
            validationPanel.Controls.Add(validationSummary);
            this.Controls.Add(validationPanel);

            Panel thanksPanel = new Panel();
            thanksPanel.ID = "thanksPanel";
            thanksPanel.CssClass = "ContactUsThanks";
            thanksPanel.Visible = false;

            P thanksLabel = new P();
            thanksLabel.InnerText = "Your message has been sent. Thank you.";
            thanksPanel.Controls.Add(thanksLabel);

            this.Controls.Add(thanksPanel);
        }

        /// <summary>
        /// Determines whether the event for the server control is passed up the page's UI server control hierarchy.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        /// <returns>
        /// true if the event has been cancelled; otherwise, false. The default is false.
        /// </returns>
        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            if (source is LinkButton || source is Button)
            {
                IButtonControl control = (IButtonControl)source;

                switch (control.CommandName.ToLower())
                {
                    case "send":
                        sendButton_Click(source, args);
                        break;
                }
            }

            return base.OnBubbleEvent(source, args);
        }
    }
}