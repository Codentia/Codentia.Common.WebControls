using System;
using System.Configuration;
using System.Web.UI;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// This class provides a simple interface to the google map control
    /// </summary>
    public class GoogleMap : CECompositeControl
    {
        private string _mapCentre;
        private string _centreTitle = string.Empty;
        private bool _markCentre;
        private string _mapDivClientId;

        /// <summary>
        /// Sets a value indicating whether [place marker at centre].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [place marker at centre]; otherwise, <c>false</c>.
        /// </value>
        public bool PlaceMarkerAtCentre
        {
            set
            {
                _markCentre = value;
            }
        }

        /// <summary>
        /// Sets the centre coordinates.
        /// </summary>
        /// <value>
        /// The centre coordinates.
        /// </value>
        public string CentreCoordinates
        {
            set
            {
                _mapCentre = value;
            }
        }

        /// <summary>
        /// Sets the centre marker title.
        /// </summary>
        /// <value>
        /// The centre marker title.
        /// </value>
        public string CentreMarkerTitle
        {
            set
            {
                _centreTitle = value;
            }
        }

        /// <summary>
        /// Sets the ClientId of the div to which the map should be attached
        /// </summary>
        public string MapDivClientId
        {
            set
            {
                _mapDivClientId = value;
            }
        }

        /// <summary>
        /// Handle Pre-Render activities
        /// </summary>
        /// <param name="e">The Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["GoogleAPIKey"]))
            {
                throw new Exception("GoogleMap control can only be used when GoogleAPIKey configuration property has been set.");
            }

            string googleMapUrl = "https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false";
            Page.ClientScript.RegisterClientScriptInclude("GoogleMap", googleMapUrl);

            string googleMapScript = "function LoadGoogleMap() {{" +
                "var mapOptions = {{" +
                "zoom: 17," +
                "center: new google.maps.LatLng({1})," +
                "mapTypeId: google.maps.MapTypeId.ROADMAP" +
            "}};" +
            "map = new google.maps.Map(document.getElementById('{0}'), mapOptions); {2}" +
            "}}";

            string centreMarker = string.Empty;

            if (_markCentre)
            {
                centreMarker = string.Format("var marker = new google.maps.Marker({{position: new google.maps.LatLng({0}), map: map, title: '{1}'}});", _mapCentre, _centreTitle);
            }

            googleMapScript = string.Format(googleMapScript, _mapDivClientId, _mapCentre, centreMarker);

            Page.ClientScript.RegisterClientScriptBlock(typeof(GoogleMap), "GoogleMapLauncher", googleMapScript, true);
            Page.ClientScript.RegisterStartupScript(typeof(GoogleMap), "GoogleMapStartup", "try { LoadGoogleMap(); } catch(exception) { alert(exception.description); }", true);
        }
    }
}
