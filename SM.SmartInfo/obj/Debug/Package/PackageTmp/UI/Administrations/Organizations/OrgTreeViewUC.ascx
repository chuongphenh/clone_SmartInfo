<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrgTreeViewUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Organizations.OrgTreeViewUC" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>
<tk:TreeViewExtended ID="tvOrg" runat="server" SelectedNodeStyle-BackColor="#CCCCCC"
    OnSelectedNodeChanged="tvOrg_SelectedNodeChanged" AutoPostBack="true">
</tk:TreeViewExtended>
