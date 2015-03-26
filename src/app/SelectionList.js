function getItemIndex(node)
{
    var parts = node.id.split('_');
    return parts[parts.length-1];
}

 function moveFromChangesToCandidates() 
 { 
    addList.removeChild(this.parentNode);

    var index = removeFromSelection(this.firstChild.nodeValue);

    var inserted = false;

    for(i=0;i<optionsList.childNodes.length;i++)
    {
        //alert('search: i='+index+', t='+getItemIndex(optionsList.childNodes[i].firstChild));
    
        if(index < getItemIndex(optionsList.childNodes[i].firstChild))
        {        
            optionsList.insertBefore(this.parentNode, optionsList.childNodes[i]);
            inserted = true;
            break;
        }        
    }

    if(!inserted)
    {
        optionsList.appendChild(this.parentNode); 
    }
    
    this.onclick=moveFromCandidatesToChanges; 

    
    return false; 
 }
 
 function moveFromCandidatesToChanges() 
 {
    var index = getItemIndex(this);    
    
    optionsList.removeChild(this.parentNode); 

    var inserted = false;

    for(i=0;i<addList.childNodes.length;i++)
    {
        //alert('search: i='+index+', t='+getItemIndex(optionsList.childNodes[i].firstChild));
    
        if(index < getItemIndex(addList.childNodes[i].firstChild))
        {        
            addList.insertBefore(this.parentNode, addList.childNodes[i]);
            inserted = true;
            break;
        }        
    }

    if(!inserted)
    {
        addList.appendChild(this.parentNode); 
    }    
    
    this.onclick=moveFromChangesToCandidates; 

    addToSelection(index, this.firstChild.nodeValue);

    return false; 
}
 
 function addToSelection(index, value) 
 {             
    selectedIndexes = (selectedIndexes.length > 0 ? selectedIndexes + ','+index : index); 
    selectedValues = (selectedValues.length > 0 ? selectedValues + ','+value : value); 
    selection.value = selectedIndexes;

    //alert(selection.value);
    //alert(selectedIndexes);
    //alert(selectedValues);
 }
 
 function removeFromSelection(value) 
 { 
    //alert('remove: ' + value);
    
    var indexes = new Array(); 
    var values = new Array(); 
    
    indexes = selectedIndexes.split(',');     
    values = selectedValues.split(','); 
    
    var index = values.indexOf(value); 
    var result = indexes[index];
    
    indexes.splice(index, 1); 
    values.splice(index, 1); 
    
    selectedIndexes=''; 
    selectedValues = ''; 
    
    for(i=0;i<indexes.length;i++) 
    { 
        addToSelection(indexes[i], values[i]); 
    } 
    
    //alert(selectedIndexes);
    //alert(selectedValues);
    
    return result;
 }
 
 function filterCandidates(filterValue)
 {
    //alert(filterValue);
 
    for(i=0;i<optionsList.childNodes.length;i++)
    {
        // li, link, text node        
        if(filterValue.length > 0 && optionsList.childNodes[i].firstChild.firstChild.nodeValue.toLowerCase().indexOf(filterValue) == -1)
        {
            optionsList.childNodes[i].style.visibility = 'hidden';
            optionsList.childNodes[i].style.display = 'none';
        }
        else        
        {
            optionsList.childNodes[i].style.visibility = '';
            optionsList.childNodes[i].style.display = '';
        }
    }  
 }