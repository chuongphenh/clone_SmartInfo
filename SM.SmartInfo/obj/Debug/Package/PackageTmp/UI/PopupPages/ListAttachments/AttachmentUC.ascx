<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.ListAttachments.AttachmentUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<asp:HiddenField ID="hidRefID" runat="server" />
<asp:HiddenField ID="hidRefType" runat="server" />
<asp:HiddenField ID="hidAllowEdit" runat="server" />

<style>
    .flex_popup-content {
        max-height: 250px;
    }

    .custom-file-input {
        opacity: 0;
        z-index: 2;
        position: relative;
    }

    .custom-file-label {
        z-index: 1;
        padding: .375rem .75rem;
        font-weight: 400;
        line-height: 1.5;
        color: #495057;
        background-color: #fff;
        border: 1px solid #ced4da;
        border-radius: .25rem;
        margin-bottom: 10px;
        position: absolute;
        top: 95px;
        width: 480px;
    }

    .new-grid {
        width: 100%;
        border: 1px solid #D9D9D9;
        box-sizing: border-box;
        border-radius: 2px;
    }

        .new-grid table {
            width: 100%;
            border: none;
        }

        .new-grid tr.grid-header {
            background-color: #fff;
        }

            .new-grid tr.grid-header > td {
                font-size: 14px;
                color: #595959 !important;
                font-weight: 600;
            }

        .new-grid tr.grid-even, .new-grid tr.grid-odd {
            border: none;
            border-top: 1px solid #D9D9D9;
        }

        .new-grid tr.grid-even {
            background: #FAFAFA;
        }

            .new-grid tr.grid-even i, .new-grid tr.grid-odd i {
                color: #000;
            }

        .new-grid tbody tr.grid-even:hover, .new-grid tr.grid-odd:hover,
        .new-grid tbody tr.grid-even:hover a, .new-grid tr.grid-odd:hover a,
        .new-grid tbody tr.grid-even:hover i, .new-grid tr.grid-odd:hover i {
            background: #F2F3F8;
            color: unset;
        }

        .new-grid tbody td {
            padding: 10px 5px;
        }

    .new-grid-page {
        border-top: 1px solid #D9D9D9;
    }

    .new-grid .new-grid-page td {
        padding-left: 10px;
    }

    .new-grid .new-grid-page span {
        font-size: 12px;
        font-weight: bold;
        cursor: pointer;
        padding: 0 3px;
        margin: 0 3px;
    }

        .new-grid .new-grid-page span:hover {
            text-decoration: underline;
        }
</style>

<div class="toolbar">
    <span style="font-family: SF Pro Display; font-weight: 600;">DANH SÁCH TÀI LIỆU</span>
    <ul class="icon_toolbar">
        <li>
            <asp:LinkButton runat="server" ID="btnShowUpload" OnClick="btnShowUpload_Click"><i class="fa fa-upload new-button" title="Tải lên" ></i> </asp:LinkButton>
        </li>
    </ul>
</div>
<asp:HiddenField ID="hidAttID" runat="server" />
<div class="row content new-grid" style="padding: 0px; width: auto; margin-left: 10px; margin-right: 10px">
    <asp:DataGrid ID="grdData" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="false"
        AllowCustomPaging="true" AutoGenerateColumns="false" OnItemDataBound="grdData_ItemDataBound"
        OnItemCommand="grdData_ItemCommand" GridLines="None">
        <HeaderStyle CssClass="grid-header" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemStyle CssClass="grid-even" />
        <AlternatingItemStyle CssClass="grid-odd" />
        <FooterStyle CssClass="grid-footer" />
        <Columns>
            <asp:TemplateColumn HeaderText="Tên tài liệu" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidAttachmentID" />
                    <asp:Literal runat="server" ID="ltrName" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Người đưa lên" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="ltrCreatedBy" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Ngày đưa lên" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="ltrCreatedDTG" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Tải về" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblDownLoad" runat="server">
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Tải lên" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="btnUpLoad" runat="server">
                        <i class="fas fa-cloud-upload-alt" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Tải lên"> Tải lên</i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Xóa" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server">
                        <i class="fa fa-trash" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Xóa"> Xóa</i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    <tk:PopupPanel runat="server" ID="popUpload" Title="Tải tài liệu" Width="500px" Height="100px"
        CancelButton="btnCancel" OnPopupClosed="popUpload_PopupClosed">
        <PopupTemplate>
            <div class="popup-content" style="margin-top: 0px; width: 100%; height: 100%">
                <table class="tabLogin" style="width: 100%">
                    <colgroup>
                        <col width="50%" />
                        <col />
                    </colgroup>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Chọn tài liệu
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding: 0px 5px">
                            <div class="custom-file mb-3">
                                <asp:FileUpload runat="server" ID="fileUpload" class="custom-file-input" Style="width: 100%"></asp:FileUpload>
                                <div id="errUpload" class="err" style="display: none;">
                                    <ul>
                                        <li>Chỉ tải được file (Ảnh)</li>
                                    </ul>
                                </div>
                                <label id="lblCustom" class="custom-file-label" for="customFile">Choose file</label>
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 30px; font-weight: bold">
                        <td colspan="2" style="padding-top: 15px; padding-bottom: 5px;">Mô tả tài liệu
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <tk:TextArea runat="server" ID="txtDescription" Width="100%" MaxLength="512" TextMode="MultiLine" Rows="3" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-toolbar" style="text-align: center">
                <asp:Button runat="server" ID="btnPopupUpload" Text="Tải lên" OnClick="btnPopupUpload_Click" class="btn btn-primary" Style="background: #434a54" />
                <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
            </div>
        </PopupTemplate>
    </tk:PopupPanel>
</div>

<script>
    $("#<%= fileUpload.ClientID %>").height($("#lblCustom").height() + 10);

    $(".custom-file-input").on("change", function (event) {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
</script>