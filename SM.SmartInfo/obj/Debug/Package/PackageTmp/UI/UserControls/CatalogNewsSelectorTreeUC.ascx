<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CatalogNewsSelectorTreeUC.ascx.cs" Inherits="SM.SmartInfo.UI.UserControls.CatalogNewsSelectorTreeUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<asp:HiddenField ID="hidMode" runat="server" />
<asp:HiddenField ID="hidOrganizationType" runat="server" />
<asp:HiddenField ID="hidEnableNodes" runat="server" />
<asp:HiddenField ID="hidDisableNodes" runat="server" />
<tk:ComboTreeView ID="treeOrg" runat="server" DropDownWidth="300" />