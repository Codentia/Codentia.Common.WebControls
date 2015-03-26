using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// This class represents an item in a LabelledControlList
    /// <see cref="LabelledControlList">LabelledControlList</see>
    /// </summary>
    public class LabelledControlListItem
    {
        private string _label;
        private Control _control;
        private List<IValidator> _validators = new List<IValidator>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlListItem"/> class.
        /// </summary>
        public LabelledControlListItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlListItem"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        public LabelledControlListItem(string label)
            : this(label, (Control)null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlListItem"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="control">The control.</param>
        public LabelledControlListItem(string label, Control control)
            : this(label, control, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlListItem"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="control">The control.</param>
        /// <param name="validator">The validator.</param>
        public LabelledControlListItem(string label, Control control, IValidator validator)
            : this(label, control, validator, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlListItem"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="control">The control.</param>
        /// <param name="validator1">The validator1.</param>
        /// <param name="validator2">The validator2.</param>
        public LabelledControlListItem(string label, Control control, IValidator validator1, IValidator validator2)
        {
            _label = label;
            _control = control;

            if (validator1 != null)
            {
                _validators.Add(validator1);
            }

            if (validator2 != null)
            {
                _validators.Add(validator2);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlListItem"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="controlLabel">The control label.</param>
        public LabelledControlListItem(string label, string controlLabel)
        {
            _label = label;
            _control = new Label();
            _control.ID = Guid.NewGuid().ToString().Substring(0, 10);
            ((Label)_control).Text = controlLabel;
        }

        /// <summary>
        /// Gets or sets the label text
        /// </summary>
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
        /// Gets or sets the Control reference
        /// </summary>
        public Control Control
        {
            get
            {
                return _control;
            }

            set
            {
                _control = value;
            }
        }

        /// <summary>
        /// Gets or sets the validator reference
        /// </summary>
        public List<IValidator> Validators
        {
            get
            {
                return _validators;
            }

            set
            {
                _validators = value;
            }
        }
    }
}
