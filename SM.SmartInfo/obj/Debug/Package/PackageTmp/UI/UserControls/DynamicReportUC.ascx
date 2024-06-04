<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicReportUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.DynamicReportUC" %>

<%@ Register Assembly="SMSR" Namespace="SoftMart.Service.Reporting" TagPrefix="uc" %>

<uc:ReportViewer ID="ucDynamicReport" runat="server" AutoLoadData="true" ShowPagerText="true" />