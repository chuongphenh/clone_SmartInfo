<%@ Page Title="Tạo mới danh mục" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" Inherits="SM.SmartInfo.UI.SmartInfos.CatalogNews.AddNew" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" Namespace="SM.SmartInfo.UI.UserControls" Assembly="SM.SmartInfo" %>
<%@ Register Src="~/UI/UserControls/EmployeeSelectorUC.ascx" TagName="Employee" TagPrefix="uc" %>
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
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="HiddenVersion" runat="server" />
    <div class="toolbar" style="top: 75px;">
        TẠO MỚI DANH MỤC
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click"
                    Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="font-size: 16px;margin-right:3px;" title="Lưu"></i
                    <span>Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="font-size: 16px;" title="Thoát"></i>
                    <span>Thoát</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content" style="background: transparent">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <div style="width: 30%; float: left;margin-top:0;padding-left:10px;">
            <div class="custom-sidebar-menu">
                <uc:CatalogNewsTreeViewUC runat="server" ID="ucCatalogNewsTreeView" />
            </div>
            
        </div>
        <div style="width: 70%; float: left; position: fixed; top: 120px; left: 31%; background-color: White; height: 100%; padding-bottom: 110px;padding-left:10px;">
            <asp:Panel ID="pnlEditInfo" runat="server" ScrollBars="Auto" Height="700px" Width="98%">
                <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
                    <colgroup>
                        <col width="200px" />
                        <col width="" />
                    </colgroup>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Mã danh mục<span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtCode" runat="server" MaxLength="64" Width="350" />
                        </td>
                    </tr>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Tên danh mục<span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <tk:TextArea ID="txtName" runat="server" MaxLength="64" Width="350px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>