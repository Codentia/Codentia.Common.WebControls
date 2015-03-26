<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeDropDownTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.TimeDropDownTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TimeDropDown Test Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ce:TimeDropDown ID="TimeDropDown1" runat="server" />
        <br /><br />
        <asp:Button ID="postbackButton" runat="server" Text="Save" OnClick="postbackButton_Click"/>
        <br /><br />
        Postback values: <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
