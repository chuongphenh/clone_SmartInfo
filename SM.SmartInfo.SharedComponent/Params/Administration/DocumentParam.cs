using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.EntityInfos;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class DocumentParam : BaseParam
    {

        public DocumentParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public int? DocumentID { get; set; }
        public Document Document { get; set; }
        public List<Document> Documents { get; set; }
        public List<int> TargetIDs { get; set; }
        public List<TargetInfo> TargetInfos { get; set; }

    }
}
