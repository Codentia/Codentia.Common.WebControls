function CheckBoxListSelect(id, state)
{    
    var chkBoxList=document.getElementById(id);                 
    var chkBoxCount= chkBoxList.getElementsByTagName("input");
    
    for(var i=0;i<chkBoxCount.length;i++)
    {
        chkBoxCount[i].checked = state;                
    }   

    return false;
}