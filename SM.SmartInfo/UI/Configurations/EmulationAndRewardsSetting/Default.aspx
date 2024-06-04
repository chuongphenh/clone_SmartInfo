<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.Configurations.EmulationAndRewardsSetting.Default" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/CatalogNewsTreeViewUC.ascx" TagPrefix="uc" TagName="CatalogNewsTreeViewUC" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .class-table {
            background: #F5F5F5;
            padding: 20px;
            margin: 15px;
        }

        table {
            width: 100%;
            font-size: 14px;
            border-collapse: collapse;
            border: 1px solid #ccc;
        }
        h3 {
            font-size: 16px;
        }
        tr {
            border-bottom: 1px solid #ccc;
        }


        th {
            background-color: #f2f2f2;
            padding: 10px;
            text-align: center;
            border: 1px solid #ccc;
        }

        td {
            padding: 10px;
            text-align: center;
            border: 1px solid #ccc;
        }

        .btn {
            display: inline-block;
            padding: 5px 10px;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin-right: 5px;
        }

            .btn:hover {
                background-color: #0056b3;
            }
            .table-title{
                display: flex;
                justify-content: space-between;
            }
    </style>
    <asp:HiddenField runat="server" ID="hidItemId" />
    <asp:HiddenField runat="server" ID="hidPageAwardingCatalog" />
    <asp:HiddenField runat="server" ID="hidPageAwardingPeriod" />
    <asp:HiddenField runat="server" ID="hidAwardingLevel" />
    <asp:HiddenField runat="server" ID="hidPageAwardingType" />
    <div class="home-block-content" style="margin-top: 16px;">
        <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px; display: flex; padding: 15px;flex-direction:column;align-items:center">
            <div class="col-md-6 class-table" >
                 <div class="table-title">
                     <h3>Cấu hình thư viện ảnh</h3>
                 </div>
                 <div  class="col-md-6" style="display: flex;">
                    <h5 style="margin-right: 15px;">Xóa ảnh thư mục gốc</h5>
                    <asp:CheckBox ID="CheckBox1" runat="server" oncheckedchanged="CheckBox1_CheckedChanged"  AutoPostBack="true" />
                </div>
            </div>
        </div>
    </div>

    <div class="home-block-content" style="margin-top: 16px;">
        <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px; display: flex; padding: 15px;flex-direction:column;align-items:center">
            <div class="col-md-6 class-table" >
                <div class="table-title">
                     <h3>Danh mục các loại khen thưởng</h3>
                    <div>
                           <asp:Button runat="server" ID="btnShowPopUpCreateAwardingCatalog" OnClick="btnShowPopUpCreateAwardingCatalog_Click" Text="Thêm"/>
                            <asp:LinkButton ID="btnExportExcelAwardingCatalog" runat="server" OnClick="btnExportExcelAwardingCatalog_Click" Style="float: right; margin-top: -5px; margin-left: 5px">
                            <i class="fas fa-file-excel" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; border-radius: 4px;" title="Xuất dữ liệu HS khen thưởng"></i>
                                <span style="color: #595959;font-weight:600;font-size:14px;"> Xuất Excel</span>
                            </asp:LinkButton>
                    </div>
                </div>

                <table width="100%" class="">
                  
                    <tr>
                        <th >Tên
                        </th>
                        <th colspan="2"> Hành động
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAwardingCatalog" OnItemDataBound="rptAwardingCatalog_ItemDataBound" OnItemCommand="rptAwardingCatalog_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td style="font-size:13px">
                                    <asp:Literal ID="ltrAwardingCatalogName" runat="server"></asp:Literal>
                                </td>
                                <td style="font-size:13px">
                                    <asp:LinkButton ID="btnShowPopUpEditAwardingCatalog" runat="server" Text="Edit"> Sửa</asp:LinkButton>
                                   
                                </td>
                                   <td style="font-size:13px">
                                   <asp:LinkButton ID="btnDeleteAwardingCatalog" runat="server" Text="Delete"> Xóa</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div style="text-align: center;">
                    <div class="text-center" style="display: inline-block">
                        <ul class="pagination">
                            <uc:PagerUC runat="server" ID="Pager" OnPageIndexChanged="Pager_PageIndexChanged" Visible="false" />
                        </ul>
                    </div>
                </div>
            </div>

            <tk:PopupPanel ID="popupUpCreateAwardingCatalog" runat="server" Title="Loại khen thưởng"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Loại khen thưởng</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCreateAwardingCatalog"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnCreateAwardingCatalog" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnCreateAwardingCatalog_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopUpAwardingCatalog" OnClick="btnClosePopUpAwardingCatalog_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>

            <tk:PopupPanel ID="popupEditAwardingCatalog" runat="server" Title="Loại khen thưởng"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Loại khen thưởng</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEditAwardingCatalog"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnEditAwardingCatalog" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnEditAwardingCatalog_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopupEditAwardingCatalog" OnClick="btnClosePopupEditAwardingCatalog_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>

            <div class="col-md-6 class-table">
                  <div class="table-title">
                     <h3>Thông tin các đợt khen thưởng</h3>
                    <div>
                              <asp:Button runat="server" ID="btnShowPopupCreateAwardingPeriod" OnClick="btnShowPopupCreateAwardingPeriod_Click" Text="Thêm" />
                            <asp:LinkButton ID="btnExportExcelAwardingPeriod" runat="server" OnClick="btnExportExcelAwardingPeriod_Click" Style="float: right; margin-top: -5px; margin-left: 5px">
                            <i class="fas fa-file-excel" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; border-radius: 4px;" title="Xuất dữ liệu HS khen thưởng"></i>
                                <span style="color: #595959;font-weight:600;font-size:14px;"> Xuất Excel</span>
                            </asp:LinkButton>
                    </div>
                </div>
                <table width="100%">
                   
                    <tr>
                        <th>Đợt khen thưởng
                        </th>
                        <th>Thời gian diễn ra
                        </th>
                        <th colspan="2">Hành động
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAwardingPeriod" OnItemDataBound="rptAwardingPeriod_ItemDataBound" OnItemCommand="rptAwardingPeriod_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td style="font-size:13px;">
                                    <asp:Literal runat="server" ID="ltrAwardingPeriodName"></asp:Literal>
                                </td>
                                <td style="font-size:13px;">
                                    <asp:Literal runat="server" ID="ltrAwardingTime"></asp:Literal>
                                </td>
                                <td style="font-size:13px;">
                                    <asp:LinkButton ID="btnShowPopupEditAwardingPeriod" runat="server" Text="Edit"> Sửa</asp:LinkButton>
                                   
                                </td>
                                 <td style="font-size:13px;">
                                    <asp:LinkButton ID="btnDeleteAwardingPeriod" runat="server" Text="Delete"> Xóa</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div style="text-align: center;">
                    <div class="text-center" style="display: inline-block">
                        <ul class="pagination">
                            <uc:PagerUC runat="server" ID="PagerUC1" OnPageIndexChanged="PagerUC1_PageIndexChanged" Visible="false" />
                        </ul>
                    </div>
                </div>
            </div>

            <tk:PopupPanel ID="PopUpCreateAwardingPeriod" runat="server" Title="Đợt khen thưởng"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Đợt khen thưởng</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCreateAwardingPeriod"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Thời gian diễn ra</th>
                                <td>
                                    <tk:DatePicker ID="dpkCreateAwardingPeriod" runat="server" Width="100%" DateFormat="DMY" Visible="true" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnSaveCreateAwardingPeriod" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSaveCreateAwardingPeriod_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopUpCreateAwardingPeriod" OnClick="btnClosePopUpCreateAwardingPeriod_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>

            <tk:PopupPanel ID="PopupEditAwardingPeriod" runat="server" Title="Đợt khen thưởng"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Đợt khen thưởng</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEditAwardingPeriodName"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Thời gian diễn ra</th>
                                <td>
                                    <tk:DatePicker ID="dpkEditAwardingPeriodTime" runat="server" Width="100%" DateFormat="DMY" Visible="true" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnSaveAwardingPeriodEdit" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSaveAwardingPeriodEdit_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopupAwardingPeriodEdit" OnClick="btnClosePopupAwardingPeriodEdit_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>
        </div>

        <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px; display: flex; padding: 15px;flex-direction:column;align-items:center">
              <div class="col-md-6 class-table">
                   <div class="table-title">
                       <h3>Thông tin cấp khen thưởng</h3>
                    <div>
                            <asp:Button runat="server" ID="btnCreateAwardingLevel" OnClick="btnCreateAwardingLevel_Click" Text="Thêm" />
                            <asp:LinkButton ID="btnExportExcelAwardingLevel" runat="server" OnClick="btnExportExcelAwardingLevel_Click" Style="float: right; margin-top: -5px; margin-left: 5px">
                            <i class="fas fa-file-excel" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; border-radius: 4px;" title="Xuất dữ liệu HS khen thưởng"></i>
                                <span style="color: #595959;font-weight:600;font-size:14px;"> Xuất Excel</span>
                            </asp:LinkButton>
                    </div>
                </div>
                <table width="100%">
                  
                    <tr>
                        <th>Cấp khen thưởng
                        </th>
                        <th>Mô tả
                        </th>
                        <th>Phân loại
                        </th>
                        <th colspan="2">Hành động
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAwardingLevel" OnItemDataBound="rptAwardingLevel_ItemDataBound" OnItemCommand="rptAwardingLevel_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td style="font-size:13px;">
                                    <asp:Literal runat="server" ID="ltrAwardingLevel"></asp:Literal>
                                </td>
                                <td style="font-size:13px;">
                                    <asp:Literal runat="server" ID="ltrAwardingLevelDescription"></asp:Literal>
                                </td>
                                <td style="font-size:13px;">
                                    <asp:Literal runat="server" ID="ltrAwardingLevelCategory"></asp:Literal>
                                </td>
                                <td style="font-size:13px;">
                                    <asp:LinkButton runat="server" ID="btnShowPopupEditAwardingLevel" Text="Edit"> Sửa</asp:LinkButton>
                                </td>
                                 <td style="font-size:13px;">
                                   <asp:LinkButton runat="server" ID="btnDeleteAwardingLevel" Text="Delete"> Xóa</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                  <div style="text-align: center;">
                    <div class="text-center" style="display: inline-block">
                        <ul class="pagination">
                            <uc:PagerUC runat="server" ID="PagerUC2" OnPageIndexChanged="PagerUC2_PageIndexChanged" Visible="false" />
                        </ul>
                    </div>
                </div>
            </div>

            <tk:PopupPanel ID="popupCreateAwardingLevel" runat="server" Title="Cấp khen thưởng"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Cấp khen thưởng</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAwardingLevel"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Mô tả</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCreateAwardingLevelDescription"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Phân loại</th>
                                <td>
                                    <asp:DropDownList ID="ddlCreateAwardingLevelCategory" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnSaveAwardingLevel" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSaveAwardingLevel_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosepopupCreateAwardingLevel" OnClick="btnClosepopupCreateAwardingLevel_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>

            <tk:PopupPanel ID="popupEditAwardingLevel" runat="server" Title="Cấp khen thưởng"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Cấp khen thưởng</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAwardingLevelEdit"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Mô tả</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEditAwardingLevelDescription"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Phân loại</th>
                                <td>
                                    <asp:DropDownList ID="ddlEditAwardingLevelCategory" DataValueField="Id" DataTextField="Name" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnSaveAwardingTypeEdit" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSaveAwardingTypeEdit_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopupAwardingTypeEdit" OnClick="btnClosePopupAwardingTypeEdit_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>

            <div class="col-md-6 class-table" >
                  <div class="table-title">
                        <h3>Loại khen</h3>
                    <div>
                              <asp:Button runat="server" ID="btnShowPopupCreateAwardingType" OnClick="btnShowPopupCreateAwardingType_Click" Text="Thêm" />
                            <asp:LinkButton ID="btnExportExcelAwardingType" runat="server" OnClick="btnExportExcelAwardingType_Click" Style="float: right; margin-top: -5px; margin-left: 5px">
                            <i class="fas fa-file-excel" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; border-radius: 4px;" title="Xuất dữ liệu HS khen thưởng"></i>
                                <span style="color: #595959;font-weight:600;font-size:14px;"> Xuất Excel</span>
                            </asp:LinkButton>
                    </div>
                </div>
                <table width="100%">
                  
                    <tr>
                        <th>Loại khen
                        </th>
                        <th colspan="2">Hành động
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAwardingType" OnItemDataBound="rptAwardingType_ItemDataBound" OnItemCommand="rptAwardingType_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td style="font-size:13px;">
                                    <asp:Literal runat="server" ID="ltrAwardingType"></asp:Literal>
                                </td>
                                <td style="font-size:13px;"> 
                                    <asp:LinkButton runat="server" ID="btnShowPopupEditAwardingType" Text="Edit"> Sửa</asp:LinkButton>
                                 
                                </td>
                                 <td style="font-size:13px;"> 
                                      <asp:LinkButton runat="server" ID="btnDeleteAwardingType" Text="Delete"> Xóa</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div style="text-align: center;">
                    <div class="text-center" style="display: inline-block">
                        <ul class="pagination">
                            <uc:PagerUC runat="server" ID="PagerUC3" OnPageIndexChanged="PagerUC3_PageIndexChanged" Visible="false" />
                        </ul>
                    </div>
                </div>
            </div>

            <tk:PopupPanel ID="PopupCreateAwardingType" runat="server" Title="Loại khen"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Loại khen</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCreateAwardingType"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnSaveCreateAwardingType" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSaveCreateAwardingType_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopupCreateAwardingType" OnClick="btnClosePopupCreateAwardingType_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>

            <tk:PopupPanel ID="PopupEditAwardingType" runat="server" Title="Loại khen"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Loại khen</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEditAwardingType"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnSaveEditAwardingType" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSaveEditAwardingType_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopupEditAwardingType" OnClick="btnClosePopupEditAwardingType_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>
        </div>
         <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px; display: none; padding: 15px;flex-direction:column;align-items:center">
            
            <div class="col-md-12 class-table" >
                 <div class="table-title">
                     <h3>Thông tin kết quả các đợt khen thưởng</h3>
                    <div>
                               <asp:Button runat="server" ID="btnShowPopupCreateAwardingPeriodResult" OnClick="btnShowPopupCreateAwardingPeriodResult_Click" Text="Thêm" />
                            <asp:LinkButton ID="btnExportExcelAwardingPeriodResult" runat="server" OnClick="btnExportExcelAwardingPeriodResult_Click" Style="float: right; margin-top: -5px; margin-left: 5px">
                            <i class="fas fa-file-excel" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Xuất dữ liệu HS khen thưởng"></i>
                                <span style="color: #595959;font-weight:600;font-size:14px;"> Xuất Excel</span>
                            </asp:LinkButton>
                    </div>
                </div>
                <table width="100%">
                  
                    <tr>
                        <th>Tên tổ chức/ Cá nhân
                        </th>
                        <th>Cấp khen thưởng
                        </th>
                        <th>Đợt khen thưởng
                        </th>
                        <th>Thời gian
                        </th>
                        <th ảnh
                        </th>
                        <th colspan="2">Hành động
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAwardingPeriodResult" OnItemDataBound="rptAwardingPeriodResult_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="ltrPaOrEmp"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="ltrRewardingLevel"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="ltrRewardedPeriod"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="ltrTime"></asp:Literal>
                                </td>
                                <td>
                                    <asp:HyperLink runat="server" ID="lnkViewAwardingImages"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnShowPopupEditAwardingPeriodResult" runat="server" Text="Edit"> Sửa</asp:LinkButton>
                         
                                </td>
                                <td>
                                     <asp:LinkButton runat="server" ID="btnDeleteAwardingPeriodResult" Text="Delete"> Xóa</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>

            <tk:PopupPanel ID="popupCreateAwardingPeriodResult" runat="server" Title="Cấp khen thưởng"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Cấp khen thưởng</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCreateAwardingPeriodResult"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Loại giấy khen</th>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox2"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnSaveCreateAwardingPeriodResult" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSaveCreateAwardingPeriodResult_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopUpCreateAwardingPeriodResult" OnClick="btnClosePopUpCreateAwardingPeriodResult_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>

            <tk:PopupPanel ID="PopupEditAwardingPeriodResult" runat="server" Title="Cấp khen thưởng"
                Width="500">
                <PopupTemplate>
                    <div class="popup-data-content">
                        <table class="tableDisplay" width="100%">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Cấp khen thưởng</th>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>Loại giấy khen</th>
                                <td>
                                    <asp:TextBox runat="server" ID="TextBox3"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="popup-footer">
                        <ul>
                            <li>
                                <asp:Button ID="btnSaveAwardingPeriodResultEdit" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSaveAwardingPeriodResultEdit_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClosePopupEditAwardingPeriodResult" OnClick="btnClosePopupEditAwardingPeriodResult_Click" runat="server" Text="Đóng" CssClass="btn-done" />
                            </li>
                        </ul>
                    </div>
                </PopupTemplate>
            </tk:PopupPanel>
        </div>
    </div>
</asp:Content>
