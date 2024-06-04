
namespace SM.SmartInfo.PermissionManager.Shared
{
    interface IPermission
    {
        void CheckPagePermission(PermissionParam param);

        void CheckItemPermission(PermissionParam param);

        void GetTemporaryViewDataPermission(PermissionParam param);
    }
}
