namespace SM.SmartInfo.SharedComponent.Params
{
    public abstract class BaseParam
    {
        [System.Xml.Serialization.XmlIgnore]
        public string BusinessType { get; private set; }
        public string FunctionType { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public SoftMart.Kernel.Entity.PagingInfo PagingInfo { get; set; }

        protected BaseParam(string businessType, string functionType)
        {
            BusinessType = businessType;
            FunctionType = functionType;
        }
    }
}
