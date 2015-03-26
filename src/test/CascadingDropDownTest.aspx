<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CascadingDropDownTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.CascadingDropDown" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">    
    <asp:Panel ID="MYPANELTEST" runat="server">
    <ce:CascadingDropDown runat="server" ID="cdd">  
         <DropDowns>
              <ce:DropDown ID="DDL1" LabelText="Computer"/>
              <ce:DropDown ID="DDL2" LabelText="Hard Drive Manufacturer" ParentDropDownID="DDL1"/>
              <ce:DropDown ID="DDL3" LabelText="Hard Drive Size" ParentDropDownID="DDL2"/>
         </DropDowns> 
         <DropDownItems>
             <ce:DropDownItem DropDownID="DDL1" ItemValue="1" ItemText="Laptop"/>
             <ce:DropDownItem DropDownID="DDL1" ItemValue="2" ItemText="Desktop"/> 
             <ce:DropDownItem DropDownID="DDL2" ItemValue="1" ItemText="Toshiba (AAAAGH)" ParentItemValue="1"/>
             <ce:DropDownItem DropDownID="DDL2" ItemValue="2" ItemText="Sony" ParentItemValue="1"/>
             <ce:DropDownItem DropDownID="DDL2" ItemValue="3" ItemText="Seagate" ParentItemValue="2"/>
             <ce:DropDownItem DropDownID="DDL2" ItemValue="4" ItemText="Western Digital" ParentItemValue="2"/> 
             <ce:DropDownItem DropDownID="DDL3" ItemValue="1" ItemText="200GB" ParentItemValue="1"/>
             <ce:DropDownItem DropDownID="DDL3" ItemValue="2" ItemText="250GB" ParentItemValue="1"/>
             <ce:DropDownItem DropDownID="DDL3" ItemValue="3" ItemText="300GB" ParentItemValue="2"/>
             <ce:DropDownItem DropDownID="DDL3" ItemValue="4" ItemText="350GB" ParentItemValue="2"/>
             <ce:DropDownItem DropDownID="DDL3" ItemValue="5" ItemText="400GB" ParentItemValue="3"/>
             <ce:DropDownItem DropDownID="DDL3" ItemValue="6" ItemText="450GB" ParentItemValue="3"/>
             <ce:DropDownItem DropDownID="DDL3" ItemValue="7" ItemText="500GB" ParentItemValue="4"/>
             <ce:DropDownItem DropDownID="DDL3" ItemValue="8" ItemText="1TB" ParentItemValue="4"/>                     
           </DropDownItems>                
    </ce:CascadingDropDown>
    </asp:Panel>
    <input id="Button1" type="button" value="button" onclick="alert('blah');Test();" runat="server" />
         
    </form>
    
</body>
</html>
