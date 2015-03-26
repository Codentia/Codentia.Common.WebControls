using System;
using System.Collections.Generic;
using System.Text;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// ControlGridColumnParameter class
    /// </summary>
    public class ControlGridColumnParameter
    {
        private string _parameterName;
        private string _parameterValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlGridColumnParameter"/> class.
        /// </summary>
        public ControlGridColumnParameter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlGridColumnParameter"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public ControlGridColumnParameter(string parameterName, string parameterValue)
        {
            _parameterName = parameterName;
            _parameterValue = parameterValue;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _parameterName;
            }

            set
            {
                _parameterName = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return _parameterValue;
            }

            set
            {
                _parameterValue = value;
            }
        }
    }
}