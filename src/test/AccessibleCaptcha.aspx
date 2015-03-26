<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessibleCaptcha.aspx.cs" Inherits="Codentia.Common.WebControls.Test.AccessibleCaptcha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AccessibleCaptcha Test Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ce:AccessibleCaptcha ID="AccessibleCaptcha1" runat="server" />
        <br />
        <asp:LinkButton ID="submitLink" runat="server" CausesValidation="true" OnClick="submitLink_Click">Submit</asp:LinkButton>
        
        <br /><br />
        <asp:Label ID="resultsLabel" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
