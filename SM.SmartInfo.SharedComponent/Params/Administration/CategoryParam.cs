using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class CategoryParam : BaseParam
    {
        public CategoryParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public int? CategoryID { get; set; }
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}
