<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.SmartInfos.Contacts.Default" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        ul.press-agency-hr > li {
            width: 32.5%;
        }

        ul.press-agency-hr {
            max-height: unset;
            padding: 0px 0px 0px 30px;
        }

        .content-address {
            height: 20px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }

        .active-li > .div-link > .row > .col-sm-8 > .p-title {
            font-size: 14px;
            font-weight: 600;
            color: #1D39C4;
        }

        .active-li > .div-link > .row > .col-sm-4 {
            font-size: 14px;
            font-weight: 600;
        }

        .description-detail {
            font-size: 14px;
            height: 18px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }

        .description-address {
            font-size: 13px;
            height: 35px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }

        .div-link:hover {
            cursor: pointer;
        }

        .negative {
            color: white;
            padding: 0 10px;
            border: 1px solid red;
            border-radius: 50px;
            background: red;
            font-size: 13px;
        }

        .medium {
            color: black;
            padding: 0 10px;
            border: 1px solid orange;
            border-radius: 50px;
            background: orange;
            font-size: 13px;
        }

        .positive {
            color: white;
            padding: 0 10px;
            border: 1px solid limegreen;
            border-radius: 50px;
            background: limegreen;
            font-size: 13px;
        }
        
        .btnImportListUser {
            padding: 0 10px;
        }
        .typeLink {
            text-decoration: none;
            display: flex;
            align-items: center;
            padding: 5px 10px;
            background: #F2F3F8;
            margin-right: 10px;
            padding-left: 10px;
            padding-right: 10px;
            border-radius: 4px;
        }
        .typeLink i{
            color: #595959;
            padding-right:5px;
        }
        .style-excel{
            padding: 0 10px;
        }
        .custom-box-title .list-table-title {
            position: relative;
            width: 230px;
        }
        .custom-box-title .list-table-title input{
                width: 100%;
        }
        .custom-box-title .list-table-title a{
            position: absolute;
            right: -15px;
        }
        .custom-box-title{
            display: flex;
            justify-content: space-between;
        }
        .right-box-title{
            display: flex;
            gap: 10px;
            align-content:center;
        }
        .right-box-title .form-group {
            margin-bottom:0;
        }
        .right-box-title .form-group a{
            color: #595959;
            background: #F2F3F8;
            padding:8px 10px;
            border-radius: 4px;
        }
        .right-box-title .form-group span{
            padding-left:5px;
        }
        .home-block .press-agency-hr{
            margin-top:25px;
        }
        .press-agency-hr .active-li{
                width: 30.5%;
                margin-right:30px;
                margin-bottom:30px;
                    box-shadow: 5px 5px 7px #33333347;
        }
        @media only screen and (max-width: 1200px) {
            .press-agency-hr .active-li{
                    width: 29.5%;
            }
        }
                @media only screen and (max-width: 991px) {
            .press-agency-hr .active-li{
                    width: 45%;
            }
        }
    </style>
    <asp:HiddenField ID="hidPage" runat="server" />
    <asp:HiddenField ID="hidIsEdit" runat="server" />
    <asp:HiddenField ID="hidPressAgencyType" runat="server" />
    <asp:HiddenField ID="hidPressAgencyID" runat="server" />
    <div class="body-content" style="padding-top: 0px;">
         <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <div class="headline" style="text-align: center; position: relative; top: -49px; right: 0px;">
        </div>
        <div class="home-block">
            <div class="home-block-title custom-box-title" style="padding-left: 0px">
                <h3 style=" font-size: 16px; font-weight: 600; padding: 0px 0 0 30px; margin: 0px;">
                    <span class="list-table-title" style="border: unset; padding: unset;display:flex;align-items:center;">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Style="border-radius: 20px;" placeholder="Tìm kiếm theo Tên"></asp:TextBox>
                        <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" class="fa fa-search" Style="margin-left: 10px"></asp:LinkButton>
                    </span>
                </h3>
                <div class="right-box-title">
                    <div class="form-group ">
                        <asp:LinkButton title="Tìm kiếm nâng cao" ID="btnAdvancedSearch" runat="server" OnClick="btnAdvancedSearch_Click" class="fas fa-filter "><span>Tìm kiếm nâng cao</span></asp:LinkButton>
                    </div>
                    <div class="form-group ">
                        <asp:LinkButton title="Hủy tìm kiếm" ID="btnCancelAdvancedSearch" runat="server" OnClick="btnCancelAdvancedSearch_Click" class="fas fa-virus-slash "><span>Hủy tìm kiếm</span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="home-block-title" style="padding-left: 0px">
                <h3 style="font-size: 16px; font-weight: 600; padding: 0px 0 0 30px; margin: 0px; display: flex; justify-content: space-between;">
                    <span class="list-table-title" style="border: unset; padding: unset; flex: 1;">
                        <asp:Repeater ID="rptPressAgencyType" runat="server" OnItemDataBound="rptPressAgencyType_ItemDataBound" OnItemCommand="rptPressAgencyType_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton style="margin-bottom: 10px; margin-right: 10px; color: #464457;  font-size: 14px; font-weight: 600;" ID="btnSwitchPressAgentType" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </span>
                    <div style="display:inline-flex; align-items: center;">
                        <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click"  CssClass="style-excel" Style="background: #F2F3F8; margin-right: 10px; padding-left: 5px; padding-right: 10px; border-radius: 4px;">
                            <i class="far fa-file-excel new-button" aria-hidden="true" title="Xuất Excel"> </i>
                            <span style="color: #595959;font-weight:bold;font-size:14px;">Xuất Excel</span>
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" class="fa fa-upload new-button" ID="btnImportListUser" OnClick="btnImportListUser_Click"><span style="padding-left:5px;">Import</span></asp:LinkButton>
                        <asp:HyperLink runat ="server" BorderStyle="None" ID ="dynamicLink"  Style="background: #F2F3F8; margin-right: 10px; padding-left: 5px; padding-right: 10px; border-radius: 4px;"></asp:HyperLink>
                    </div>
                </h3>
            </div>
            <ul class="press-agency-hr">
                <asp:Button ID="btnReloadAppendix" runat="server" Text="Tải lại trang" OnClick="btnReloadAppendix_Click" Style="display: none;" />
                <asp:Repeater ID="rptHR" runat="server" OnItemDataBound="rptHR_ItemDataBound" OnItemCommand="rptHR_ItemCommand">
                    <ItemTemplate>
                        <li class="active-li">
                            <div class="div-link" id="divLink" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <a id="linkItem" runat="server" href="#">
                                            <img alt ="" src="" id="img" runat="server" class="img-responsive" style="height: 60px; width: 60px; border-radius: 50%; object-fit: cover"/>
                                        </a>
                                    </div>
                                    <div class="col-sm-4">
                                        <p class="p-title" style="margin: 15px 0 0; font-weight:bold; font-size: 14px">
                                            <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                                        </p>
                                        <p style="color: #1D39C4;font-size:13px;">
                                            <asp:Literal ID="ltrPosition" runat="server"></asp:Literal>
                                        </p>
                                    </div>
                                    <div class="col-sm-4"  style="text-align: right;">
                                        <%--<asp:Image runat="server" ID="qrcode" AlternateText="QR Code"/>--%>
                                        <img runat="server" src="" id="qrcode" style="
                                        width: 100%;
                                        height: 100%;
                                    " alt="QR Code"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Tuổi</div>
                                    <div class="col-sm-8 description-detail">
                                        <asp:Literal ID="ltrAge" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Ngày sinh</div>
                                    <div class="col-sm-8 description-detail">
                                        <asp:Literal ID="ltrDOB" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Điện thoại</div>
                                    <div class="col-sm-8 description-detail">
                                        <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Email</div>
                                    <div class="col-sm-8 description-detail">
                                        <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Địa chỉ</div>
                                    <div class="col-sm-8 description-address">
                                        <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Thái độ</div>
                                    <div class="col-sm-8 description-detail">
                                        <asp:Label ID="ltrAttitude" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Tổ chức</div>
                                    <div class="col-sm-8 description-detail">
                                        <asp:Label ID="ltrPressAgencyName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Loại tổ chức</div>
                                    <div class="col-sm-8 description-detail">
                                        <asp:Label ID="ltrPressAgencyType" runat="server"></asp:Label>
                                    </div>
                                </div>
                                 <div class="row">
                                    <div class="col-sm-4">Nhóm</div>
                                    <div class="col-sm-8 description-address">
                                        <asp:Label ID="ltrPermissionGroup" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">Ghi nhớ</div>
                                    <div class="col-sm-8 description-detail">
                                        <span class="circle-note">&nbsp;</span>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div class="col-sm-12">
                                    <ul class="list-unstyled list-inline blog-info">
                                        <li style="float: right">
                                            <asp:LinkButton ID="btnDelete" runat="server" Style="font-size: 16px; float: right;" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </li>
                                        <li style="float: right">
                                            <asp:HyperLink ID="lnkEdit" runat="server" Style="font-size: 16px"></asp:HyperLink>
                                        </li>
                                        <li style="float:right">
                                            <asp:HyperLink ID="lnkShare" runat="server" Style="font-size: 16px"></asp:HyperLink>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div style="text-align: center;">
                <div class="text-center" style="display: inline-block">
                    <ul class="pagination">
                        <uc:PagerUC runat="server" ID="Pager" OnPageIndexChanged="ucPager_PageIndexChanged" Visible="false" />
                    </ul>
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
    <tk:PopupPanel ID="popSearch" runat="server" Title="TÌM KIẾM NÂNG CAO" Width="700" CancelButton="btnCancel">
        <PopupTemplate>
            <div class="row">
                <div class="col-md-12">
                    <table class="table" style="width: 100%; margin-bottom: 0px">
                        <colgroup>
                            <col width="150" />
                            <col />
                        </colgroup>
                        <tr>
                            <th>Tên
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Điện thoại
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtMobile" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <th>Địa chỉ
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtAddress" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Ghi chú
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtRelatedInfomation" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                         <tr style="visibility:hidden !important;">
                            <th>Loại đơn vị
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtType" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="popup-toolbar" style="text-align: center">
                <asp:Button runat="server" ID="btnStartAdvancedSearch" Text="Tìm kiếm" OnClick="btnStartAdvancedSearch_Click" class="btn btn-primary" Style="background: #434a54" />
                <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
            </div>
        </PopupTemplate>
    </tk:PopupPanel>
</asp:Content>