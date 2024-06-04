using System;
using SM.SmartInfo.DAO.Comment;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Notification;

namespace SM.SmartInfo.BIZ.Comment
{
    class CommentBiz : BizBase
    {
        private CommentDao _dao = new CommentDao();
        private NotificationDao _daoNoti = new NotificationDao();

        public void GetAllCommentByRefIDAndRefType(CommentParam param)
        {
            int? refID = param.RefID;
            int? refType = param.RefType;
            int? notiType = param.TypeNoti;
            param.ListComment = _dao.GetAllCommentByRefIDAndRefType(refID, refType, notiType);
        }

        public void InsertUpdateCommentByRefIDAndRefType(CommentParam param)
        {
            if (Profiles.MyProfile == null) return;
            var comment = param.Comment;
            if (comment.CommentID != null && comment.CommentedByID != Profiles.MyProfile.EmployeeID)
                throw new SMXException("Không được quyền cập nhật comment của người khác");

            if (string.IsNullOrWhiteSpace(comment.Content))
                throw new SMXException("Bạn chưa nhập nội dung bình luận");

            switch (comment.RefType)
            {
                case SMX.CommentRefType.Birthday:
                case SMX.CommentRefType.Anniversary:
                case SMX.CommentRefType.Establishday:
                case SMX.CommentRefType.Holiday:
                case SMX.CommentRefType.Other:
                case SMX.CommentRefType.Notification:
                    comment.RefTitle = _daoNoti.GetNotificationByID(comment.RefID, (int)comment.RefType)?.Content;
                    break;
                case SMX.CommentRefType.NegativeNews:
                case SMX.CommentRefType.News:
                    comment.RefTitle = _dao.GetItemByID<SharedComponent.Entities.News>(comment.RefID)?.Name;
                    break;
                case SMX.CommentRefType.PressAgency:
                    comment.RefTitle = _dao.GetItemByID<agency_PressAgency>(comment.RefID)?.Name;
                    break;
            }

            if (comment.CommentID == null)
            {
                comment.CommentedDTG = DateTime.Now;
                comment.Version = SMX.smx_FirstVersion;
                _dao.InsertItem(comment);
            }
            else
                _dao.UpdateItem(comment);
        }

        public void DeleteCommentByID(CommentParam param)
        {
            var commentID = param.CommentID;
            var cm = param.Comment;
            if (!_dao.CheckDeleteComment(cm.CommentID, cm.Version))
                throw new SMXException("Không thành công");
            _dao.DeleteCommentByID(commentID, cm.Version, cm.CommentedByID, cm.Rate);
        }
    }
}