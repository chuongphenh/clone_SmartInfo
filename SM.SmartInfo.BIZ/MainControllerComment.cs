using SM.SmartInfo.BIZ.Comment;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private void ExecuteComment(CommentParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.Comment.GetAllCommentByRefIDAndRefType:
                    {
                        var biz = new CommentBiz();
                        biz.GetAllCommentByRefIDAndRefType(param);
                        break;
                    }
                case FunctionType.Comment.InsertUpdateCommentByRefIDAndRefType:
                    {
                        var biz = new CommentBiz();
                        biz.InsertUpdateCommentByRefIDAndRefType(param);
                        break;
                    }
                case FunctionType.Comment.DeleteCommentByID:
                    {
                        var biz = new CommentBiz();
                        biz.DeleteCommentByID(param);
                        break;
                    }
            }
        }
    }
}