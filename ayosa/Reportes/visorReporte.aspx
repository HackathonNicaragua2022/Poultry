<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/master-admin.Master" AutoEventWireup="true" CodeBehind="visorReporte.aspx.cs" Inherits="POULTRY.Reportes.visorReporte" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>REPORTE</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <br />    
    <dx:ASPxDocumentViewer ID="visorReportes" runat="server" Theme="PlasticBlue" ToolbarMode="Ribbon">
        <SettingsReportViewer EnableRequestParameters="False" PrintUsingAdobePlugIn="true" />
        <SettingsParametersPanelCaption HorizontalAlign="Right" />
    </dx:ASPxDocumentViewer>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="debajoScripts" runat="server">
</asp:Content>
