<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectionListTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.SelectionListTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style>
    .list
    {
        border: solid 1px black;        
    }
    
    .list label
    {
        margin-right: 20px;
    }
    
    .list input
    {
        width: 150px;
    }
    
    .list p a
    {
    margin-right: 30px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="900"></asp:ScriptManager>
        <asp:UpdateProgress ID="updateProgress1" runat="server">
            <ProgressTemplate>
                <div style="float:right; color:White; background-color: Red;">Loading...</div>
            </ProgressTemplate>
        </asp:UpdateProgress>
                <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
            <ContentTemplate>
                Basic list with static items (in updatepanel):
                <br />
                <ce:SelectionList ID="selectionList2" runat="server" CssClass="list" CandidateTitle="Candidates" SelectionTitle="Selections" OnSelectedIndexChanged="selectionList2_selectedIndexChanged" OnSelectionCancelled="selectionList2_selectionCancelled">
                    <Items>
                        <ce:SelectionListItem Description="One" />
                        <ce:SelectionListItem Description="Two" />
                        <ce:SelectionListItem Description="Three" />
                        <ce:SelectionListItem Description="Four"/>
                        <ce:SelectionListItem Description="Five"/>
                        <ce:SelectionListItem Description="Six"/>
                    </Items>
                </ce:SelectionList>
                <br />
                <asp:Label ID="messageLabel2" runat="server" Text="No messages"></asp:Label>
                <br /><br />                
            </ContentTemplate>
        </asp:UpdatePanel>
            
    </form>
</body>
</html>
