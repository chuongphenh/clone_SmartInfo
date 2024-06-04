<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="EventSharing.aspx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.PressAgencyHRs.EventSharing"
    Title="Chia sẻ thông tin"
    MasterPageFile="~/UI/MasterPages/Common/Popup.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRUC.ascx" TagName="PressAgencyHRUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRAlertUC.ascx" TagName="PressAgencyHRAlertUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRHistoryUC.ascx" TagName="PressAgencyHRHistoryUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRRelativesUC.ascx" TagName="PressAgencyHRRelativesUC" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidPressAgencyID" />
    <asp:HiddenField runat="server" ID="hidPressAgencyHRID" />
    <style>
        .button-acm {
            align-items: center;
            text-align: center;
            color: white;
            padding: 10px 20px;
            border-radius: 5%;
            font-size: 15px;
            border: 1px solid #597EF7;
            background: #597EF7;
        }

        .style-center {
            justify-content: center;
        }

        #ddlEmailList {
            width: 100% !important;
        }

        .home-block-content {
            font-size: 16px;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('#<%= ddlEmailList.ClientID %>').hide();
            $('#<%= txtFindShare.ClientID %>').on('keyup', function () {
                var searchValue = $(this).val();
                $.ajax({
                    type: 'POST',
                    url: "EventSharing.aspx/Search",
                    data: JSON.stringify({ searchValue: searchValue }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        var ddlEmailList = $('#<%= ddlEmailList.ClientID %>');
                       ddlEmailList.empty(); // Xóa dữ liệu hiện tại của Listbox

                       if (response.d.length > 0) {
                           // Hiển thị Listbox nếu có kết quả tìm kiếm
                           ddlEmailList.show();

                           // Thêm các mục vào Listbox
                           $.each(response.d, function (key, value) {
                               var option = $('<option></option>').val(value).text(value);
                               ddlEmailList.append(option);
                           });
                       } else {
                           // Ẩn Listbox nếu không có kết quả tìm kiếm
                           ddlEmailList.hide();
                       }
                   },
                   error: function (xhr, textStatus, error) {
                       console.log(error);
                   }
               });
           });
        });
        function pushData() {
            var selectedValues = [];
            $('#<%= ddlEmailList.ClientID %> option:selected').each(function () {
                selectedValues.push($(this).val());
            });
            var group = $('#<%= dropDownListStaffGroup.ClientID %> option:selected').val();
            var hidPressAgencyHRID = $('#<%= hidPressAgencyHRID.ClientID %>').val();


            if (group !== null && hidPressAgencyHRID !== null) {
                var dtRepeater = $('#<%= rptData.ClientID %>');
                console.log(dtRepeater);
                $.ajax({
                    type: 'POST',
                    url: 'EventSharing.aspx/PushData', // Đường dẫn đến phương thức xử lý yêu cầu AJAX trên máy chủ
                    data: JSON.stringify({ selectedValues: selectedValues, group: group, hidPressAgencyHRID: hidPressAgencyHRID, dt: dtRepeater }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        window.location.reload();
                    },
                    error: function (xhr, textStatus, error) {
                        console.log(error);
                    }
                });
            }
           
        }

    </script>
    <div class="home-block-content">
        <style>
            
            .style-center {
                justify-content: center;
            }

            .style-input {
                width: 100% !important;
            }

            .search {
                padding: 3px 15px;
            }

            .repeater-container {
                max-height: 450px; /* Điều chỉnh chiều cao tùy ý */
                overflow-y: auto; /* Tự động hiển thị thanh cuộn dọc khi nội dung vượt quá kích thước đã định */
            }
        </style>

        <h1 style="text-align: left; font-size: 18px; text-align: center;">Chia sẻ đến cá nhân/nhóm người dùng</h1>
        <div style="padding: 10px 20px;">
            <div style="display: flex; align-items: center; padding: 10px 0;">
                <div style="padding: 0 10px; flex: 1;">
                    <span>Chia sẻ đến cá nhân:</span>
                </div>
                <div style="padding: 0 10px; flex: 1;">
                    <asp:TextBox CssClass="style-input" runat="server" ID="txtFindShare"></asp:TextBox>
                    <asp:Label runat="server" ID="lbSearch"></asp:Label>
                    <div id="searchResults"></div>
                    <%--            <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click"/>--%>
                    <asp:ListBox CssClass="style-input" ID="ddlEmailList" runat="server" SelectionMode="Multiple" AutoPostBack="False"></asp:ListBox>
                    <%--<asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click"/>--%>
                    <asp:Label runat="server" ID="lbError"></asp:Label>
                </div>
            </div>

            <div style="display: flex; align-items: center; padding: 10px 0;">
                <div style="padding: 0 10px; flex: 1;">
                    <span>Chia sẻ đến nhóm người dùng:</span>
                </div>
                <div style="padding: 0 10px; flex: 1;">
                    <asp:DropDownList CssClass="style-input" runat="server" ID="dropDownListStaffGroup" DataTextField="Name" DataValueField="RoleID"></asp:DropDownList>
                </div>
            </div>
        </div>

        <div style="display: flex; align-items: center; padding: 10px 0; justify-content: center;">
            <button type="button" class="button-acm" onclick="pushData()">Chia sẻ</button>
        </div>
        <br />
        <br />
        <div style="display: flex; align-items: center; padding: 10px 20px;">
            <div style="padding: 0 10px; flex: 2;">
                <span style="font-size: 20px;">Danh sách người được chia sẻ</span>
            </div>
            <div style="padding: 0 10px; flex: 1; display: flex">
                <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
                <asp:Button CssClass="button-acm search" runat="server" ID="btnSearch" OnClick="btnSearch_Click1" Text="Tìm kiếm" />
            </div>
        </div>
        <br />
        <div style="padding: 20px" class="repeater-container">
            <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound" OnItemCommand="rptData_ItemCommand">
                <HeaderTemplate>
                    <table style="width: 100%; border-collapse: collapse;">
                        <tr>
                            <th style="padding: 10px; border: 1px solid #ccc;">Họ và tên</th>
                            <th style="padding: 10px; border: 1px solid #ccc;">Email</th>
                            <th style="padding: 10px; border: 1px solid #ccc;">Vị trí</th>
                            <th style="padding: 10px; border: 1px solid #ccc;">Xóa</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="padding: 10px; border: 1px solid #ccc;">
                            <asp:Literal ID="ltrName" runat="server"></asp:Literal></td>
                        <td style="padding: 10px; border: 1px solid #ccc;">
                            <asp:Literal ID="ltrEmail" runat="server"></asp:Literal></td>
                        <td style="padding: 10px; border: 1px solid #ccc;">
                            <asp:Literal ID="ltrDescription" runat="server"></asp:Literal></td>
                        <td style="padding: 10px; border: 1px solid #ccc;">
                            <asp:LinkButton ID="btnDelete" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')" runat="server">Delete</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

        </div>

    </div>

</asp:Content>
