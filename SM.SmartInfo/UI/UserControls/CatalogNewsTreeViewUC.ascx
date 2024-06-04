<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CatalogNewsTreeViewUC.ascx.cs" 
    Inherits="SM.SmartInfo.UI.UserControls.CatalogNewsTreeViewUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>
<tk:TreeViewExtended ID="tvCatalogNews" runat="server" SelectedNodeStyle-BackColor="#CCCCCC"
    OnSelectedNodeChanged="tvCatalogNews_SelectedNodeChanged" AutoPostBack="true">
</tk:TreeViewExtended>
