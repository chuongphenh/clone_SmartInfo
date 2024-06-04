<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.CommentUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<style>
    .table-comment {
        width: 100%;
    }

        .table-comment > tbody > tr {
            height: 50px;
        }

            .table-comment > tbody > tr > td {
                vertical-align: middle;
                border: unset;
                padding-left: 0px;
                padding-right: 0px;
                padding: 7px;
            }

    .comment-content {
        margin-left: 40px;
        border-radius: 25px;
        padding-bottom: 5px;
        padding-top: 5px;
        padding-right: 20px;
        padding-left: 20px;
    }

    .comment-button {
        position: absolute;
        right: 11px;
        z-index: 9;
        margin-top: 9px;
        color: #595959;
    }

        .comment-button:hover {
            text-decoration: unset;
            color: #595959;
        }

    .normal {
        background: #f2f3f8 !important;
    }

    .important {
        background: #ffc0bc !important;
    }
</style>

<asp:HiddenField ID="hidRefID" runat="server" />
<asp:HiddenField ID="hidIsEdit" runat="server" />
<asp:HiddenField ID="hidRefType" runat="server" />
<asp:HiddenField ID="hidTypeNotification" runat="server" />
<div class="" style="width:100%">
    <div class="data-table" style="border: unset; max-height: 320px; overflow: auto;">
        <table class="table-comment" style="border: unset">
            <tbody>
                <tr id="trComment" runat="server">
                    <td style="width: 15%">
                        <asp:DropDownList ID="ddlRate" runat="server" Style="font-size: 13px; width: 100%; border-radius: 8px; height: 32px;"></asp:DropDownList>
                    </td>
                    <td style="width: 85%">
                        <div style="position: relative">
                            <asp:LinkButton ToolTip="Gửi bình luận" CssClass="comment-button fa fa-paper-plane" ID="btnComment" runat="server" OnClick="btnComment_Click"></asp:LinkButton>
                            <tk:TextArea ID="txtCommentContent" runat="server" CssClass="form-control" Style="border-radius: 8px; padding-right: 30px" placeholder="Viết bình luận ..." TextMode="MultiLine" Rows="1"></tk:TextArea>
                        </div>
                    </td>
                </tr>
                <asp:Repeater ID="rptComment" runat="server" OnItemDataBound="rptComment_ItemDataBound" OnItemCommand="rptComment_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 100%" colspan="2">
                                <svg style="float: left; margin-right: 20px; margin-top: 7px;" width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <circle cx="16" cy="16" r="16" fill="#85A5FF" />
                                    <path d="M22 22V20.6667C22 19.1939 20.6569 18 19 18H13C11.3431 18 10 19.1939 10 20.6667V22" stroke="white" stroke-linecap="round" stroke-linejoin="round" />
                                    <path fill-rule="evenodd" clip-rule="evenodd" d="M16 15C17.6569 15 19 13.6569 19 12C19 10.3431 17.6569 9 16 9C14.3431 9 13 10.3431 13 12C13 13.6569 14.3431 15 16 15Z" stroke="white" stroke-linecap="round" stroke-linejoin="round" />
                                </svg>
                                <div class="comment-content" id="divComment" runat="server">
                                    <asp:HiddenField ID="hidCMID" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidRate" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidCommentedByID" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidVersion" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidTypeNoti" runat="server" />
                                    <span style="font-weight: 600; font-size: 14px">
                                        <asp:Literal ID="ltrName" runat="server"></asp:Literal></span>
                                    <span style="float: right; font-size: 13px;">
                                        <asp:Literal ID="ltrCommentedDTG" runat="server"></asp:Literal></span>
                                    <br />
                                    <span style="font-size: 13px; display: block;">
                                        <asp:Literal ID="ltrContent" runat="server"></asp:Literal></span>
                                    <asp:LinkButton ID="btnEdit" runat="server" Style="float: right; margin-top: -18px; margin-right: 25px;">
                                        <i class="fas fa-pencil-alt" aria-hidden="true" style="color: #595959;" title="Chỉnh sửa bình luận"></i>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnDelete" runat="server" Style="float: right; margin-top: -18px;" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa bình luận này?')">
                                        <i class="fas fa-trash" aria-hidden="true" style="color: #595959;" title="Xóa bình luận"></i>
                                    </asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</div>
