<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CookiePermissionTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.CookiePermissionTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ce:CookiePermission ID="CookiePermission1" runat="server" CssClass="CP">
            <Message>
                <p>This page doesn't really use cookies. It's a test page. So ner.</p>
            </Message>
        </ce:CookiePermission>
    </form>
</body>
</html>
