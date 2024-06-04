using SM.SmartInfo.SharedComponent.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.SmartInfo.BIZ.Administration
{
    class CountryBiz : SystemParameterCRUDBaseBiz
    {
        public CountryBiz() : base(SMX.Features.CountryOfManufacturer) { }
    }
}
