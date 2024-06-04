namespace SM.SmartInfo.SharedComponent.Constants
{
    public enum EmployeeSelectorMode
    {
        /// <summary>
        /// Tim trong tat ca cac phong
        /// </summary>
        All,
        /// <summary>
        /// Tim trong phong dc chi dinh
        /// </summary>
        OrganizationID,
        /// <summary>
        /// Tim trong phong cua user hien tai
        /// </summary>
        CurrentUserOrg,
        /// <summary>
        /// Tim trong phong cua user hien tai va phia duoi
        /// </summary>
        CurrentUserOrgAndBelow,

        /// <summary>
        /// tim trong phòng với điều kiện type
        /// </summary>
        OrganizationType,

        /// <summary>
        /// Search by list of RoleID
        /// </summary>
        RoleIDs
    }

    public enum OrganizationSelectorTreeMode
    {
        /// <summary>
        /// Tim tat ca
        /// </summary>
        All,
        /// <summary>
        /// Tim theo loai phong
        /// </summary>
        Type,
    }
    public enum FlexWorkflowStatus : int
    {
        Open = SoftMart.Core.Workflow.SharedComponent.Constants.ApprovalStatus.Open,
        Processing = SoftMart.Core.Workflow.SharedComponent.Constants.ApprovalStatus.Processing,
        Approving = SoftMart.Core.Workflow.SharedComponent.Constants.ApprovalStatus.Approving,
        Done = SoftMart.Core.Workflow.SharedComponent.Constants.ApprovalStatus.Done,
        Rejected = SoftMart.Core.Workflow.SharedComponent.Constants.ApprovalStatus.Rejected,
        Cancelled = SoftMart.Core.Workflow.SharedComponent.Constants.ApprovalStatus.Cancelled,
    }

    public enum ModifyMode
    {
        None,
        Edit,
        Display,
    }
}
