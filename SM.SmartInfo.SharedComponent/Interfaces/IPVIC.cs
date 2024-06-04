using System;

namespace SM.SmartInfo.SharedComponent.Interfaces
{
    public interface ISystemEntity
    {
        int? ItemID { get; set; }
        int? Version { get; set; }
        int? Deleted { get; set; }
        string CreatedBy { get; set; }
        DateTime? CreatedDTG { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdatedDTG { get; set; }
    }

    public interface IComment
    {
        int? TableType { get; set; }
        string PropertyName { get; set; }
        string CommentContent { get; set; }
        DateTime? CommentedDTG { get; set; }
        int? CommentedByID { get; set; }
        string CommentedByName { get; set; }
    }

    public interface IValuationDocumentComment
    {
        string Content { get; set; }
        DateTime? CommentedDTG { get; set; }
        string CommentedBy { get; set; }
    }
}