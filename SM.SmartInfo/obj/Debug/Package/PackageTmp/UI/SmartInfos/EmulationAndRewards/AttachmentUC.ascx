<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.EmulationAndRewards.AttachmentUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<div class="err">
    <uc:ErrorMessage ID="ucErr" runat="server" />
</div>
<asp:HiddenField runat="server" ID="hidRefType" />
<asp:HiddenField runat="server" ID="hidCode"/>

<style>
    .flex_popup-content {
        max-height: 250px;
    }

    .custom-file-input-att {
        opacity: 0;
        z-index: 2;
        position: relative;
    }

    .custom-file-label-att {
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
        top: 58px;
        width: 214px;
        height: 28px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 1;
        overflow: hidden;
    }

    .grid-header-att > td {
        border: unset;
    }
</style>

<div class="home-block-content">
    <ul class="home-notify" style="padding: 0px">
        <div class="row">
            <div class="col-sm-12" style="background: #fff; padding: 20px 0 0;">
                <div style="width: 100%;">
                    <div class="new-grid">
                        <asp:DataGrid runat="server" ID="grdListDocuments"
                            AutoGenerateColumns="false" AllowPaging="false" AllowCustomPaging="false" HeaderStyle-HorizontalAlign="Center"
                            OnItemDataBound="grdListDocuments_ItemDataBound"
                            OnItemCommand="grdListDocuments_ItemCommand">
                            <HeaderStyle CssClass="grid-header grid-header-att" />
                            <ItemStyle CssClass="grid-item-even" />
                            <AlternatingItemStyle CssClass="grid-item-odd" />
                            <FooterStyle CssClass="grid-footer" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="STT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# (Container.DataSetIndex + 1) %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Loại tài liệu" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" ID="ltrRefCodeName"></asp:Literal>
                                        <asp:HiddenField runat="server" ID="hidAttachmentID" />
                                        <asp:HiddenField runat="server" ID="hidECMItemID" />
                                        <asp:HiddenField runat="server" ID="hidRefCode" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tên tài liệu" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrFileName" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Ngày tải lên" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" ID="ltrCreatedDTG"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Người tải lên" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" ID="ltrCreatedBy"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tải lên" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtUpload" CommandName="Upload" OnClientClick="setOffsetPosition();"><i class="fa fa-upload"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages" CssClass="grid-pager" PageButtonCount="10" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
    </ul>
</div>

<tk:PopupPanel runat="server" ID="popAttachment" Width="800px" Title="TẢI LÊN TÀI LIỆU">
    <PopupTemplate>
        <table class="tableDisplay" width="100%">
            <col width="250" />
            <col />
            <col width="250" />
            <col />
            <tr>
                <th>Loại tài liệu <span class="star">*</span></th>
                <td>
                    <asp:Label ID="lbRefCode" runat="server"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlRefCode" Width="220px" DataValueField="Code" DataTextField="Name" AutoPostBack="true" Visible="false" />
                </td>
                <th>Chọn tài liệu<span class="star">*</span></th>
                <td>
                    <div class="custom-file mb-3">
                        <asp:FileUpload runat="server" ID="fupDocument" class="custom-file-input-att" Style="width: 220px" AllowMultiple="true" accept=".doc,.docx,.xls,.xlsx,.pdf,.png,.jpg,.gif,.jpeg,.bmp" />
                        <label id="lblCustomAtt" class="custom-file-label-att" for="customFile">Choose file</label>
                    </div>
                    <asp:RegularExpressionValidator ID="revfulDocument"
                        Display="Dynamic" ValidationGroup="groupPopupAttachment"
                        ErrorMessage="Chỉ tải được file(Ảnh, Word, Excel và PDF)."
                        ControlToValidate="fupDocument" runat="server" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.gif|.GIF|.jpg|.JPG|.jpeg|.JPEG|.png|PNG|.doc|.DOC|.docx|.DOCX|.xls|.XLS|.xlsx|.XLSX|.pdf|.PDF)$" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <th>
                    Mô tả
                </th>
                <td colspan="3">
                    <tk:TextArea ID="txtDescription" runat="server" Width="99%" TextMode="MultiLine" Rows="4">
                    </tk:TextArea>
                </td>
            </tr>
        </table>
        <div class="submit popup-toolbar" style="text-align: center; padding-bottom: 10px">
            <asp:Button runat="server" ID="btnAddDocmument" Text="Tải lên" OnClick="btnAddDocmument_Click" class="btn btn-primary" Style="background: #434a54" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>

<%--<script>
    $("#<%= fupDocument.ClientID %>").height($("#lblCustomAtt").height() + 10);

    $(".custom-file-input-att").on("change", function (event) {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label-att").addClass("selected").html(fileName);
    });
</script>--%>
