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
    internal class ControlGridTestObject
    {
        private int _a, _b, _c, _d;
        private int[] _e;

        public ControlGridTestObject(int a, int b, int c, int d)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;

            _e = new int[] { a, b, c, d };
        }

        public int Property1
        {
            get
            {
                return _a;
            }
        }

        public int Property2
        {
            get
            {
                return _b;
            }
        }

        public int Property3
        {
            get
            {
                return _c;
            }
        }

        public int Property4
        {
            get
            {
                return _d;
            }
        }

        public int[] Property5
        {
            get
            {
                return _e;
            }
        }
    }

    public partial class ControlGridTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // set data for controlGrid1
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Col1", typeof(string));
            dt1.Columns.Add("Col2", typeof(string));
            dt1.Columns.Add("Col3", typeof(string));
            dt1.Columns.Add("Col4", typeof(string));

            dt1.Rows.Add(new object[] { "1", "2", "3", "4" });
            dt1.Rows.Add(new object[] { "5", "6", "7", "8" });
            dt1.Rows.Add(new object[] { "9", "10", "11", "12" });
            dt1.Rows.Add(new object[] { "13", "14", "15", "16" });

            controlGrid1.BindTo(dt1);

            // use the same data for grid 2
            controlGrid2.BindTo(dt1);

            // use an object array for grid 3
            ControlGridTestObject[] objectArray = new ControlGridTestObject[4];
            objectArray[0] = new ControlGridTestObject(1, 2, 3, 4);
            objectArray[1] = new ControlGridTestObject(5, 6, 7, 8);
            objectArray[2] = new ControlGridTestObject(9, 10, 11, 12);
            objectArray[3] = new ControlGridTestObject(13, 14, 15, 16);

            controlGrid3.BindTo<ControlGridTestObject>(objectArray);
        }

        protected void controlGrid2_EditClick(ControlGridColumn sender, int rowIndex)
        {
            selectedIndex1.Text = string.Format("Selected Row: {0}", rowIndex);
        }

        protected void controlGrid3_EditClick(ControlGridColumn sender, int rowIndex)
        {
            selectedIndex2.Text = string.Format("Selected Row: {0}", rowIndex);
        }
    }
}
