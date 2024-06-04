using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using System.Collections.Generic;

namespace SM.SmartInfo.BIZ.Administration
{
    class SegmentBiz : SystemParameterCRUDBaseBiz
    {
        public SegmentBiz() : base(SMX.Features.smx_Segment) { }

        protected override void ValidateItemDetail(SystemParameter item, List<string> lstMsg)
        {
            if (item.Ext1i == null)
                AddRequireErrorMessage("Đường/Phố", lstMsg);

            if (string.IsNullOrWhiteSpace(item.Ext1))
                AddRequireErrorMessage("Đoạn từ", lstMsg);
        }
    }
}
