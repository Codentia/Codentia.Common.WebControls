using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// UnorderedCheckBoxList class
    /// </summary>
    public class UnorderedCheckBoxList : CheckBoxList 
    {
        /// <summary>
        /// Displays the <see cref="T:System.Web.UI.WebControls.CheckBoxList"/> on the client.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that contains the output stream for rendering on the client.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            // We start our un-ordered list tag.
            writer.WriteBeginTag("ul");

            // If the CssClass property has been assigned, we will add
            // the attribute here in our <ul> tag.
            if (this.CssClass.Length > 0)
            {
                writer.WriteAttribute("class", this.CssClass);
            }

            // We need to close off the <ul> tag, but we are not ready
            // to add the closing </ul> tag yet.
            writer.Write(">");

            // Now we will render each child item, which in this case
            // would be our checkboxes.
            for (int i = 0; i < this.Items.Count; i++)
            {
                // We start the <li> (list item) tag.
                writer.WriteFullBeginTag("li");

                this.RenderItem(ListItemType.Item, i, new RepeatInfo(), writer);

                // Close the list item tag. Some people think this is not
                // necessary, but it is for both XHTML and semantic reasons.
                writer.WriteEndTag("li");
            }

            // We finish off by closing our un-ordered list tag.
            writer.WriteEndTag("ul");
        }
    }
}
