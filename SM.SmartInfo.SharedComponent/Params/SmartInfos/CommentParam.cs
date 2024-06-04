using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.SmartInfos
{
    public class CommentParam : BaseParam
    {
        public CommentParam(string functionType)
            : base(Constants.BusinessType.Comment, functionType)
        {
        }

        public Comment Comment { get; set; }

        public List<Comment> ListComment { get; set; }

        public int? CommentID { get; set; }

        public int? TypeNoti { get; set; }
        public int? RefID { get; set; }

        public int? RefType { get; set; }
    }
}