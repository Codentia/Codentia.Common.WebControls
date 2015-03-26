<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CEValidationSummaryTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.CEValidationSummaryTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ControlToValidate="TextBox1" ErrorMessage="TextBox Is Required" ValidationGroup="Test"  EnableClientScript="false"></asp:RequiredFieldValidator>
    
    <br />

    <cev:CEValidationSummary Id="VS" runat="server" ValidationGroup="Test" />
    <br />
    
    <asp:Button ID="Button1" runat="server" Text="Test PostBack" OnClick="Button1_Click"  CausesValidation="true" ValidationGroup="Test" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text=string.Empty></asp:Label>
        
    </div>
       
    </form>
    
</body>
</html>
