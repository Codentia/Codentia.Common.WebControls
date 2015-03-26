using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web.UI;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// ColumnDataType enum
    /// </summary>
    public enum ColumnDataType
    {
        /// <summary>
        /// Int value
        /// </summary>
        Int,

        /// <summary>
        /// Decimal value
        /// </summary>
        Decimal,

        /// <summary>
        /// DateTime value
        /// </summary>
        DateTime,

        /// <summary>
        /// Time value
        /// </summary>
        Time,

        /// <summary>
        /// String value
        /// </summary>
        String
    }

    /// <summary>
    /// ControlGridColumnType enum
    /// </summary>
    public enum ControlGridColumnType
    {
        /// <summary>
        /// Databound value
        /// </summary>
        DataBound,

        /// <summary>
        /// LinkButton value
        /// </summary>
        LinkButton,

        /// <summary>
        /// Button value
        /// </summary>
        Button,

        /// <summary>
        /// ObjectBound value
        /// </summary>
        ObjectBound
    }

    /// <summary>
    /// ControlGridColumn class
    /// </summary>
    public class ControlGridColumn
    {
        private string _name;
        private string _binding;
        private string _text;
        private PropertyInfo _boundProperty;
        private string _cssClass;
        private string _formatString;
        private ColumnDataType _dataType = ColumnDataType.String;
        private bool _editable = true;
        private string _defaultValue = null;
        private ControlGridColumnParameterCollection _parameters = new ControlGridColumnParameterCollection();
        private bool _hasTotal = true;

        private ControlGridColumnType _type = ControlGridColumnType.DataBound;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlGridColumn"/> class.
        /// </summary>
        public ControlGridColumn()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlGridColumn"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="text">The text.</param>
        public ControlGridColumn(ControlGridColumnType type, ColumnDataType dataType, string columnName, string binding, string text)
            : this(type, dataType, columnName, binding, text, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlGridColumn"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="text">The text.</param>
        /// <param name="cssClass">The CSS class.</param>
        public ControlGridColumn(ControlGridColumnType type, ColumnDataType dataType, string columnName, string binding, string text, string cssClass)
            : this(type, dataType, columnName, binding, text, cssClass, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlGridColumn"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="text">The text.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="formatString">The format string.</param>
        public ControlGridColumn(ControlGridColumnType type, ColumnDataType dataType, string columnName, string binding, string text, string cssClass, string formatString)
        {
            _type = type;
            _dataType = dataType;
            _name = columnName;
            _binding = binding;
            _text = text;
            _cssClass = cssClass;
            _formatString = formatString;
        }

        /// <summary>
        /// ControlGridColumnEvent event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="rowIndex">Index of the row.</param>
        public delegate void ControlGridColumnEvent(ControlGridColumn sender, int rowIndex);

        /// <summary>
        /// Occurs when [column link clicked].
        /// </summary>
        public event ControlGridColumnEvent ColumnLinkClicked;

        /// <summary>
        /// Gets the ControlGridColumnCollection assigned to this control
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlGridColumnParameterCollection Parameters
        {
            get
            {
                return _parameters;
            }
        }

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string ColumnName
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the binding.
        /// </summary>
        /// <value>
        /// The binding.
        /// </value>
        public string Binding
        {
            get
            {
                if (string.IsNullOrEmpty(_binding))
                {
                    _binding = _name;
                }

                return _binding;
            }

            set
            {
                _binding = value;
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get
            {
                if (string.IsNullOrEmpty(_text))
                {
                    _text = _name;
                }

                return _text;
            }

            set
            {
                _text = value;
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
        /// Gets or sets the format string.
        /// </summary>
        /// <value>
        /// The format string.
        /// </value>
        public string FormatString
        {
            get
            {
                return _formatString;
            }

            set
            {
                _formatString = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the column.
        /// </summary>
        /// <value>
        /// The type of the column.
        /// </value>
        public ControlGridColumnType ColumnType
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
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public ColumnDataType DataType
        {
            get
            {
                return _dataType;
            }

            set
            {
                _dataType = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ControlGridColumn"/> is editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if editable; otherwise, <c>false</c>.
        /// </value>
        public bool Editable
        {
            get
            {
                return _editable;
            }

            set
            {
                _editable = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has total.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has total; otherwise, <c>false</c>.
        /// </value>
        public bool HasTotal
        {
            get
            {
                return _hasTotal;
            }

            set
            {
                _hasTotal = value;
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
        /// Gets or sets the bound property.
        /// </summary>
        /// <value>
        /// The bound property.
        /// </value>
        internal PropertyInfo BoundProperty
        {
            get
            {
                return _boundProperty;
            }

            set
            {
                _boundProperty = value;
            }
        }

        /// <summary>
        /// Clicks the specified row index.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        public void Click(int rowIndex)
        {
            if (ColumnLinkClicked != null)
            {
                ColumnLinkClicked(this, rowIndex);
            }
        }

        /// <summary>
        /// Calculates the total.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>object expressing total</returns>
        public object CalculateTotal(DataTable data)
        {
            object value = null;

            if (_hasTotal)
            {
                switch (_dataType)
                {
                    case ColumnDataType.Int:
                    case ColumnDataType.Decimal:

                        decimal calcValue = 0.0m;
                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            calcValue += Convert.ToDecimal(data.Rows[i][_binding]);
                        }

                        switch (_dataType)
                        {
                            case ColumnDataType.Decimal:
                                value = (object)calcValue;
                                break;
                            case ColumnDataType.Int:
                                value = (object)Convert.ToInt32(calcValue);
                                break;
                        }

                        break;
                    case ColumnDataType.Time:
                        TimeSpan total = new TimeSpan();

                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            TimeSpan current = (TimeSpan)data.Rows[i][_binding];

                            if (current != TimeSpan.MinValue)
                            {
                                total = total.Add(current);
                            }
                        }

                        value = (object)total;
                        break;
                }
            }

            return value;
        }
    }
}
