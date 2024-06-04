<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBarTreeViewUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.EmulationAndRewards.SideBarTreeViewUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<tk:TreeViewExtended ID="tvEmulationAndReward" runat="server" SelectedNodeStyle-BackColor="#CCCCCC"
    OnSelectedNodeChanged="tvEmulationAndReward_SelectedNodeChanged" AutoPostBack="true"></tk:TreeViewExtended>