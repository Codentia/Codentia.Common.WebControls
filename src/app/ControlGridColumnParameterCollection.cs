using System;
using System.Collections.Generic;
using System.Text;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// ControlGridColumnParameterCollection class
    /// </summary>
    public class ControlGridColumnParameterCollection : List<ControlGridColumnParameter>
    {
        /// <summary>
        /// Gets the <see cref="Codentia.Common.WebControls.ControlGridColumnParameter"/> at the specified index.
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than 0.-or-index is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count"></see>. </exception>
        public ControlGridColumnParameter this[string index]
        {
            get
            {
                ControlGridColumnParameter value = null;

                for (int i = 0; i < this.Count && value == null; i++)
                {
                    if (this[i].Name == index)
                    {
                        value = this[i];
                    }
                }

                return value;
            }
        }
    }
}