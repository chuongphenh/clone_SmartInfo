using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Setting
{
    public class ItemTemplateCheckBox : ITemplate
    {
        private string colName;
        public ItemTemplateCheckBox(string name)
        {
            colName = name;
        }
        public void InstantiateIn(Control container)
        {
            CheckBox box = new CheckBox();
            box.ID = colName;
            container.Controls.Add(box);
        }

    }

    public class ItemTemplateCheckBoxReadOnly : ITemplate
    {
        private string colName;
        public ItemTemplateCheckBoxReadOnly(string name)
        {
            colName = name;
        }
        public void InstantiateIn(Control container)
        {
            CheckBox box = new CheckBox();
            box.ID = colName;
            box.Attributes.Add("onclick", "return false;");
            container.Controls.Add(box);
        }

    }

    public class ItemTemplateHiddenField : ITemplate
    {
        private string _ID;
        public ItemTemplateHiddenField(string id)
        {
            _ID = id;
        }
        public void InstantiateIn(Control container)
        {
            HiddenField hi = new HiddenField();
            hi.ID = _ID;
            container.Controls.Add(hi);
        }
    }

    public class ItemTemplateLiteral : ITemplate
    {
        private string _ID;
        public ItemTemplateLiteral(string id)
        {
            _ID = id;
        }
        public void InstantiateIn(Control container)
        {
            Literal hi = new Literal();
            hi.ID = _ID;
            container.Controls.Add(hi);
        }
    }
}