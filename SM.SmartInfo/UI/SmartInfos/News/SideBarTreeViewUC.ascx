<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBarTreeViewUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.News.SideBarTreeViewUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<tk:TreeViewExtended ID="tvNews" runat="server" SelectedNodeStyle-BackColor="#CCCCCC"
    OnSelectedNodeChanged="tvNews_SelectedNodeChanged" AutoPostBack="true"></tk:TreeViewExtended>