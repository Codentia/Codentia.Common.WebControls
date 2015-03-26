function ExecuteJavascriptWithIdempotency(element, scriptToRun)
{
    if(!element.disabled)
    {
        // hook into autocomplete functionality
        SubmitAutoCompleteData();
    
        var oldHref = '';
        
        element.disabled = true;
    
        if(element.href)
        {
            oldHref = element.href;
            element.href = "javascript:void(0);";
        }
        
        window.setTimeout("ReEnableElement('"+element.id+"', \""+oldHref+"\");", 5000);

        eval(scriptToRun);
    }
}

function ReEnableElement(elementId, oldHref)
{
    var elem = document.getElementById(elementId);
    
    if(elem != null)
    {
        elem.disabled = false;
        elem.href = oldHref;
    }
}
