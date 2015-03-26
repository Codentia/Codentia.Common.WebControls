function RefreshDDLs(ddl, ddlServerId)
{
    //ddls are in a list control with prefix ctl00_ - needs to remove that from prefix
    var prefix=ddl.id.replace('ctl00_' + ddlServerId, '');

    var xml=document.getElementById(prefix + 'X'); //get xml Area
    var xDoc=ConvertXMLStringToDoc(xml.innerHTML);
    var summaryNode=GetTopNode(xDoc, 'summary')
    var summaryNodeByPos=GetTopNode(xDoc, 'summarybypos')
    var summaryNodeByName=GetTopNode(xDoc, 'summarybyname')
    var summaryNodeOfChildren=GetTopNode(xDoc, 'summaryofchildren')    

    var ddlCount=parseFloat(GetAttributeForElement(summaryNode, 'ddlcount'));    
    var currentDDLPos =parseFloat(GetAttributeForElement(summaryNodeByName, ddlServerId));   
    
    // if the final ddl is used do nothing
    if (currentDDLPos == ddlCount-1)
    {
        return true;
    }

    // clear the child DDLs
    for (i=currentDDLPos + 1;i<ddlCount;i++)
    {            
        var ddlName=prefix + 'ctl00_' + GetAttributeForElement(summaryNodeByPos, 'pos' + i);
        RemoveAllOptionsFromDDL(ddlName);            
    }

    var currentDDLValue=GetDDValue(ddl.id);
    var currentDDLName=ddlServerId.replace('ddl_', '');
    
    var childDDLName=prefix + 'ctl00_' + GetAttributeForElement(summaryNodeOfChildren, currentDDLName);
    var childDDLSearchNode= GetAttributeForElement(summaryNodeOfChildren, currentDDLName).replace('ddl_', '');        
    
    //var xpath='ddlitem[@ddlparentid="' + currentDDLName + '" and  @ddlparentvalue="' + currentDDLValue + '"]';
    
    // NOTE: consider aborting if 'currentDDLValue' is empty - no point searching if we can never find something
    
    //var xpath=itemElementName + '[@ddlid="' + childDDLSearchNode + '" and  @ddlparentvalue="' + currentDDLValue + '"]';
    //alert(xpath);
    var nodes=GetMatchingNodes(xDoc.documentElement, childDDLSearchNode, currentDDLValue);
         
    var ddlArray=new Array();
    
    for (j=0;j<nodes.length;j++)
    {
        var node=nodes[j];        
        ddlArray[j]=GetAttributeForElement(node, 'ddltext') + '~' + GetAttributeForElement(node, 'ddlvalue');         
    }   
    
    PopulateDDLWithArray(childDDLName, ddlArray);   
    
    //refresh children
    var ddlChild=document.getElementById(childDDLName);
    
    RefreshDDLs(ddlChild, 'ddl_' + childDDLSearchNode) 
}


function RemoveAllOptionsFromDDL(ddlName)
  {
      var obj=document.getElementById(ddlName)
      while (obj.selectedIndex!=-1)
      {
        obj.remove(obj.selectedIndex)
      }
  }
  
function GetDDValue(ddlName)
{    
    var dd=document.getElementById(ddlName);
    var returnString='';
    if (dd.selectedIndex!=-1)
    {
        returnString=dd.options[dd.selectedIndex].value;
    }
    return returnString;
}   



function GetNewOption(displaytext, value)
{  
    var opt = document.createElement('option');
    opt.text = displaytext;
    opt.value = value;
    return opt;
}  

function PopulateDDLWithArray(ddlName, itemarray)
{
    var ddl=document.getElementById(ddlName);
    
    if (itemarray.length>0)
        {    
            var i;
            var option;
            if (ddl)
            {        
                for (i=0; i<itemarray.length; i++)
                 {
                    var list = itemarray[i].split('~');                    
                    value=list[1];
                    displaytext=list[0];    
                   
                    //ie
                    if (document.createEventObject)     
                        {     
                            option=GetNewOption(displaytext, value);                          
                            ddl.add(option, i);    
                        }
                    else
                        // non-ie
                        {
                            option=GetNewOption(displaytext, value);
                            ddl.add(option, null);     
                            option=null;
                        }      
                 }
            }                
        }            
} 

function GetMatchingNodes(node, ddlId, parentDDLValue)
{
        var resultsArray=new Array();
        var count=0;
        var itemElementName='ddlitem';
        if (GetBrowserType()=='ie')
        {
            itemElementName='DDLITEM';
        }
        var nodeList=node.getElementsByTagName(itemElementName);
        
        if (nodeList.length>0)
        {
            for (i=0;i<nodeList.length;i++)
            {
                var childNode=nodeList[i];            
                if (childNode.nodeName==itemElementName)
                {              
                    if ((childNode.getAttribute('ddlid')==ddlId) && (childNode.getAttribute('ddlparentvalue')==parentDDLValue))
                    {
                        resultsArray.push(childNode);
                    }                  
                }
            }
        }
               
        return resultsArray
}

