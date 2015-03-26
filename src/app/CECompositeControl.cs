using System;
using System.Configuration;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// Abstract base class for all CE custom controls
    /// </summary>
    [ValidationProperty("DummyValidation")]
    public abstract class CECompositeControl : CompositeControl
    {
        private static object _reflectionLock = new object();
        private static bool _methodsInitialized;
        private static MethodInfo _registerClientScriptResourceMethod;
        private static MethodInfo _registerStartupScriptMethod;
        private bool _showControlsAsButtons;

        /// <summary>
        /// Initializes a new instance of the <see cref="CECompositeControl"/> class.
        /// </summary>
        public CECompositeControl()
        {
            if (ConfigurationManager.AppSettings["ShowControlsAsButtons"] != null)
            {
                _showControlsAsButtons = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowControlsAsButtons"]);
            }
            else
            {
                _showControlsAsButtons = false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show controls as buttons].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show controls as buttons]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowControlsAsButtons
        {
            get
            {
                return _showControlsAsButtons;
            }

            set
            {
                _showControlsAsButtons = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [dummy validation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dummy validation]; otherwise, <c>false</c>.
        /// </value>
        public bool DummyValidation
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the TagKey for this control
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        
        /// <summary>
        /// Perform a recursive search for a child control with a given id
        /// </summary>
        /// <param name="baseControl">Point at which to start searching</param>
        /// <param name="id">Id to search for</param>
        /// <returns>The Control</returns>
        public static Control FindChildControlRecursive(Control baseControl, string id)
        {
            Control child = baseControl.FindControl(id);

            if (child == null)
            {
                if (baseControl.HasControls())
                {
                    for (int i = 0; i < baseControl.Controls.Count; i++)
                    {
                        child = CECompositeControl.FindChildControlRecursive(baseControl.Controls[i], id);

                        if (child != null)
                        {
                            break;
                        }
                    }
                }
            }

            return child;
        }

        /// <summary>
        /// Registers the client script resource.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="type">The type.</param>
        /// <param name="resourceName">Name of the resource.</param>
        protected static void RegisterClientScriptResource(Control control, Type type, string resourceName)
        {
            if (!_methodsInitialized)
            {
                CECompositeControl.InitializeReflection();
            }

            if (_registerClientScriptResourceMethod != null)
            {
                // ASP.NET AJAX exists, so we use the ScriptManager
                _registerClientScriptResourceMethod.Invoke(null, new object[] { control, type, resourceName });
            }
            else
            {
                // No ASP.NET AJAX, so we just call to the ASP.NET 2.0 method
                // control.Page.ClientScript.RegisterClientScriptResource(type, resourceName);
                ScriptManager.RegisterClientScriptResource(control, type, resourceName);
            }
        }

        /// <summary>
        /// Registers the startup script.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="type">The type.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="script">The script.</param>
        /// <param name="addScriptTags">if set to <c>true</c> [add script tags].</param>
        protected static void RegisterStartupScript(Control control, Type type, string resourceName, string script, bool addScriptTags)
        {
            if (!_methodsInitialized)
            {
                CECompositeControl.InitializeReflection();
            }

            if (_registerStartupScriptMethod != null)
            {
                // ASP.NET AJAX exists, so we use the ScriptManager
                _registerStartupScriptMethod.Invoke(null, new object[] { control, type, resourceName, script, addScriptTags });
            }
            else
            {
                // No ASP.NET AJAX, so we just call to the ASP.NET 2.0 method
                ScriptManager.RegisterStartupScript(control, type, resourceName, script, addScriptTags);
            }
        }
        
        /// <summary>
        /// EventHandler prior to control rendering
        /// </summary>
        /// <param name="e">The Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.Page != null)
            {
                // Register JS compatibility library to add missing functions, common code, etc
                this.Page.ClientScript.RegisterClientScriptResource(typeof(CECompositeControl), "Codentia.Common.WebControls.JavaScriptCompatibilityLibrary.js");
            }
        }

        /// <summary>
        /// Find a child control within the scope of this control (recursively).
        /// Alternative to the default .net FindControl which is NOT recursive
        /// </summary>
        /// <param name="id">ID of the control to find</param>
        /// <returns>The Control</returns>
        protected Control FindChildControl(string id)
        {
            Control child = CECompositeControl.FindChildControlRecursive(this, id);

            return (child == null || child.ID != id) ? null : child;
        }
        
        /// <summary>
        /// Perform the required operations to make a user-entered string safe (prevent xss, etc)
        /// </summary>
        /// <param name="input">string to be made safe</param>
        /// <returns>safe version of string</returns>
        protected string MakeInputStringSafe(string input)
        {
            return this.Page.Server.HtmlEncode(input);
        }

        /// <summary>
        /// Convert an input string value to an integer, first making sure it is xss/etc safe.
        /// Return 0 if invalid.
        /// </summary>
        /// <param name="input">Value to be converted to integer</param>
        /// <returns>The int</returns>
        protected int ConvertInputStringToInt32(string input)
        {
            int value = 0;

            input = MakeInputStringSafe(input);

            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    value = int.Parse(input);
                }
                catch (Exception)
                {
                }
            }

            return value;
        }

        /// <summary>
        /// Gets the value of a given child control, based upon its ID and Type.
        /// Values are passed through MakeInputStringSafe prior to return (where appropriate)
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="childType">Type corresponding to the child control e.g. typeof(TextBox)</param>
        /// <param name="childId">Id of the child control</param>
        /// <returns>
        /// The string
        /// </returns>
        /// <see cref="MakeInputStringSafe">MakeInputStringSafe</see>
        protected T GetChildControlValue<T>(Type childType, string childId)
        {
            T value = default(T);

            Control child = this.FindChildControl(childId);

            if (child != null)
            {
                switch (childType.ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        TextBox t = (TextBox)child;

                        switch (typeof(T).ToString())
                        {
                            case "System.Decimal":
                                string temp = this.MakeInputStringSafe(t.Text);
                                if (string.IsNullOrEmpty(temp))
                                {
                                    value = (T)(object)0.0m;
                                }
                                else
                                {
                                    decimal result = 0.0m;

                                    if (!decimal.TryParse(temp, out result))
                                    {
                                        throw new Exception(string.Format("Unable to convert value '{0}' to type System.Decimal", temp));
                                    }
                                    else
                                    {
                                        value = (T)(object)result;
                                    }
                                }

                                break;
                            case "System.Int32":
                                temp = this.MakeInputStringSafe(t.Text);
                                if (string.IsNullOrEmpty(temp))
                                {
                                    value = (T)(object)0;
                                }
                                else
                                {
                                    int intResult = 0;

                                    if (!int.TryParse(temp, out intResult))
                                    {
                                        throw new Exception(string.Format("Unable to convert value '{0}' to type System.Int32", temp));
                                    }
                                    else
                                    {
                                        value = (T)(object)intResult;
                                    }
                                }

                                break;
                            default:
                                value = (T)(object)this.MakeInputStringSafe(t.Text);
                                break;
                        }

                        break;
                    case "System.Web.UI.WebControls.CheckBox":
                        CheckBox cbx = (CheckBox)child;
                        value = (T)(object)cbx.Checked;
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                        DropDownList ddl = (DropDownList)child;

                        switch (typeof(T).ToString())
                        {
                            default: // string
                                value = (T)(object)ddl.SelectedValue;
                                break;
                        }

                        break;
                    case "Codentia.Common.WebControls.TimeDropDown":
                        TimeDropDown tdd = (TimeDropDown)child;

                        value = (T)(object)tdd.Time;
                        break;
                    default:
                        throw new System.NotImplementedException(string.Format("Type {0} not supported", childType.ToString()));
                }
            }

            return value;
        }        

        private static void InitializeReflection()
        {
            lock (_reflectionLock)
            {
                if (!_methodsInitialized)
                {
                    Type scriptManagerType = Type.GetType("System.Web.UI.ScriptManager"); 

                    if (scriptManagerType != null)
                    {
                        _registerClientScriptResourceMethod = scriptManagerType.GetMethod("RegisterClientScriptResource");
                        _registerStartupScriptMethod = scriptManagerType.GetMethod("RegisterStartupScript");
                    }

                    _methodsInitialized = true;
                }
            }
        }
    }
}