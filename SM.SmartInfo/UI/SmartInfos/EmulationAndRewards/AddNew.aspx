<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.SmartInfos.EmulationAndRewards.AddNew" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/SmartInfos/EmulationAndRewards/AttachmentUC.ascx" TagName="AttachmentUC" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        p {
            font-family: Tahoma;
            font-size: 12px;
        }

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
            top: 2px;
            width: 100%;
            text-align: center;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }
        .custom-btn-action a{
            background: #fff;
            border-radius:4px;
            padding:5px 10px;
        }
    </style>
    <div style="margin-top: 10px;">
        <div class="body-content">
            <asp:HiddenField ID="hidFileDonVi_Name" runat="server" />
            <asp:HiddenField ID="hidFileCaNhan_Name" runat="server" />
            <section style="padding-right: 35px; padding-left: 35px">
                <div class="row blog-page">
                    <div class="col-sm-12 md-margin-bottom-60">
                        <div class="row">
                            <div class="col-md-10" style="padding-left: 0px; color: #595959">
                                <span style="font-size: 16px; color: #262626; text-align: left; font-weight: bold">Hồ sơ khen thưởng</span>
                            </div>
                            <div class="col-md-2 custom-btn-action custom-link-focus" style="padding-right: 0px; padding-bottom: 5px">
                                <asp:LinkButton ID="btnExit" runat="server" OnClick="btnExit_Click" Style="float: right; margin-left: 5px">
                                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16px;  border-radius: 4px;" title="Thoát"></i>
                                     <span style="color: black;font-weight:bold;font-size:14px;">Thoát</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Style="float: right; margin-left: 5px">
                                    <i class="far fa-save" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16px; border-radius: 4px;" title="Lưu"></i>
                                     <span style="color: black;font-weight:bold;font-size:14px;">Lưu</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <ul class="press-agency-info" style="margin-bottom: 10px">
                            <li>
                                <div class="row">
                                    <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">Năm: <span class="star">*</span></span></span>
                                    </div>
                                    <div class="col-sm-8" style="padding-right: 0px;">
                                        <tk:NumericTextBox ID="numYear" runat="server" Width="100%" AllowThousandDigit="false" NumberDecimalDigit="0" MinValue="1970" MaxValue="9999"></tk:NumericTextBox>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="row">
                                    <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">Đợt: </span>
                                    </div>
                                    <div class="col-sm-8" style="padding-right: 0px;">
                                        <asp:DropDownList ID="ddlAwardingPeriod" runat="server" Width="50%" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="row">
                                    <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">Đơn vị khen thưởng: <span class="star">*</span></span>
                                    </div>
                                    <div class="col-sm-8" style="padding-right: 0px;">
                                        <asp:TextBox ID="txtUnit" runat="server" MaxLength="256" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="row">
                                    <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">Khen thưởng:</span>
                                    </div>
                                    <div class="col-sm-8" style="padding-right: 0px;">
                                        <asp:DropDownList Width="50%" ID="ddlAwardingLevel" runat="server" OnSelectedIndexChanged="ddlAwardingLevel_SelectedIndexChanged" DataValueField="Category" DataTextField="Level" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="row">
                                    <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">Hình thức khen thưởng:</span>
                                    </div>
                                    <div class="col-sm-8" style="padding-right: 0px;">
                                        <asp:DropDownList Width="50%" ID="ddlAwardingType" runat="server" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                                    </div>
                                </div>
                            </li>
                            <%--<li style="width: 100%">
                                <div class="row">
                                    <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">Tải ảnh lên:</span>
                                    </div>
                                    <div class="col-sm-8" style="padding-right: 0px;">
                                        <asp:FileUpload ID="fileUploadImages" runat="server" AllowMultiple="true" />
                                    </div>
                                </div>
                            </li>--%>
                            <li style="width: 100%" id="liCaNhan" runat="server">
                                <div class="row">
                                    <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">File Upload Cá Nhân:</span>
                                    </div>
                                    <div class="col-sm-8" style="padding-right: 0px;">
                                        <asp:FileUpload ID="fileUploadCaNhan" runat="server" />
                                        <asp:LinkButton runat="server" ID="btnDownloadTemplate_CaNhan" OnClick="btnDownloadTemplate_CaNhan_Click">Tải file mẫu</asp:LinkButton>
                                    </div>
                                </div>
                            </li>
                            <li style="width: 100%" id="liDonVi" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">File Upload Đơn Vị:</span>
                                    </div>
                                    <div class="col-sm-8" style="padding-right: 0px;">
                                        <asp:FileUpload ID="fileUploadDonVi" runat="server" />
                                        <asp:LinkButton runat="server" ID="btnDownloadTemplate_DonVi" OnClick="btnDownloadTemplate_DonVi_Click">Tải file mẫu</asp:LinkButton>
                                    </div>
                                </div>
                            </li>
                            <%--<li style="width:100%" id="liCaNhan" runat="server">
                                <div class="row">
                                    <div class="col-sm-2" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">Upload danh sách cá nhân được khen thưởng</span>
                                    </div>
                                    <div class="col-sm-1" style="padding-right: 0px; margin-left: -2px; padding-left: 0px">
                                        <div class="custom-file mb-3">
                                            <asp:FileUpload title=" " runat="server" ID="fileUploadCaNhan" class="custom-file-input custom-file-input-canhan"></asp:FileUpload>
                                            <div id="errUploadCaNhan" class="err" style="display: none;">
                                                <ul>
                                                    <li>Chỉ tải được file (Excel)</li>
                                                </ul>
                                            </div>
                                            <label id="lblCustomCaNhan" class="custom-file-label custom-file-label-canhan" for="customFile">Choose file</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-1" style="padding-right: 0px;" id="divTemplate_CaNhan">
                                        <div class="custom-file mb-3" style="margin-top: 10px">
                                            <asp:LinkButton runat="server" ID="btnDownloadTemplate_CaNhan" OnClick="btnDownloadTemplate_CaNhan_Click">Tải file mẫu</asp:LinkButton>
                                            <asp:Button runat="server" ID="btnReuploadFile_CaNhan" OnClick="btnReuploadFile_CaNhan_Click" style="display: none"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            
                            <li style="width:100%" id="liDonVi" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-sm-2" style="padding-left: 0px; display: flex;">
                                        <span style="font-weight: 600">Upload danh sách đơn vị được khen thưởng</span>
                                    </div>
                                    <div class="col-sm-1" style="padding-right: 0px; margin-left: -2px; padding-left: 0px">
                                        <div class="custom-file mb-3">
                                            <asp:FileUpload title=" " runat="server" ID="fileUploadDonVi" class="custom-file-input custom-file-input-donvi"></asp:FileUpload>
                                            <div id="errUploadDonVi" class="err" style="display: none;">
                                                <ul>
                                                    <li>Chỉ tải được file (Excel)</li>
                                                </ul>
                                            </div>
                                            <label id="lblCustomDonVi" class="custom-file-label custom-file-label-donvi" for="customFile">Choose file</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-9" style="padding-right: 0px;" id="divTemplate_DonVi">
                                        <div class="custom-file mb-3" style="margin-top: 10px">
                                            <asp:LinkButton runat="server" ID="btnDownloadTemplate_DonVi" OnClick="btnDownloadTemplate_DonVi_Click">Tải file mẫu</asp:LinkButton>
                                            <asp:Button runat="server" ID="btnReuploadFile_DonVi" OnClick="btnReuploadFile_DonVi_Click" style="display: none"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </li>--%>
                        </ul>
                        <div class="row" style="margin-bottom: 5px">
                            <div class="col-md-12" style="padding-left: 0px; color: #595959">
                                <span style="font-size: 14px; color: #595959; text-align: left; font-weight: 600">Hồ sơ khen thưởng</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="padding-left: 0px; color: #595959">
                                <uc:AttachmentUC ID="ucAttachment" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>

    <%--<script>
        $("#<%= fileUploadCaNhan.ClientID %>").height($("#lblCustomCaNhan").height() + 10);
        $("#<%= fileUploadCaNhan.ClientID %>").width($("#lblCustomCaNhan").width() + 20);

        $("#divTemplate_CaNhan").height($("#lblCustomCaNhan").height() + 10);

        $("#<%= fileUploadDonVi.ClientID %>").height($("#lblCustomDonVi").height() + 10);
        $("#<%= fileUploadDonVi.ClientID %>").width($("#lblCustomDonVi").width() + 20);

        $("#divTemplate_DonVi").height($("#lblCustomDonVi").height() + 10);

        $(".custom-file-input-canhan").on("change", function (event) {
            var fileName = $(this).val().split("\\").pop();
            if (fileName != "")
                $(this).siblings(".custom-file-label-canhan").addClass("selected").html(fileName);
            else
                $(this).siblings(".custom-file-label-canhan").addClass("selected").html("Choose file");

            $("#<%= btnReuploadFile_CaNhan.ClientID %>").click();
        });

        $(".custom-file-input-donvi").on("change", function (event) {
            var fileName = $(this).val().split("\\").pop();
            if (fileName != "")
                $(this).siblings(".custom-file-label-donvi").addClass("selected").html(fileName);
            else
                $(this).siblings(".custom-file-label-donvi").addClass("selected").html("Choose file");

            $("#<%= btnReuploadFile_DonVi.ClientID %>").click();
        });

        function resetFileName() {
            var fileName_CaNhan = $("#<%= hidFileCaNhan_Name.ClientID %>").val();
            if (fileName_CaNhan != "")
                $("#lblCustomCaNhan").html(fileName_CaNhan);
            else
                $("#lblCustomCaNhan").html("Choose file");

            var fileName_DonVi = $("#<%= hidFileDonVi_Name.ClientID %>").val();
            if (fileName_DonVi != "")
                $("#lblCustomDonVi").html(fileName_DonVi);
            else
                $("#lblCustomDonVi").html("Choose file");
        }
    </script>--%>
</asp:Content>