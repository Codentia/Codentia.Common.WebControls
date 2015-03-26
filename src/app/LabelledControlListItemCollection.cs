using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// Class representing a collection (list) of LabelledControlListItems
    /// </summary>
    public class LabelledControlListItemCollection : List<LabelledControlListItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlListItemCollection"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public LabelledControlListItemCollection(int capacity)
            : base(capacity)
        {
        }
    }
}
