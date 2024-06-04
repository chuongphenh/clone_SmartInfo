<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.SmartInfos.EmulationAndRewards.Default" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/SmartInfos/EmulationAndRewards/DisplayUC.ascx" TagPrefix="uc" TagName="DisplayUC" %>
<%@ Register Src="~/UI/SmartInfos/EmulationAndRewards/SideBarTreeViewUC.ascx" TagPrefix="uc" TagName="SideBarTreeViewUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content" style="padding-top: 0px; margin-top: 40px">
        <style>
            ::-webkit-scrollbar {
                background: #fff;
                border-radius: 4px;
                height: 5px;
                width: 5px;
            }

            ::-webkit-scrollbar-thumb {
                background: #e8e8e8;
                border-radius: 4px;
            }

            span {
                font-size: 13px;
            }
            .item-btn a{
                background: #fff;
                padding:5px 10px;
                border-radius:4px;
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
        <asp:HiddenField ID="hidUnit" runat="server" />
        <section style="padding-right: 35px; padding-left: 35px; margin-top: 10px; padding-bottom: 25px">
            <div class="row">
                <div class="col-sm-3">
                    <div class="content-box-kt">
                        <h3 style="font-size:16px; font-weight: 700; color: #000000;">
                            DS HS khen thưởng
                        </h3>
                    </div>
                    <div class="custom-sidebar-menu">
                        <uc:SideBarTreeViewUC ID="ucSideBarTreeView" runat="server" OnSelectedNodeChanged="ucSideBarTreeView_SelectedNodeChanged" />
                    </div>
                    
                </div>
                <div class="col-sm-9 custom-link-focus">
                    <div class="content-search-kt">
                       <h3 style="font-size:16px; font-weight: 700;color: #000000;">
                            Tìm kiếm
                        </h3>
                        <div class="right-content-search-kt">
                            <span class="item-btn">
                                <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="float: right; margin-left: 5px">
                                    <i class="fas fa-plus" style="color: black; margin-top: 4px; font-size: 14px; padding-bottom: 5px; padding-top: 5px; border-radius: 4px;" title="Thêm"></i>
                                    <span style="color:black;font-weight:bold;font-size:16px;">Thêm</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Style="float: right; margin-left: 5px">
                                    <i class="fas fa-file-excel" aria-hidden="true" style="color: black; margin-top: 3px; font-size: 14px; background: unset  ; padding-bottom: 6px; padding-top: 6px;  border-radius: 4px;" title="Xuất dữ liệu HS khen thưởng"></i>
                                    <span style="color: black;font-weight:bold;font-size:16px;">Xuất Excel</span>
                                </asp:LinkButton>
                            </span>
                        </div>
                    </div>  

                    <table class="table" style="width: 100%; margin-bottom: 0px">
                        <colgroup>
                            <col width="250" />
                            <col />
                            <col width="250" />
                            <col />
                        </colgroup>
                        <tr>
                            <th style="font-size:14px;">Năm</th>
                            <td>
                                <tk:NumericTextBox ID="numYear" runat="server" Width="100%" AllowThousandDigit="false" NumberDecimalDigit="0"></tk:NumericTextBox>
                            </td>
                            <th style="font-size:14px;">Đợt</th>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlAwardingPeriod" Width="100%" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th style="font-size:14px;">Đơn vị khen thưởng</th>
                            <td>
                                <asp:TextBox ID="txtUnit" runat="server" MaxLength="256" Width="100%"></asp:TextBox>
                            </td>
                            <th style="font-size:14px;">Đơn vị/Cá nhân được khen thưởng</th>
                            <td>
                                <asp:TextBox ID="txtTextSearchSubject" runat="server" MaxLength="256" Width="100%" placeholder="Họ tên, Email, Điện thoại, ..."></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="popup-toolbar" style="text-align: center">
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" class="btn btn-primary" Style="background: #434a54;font-size:14px;" />
                    </div>
                    <uc:DisplayUC ID="ucDisplay" runat="server" />
                </div>
            </div>`
        </section>
    </div>
</asp:Content>