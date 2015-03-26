// IE7 appears to not have this!
var applyCompatibility = false;

/* Ensure Array.indexOf is available */
if (!Array.prototype.indexOf)
{
    applyCompability = true;

  Array.prototype.indexOf = function(elt /*, from*/)
  {
    var len = this.length;

    var from = Number(arguments[1]) || 0;
    from = (from < 0)
         ? Math.ceil(from)
         : Math.floor(from);
    if (from < 0)
      from += len;

    for (; from < len; from++)
    {
      if (from in this &&
          this[from] === elt)
        return from;
    }
    return -1;
  };
}


function removeChildIE7Fix(parentNode, childNode)
{
    if(applyCompatibility)
    {
        childNode.outerHTML=' ';
        //alert('rci7');
    }
    else    
    {
        parentNode.removeChild(childNode);
        //alert('rc_normal');
    }
}

// Xml Methods
function ConvertXMLStringToDoc(xmlstring)
{
    if (GetBrowserType()=='moz')
    { 
         // Mozilla, Firefox, and related browsers   
        return (new DOMParser()).parseFromString(xmlstring, "application/xml");   
     }   
     else 
     { 
         // Internet Explorer. 
        var doc = new ActiveXObject("MSXML2.DOMDocument");  // Create an empty document   
        doc.loadXML(xmlstring);                             // Parse text into it   
        return doc;  
     }
}

function GetBrowserType()
{
    var retVal='ie'
    if (typeof DOMParser != "undefined")
    {
        retVal='moz'
    }
    return retVal;
}


function GetTopNode(xDoc, tagName)
{   
    if (GetBrowserType()=='ie')
        {
            tagName=tagName.toUpperCase();            
        }
    
    return xDoc.documentElement.getElementsByTagName(tagName)[0];

}

// function for fixing 'autocomplete' issues - currently only supports IE6/7
function SubmitAutoCompleteData()
{
    try
    {
        window.external.AutoCompleteSaveForm(document.getElementById('aspnetForm'));
    }
    catch(exception)
    {
    }
}

function GetAttributeForElement(elementNode, attributeName)
{

    if (GetBrowserType()=='ie')
    {
        return elementNode.getAttribute(attributeName);
    }

    return elementNode.getAttribute(attributeName.toLowerCase());
}


