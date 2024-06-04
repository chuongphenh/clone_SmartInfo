namespace SM.SmartInfo.SharedComponent.EntityInfos
{
    public class BatchProcessingInfo
    {
        public int FunctionType { get; set; }
        /// <summary>
        /// Title to show on form
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Mapping file name. Ex:..HopDong.xml
        /// </summary>
        public string MappingFileName { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="functionType">Function type</param>
        /// <param name="title">Title to show on form</param>
        /// <param name="mappingFileName">Name of Mapping file</param>
        public BatchProcessingInfo(int functionType, string title, string mappingFileName)
        {
            this.FunctionType = functionType;
            this.Title = title;
            this.MappingFileName = mappingFileName;
        }
        public static class BatchProcessingType
        {
        }
    }
}
