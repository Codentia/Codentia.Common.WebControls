<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IdempotentLinkAndButtonTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.IdempotentLinkButtonTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ce:IdempotentLinkButton ID="idempotentLinkButton1" runat="server" Text="link1 - no custom js" OnClick="idempotentLink_Click"></ce:IdempotentLinkButton>
        <br /><br />
        <ce:IdempotentLinkButton ID="idempotentLinkButton2" runat="server" Text="link2 - custom js" OnClick="idempotentLink_Click" OnClientClick="alert('link2 clicked');"></ce:IdempotentLinkButton>
        <br /><br />
        <ce:IdempotentButton ID="idempotentButton1" runat="server" Text="button1 - no custom js" OnClick="idempotentLink_Click"></ce:IdempotentButton>
        <br /><br />
        <ce:IdempotentButton ID="idempotentButton2" runat="server" Text="button2 - custom js" OnClick="idempotentLink_Click" OnClientClick="alert('button2 clicked');"></ce:IdempotentButton>
    </div>
    <br /><br />
    <div>
        With validators
        <br /><br />
        <asp:TextBox ID="TextBox1" runat="server" ValidationGroup="Test1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ValidationGroup="Test1" ErrorMessage="Required">*</asp:RequiredFieldValidator>
        <ce:IdempotentLinkButton ID="IdempotentLinkButton3" runat="server" OnClick="idempotentLink_WithValidation_Click" CausesValidation="true" ValidationGroup="Test1">Save</ce:IdempotentLinkButton>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Test1" />
    </div>
    </form>
</body>
</html>
