<%@ Page Title="Chi tiết danh mục" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.SmartInfos.CatalogNews.Display" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/CatalogNewsTreeViewUC.ascx" TagPrefix="uc" TagName="CatalogNewsTreeViewUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
       .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            font-weight:bold;
            color: #000;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
                            .custom-sidebar-menu .flex_tree-ul li{
                font-weight: bold;
                font-size: 14px;
                background: #e0f0ff;
                margin-bottom: 4px;
                padding: 7px 5px!important;
            }
            .custom-sidebar-menu .flex_tree-cover{
                background: #cbcbcb4f;
                border-radius: 5px;
                padding:5px;
                border-radius:5px;
            }
            .custom-sidebar-menu .flex_tree-cover li ul{
                background: #fff;
                padding:0;
            }
            .custom-sidebar-menu .flex_tree-cover li ul li{
                background: #fff;
                padding-left:10px!important;
                border-bottom:1px solid #ddd;
            }
            .custom-sidebar-menu .flex_tree-cover li ul li:last-child{
                border-bottom:none;
            }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hidVersion" runat="server" />
            <asp:HiddenField ID="hidDefaultFlowID" runat="server" />
            <asp:HiddenField ID="hidID" runat="server" />
            <div class="toolbar" style="top: 75px;">
                THÔNG TIN DANH MỤC
                <ul class="icon_toolbar">
                    <li>
                        <asp:HyperLink runat="server" ID="lnkAddNew" Visible="false">
                            <i class="fa fa-plus" aria-hidden="true" style="font-size: 16px;" title="Tạo mới"></i>
                            <span>Tạo mới</span>
                        </asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="lnkEdit" runat="server" Visible="false">
                            <i class="fas fa-pencil-alt" aria-hidden="true" style="font-size: 16px;" title="Sửa"></i>
                            <span>Sửa</span>
                        </asp:HyperLink>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                            OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tổ chức không?')" Visible="false">
                            <i class="fa fa-trash" aria-hidden="true" style=" font-size: 16px;" title="Xóa"></i>
                            <span>Xóa</span>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div class="content" style="margin-top: 60px;background:transparent">
                <div class="err">
                    <uc:ErrorMessage ID="ucErr" runat="server" />
                </div>
                <div style="width: 30%; float: left;margin-top:-10px;padding-left:10px;">
                    <div class="custom-sidebar-menu">
                        <uc:CatalogNewsTreeViewUC runat="server" ID="ucCatalogNewsTreeView" />
                    </div>
                    
                </div>
                <div style="width: 70%; float: left; position: fixed; top: 120px; left: 31%; background-color: White; height: 100%; padding-bottom: 110px;padding-left:10px;">
                    <asp:Panel ID="pnlEditInfo" runat="server" ScrollBars="Auto" Height="700px" Width="98%">
                        <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
                            <colgroup>
                                <col width="150px" />
                                <col width="" />
                            </colgroup>
                            <tr style="height: 40px;">
                                <td style="text-align: left; font-weight: bold; font-size:14px;">Mã danh mục:
                                </td>
                                <td style="font-size:13px;">
                                    <asp:Label ID="lblCode" runat="server" />
                                </td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="text-align: left; font-weight: bold;font-size:14px;">Tên danh mục:
                                </td>
                                <td style="font-size:13px;">
                                    <asp:Label ID="lblName" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>