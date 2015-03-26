<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabelledControlListTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.LabelledControlListTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style>
        .StyledList ul
        {
            list-style-type: none;
        }    

        .StyledList ul li
        {
            padding-left: 0px;
            margin-left: 0px;
            width: 300px;
        }
        
        .StyledList ul label
        {
            display:block;
            width: 100px;
            margin-right: 10px;
            float: left;
        }
        
        .StyledList ul input
        {
            width: 180px;
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ce:LabelledControlList Id="LabelledControlList1" runat="server">
        </ce:LabelledControlList>
        <br /><br />
        <ce:LabelledControlList Id="LabelledControlList2" runat="server" CssClass="StyledList">
        </ce:LabelledControlList>
        
        <!-- need to add a test with validator here -->
    </div>
    </form>
</body>
</html>
