using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Codentia.Common.Data.Caching;
using Codentia.Common.WebControls.HtmlElements;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// ControlGrid class
    /// </summary>
    public class ControlGrid : CECompositeControl
    {
        private ControlGridColumnCollection _columns;
        private Guid _cacheKey = Guid.Empty;
        private DataTable _data;

        private string _rowCssClass;
        private string _headerCssClass;
        private string _alternateRowCssClass;
        private string _lastRowCssClass;
        private string _firstColumnCssClass;
        private string _footerRowCssClass;
        private string _totalCssClass;

        private bool _editable = false;
        private bool _hasTotalRow = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlGrid"/> class.
        /// </summary>
        public ControlGrid()
        {
            _columns = new ControlGridColumnCollection();
        }

        /// <summary>
        /// ControlGridEvent event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public delegate void ControlGridEvent(ControlGrid sender, EventArgs args);

        /// <summary>
        /// Occurs when [save changes].
        /// </summary>
        public event ControlGridEvent SaveChanges;

        /// <summary>
        /// Gets the ControlGridColumnCollection assigned to this control
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlGridColumnCollection Columns
        {
            get
            {
                return _columns;
            }
        }

        /// <summary>
        /// Sets the row CSS class.
        /// </summary>
        /// <value>
        /// The row CSS class.
        /// </value>
        public string RowCssClass
        {
            set
            {
                _rowCssClass = value;
            }
        }

        /// <summary>
        /// Sets the header CSS class.
        /// </summary>
        /// <value>
        /// The header CSS class.
        /// </value>
        public string HeaderCssClass
        {
            set
            {
                _headerCssClass = value;
            }
        }

        /// <summary>
        /// Sets the alternate row CSS class.
        /// </summary>
        /// <value>
        /// The alternate row CSS class.
        /// </value>
        public string AlternateRowCssClass
        {
            set
            {
                _alternateRowCssClass = value;
            }
        }

        /// <summary>
        /// Sets the last row CSS class.
        /// </summary>
        /// <value>
        /// The last row CSS class.
        /// </value>
        public string LastRowCssClass
        {
            set
            {
                _lastRowCssClass = value;
            }
        }

        /// <summary>
        /// Sets the first column CSS class.
        /// </summary>
        /// <value>
        /// The first column CSS class.
        /// </value>
        public string FirstColumnCssClass
        {
            set
            {
                _firstColumnCssClass = value;
            }
        }

        /// <summary>
        /// Sets the footer row CSS class.
        /// </summary>
        /// <value>
        /// The footer row CSS class.
        /// </value>
        public string FooterRowCssClass
        {
            set
            {
                _footerRowCssClass = value;
            }
        }

        /// <summary>
        /// Sets the total CSS class.
        /// </summary>
        /// <value>
        /// The total CSS class.
        /// </value>
        public string TotalCssClass
        {
            set
            {
                _totalCssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ControlGrid"/> is editable.
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
                if (_editable != value)
                {
                    _editable = value;
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has total row.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has total row; otherwise, <c>false</c>.
        /// </value>
        public bool HasTotalRow
        {
            get
            {
                return _hasTotalRow;
            }

            set
            {
                if (!_hasTotalRow == value)
                {
                    _hasTotalRow = value;
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public DataTable Data
        {
            get
            {
                return _data;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has data; otherwise, <c>false</c>.
        /// </value>
        public bool HasData
        {
            get
            {
                return _data != null;
            }
        }

        /// <summary>
        /// Binds to.
        /// </summary>
        /// <param name="data">The data.</param>
        public void BindTo(DataTable data)
        {
            _data = data;
            CreateChildControls();
        }

        /// <summary>
        /// Binds to.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="data">The data.</param>
        public void BindTo<T>(T[] data)
        {
            // build a DataTable based upon the object array
            DataTable derivedData = new DataTable();

            // columns
            for (int i = 0; i < _columns.Count; i++)
            {
                if (_columns[i].ColumnType == ControlGridColumnType.ObjectBound)
                {
                    _columns[i].BoundProperty = typeof(T).GetProperty(_columns[i].Binding);
                }

                // always add the column, so that we can update databound ones later
                derivedData.Columns.Add(_columns[i].Binding, typeof(string));
            }

            // rows
            for (int i = 0; i < data.Length; i++)
            {
                object[] rowData = new object[derivedData.Columns.Count];
                int index = 0;

                for (int j = 0; j < _columns.Count; j++)
                {
                    if (_columns[j].ColumnType == ControlGridColumnType.ObjectBound)
                    {
                        System.Reflection.PropertyInfo pi = typeof(T).GetProperty(_columns[j].Binding);
                        object item = pi.GetValue(data[i], null);

                        if (item is Array)
                        {
                            Array x = (Array)item;
                            StringBuilder arrayDisplay = new StringBuilder();
                            for (int k = 0; k < x.Length; k++)
                            {
                                arrayDisplay.AppendFormat("{0}{1}", arrayDisplay.Length > 0 ? ", " : string.Empty, x.GetValue(k).ToString());
                            }

                            rowData[index] = arrayDisplay.ToString();
                        }
                        else
                        {
                            if (item is LookupPair)
                            {
                                rowData[index] = ((LookupPair)item).Value;
                            }
                            else
                            {
                                rowData[index] = Convert.ToString(item);
                            }
                        }
                    }

                    // increment index in any case, so we leave blanks for data-bound columns which are updated elsewhere
                    index++;
                }

                derivedData.Rows.Add(rowData);
            }

            _data = derivedData;
            CreateChildControls();
        }

        /// <summary>
        /// Updates the column.
        /// </summary>
        /// <param name="columnBinding">The column binding.</param>
        /// <param name="data">The data.</param>
        /// <param name="redrawGrid">if set to <c>true</c> [redraw grid].</param>
        public void UpdateColumn(string columnBinding, object[] data, bool redrawGrid)
        {
            if (HasData)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    _data.Rows[i][columnBinding] = data[i];
                }

                if (redrawGrid)
                {
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Sorts the grid data by expression.
        /// </summary>
        /// <param name="sortByExpression">The sort by expression.</param>
        public void Sort(string sortByExpression)
        {
            if (HasData)
            {
                DataTable sortedTable = _data.Clone();
                DataRow[] sortedData = _data.Select(string.Empty, sortByExpression);

                for (int i = 0; i < sortedData.Length; i++)
                {
                    sortedTable.Rows.Add(sortedData[i].ItemArray);
                }

                _data = sortedTable;
                CreateChildControls();
            }
        }

        /// <summary>
        /// Handles the Click event of the columnLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void columnLink_Click(object sender, EventArgs e)
        {
            string[] parts = ((Control)sender).ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            int columnIndex = Convert.ToInt32(parts[0]);
            int rowIndex = Convert.ToInt32(parts[1]);

            _columns[columnIndex].Click(rowIndex);
        }

        /// <summary>
        /// Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState"></see> method was invoked.
        /// </summary>
        /// <returns>
        /// An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, null.
        /// </returns>
        protected override object SaveViewState()
        {
            if (_data != null)
            {
                if (_cacheKey == Guid.Empty)
                {
                    _cacheKey = Guid.NewGuid();
                }

                DataCache.AddSingleObject<DataTable>(_cacheKey.ToString(), _data);
            }

            return new object[] { _editable, _cacheKey };
        }

        /// <summary>
        /// Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState"></see> method.
        /// </summary>
        /// <param name="savedState">An object that represents the control state to restore.</param>
        protected override void LoadViewState(object savedState)
        {
            object[] saved = (object[])savedState;

            if (saved != null)
            {
                _editable = (bool)saved[0];
                _cacheKey = (Guid)saved[1];

                if (_cacheKey != null && _cacheKey != Guid.Empty)
                {
                    _data = DataCache.GetSingleObject<DataTable>(_cacheKey.ToString());
                }

                CreateChildControls();
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            base.CreateChildControls();

            // header row
            Div rowContainer = new Div();
            rowContainer.Attributes.Add("class", _rowCssClass);

            Ul row = new Ul();
            row.Attributes.Add("class", _headerCssClass);

            for (int i = 0; i < _columns.Count; i++)
            {
                Li cell = new Li();

                cell.Attributes.Add("class", string.Format("{0}{1}{2}", i == 0 ? _firstColumnCssClass : string.Empty, i == 0 ? " " : string.Empty, _columns[i].CssClass));

                cell.InnerText = _columns[i].ColumnName;
                row.Controls.Add(cell);
            }

            rowContainer.Controls.Add(row);
            this.Controls.Add(rowContainer);

            // now do the data
            if (_data != null)
            {
                for (int i = 0; i < _data.Rows.Count; i++)
                {
                    bool isAlternateRow = i > 0 && (i % 2 != 0);
                    bool isLastRow = i == _data.Rows.Count - 1;

                    rowContainer = new Div();
                    rowContainer.ID = string.Format("row{0}", i);
                    rowContainer.Attributes.Add("class", string.Format("{0}{1}{2}{3}{4}", _rowCssClass, isAlternateRow ? " " : string.Empty, isAlternateRow ? _alternateRowCssClass : string.Empty, isLastRow ? " " : string.Empty, isLastRow ? _lastRowCssClass : string.Empty));

                    row = new Ul();

                    for (int j = 0; j < _columns.Count; j++)
                    {
                        Li cell = new Li();
                        cell.ID = string.Format("cell{0}x{1}", i, j);

                        cell.Attributes.Add("class", string.Format("{0}{1}{2}", j == 0 ? _firstColumnCssClass : string.Empty, j == 0 ? " " : string.Empty, _columns[j].CssClass));

                        if (!_editable || !_columns[j].Editable || (_data.Columns.Contains("_IsEditable") && !Convert.ToBoolean(_data.Rows[i]["_IsEditable"])))
                        {
                            // values from data
                            switch (_columns[j].ColumnType)
                            {
                                case ControlGridColumnType.DataBound:
                                case ControlGridColumnType.ObjectBound:
                                    object data = _data.Rows[i][_columns[j].Binding];

                                    this.RenderData(cell, j, data);

                                    break;
                                case ControlGridColumnType.LinkButton:
                                case ControlGridColumnType.Button:
                                    // show linkbutton if column is unbound, or binding evaluates to true in data
                                    if (string.IsNullOrEmpty(_columns[j].Binding) || !_data.Columns.Contains(_columns[j].Binding) || (_data.Rows[i][_columns[j].Binding] != DBNull.Value && Convert.ToBoolean(_data.Rows[i][_columns[j].Binding])))
                                    {
                                        if (_columns[j].ColumnType == ControlGridColumnType.LinkButton)
                                        {
                                            LinkButton lb = new LinkButton();
                                            lb.ID = string.Format("{0}_{1}", j, i);
                                            lb.Click += new EventHandler(columnLink_Click);
                                            lb.Text = _columns[j].Text;
                                            cell.Controls.Add(lb);
                                        }
                                        else
                                        {
                                            Button lb = new Button();
                                            lb.ID = string.Format("{0}_{1}", j, i);
                                            lb.Click += new EventHandler(columnLink_Click);
                                            lb.Text = _columns[j].Text;
                                            cell.Controls.Add(lb);
                                        }
                                    }
                                    else
                                    {
                                        cell.InnerHtml = "&nbsp;";
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (_columns[j].ColumnType)
                            {
                                case ControlGridColumnType.DataBound:
                                case ControlGridColumnType.ObjectBound:
                                    switch (_columns[j].DataType)
                                    {
                                        case ColumnDataType.Int:
                                            TextBox intTextBox = new TextBox();
                                            intTextBox.ID = string.Format("int{0}{1}", i, j);
                                            intTextBox.Text = Convert.ToString(_data.Rows[i][_columns[j].Binding]);
                                            cell.Controls.Add(intTextBox);
                                            break;
                                        case ColumnDataType.Decimal:
                                            TextBox decimalTextBox = new TextBox();
                                            decimalTextBox.ID = string.Format("decimal{0}{1}", i, j);
                                            decimalTextBox.Text = Convert.ToString(_data.Rows[i][_columns[j].Binding]);
                                            cell.Controls.Add(decimalTextBox);
                                            break;
                                        case ColumnDataType.String:
                                            TextBox stringTextBox = new TextBox();
                                            stringTextBox.ID = string.Format("string{0}{1}", i, j);
                                            stringTextBox.Text = Convert.ToString(_data.Rows[i][_columns[j].Binding]);
                                            cell.Controls.Add(stringTextBox);
                                            break;
                                        case ColumnDataType.Time:
                                            TimeDropDown timeDropDown = null;
                                            
                                            // do we have parameters?
                                            if (_columns[j].Parameters.Count > 0)
                                            {
                                                timeDropDown = new TimeDropDown(_columns[j].Parameters["hours"].Value, _columns[j].Parameters["minutes"].Value);
                                            }
                                            else
                                            {
                                                timeDropDown = new TimeDropDown();
                                            }

                                            timeDropDown.ID = string.Format("timespan{0}{1}", i, j);
                                            timeDropDown.Time = (TimeSpan)_data.Rows[i][_columns[j].Binding];
                                            cell.Controls.Add(timeDropDown);
                                            break;
                                        default:
                                            throw new System.NotImplementedException(string.Format("Editing of ColumnDataType '{0}' not yet supported", _columns[j].DataType.ToString()));
                                    }

                                    break;
                                case ControlGridColumnType.LinkButton:
                                case ControlGridColumnType.Button:
                                    // do nothing
                                    cell.InnerHtml = "&nbsp;";
                                    break;
                            }
                        }

                        row.Controls.Add(cell);
                    }

                    rowContainer.Controls.Add(row);
                    this.Controls.Add(rowContainer);
                }

                // add total row if appropriate
                if (_hasTotalRow && !_editable)
                {
                    bool isAlternateRow = Data.Rows.Count > 0 && (Data.Rows.Count % 2 != 0);
                    bool isLastRow = _data.Rows.Count == _data.Rows.Count - 1;

                    rowContainer = new Div();
                    rowContainer.ID = string.Format("row{0}", Data.Rows.Count);
                    rowContainer.Attributes.Add("class", string.Format("{0}{1}{2}{3}{4} {5}", _rowCssClass, isAlternateRow ? " " : string.Empty, isAlternateRow ? _alternateRowCssClass : string.Empty, isLastRow ? " " : string.Empty, isLastRow ? _lastRowCssClass : string.Empty, _totalCssClass));

                    row = new Ul();

                    for (int j = 0; j < _columns.Count; j++)
                    {
                        Li cell = new Li();
                        cell.ID = string.Format("total{0}{1}", Data.Rows.Count, j);

                        cell.Attributes.Add("class", string.Format("{0}{1}{2}", j == 0 ? _firstColumnCssClass : string.Empty, j == 0 ? " " : string.Empty, _columns[j].CssClass));

                        if (_columns[j].HasTotal)
                        {
                            object totalValue = _columns[j].CalculateTotal(_data);
                            this.RenderData(cell, j, totalValue);

                            // MC: Consider some sort of way to tot up totals as we go!
                            // this would remove a whole iteration and be far more efficient
                        }
                        else
                        {
                            cell.InnerHtml = "&nbsp;";
                        }

                        row.Controls.Add(cell);
                    }

                    rowContainer.Controls.Add(row);
                    this.Controls.Add(rowContainer);
                }

                // add footer row if appropriate
                if (_editable)
                {
                    rowContainer = new Div();
                    rowContainer.Attributes.Add("class", _rowCssClass + " " + _footerRowCssClass);
                    rowContainer.ID = "footer";

                    Div buttonCell = new Div();
                    buttonCell.ID = "footerButtons";

                    Button saveButton = new Button();
                    saveButton.ID = "saveChanges";
                    saveButton.Text = "Save";
                    saveButton.Click += new EventHandler(saveButton_Click);
                    buttonCell.Controls.Add(saveButton);

                    Button discardButton = new Button();
                    discardButton.ID = "discardButton";
                    discardButton.Text = "Discard";
                    discardButton.Click += new EventHandler(discardButton_Click);
                    buttonCell.Controls.Add(discardButton);

                    rowContainer.Controls.Add(buttonCell);
                    this.Controls.Add(rowContainer);
                }
            }
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _data.Rows.Count; i++)
            {
                for (int j = 0; j < _columns.Count; j++)
                {
                    if (_columns[j].Editable && (!_data.Columns.Contains("_IsEditable") || Convert.ToBoolean(_data.Rows[i]["_IsEditable"])))
                    {
                        if (_columns[j].ColumnType != ControlGridColumnType.LinkButton && _columns[j].ColumnType != ControlGridColumnType.Button)
                        {
                            switch (_columns[j].DataType)
                            {
                                case ColumnDataType.Int:
                                    _data.Rows[i][_columns[j].Binding] = this.GetChildControlValue<int>(typeof(TextBox), string.Format("int{0}{1}", i, j));
                                    break;
                                case ColumnDataType.Decimal:
                                    _data.Rows[i][_columns[j].Binding] = this.GetChildControlValue<decimal>(typeof(TextBox), string.Format("decimal{0}{1}", i, j));
                                    break;
                                case ColumnDataType.String:
                                    _data.Rows[i][_columns[j].Binding] = this.GetChildControlValue<string>(typeof(TextBox), string.Format("string{0}{1}", i, j));
                                    break;
                                case ColumnDataType.Time:
                                    _data.Rows[i][_columns[j].Binding] = this.GetChildControlValue<TimeSpan>(typeof(TimeDropDown), string.Format("timespan{0}{1}", i, j));
                                    break;
                                default:
                                    throw new System.NotImplementedException(string.Format("Editing of ColumnDataType '{0}' not yet supported", _columns[j].DataType.ToString()));
                            }
                        }
                    }
                }
            }

            if (SaveChanges != null)
            {
                SaveChanges(this, e);
            }

            this.Editable = false;
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            this.Editable = false;
        }

        private void RenderData(Li cell, int columnIndex, object data)
        {
            switch (_columns[columnIndex].DataType)
            {
                case ColumnDataType.Int:
                    if (data == null)
                    {
                        data = 0;
                    }

                    if (Convert.ToInt32(data) == 0)
                    {
                        cell.InnerText = _columns[columnIndex].DefaultValue;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_columns[columnIndex].FormatString))
                        {
                            cell.InnerText = Convert.ToInt32(data).ToString(_columns[columnIndex].FormatString);
                        }
                        else
                        {
                            cell.InnerText = HttpUtility.HtmlEncode(Convert.ToString(data));
                        }
                    }

                    break;
                case ColumnDataType.Decimal:
                    if (data == null)
                    {
                        data = 0.0m;
                    }

                    if (Convert.ToDecimal(data) == 0.0m)
                    {
                        cell.InnerText = _columns[columnIndex].DefaultValue;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_columns[columnIndex].FormatString))
                        {
                            cell.InnerText = Convert.ToDecimal(data).ToString(_columns[columnIndex].FormatString);
                        }
                        else
                        {
                            cell.InnerText = HttpUtility.HtmlEncode(Convert.ToString(data));
                        }
                    }

                    break;
                case ColumnDataType.DateTime:
                    if (data == null)
                    {
                        data = DateTime.MinValue;
                    }

                    if (Convert.ToDateTime(data) == DateTime.MinValue)
                    {
                        cell.InnerText = _columns[columnIndex].DefaultValue;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_columns[columnIndex].FormatString))
                        {
                            cell.InnerText = Convert.ToDateTime(data).ToString(_columns[columnIndex].FormatString);
                        }
                        else
                        {
                            cell.InnerText = HttpUtility.HtmlEncode(Convert.ToString(data));
                        }
                    }

                    break;
                case ColumnDataType.Time:
                    if (data == null)
                    {
                        data = TimeSpan.MinValue;
                    }

                    if (((TimeSpan)data) == TimeSpan.MinValue || ((TimeSpan)data) == new TimeSpan(0, 0, 0))
                    {
                        cell.InnerText = _columns[columnIndex].DefaultValue;
                    }
                    else
                    {
                        // MC: Need to cope with totalhours>24 when printing a timespan out
                        TimeSpan timeData = (TimeSpan)data;

                        if (timeData.Days > 0)
                        {
                            if (!string.IsNullOrEmpty(_columns[columnIndex].FormatString))
                            {
                                string tempFormat = _columns[columnIndex].FormatString.Replace("HH", "{0}").Replace("mm", "{1}");
                                cell.InnerText = string.Format(tempFormat, Convert.ToInt32(timeData.TotalHours).ToString("D2"), timeData.Minutes.ToString("D2"));
                            }
                            else
                            {
                                cell.InnerText = string.Format("{0}:{1}", Convert.ToInt32(timeData.TotalHours).ToString("D2"), timeData.Minutes.ToString("D2"));
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_columns[columnIndex].FormatString))
                            {
                                cell.InnerText = DateTime.Now.Date.Add(timeData).ToString(_columns[columnIndex].FormatString);
                            }
                            else
                            {
                                cell.InnerText = HttpUtility.HtmlEncode(Convert.ToString(DateTime.Now.Date.Add(timeData)));
                            }
                        }
                    }

                    break;
                case ColumnDataType.String:
                    if (string.IsNullOrEmpty(Convert.ToString(data)))
                    {
                        cell.InnerText = _columns[columnIndex].DefaultValue;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_columns[columnIndex].FormatString))
                        {
                            cell.InnerText = string.Format(_columns[columnIndex].FormatString, data);
                        }
                        else
                        {
                            cell.InnerText = HttpUtility.HtmlEncode(Convert.ToString(data));
                        }
                    }

                    break;
            }

            if (string.IsNullOrEmpty(cell.InnerText))
            {
                cell.InnerHtml = "&nbsp;";
            }
        }
    }
}