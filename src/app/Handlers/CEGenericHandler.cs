using System;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using Codentia.Common.Helper;

namespace Codentia.Common.WebControls.Handlers
{
    /// <summary>
    /// Generic Handler base class containing utility methods and performing basic functions otherwise repeated.
    /// </summary>
    public class CEGenericHandler : IHttpHandler
    {
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public virtual void ProcessRequest(HttpContext context)
        {
        }

        /// <summary>
        /// Gets the XML text writer.
        /// </summary>
        /// <param name="openingTag">The opening tag.</param>
        /// <returns>The XmlTextWriter</returns>
        protected XmlTextWriter GetXmlTextWriter(string openingTag)
        {
            return XMLHelper.GetXmlTextWriter(openingTag);
        }

        /// <summary>
        /// Gets the XML document.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <returns>The XmlDocument</returns>
        protected XmlDocument GetXmlDocument(XmlTextWriter writer)
        {
            return XMLHelper.GetXmlDocument(writer);
        }

        /// <summary>
        /// Writes the XML transform.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="xslPath">The XSL path.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="transformIfEmpty">if set to <c>true</c> [transform if empty].</param>
        /// <param name="extensionUrn">The extension urn (urn: prepended automatically)</param>
        /// <param name="extension">The extension.</param>
        protected void WriteXMLTransform(XmlDocument data, string xslPath, XsltArgumentList arguments, bool transformIfEmpty, string extensionUrn, object extension)
        {
            if (arguments == null)
            {
                arguments = new XsltArgumentList();
            }

            if (extension != null)
            {
                arguments.AddExtensionObject(string.Format("urn:{0}", extensionUrn), extension);
            }

            if (transformIfEmpty || data.DocumentElement.ChildNodes.Count > 0)
            {
                XslCompiledTransform tx = new XslCompiledTransform();
                tx.Load(HttpContext.Current.Server.MapPath(xslPath));
                tx.Transform(data, arguments, HttpContext.Current.Response.OutputStream);
            }
            else
            {
                HttpContext.Current.Response.Write("<div></div>"); // write out an empty so that the clientside ajax object doesn't error 
            }
        }

        /// <summary>
        /// Writes the XML transform.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="xslPath">The XSL path.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="transformIfEmpty">if set to <c>true</c> [transform if empty].</param>
        protected void WriteXMLTransform(XmlDocument data, string xslPath, XsltArgumentList arguments, bool transformIfEmpty)
        {
            this.WriteXMLTransform(data, xslPath, arguments, transformIfEmpty, null, null);
        }

        /// <summary>
        /// Writes the XML transform.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="xslPath">The XSL path.</param>
        /// <param name="transformIfEmpty">if set to <c>true</c> [transform if empty].</param>
        protected void WriteXMLTransform(XmlDocument data, string xslPath, bool transformIfEmpty)
        {
            this.WriteXMLTransform(data, xslPath, null, transformIfEmpty);
        }

        /// <summary>
        /// Gets the form parameter.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="formFieldName">Name of the form field.</param>
        /// <param name="parameterFound">if set to <c>true</c> [parameter found].</param>
        /// <returns>The value as TValue</returns>
        protected TValue GetFormParameter<TValue>(string formFieldName, out bool parameterFound)
        {
            TValue result = default(TValue);

            string rawData = HttpContext.Current.Request.Form[formFieldName];

            if (string.IsNullOrEmpty(rawData))
            {
                rawData = HttpContext.Current.Request.QueryString[formFieldName];
            }

            parameterFound = false;

            if (!string.IsNullOrEmpty(rawData))
            {
                parameterFound = true;

                IConvertible convertableString = rawData as IConvertible;

                if (convertableString != null)
                {
                    string type = default(TValue) == null ? "System.String" : default(TValue).GetType().ToString();

                    switch (type)
                    {
                        case "System.Int32":
                            int resultInt = 0;
                            int.TryParse(convertableString.ToString(), out resultInt);

                            result = (TValue)(object)resultInt;
                            break;
                        case "System.Guid":
                            try
                            {
                                result = (TValue)(object)new Guid(convertableString.ToString());
                            }
                            catch (Exception)
                            {
                                result = (TValue)(object)Guid.Empty;
                            }

                            break;
                        default:
                            result = (TValue)convertableString.ToType(typeof(TValue), System.Globalization.CultureInfo.CurrentCulture);
                            break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the form parameter.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="formFieldName">Name of the form field.</param>
        /// <returns>The parameter as TValue</returns>
        protected TValue GetFormParameter<TValue>(string formFieldName)
        {
            bool dummyFlag = false;
            return this.GetFormParameter<TValue>(formFieldName, out dummyFlag);
        }
    }
}