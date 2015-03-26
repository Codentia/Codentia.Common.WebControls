<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LightboxManagerTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.LightboxManagerTest"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>LightboxManager Test Page</title>
    <link href="css/lightbox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <MIT:LightboxManager ID="LightboxManager1" runat="server" Label="Picture" LoadingImage="~/images/lightbox/loading.gif" CloseImage="~/images/lightbox/closelabel.gif"/>
        
        <asp:Panel ID="TestPanel1" runat="server"></asp:Panel>    
    </form>
</body>
</html>

