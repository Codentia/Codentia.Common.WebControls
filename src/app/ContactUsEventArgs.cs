using System.Collections.Generic;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// ContactUsEventArgs class
    /// </summary>
    public class ContactUsEventArgs
    {
        private Dictionary<string, string> _values = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public Dictionary<string, string> Values
        {
            get
            {
                return _values;
            }

            set
            {
                _values = value;
            }
        }
    }
}