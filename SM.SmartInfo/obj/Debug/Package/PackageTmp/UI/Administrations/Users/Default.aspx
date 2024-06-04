<%@ Page Title="Danh sách người dùng" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Users.Default" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            color: #595959;
            font-weight:bold;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <div class="toolbar">
        DANH SÁCH NGƯỜI DÙNG
        <ul class="icon_toolbar">
            <li>
                <a id="hypAddNew" runat="server" href="AddNew.aspx" visible="false">
                    <i class="fa fa-plus new-button" aria-hidden="true" title="Thêm mới"></i>
                    <span>Thêm mới</span>
                </a>
            </li>
            <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa các mục đã chọn?')" Visible="false">
                    <i class="fa fa-trash new-button" aria-hidden="true" title="Xóa"></i>
                    <span>Xóa</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btnImportState" runat="server" OnClick="btnImportState_Click" UseSubmitBehavior="false">
                    <i class="fas fa-sync-alt new-button" aria-hidden="true" title="Đổi trạng thái"></i
                    <span>Đổi trạng thái</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btnImport" runat="server" Text="Import" OnClick="btnImport_Click" UseSubmitBehavior="false">
                    <i class="fa fa-upload new-button" aria-hidden="true" title="Import"></i>
                    <span>Import</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">
                    <i class="far fa-file-excel new-button" aria-hidden="true" title="Xuất Excel"></i>
                    <span>Xuất Excel</span>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div class="body-content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <div style="padding-right: 35px; padding-left: 35px; margin-top: 40px;">
            <div class="row">
                <div class="col-sm-12" style="background: #fff; padding: 20px 0 0;">
                    <table width="100%" class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
                        <col width="20%" />
                        <col width="30%" />
                        <col width="30%" />
                        <col />
                        <tr style="height: 30px; font-weight: 600;font-size:14px;">
                            <td></td>
                            <td>Họ tên hoặc tên đăng nhập
                            </td>
                            <td>Nhóm quyền
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtUserNameSearch" Width="99%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="dropRole" Width="99%"></asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr style="height: 30px; font-weight: 600;">
                            <td></td>
                            <td colspan="2">Email
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtEmail" Width="50%" MaxLength="256"></asp:TextBox>
                                <asp:LinkButton ID="btnSearch" runat="server" Text="Tìm kiếm"  style="font-size:14px;" CssClass="btn-search"
                                    OnClick="btnSearch_Click"></asp:LinkButton>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <div style="width: 100%; padding: 20px;">
                        <div class="new-grid table-list-user">
                            <asp:DataGrid ID="grdMain" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="true"
                                AllowCustomPaging="true" AutoGenerateColumns="false" OnItemDataBound="grdMain_ItemDataBound"
                                OnItemCommand="grdMain_ItemCommand" OnPageIndexChanged="grdMain_PageIndexChanged" GridLines="None">
                                <HeaderStyle CssClass="grid-header" />
                                <ItemStyle CssClass="grid-even" />
                                <AlternatingItemStyle CssClass="grid-odd" />
                                <FooterStyle CssClass="grid-footer" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="ckSelectAll" runat="server" onclick="SelectAll(this,'ckSelect')" />
                                        </HeaderTemplate>
                                        <HeaderStyle Width="20px" Font-Bold="true" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ckSelect" runat="server" name="SELECTED" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn FooterStyle-Font-Size="14px"  HeaderText="Tên đăng nhập">
                                        <HeaderStyle  Font-Size="14px"/>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hiID" runat="server" />
                                            <asp:HiddenField ID="hiVersion" runat="server" />
                                            <asp:HyperLink runat="server" ID="hplEmployeeID"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Tên đầy đủ">
                                        <HeaderStyle  Font-Size="14px"/>
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrName" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Trạng thái">
                                        <HeaderStyle  Font-Size="14px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrStatus" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Email">
                                        <HeaderStyle   Font-Size="14px"/>
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrEmail" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Cơ cấu tổ chức">
                                        <HeaderStyle   Font-Size="14px"/>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblOrganizationName"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Là quản lý">
                                        <HeaderStyle   Font-Size="14px"/>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblIsOrgManager"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Sửa" Visible="false">
                                        <HeaderStyle   Font-Size="14px"/>
                                        <HeaderStyle Width="30px" />
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" ID="hypEdit">
                                        <i class="fas fa-pencil-alt" aria-hidden="true" style="font-size: 16px; margin-left: 5px;" title="Sửa"></i>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Mode="NumericPages" CssClass="new-grid-page" PageButtonCount="10" />
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <tk:PopupPanel ID="popupUploadFile" runat="server" Title="IMPORT Thông tin người dùng"
        Width="500">
        <PopupTemplate>
            <div class="popup-data-content">
                <table class="tableDisplay" width="100%">
                    <colgroup>
                        <col width="150" />
                        <col />
                    </colgroup>
                    <tr>
                        <th>Mẫu import</th>
                        <td>
                            <asp:HyperLink ID="hplDownload" runat="server">Tải về tại đây</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <th>File Upload<span class="star">*</span>
                        </th>
                        <td>
                            <asp:FileUpload ID="fuImportExcel" runat="server" />
                            <i>(*.xls, *.xlsx)</i>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-footer">
                <ul>
                    <li>
                        <asp:Button ID="btnUploadFile" CssClass="btn-done" runat="server" Text="Import" OnClick="btnUploadFile_Click" UseSubmitBehavior="false" />
                    </li>
                </ul>
            </div>
        </PopupTemplate>
    </tk:PopupPanel>
    <tk:PopupPanel ID="popupImportUserState" runat="server" Title="Đổi trạng thái"
        Width="500">
        <PopupTemplate>
            <div class="popup-data-content">
                <table class="tableDisplay" width="100%">
                    <colgroup>
                        <col width="150" />
                        <col />
                    </colgroup>
                    <tr>
                        <th>Mẫu import</th>
                        <td>
                            <asp:HyperLink ID="hplDownload1" runat="server">Tải về tại đây</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <th>File Upload<span class="star">*</span>
                        </th>
                        <td>
                            <asp:FileUpload ID="fuImportExcel1" runat="server" />
                            <i>(*.xls, *.xlsx)</i>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-footer">
                <ul>
                    <li>
                        <asp:Button ID="btnUploadState" CssClass="btn-done" runat="server" Text="Import" OnClick="btnUploadState_Click" UseSubmitBehavior="false" />
                    </li>
                </ul>
            </div>
        </PopupTemplate>
    </tk:PopupPanel>
</asp:Content>