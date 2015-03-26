<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlGridTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.ControlGridTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ControlGrid Test Page</title>
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Simple grid (bound to a DataTable):
        <br /><br />
    <div style="width:100%; height:100px;">
        <ce:ControlGrid ID="controlGrid1" runat="server" style="width:843px;" CssClass="grdtable" RowCssClass="grdrow" HeaderCssClass="grdheader" AlternateRowCssClass="zebrastripe" LastRowCssClass="last" FirstColumnCssClass="first">
            <Columns>
                <ce:ControlGridColumn ColumnName="Column 1" Binding="Col1" CssClass="width200"/>
                <ce:ControlGridColumn ColumnName="Column 2" Binding="Col2" CssClass="width200" />
                <ce:ControlGridColumn ColumnName="Column 3" Binding="Col3" CssClass="width200" />
                <ce:ControlGridColumn ColumnName="Column 4" Binding="Col4" CssClass="width200" />
            </Columns>            
        </ce:ControlGrid>
    </div>
        <br /><hr /><br />
        Grid with a link in each row (e.g. edit link):
        <br /><br />
    <div style="width:100%; height:100px;">
        <ce:ControlGrid ID="controlGrid2" runat="server" style="width:1054px;" CssClass="grdtable" RowCssClass="grdrow" HeaderCssClass="grdheader" AlternateRowCssClass="zebrastripe" LastRowCssClass="last" FirstColumnCssClass="first">
            <Columns>
                <ce:ControlGridColumn ColumnName="Column 1" Binding="Col1" CssClass="width200" FormatString="F2" DataType="Int" />
                <ce:ControlGridColumn ColumnName="Column 2" Binding="Col2" CssClass="width200" FormatString="p" DataType="Int" />
                <ce:ControlGridColumn ColumnName="Column 3" Binding="Col3" CssClass="width200" />
                <ce:ControlGridColumn ColumnName="Column 4" Binding="Col4" CssClass="width200" />
                <ce:ControlGridColumn ColumnName="Edit" Text="Edit row" ColumnType="LinkButton" OnColumnLinkClicked="controlGrid2_EditClick" CssClass="width200" />
            </Columns>            
        </ce:ControlGrid>
    </div>
        <br />
        <asp:Label ID="selectedIndex1" runat="server" Text="Seleted Row: None"></asp:Label>
        <br /><hr /><br />
        Grid bound to an array of objects (not a pure DataTable):
        <br /><br />
    <div style="width:100%; height:100px;">
        <ce:ControlGrid ID="controlGrid3" runat="server" style="width:1040px;" CssClass="grdtable" RowCssClass="grdrow" HeaderCssClass="grdheader" AlternateRowCssClass="zebrastripe" LastRowCssClass="last" FirstColumnCssClass="first">
            <Columns>
                <ce:ControlGridColumn ColumnName="A" Binding="Property1" ColumnType="ObjectBound" CssClass="width200" />
                <ce:ControlGridColumn ColumnName="B" Binding="Property2" ColumnType="ObjectBound" CssClass="width200" />
                <ce:ControlGridColumn ColumnName="C" Binding="Property3" ColumnType="ObjectBound" CssClass="width200" />
                <ce:ControlGridColumn ColumnName="D" Binding="Property4" ColumnType="ObjectBound" CssClass="width200" />
                <ce:ControlGridColumn ColumnName="Array (E)" Binding="Property5" ColumnType="ObjectBound" CssClass="width100" />
                <ce:ControlGridColumn ColumnName="Edit" Text="Edit row" ColumnType="LinkButton" OnColumnLinkClicked="controlGrid3_EditClick" CssClass="width75" />
            </Columns>            
        </ce:ControlGrid>
    </div>
        <br />
        <asp:Label ID="selectedIndex2" runat="server" Text="Seleted Row: None"></asp:Label>
    </div>
    </form>
</body>
</html>
