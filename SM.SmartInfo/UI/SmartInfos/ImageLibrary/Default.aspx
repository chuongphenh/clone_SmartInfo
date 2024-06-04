<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.SmartInfos.ImageLibrary.Default" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/CatalogNewsTreeViewUC.ascx" TagPrefix="uc" TagName="CatalogNewsTreeViewUC" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidPage" />
    <asp:HiddenField runat="server" ID="hidrefType" />
    <asp:HiddenField runat="server" ID="hidNodeId" />
    <asp:HiddenField runat="server" ID="hidRecordCount" />

    <style>
        .image-container {
            height: 75px;
            display: flex;
            align-items: center;
            margin: 0;
            padding: 20px 10px;
        }

        .button-image {
            margin: 0 15px;
            background: white;
            color: #2a6496;
            border: 1px solid #2a6496;
            border-radius: 5px;
            transition: all 300ms;
            font-size: 14px;
            width: 120px;
            font-weight:600;
        }

            .button-image:hover {
                color: #494949;
                border: 1px solid #494949;
                /*            border-radius: 50px;*/
            }

        .content-row {
            margin-top: 90px;
            display: flex;
        }

        .content-left {
            flex: 2;
        }

        .content-right {
            flex: 8;
        }

        .image-wrapper img {
            display: block;
            width: 100%;
            height: 100%;
            object-fit: fill;
        }

        .image-wrapper, label {
            height: 250px;
            padding: 10px;
            display: inline-block;
            cursor: pointer;
            position: relative;
        }

        .checkbox-image {
            display: none;
            opacity: 0;
        }

        input[type="checkbox"][id^="ckbImg"] {
            display: none;
        }

        label:before {
            z-index: 2;
            background-color: white;
            color: white;
            content: " ";
            display: block;
            border-radius: 50%;
            border: 1px solid grey;
            position: absolute;
            /*      top: -5px;
                left: -5px;*/
            width: 25px;
            height: 25px;
            text-align: center;
            line-height: 24px;
            transition-duration: 0.4s;
            transform: scale(0);
        }

        label.checked {
            border-color: #ddd;
        }

            label.checked:before {
                content: "✓";
                background-color: grey;
                transform: scale(1);
            }

            label.checked img {
                transform: scale(0.9);
                /* box-shadow: 0 0 5px #333; */
                z-index: -1;
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
            .custom-right-images .box-imgs{
                width:100%;
            }
            .custom-right-images .box-imgs img{
                object-fit:cover;
                    box-shadow: 2px 2px 8px #00000036;
            }
    </style>
    <script>
        function toggleCheckbox(image) {
            var checkbox = image.parentElement.parentElement.querySelector('input[type="checkbox"]');
            checkbox.checked = !checkbox.checked;

            var imageWrapper = image.parentElement;

            if (checkbox.checked) {
                imageWrapper.classList.add('checked');
            } else {
                imageWrapper.classList.remove('checked');
            }
        }

        function viewImageDetail(image) {

        }
    </script>
    <div class="home-block-content ">
        <div class="toolbar image-container">
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnViewAll" OnClick="btnViewAll_Click" Text="Xem tất cả" />
            </div>
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnAddNewNode" OnClick="btnAddNewNode_Click" Text="Thêm thư mục" />
            </div>
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnEditNode" OnClick="btnEditNode_Click" Text="Sửa thư mục" />
            </div>
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnDelete" OnClick="btnDelete_Click" Text="Xóa thư mục" />
            </div>
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnDeleteImg" OnClick="btnDeleteImg_Click" Text="Xóa ảnh" />
            </div>
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnAddImageToNode" OnClick="btnAddImageToNode_Click" Text="Thêm ảnh" />
            </div>
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnViewDeletedItem" OnClick="btnViewDeletedItem_Click" Text="Thùng rác" Visible="false" />
            </div>
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnRevertDeletedItem" OnClick="btnRevertDeletedItem_Click" Text="Khôi phục" Visible="false" />
            </div>
            <div class="button-box">
                <asp:Button CssClass="button-image" runat="server" ID="btnDeletePermanently" OnClick="btnDeletePermanently_Click" Text="Xóa vĩnh viễn" Visible="false"/>
            </div>
            <div class="button-box" style="margin-left: auto; ">
                <asp:DropDownList CssClass="select__css" runat="server" ID="ddlYearSorting" OnSelectedIndexChanged="ddlYearSorting_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="form-group " style="padding-right:27px;">
                <asp:LinkButton title="Tìm kiếm nâng cao" ID="btnAdvancedFilter" runat="server" OnClick="btnAdvancedFilter_Click" class="fas fa-filter "><span>Bộ lọc nâng cao</span></asp:LinkButton>
            </div>
        </div>
        <div style="display: flex; flex-direction: column;">
            <div class="content-row">
                <div class="content-left">
                    <div class="col-sm-12">
                        <div class="home-col-12">
                            <div class="content" style="margin: unset; font-size: 14px;background:transparent">
                                <div class="custom-sidebar-menu">
                                    <tk:TreeViewExtended ID="tvCatalogNews" runat="server" SelectedNodeStyle-BackColor="#CCCCCC" OnSelectedNodeChanged="tvCatalogNews_SelectedNodeChanged" AutoPostBack="true"></tk:TreeViewExtended>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content-right custom-right-images">
                    <div class="col-sm-12">
                        <asp:Repeater runat="server" ID="rptImage" OnItemDataBound="rptImage_ItemDataBound" OnItemCommand="rptImage_ItemCommand">
                            <ItemTemplate>
                                <div class="col-md-3 image-wrapper">
                                    <asp:CheckBox runat="server" ID="ckbImg" CssClass="checkbox-image" AutoPostBack="true"/>
                                    <label for="ckbImg" class="box-imgs">
                                        <img id="img" runat="server" class="img-responsive image-ava" src="" alt="" onclick=" toggleCheckbox(this)" />
                                    </label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        <div style="text-align: center; margin-top: 20px;">
            <div class="text-center" style="display: inline-block">
                <ul class="pagination">
                    <uc:PagerUC runat="server" ID="Pager" OnPageIndexChanged="Pager_PageIndexChanged" />
                </ul>
            </div>
        </div>
        </div>
    </div>

    <tk:PopupPanel ID="popupAdvancedFilter" runat="server" Title="Bộ lọc nâng cao" Width="500" CancelButton="btnCancelAdvancedFilter" OnPopupClosed="popupAdvancedFilter_PopupClosed">
        <PopupTemplate>
            <div class="popup-data-content">
                <table class="tableDisplay" width="100%">
                    <colgroup>
                        <col width="50" />
                        <col width="100" />
                        <col width="50" />
                        <col width="100" />
                    </colgroup>
                    <tr>
                        <th colspan="2" style="text-align:center">
                            <asp:Label runat="server" ID="lblPostedDTG" Text="Ngày đăng:"></asp:Label></th>
                        <td colspan="2">
                            <tk:DatePicker runat="server" ID="dptPostedDTG"/>
                        </td>
                    </tr>
                    <tr>
                        <th style="text-align:center">
                            <asp:Label runat="server" ID="lblPostedDTGFrom" Text="Từ:"></asp:Label></th>
                        <td>
                            <tk:DatePicker runat="server" ID="dptPostedDTGFrom"/>
                        </td>
                        <th style="text-align:center">
                            <asp:Label runat="server" ID="lblPostedDTGTo" Text="Đến:"></asp:Label></th>
                        <td>
                            <tk:DatePicker runat="server" ID="dptPostedDTGTo"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-footer">
                <ul>
                    <li>
                        <asp:Button ID="btnApply" CssClass="btn-done" runat="server" Text="Áp dụng" OnClick="btnApply_Click" UseSubmitBehavior="false" />
                        <asp:Button runat="server" ID="btnCancelAdvancedFilter" Text="Hủy" />
                    </li>
                </ul>
            </div>
        </PopupTemplate>
    </tk:PopupPanel>

    <tk:PopupPanel ID="popupCreate" runat="server" Title="Tạo mới thư mục/ album" Width="500" CancelButton="btnCancelCreate" OnPopupClosed="popupCreate_PopupClosed">
        <PopupTemplate>
            <div class="popup-data-content">
                <table class="tableDisplay" width="100%">
                    <colgroup>
                        <col width="150" />
                        <col />
                    </colgroup>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lbNodeName"></asp:Label></th>
                        <td>
                            <asp:TextBox runat="server" ID="txtNodeName" CssClass="input-style"></asp:TextBox>
                            <asp:Label ID="lbEmtyWarning" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-footer">
                <ul>
                    <li>
                        <asp:Button ID="btnCreateNewNode" CssClass="btn-done" runat="server" Text="Thêm" OnClick="btnCreateNewNode_Click" UseSubmitBehavior="false" />
                        <asp:Button runat="server" ID="btnCancelCreate" Text="Hủy" />
                    </li>
                </ul>
            </div>
        </PopupTemplate>
    </tk:PopupPanel>

    <tk:PopupPanel ID="popupEdit" runat="server" Title="Chỉnh sửa thư mục/ album" Width="500" CancelButton="btnCancelEdit" OnPopupClosed="popupEdit_PopupClosed">
        <PopupTemplate>
            <div class="popup-data-content">
                <table class="tableDisplay" width="100%">
                    <colgroup>
                        <col width="150" />
                        <col />
                    </colgroup>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lbNodeName2"></asp:Label></th>
                        <td>
                            <asp:TextBox runat="server" ID="txtNodeNameEdit" CssClass="input-style"></asp:TextBox>
                            <asp:Label ID="lbEmtyWarning2" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-footer">
                <ul>
                    <li>
                        <asp:Button ID="btnSave" CssClass="btn-done" runat="server" Text="Lưu" OnClick="btnSave_Click" UseSubmitBehavior="false" />
                        <asp:Button runat="server" ID="btnCancelEdit" Text="Hủy" />
                    </li>
                </ul>
            </div>
        </PopupTemplate>
    </tk:PopupPanel>

    <tk:PopupPanel ID="nodeSelector" runat="server" Title="Chọn thư mục/ album" Width="500" CancelButton="btnExitSelector" OnPopupClosed="nodeSelector_PopupClosed">
        <PopupTemplate>
            <div class="popup-data-content">
                <table class="tableDisplay" width="100%">
                    <colgroup>
                        <col width="150" />
                        <col />
                    </colgroup>
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="lbCbBox" Text="Thư mục/ album"></asp:Label></th>
                        <td>
                            <asp:DropDownList ID="dlNodeSelector" runat="server" DataValueField="Id" DataTextField="CatalogName"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-footer">
                <ul>
                    <li>
                        <asp:Button ID="saveToNode" CssClass="btn-done" runat="server" Text="Lưu" OnClick="saveToNode_Click" UseSubmitBehavior="false" />
                        <asp:Button runat="server" ID="btnExitSelector" Text="Hủy" />
                    </li>
                </ul>
            </div>
        </PopupTemplate>
    </tk:PopupPanel>
</asp:Content>
