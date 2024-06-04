using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.CommonList
{
    public class AttachmentParam : BaseParam
    {
        public AttachmentParam(string functionType)
            : base(Constants.BusinessType.CommonList, functionType)
        {
        }

        public adm_Attachment adm_Attachment { get; set; }
        public List<adm_Attachment> adm_Attachments { get; set; }
        public List<int> AttachmentIDs { get; set; }
        public int? AttachmentID { get; set; }
    }
}