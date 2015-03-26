<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnorderedCheckBoxListTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.UnorderedCheckBoxListTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Main" runat="server">
        Not Selectable
        <ce:UnorderedCheckBoxList ID="UCBL" runat="server" />        
        Selectable
        <ce:UnorderedCheckBoxListSelectable ID="UCBL2" runat="server" />
        Selectable Test 2
        <ce:UnorderedCheckBoxListSelectable ID="UCBL3" runat="server" />
    </asp:Panel>
    </form>
</body>
</html>
