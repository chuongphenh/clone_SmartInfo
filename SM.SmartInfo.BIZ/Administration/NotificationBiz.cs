using SM.SmartInfo.SharedComponent.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.BIZ.Administration
{
    class NotificationBiz : SystemParameterCRUDBaseBiz
    {
        public NotificationBiz() : base(SMX.Features.Notification) { }
    }
}
