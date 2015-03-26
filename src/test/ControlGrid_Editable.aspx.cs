using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.Test
{
    public partial class ControlGrid_Editable : System.Web.UI.Page
    {
        private DataTable _dt1;

        protected void Page_Load(object sender, EventArgs e)
        {
            // set data for controlGrid1
            if (_dt1 == null)
            {
                _dt1 = new DataTable();
                _dt1.Columns.Add("Col1", typeof(string));
                _dt1.Columns.Add("Col2", typeof(string));
                _dt1.Columns.Add("Col3", typeof(string));
                _dt1.Columns.Add("Col4", typeof(TimeSpan));

                _dt1.Rows.Add(new object[] { "1", "2", "3", new TimeSpan(2, 45, 0) });
                _dt1.Rows.Add(new object[] { "2", "6", "7", new TimeSpan(3, 15, 0) });
                _dt1.Rows.Add(new object[] { "3", "10", "11", new TimeSpan(1, 30, 0) });
                _dt1.Rows.Add(new object[] { "4", "12", "14", TimeSpan.MinValue });
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!controlGrid1.HasData)
            {
                controlGrid1.BindTo(_dt1);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            controlGrid1.Editable = true;
        }

        protected void controlGrid1_SaveChanges(object sender, EventArgs e)
        {
            _dt1 = controlGrid1.Data;
        }

        protected override object SaveViewState()
        {
            return _dt1;
        }

        protected override void LoadViewState(object savedState)
        {
            _dt1 = (DataTable)savedState;
        }
    }
}
