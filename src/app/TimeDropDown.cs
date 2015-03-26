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
    /// DropDown for the input of time via timeslots
    /// </summary>
    public class TimeDropDown : CECompositeControl
    {
        private string _hourSlots = null;
        private string _minuteSlots = "00,15,30,45";
        private TimeSpan _initialValue = TimeSpan.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeDropDown"/> class.
        /// </summary>
        public TimeDropDown()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeDropDown"/> class.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        public TimeDropDown(string hours, string minutes)
        {
            _hourSlots = hours;
            _minuteSlots = minutes;
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public TimeSpan Time
        {
            get
            {
                string hours = this.GetChildControlValue<string>(typeof(DropDownList), "hours");
                string minutes = this.GetChildControlValue<string>(typeof(DropDownList), "minutes");

                return new TimeSpan(Convert.ToInt32(hours), Convert.ToInt32(minutes), 0);
            }

            set
            {
                if (_initialValue != value)
                {
                    _initialValue = value;
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Sets the minute slots.
        /// </summary>
        /// <value>The minute slots.</value>
        public string MinuteSlots
        {
            set
            {
                if (_minuteSlots != value)
                {
                    _minuteSlots = value;
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Sets the hour slots.
        /// </summary>
        /// <value>The hour slots.</value>
        public string HourSlots
        {
            set
            {
                if (_hourSlots != value)
                {
                    _hourSlots = value;
                    CreateChildControls();
                }
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            base.CreateChildControls();

            DropDownList hours = new DropDownList();
            hours.ID = "hours";

            if (_hourSlots == null)
            {
                for (int i = 0; i < 24; i++)
                {
                    hours.Items.Add(new ListItem(i.ToString("D2")));

                    if (_initialValue.Hours == i)
                    {
                        hours.SelectedIndex = i;
                    }
                }
            }
            else
            {
                string[] hourSlots = _hourSlots.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < hourSlots.Length; i++)
                {
                    hours.Items.Add(new ListItem(hourSlots[i]));

                    if (_initialValue.Hours == Convert.ToInt32(hourSlots[i]))
                    {
                        hours.SelectedIndex = i;
                    }
                }
            }

            string[] minuteSlots = _minuteSlots.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            DropDownList minutes = new DropDownList();
            minutes.ID = "minutes";

            for (int i = 0; i < minuteSlots.Length; i++)
            {
                minutes.Items.Add(new ListItem(minuteSlots[i]));

                if (_initialValue.Minutes == Convert.ToInt32(minuteSlots[i]))
                {
                    minutes.SelectedIndex = i;
                }
            }

            this.Controls.Add(hours);
            this.Controls.Add(minutes);
        }
    }
}
