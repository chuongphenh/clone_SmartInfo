using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class EmailTemplateParam : BaseParam
    {

        public EmailTemplateParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public int? EmailTemplateID { get; set; }
        public Flex_EmailTemplate EmailTemplate { get; set; }
        public List<Flex_EmailTemplate> EmailTemplates { get; set; }

    }
}
