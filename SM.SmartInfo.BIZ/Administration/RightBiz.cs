using System;
using System.Collections.Generic;
using System.Linq;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Core.Dao;
using SM.SmartInfo.DAO.Commons;
using SM.SmartInfo.BIZ.Commons;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.BIZ.Administration
{
    class RightBiz : BizBase
    {
        RightDao _dao = new RightDao();

        FeatureDao _daoFeature = new FeatureDao();

        RoleDao _daoRole = new RoleDao();

        public void SetupViewForm(RightParam param)
        {
            param.Features = _daoFeature.GetAllActiveFeature();
        }

        public void GetItemsForView(RightParam param)
        {
            int featureID = param.Right.FeatureID.Value;
            int? employeeID = param.Right.EmployeeID;

            //01.Get FeatureFunction (Add, Edit, Delete, Detail,...)
            param.Functions = _dao.GetFunctionsByFeatureID(featureID);

            List<BuildRightConfig> lstRight = new List<BuildRightConfig>();
            if (employeeID != null)
            {
                //02.Get Employee
                UserDao daoUser = new UserDao();
                Employee employee = daoUser.GetActiveEmployee(employeeID.Value);
                lstRight.Add(new BuildRightConfig() { ItemID = employeeID.Value, Name = string.Format("{0} - {1}", employee.Username, employee.Name) });

                //03.Get FeatureFunctionID for EmployeeID-FeatureID
                foreach (var item in lstRight)
                {
                    item.FunctionIDs = _dao.GetFunctionIDsByItemID(featureID, null, item.ItemID);
                }
            }
            else
            {
                //04.Get AllRole
                List<Role> lstRole = _daoRole.GetAllActiveRole();
                foreach (var item in lstRole)
                {
                    // VD: Chuyên viên, QTHT,...
                    lstRight.Add(new BuildRightConfig() { ItemID = item.RoleID.Value, Name = item.Name });
                }

                //05.Get FeatureFunctionID for RoleID-FeatureID
                foreach (var item in lstRight)
                {
                    item.FunctionIDs = _dao.GetFunctionIDsByItemID(featureID, item.ItemID, null);
                }
            }

            param.BuildRightConfigs = lstRight;
        }

        public void SaveItem(RightParam param)
        {
            int featureID = param.FeatureId.Value;

            bool isEmployee = param.IsEmployee;
            //Prepare Data
            List<Right> lstRight = new List<Right>();
            foreach (var item in param.BuildRightConfigs)
            {
                foreach (int? functionID in item.FunctionIDs)
                {
                    Right right = new Right();
                    right.HasPermission = true;

                    if (isEmployee)
                        right.EmployeeID = item.ItemID;
                    else
                        right.RoleID = item.ItemID;

                    right.FunctionID = functionID;
                    right.FeatureID = featureID;
                    lstRight.Add(right);
                }
            }

            //data tracking
            DataTrackingBiz dataTrackingBiz = new DataTrackingBiz();
            string logString = "";
            string strRightName = "";
            string featureName = _daoFeature.GetAllActiveFeature().FirstOrDefault(f => f.FeatureID == featureID).Name;
            List<Function> lstFunctions = _dao.GetFunctionsByFeatureID(featureID);
            if (isEmployee)
            {
                UserDao userDao = new UserDao();
                Employee em = userDao.GetActiveEmployee(param.BuildRightConfigs.FirstOrDefault().ItemID.Value);
                foreach (Right right in lstRight)
                {
                    strRightName += (lstFunctions.FirstOrDefault(f => f.FunctionID == right.FunctionID).Name + ", ");
                }
                logString += "Người dùng " + em.Username + ", Nhóm chức năng: " + featureName + Environment.NewLine + "Chức năng: " + strRightName;
            }
            else
            {
                List<Role> lstRole = _daoRole.GetAllActiveRole();
                foreach (Role role in lstRole)
                {
                    if (lstRight.Where(r => r.RoleID == role.RoleID).ToList().Count == 0) continue;
                    strRightName += role.Name + ": ";
                    foreach (Right right in lstRight.Where(r => r.RoleID == role.RoleID).ToList())
                    {
                        strRightName += (lstFunctions.FirstOrDefault(f => f.FunctionID == right.FunctionID).Name + ", ");
                    }
                    strRightName += Environment.NewLine;
                }
                logString += "Nhóm người dùng, " + "Nhóm chức năng:" + featureName + Environment.NewLine + "Chức năng: " + strRightName;
            }
            using (var trans = new TransactionScope())
            {

                if (isEmployee)
                {
                    _dao.DeleteItems(featureID, param.BuildRightConfigs.FirstOrDefault().ItemID, null);
                }
                else
                {
                    foreach (var item in param.BuildRightConfigs)
                    {
                        _dao.DeleteItems(featureID, null, item.ItemID);
                    }
                }

                //Insert all Rights
                _dao.InsertRight(lstRight);
                dataTrackingBiz.LogDataTracking(SMX.DataTracking.Feature.Administrator, SMX.DataTracking.ActionType.ConfigRight,
                    "", logString, SMX.DataTracking.RefType.AdminRightConfigsRole, null);

                trans.Complete();
            }
        }
    }
}