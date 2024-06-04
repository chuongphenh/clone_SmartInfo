using SM.SmartInfo.SharedComponent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Params.SmartInfos
{
    public class FirebaseParam : BaseParam
    {
        public FirebaseParam(string functionType)
            : base(Constants.BusinessType.Comment, functionType)
        {
        }

        public ntf_NotificationPushHistory ntf_NotificationPushHistory { get; set; }

        public List<ntf_NotificationPushHistory> ntf_NotificationPushHistorys { get; set; }
    }
}
