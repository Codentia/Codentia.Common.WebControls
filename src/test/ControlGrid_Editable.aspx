<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlGrid_Editable.aspx.cs" Inherits="Codentia.Common.WebControls.Test.ControlGrid_Editable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Editable control grid test page</title>
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:100%; height:200px;">
            <ce:ControlGrid ID="controlGrid1" runat="server" style="width:843px;" CssClass="grdtable" RowCssClass="grdrow" HeaderCssClass="grdheader" AlternateRowCssClass="zebrastripe" LastRowCssClass="last" FirstColumnCssClass="first" FooterRowCssClass="footer" TotalCssClass="total" OnSaveChanges="controlGrid1_SaveChanges" HasTotalRow="true">
                <Columns>
                    <ce:ControlGridColumn ColumnName="Column 1" Binding="Col1" CssClass="width200" Editable="false" DataType="Decimal" />
                    <ce:ControlGridColumn ColumnName="Column 2" Binding="Col2" CssClass="width200" DataType="Int" />
                    <ce:ControlGridColumn ColumnName="Column 3" Binding="Col3" CssClass="width200" DataType="Int" HasTotal="false" />
                    <ce:ControlGridColumn ColumnName="Column 4" Binding="Col4" CssClass="width200" DataType="Time" FormatString="HH:mm" DefaultValue="-">
                        <Parameters>
                            <ce:ControlGridColumnParameter Name="hours" Value="1,2,3,4,5,6" />
                            <ce:ControlGridColumnParameter Name="minutes" Value="10,20,30,40,50" />
                        </Parameters>
                    </ce:ControlGridColumn>
                </Columns>            
            </ce:ControlGrid>
        </div>    
        <br /><br />
        <asp:Button ID="Button1" runat="server" Text="Edit Values" OnClick="Button1_Click" />
    </form>
</body>
</html>
