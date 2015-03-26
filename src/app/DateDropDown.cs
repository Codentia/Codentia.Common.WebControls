using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// Enumerated type describing the configuration options for DateDropDown control
    /// </summary>
    public enum DateDropDownOption
    {
        /// <summary>
        /// Hide the days field
        /// </summary>
        HideDays = 1,

        /// <summary>
        /// Hide the months field
        /// </summary>
        HideMonths = 2,

        /// <summary>
        /// Hide the years field
        /// </summary>
        HideYears = 4,

        /// <summary>
        /// Show months as names not numbers
        /// </summary>
        ShowMonthNames = 8,

        /// <summary>
        /// Include blank selections
        /// </summary>
        IncludeBlanks = 16
    }

    /// <summary>
    /// Date (dd, mm, yy) selection control composed of a set of drop down lists
    /// </summary>
    public class DateDropDown : CECompositeControl
    {
        private int _options = 1;
        private int _startYear = DateTime.Now.Year;
        private int _defaultYear = DateTime.Now.Year;
        private int _defaultMonth = DateTime.Now.Month;
        private int _numberOfYears = 20;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateDropDown"/> class.
        /// </summary>
        public DateDropDown()
        {
        }

        /// <summary>
        /// Gets or sets the options bitmask
        /// </summary>
        public int Options
        {
            get
            {
                return _options;
            }

            set
            {
                _options = value;
            }
        }

        /// <summary>
        /// Gets or sets the StartYear
        /// </summary>
        public int StartYear
        {
            get
            {
                return _startYear;
            }

            set
            {
                if (_startYear != value)
                {
                    _startYear = value;
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Gets or sets the default selected year.
        /// </summary>
        /// <value>The default selected year.</value>
        public int DefaultSelectedYear
        {
            get
            {
                return _defaultYear;
            }

            set
            {
                if (_defaultYear != value)
                {
                    _defaultYear = value;
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Gets or sets the default selected month.
        /// </summary>
        /// <value>The default selected month.</value>
        public int DefaultSelectedMonth
        {
            get
            {
                return _defaultMonth;
            }

            set
            {
                if (_defaultMonth != value)
                {
                    _defaultMonth = value;
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Gets or sets the NumberOfYears
        /// </summary>
        public int NumberOfYears
        {
            get
            {
                return _numberOfYears;
            }

            set
            {
                _numberOfYears = value;
            }
        }

        /// <summary>
        /// Gets the selected day
        /// </summary>
        public int Day
        {
            get
            {
                throw new System.NotImplementedException("Days not current implemented");
            }
        }

        /// <summary>
        /// Gets the selected month
        /// </summary>
        public int Month
        {
            get
            {
                return Convert.ToInt32(((DropDownList)this.FindControl("Months")).SelectedValue);
            }
        }

        /// <summary>
        /// Gets the selected year
        /// </summary>
        public int Year
        {
            get
            {
                return Convert.ToInt32(((DropDownList)this.FindControl("Years")).SelectedValue);
            }
        }

        /// <summary>
        /// Create the child controls which compose this control
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            if (!HasOption(DateDropDownOption.HideDays))
            {
                throw new System.NotImplementedException("Days not current implemented");
            }

            if (!HasOption(DateDropDownOption.HideMonths))
            {
                DropDownList months = new DropDownList();
                months.ID = "Months";

                if (HasOption(DateDropDownOption.IncludeBlanks))
                {
                    months.Items.Add(new ListItem(string.Empty, "0"));
                }

                if (HasOption(DateDropDownOption.ShowMonthNames))
                {
                    for (int i = 1; i < 13; i++)
                    {
                        months.Items.Add(new ListItem(GetMonthName(i), Convert.ToString(i)));
                    }
                }
                else
                {
                    for (int i = 1; i < 13; i++)
                    {
                        months.Items.Add(new ListItem(i < 10 ? string.Format("0{1}", i) : Convert.ToString(i), Convert.ToString(i)));
                    }
                }

                if (_defaultMonth > 0)
                {
                    months.SelectedIndex = _defaultMonth - 1;
                }

                this.Controls.Add(months);
            }

            if (!HasOption(DateDropDownOption.HideYears))
            {
                DropDownList years = new DropDownList();
                years.ID = "Years";

                if (HasOption(DateDropDownOption.IncludeBlanks))
                {
                    years.Items.Add(new ListItem(string.Empty, "0"));

                    if (_defaultYear == 0)
                    {
                        years.SelectedIndex = 0;
                    }
                }

                for (int i = 0; i < _numberOfYears; i++)
                {
                    years.Items.Add(new ListItem(Convert.ToString(_startYear + i), Convert.ToString(_startYear + i)));

                    if (_defaultYear == (_startYear + i))
                    {
                        years.SelectedIndex = i + (HasOption(DateDropDownOption.IncludeBlanks) ? 1 : 0);
                    }
                }

                this.Controls.Add(years);
            }
        }

        private bool HasOption(DateDropDownOption option)
        {
            return (_options & (int)option) > 0;
        }

        private string GetMonthName(int monthIndex)
        {
            string value = string.Empty;

            switch (monthIndex)
            {
                case 1:
                    value = "Jan";
                    break;
                case 2:
                    value = "Feb";
                    break;
                case 3:
                    value = "Mar";
                    break;
                case 4:
                    value = "Apr";
                    break;
                case 5:
                    value = "May";
                    break;
                case 6:
                    value = "Jun";
                    break;
                case 7:
                    value = "Jul";
                    break;
                case 8:
                    value = "Aug";
                    break;
                case 9:
                    value = "Sep";
                    break;
                case 10:
                    value = "Oct";
                    break;
                case 11:
                    value = "Nov";
                    break;
                case 12:
                    value = "Dec";
                    break;
            }

            return value;
        }
    }
}