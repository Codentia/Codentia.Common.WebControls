<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactUsTest.aspx.cs" Inherits="Codentia.Common.WebControls.Test.ContactUsTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ContactUs Test Page</title>
        <style type="text/css">    
        .ContactUsSample
        {
            width: 750px;
            height: 300px;
            border: solid 1px black;
        }    
        
        .ContactUsSample ul
        {
            display: inline;   
            list-style-type: none;
            width: 550px;
        }

        .ContactUsSample li
        {
            width: 600px;
        }
            
        .ContactUsSample System.Web.UI.WebControls.Label
        {
            width: 140px;
            float:left;
        }

        .ContactUsSample div div
        {
            width: 120px;
            float:left;
        }
        
        .ContactUsSample span
        {
            width: 10px;
            float: left;
        }
            
        .ContactUsButton
        {
            width: 100px;
            float: left;
        }

        .ContactUsControls input
        {
            width: 350px;
            float:left; 
        }

        .ContactUsSample a
        {
            width: 100px;
            float: left;
        }
        


        .ContactUsSample textarea
        {
            width: 350px;
            height: 150px;
            
        }
        
        .ContactUsThanks
        {
            border: solid 1px red;
            width: 550px;
            height: 100px;        
            float:left;
        }
        
        .ContactUsValidation
        {
            border: solid 1px green;
            width: 215px;
            height: 295px;        
            float:left;
        }

        .ContactUsValidation span
        {
            width: 200px;
            height: 395px;        
        }
        
        .ContactUsControls
        {
            border: solid 1px blue;
            width: 530px;
            height: 295px;        
            float:left;
        }
        
        .ContactUsButton
        {
            width: 100px;
        }
        </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        No dynamic sections
        <ce:ContactUs ID="ContactUs1" runat="server" CssClass="ContactUsSample" EmailTo="test@mattchedit.com"/>
        <br /><hr /><br />
        With FooterTemplate
        <br />
        <ce:ContactUs ID="ContactUs5" runat="server" CssClass="ContactUsSample" EmailTo="test@mattchedit.com">
            <FooterTemplate>
                <asp:LinkButton ID="dynamicPageLink" runat="server" CommandName="send">Use my link to send</asp:LinkButton>
            </FooterTemplate>
        </ce:ContactUs>
        With FooterTemplate and ContactFields
        <br />
        <ce:ContactUs ID="ContactUs2" runat="server" CssClass="ContactUsSample" EmailTo="test@mattchedit.com">
            <ContactFields>
                <ce:ContactUsField Type="TextBox" Label="Email" EmailAs="email" EmailType="Email"/>     
                <ce:ContactUsField Type="Hidden" EmailType="Subject" DefaultValue="Newsletter Subscribe" />       
                <ce:ContactUsField Type="TextBox" Label="First" EmailAs="first" />            
            </ContactFields>
        </ce:ContactUs>
        <br /><hr /><br />
    </div>
    </form>
</body>
</html>
