using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.BIZ.Administration
{
    class ZoneBiz : SystemParameterCRUDBaseBiz
    {
        public ZoneBiz() : base(SMX.Features.smx_Zone) { }
    }
}
