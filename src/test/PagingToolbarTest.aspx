<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagingToolbarTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.PagingToolbarTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: solid 1px black;">
        Options
        <br /><br />
        <asp:CheckBox ID="HideFirstPageControl" runat="server" Text="HideFirstPageControl" Checked="false"/>
        <br />
        <asp:CheckBox ID="HidePreviousPageControl" runat="server" Text="HidePreviousPageControl" Checked="false"/>
        <br />
        <asp:CheckBox ID="HideNextPageControl" runat="server" Text="HideNextPageControl" Checked="false" />
        <br />
        <asp:CheckBox ID="HideLastPageControl" runat="server" Text="HideLastPageControl" Checked="false"/>
        <br />
        <asp:CheckBox ID="IsItemBased" runat="server" Text="IsItemBased" Checked="false" />
        <br />
        <asp:CheckBox ID="HidePageNumberLabel" runat="server" Text="HidePageNumberLabel" Checked="false" />
        <br />
        <asp:CheckBox ID="UseImages" runat="server" Text="UseImages" Checked="false" />
        <br />
        Total Pages <asp:TextBox ID="totalPages" runat="server" Text="10"></asp:TextBox>
        <br />
        Total Items <asp:TextBox ID="totalItems" runat="server" Text="100"></asp:TextBox>
        <br />
        <asp:Button ID="updateControl" runat="server" Text="Update" OnClick="updateControl_Clicked" />
    </div>
    <br /><br />
    <div>    
        <ce:PagingToolbar ID="PagingToolbar1" runat="server" BeginningControlText="First" PreviousControlText="Previous" NextControlText="Next" EndControlText="Last" />
        <br /><br />
        <ce:PagingToolbar ID="PagingToolbar2" runat="server" BeginningControlText="First" PreviousControlText="Previous" NextControlText="Next" EndControlText="Last">
            <ItemsPerPageOptions>
                <asp:ListItem Text="1" Value="1" />
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="10" Value="10" />
            </ItemsPerPageOptions>
        </ce:PagingToolbar>
    </div>
    </form>
</body>
</html>
