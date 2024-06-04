using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.DAO.Common;

using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System.Data.SqlClient;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SoftMart.Core.Dao;
using SoftMart.Kernel.Exceptions;

namespace SM.SmartInfo.DAO.Administration
{
    public class OrganizationConfigDao : BaseDao
    {
        public void InsertOrganization(Organization item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<Organization>(item);
            }
        }

        public void UpdateOrganization(Organization item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<Organization>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(Messages.ItemNotExitOrChanged);
            }
        }

        public void InsertOrganizationEmployee(List<OrganizationEmployee> lstItem)
        {
            using (var dataContext = new DataContext())
            {
                foreach (OrganizationEmployee item in lstItem)
                {
                    dataContext.InsertItem<OrganizationEmployee>(item);
                }
            }
        }

        public void InsertOrganizationManager(List<OrganizationManager> lstItem)
        {
            using (var dataContext = new DataContext())
            {
                foreach (var item in lstItem)
                {
                    dataContext.InsertItem<OrganizationManager>(item);
                }
            }
        }

        public bool CheckDuplicatedCode(int? organizationID, string code)
        {
            string cmdText = @"select count(*)
                                from Organization
                                where Deleted=0 and Code=@Code 
                                    and (@OrganizationID is null Or OrganizationID<>@OrganizationID)";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@OrganizationID", organizationID);
            sqlCmd.Parameters.AddWithValue("@Code", code);

            using (var dataContext = new DataContext())
            {
                int count = dataContext.ExecuteSelect<int>(sqlCmd).FirstOrDefault();
                return count > 0;
            }
        }

        public List<Organization> GetAllShortOrganization()
        {
            using (var dataContext = new DataContext())
            {
                var res = dataContext.SelectFieldsByColumnName<Organization>(
                    new string[] { Organization.C_OrganizationID, Organization.C_ParentID, Organization.C_Name, Organization.C_OfficeID },
                                                                             new ConditionList());
                return res;
            }
        }

        public int GetChildrenCount(int organizationID)
        {
            using (var dataContext = new DataContext())
            {
                int count = dataContext.CountItemByColumnName<Organization>(Organization.C_ParentID, organizationID);
                return count;
            }
        }

        public Organization GetOrganizationInfoByID(int organizationID)
        {
            string sql = @"select enVOrg.BreadCrumb, enOrganization.ParentID,
                                enOrganization.OrganizationID,
                                enOrganization.Code, enOrganization.Name,
                                enOrganization.RuleID, enOrganization.Priority,
                                enOrganization.ZoneID, enOrganization.CommitteeID,
                                enOrganization.Description, enOrganization.Version,
                                enOrganization.DispatchEmployeeRuleID,enOrganization.Type,
                                enOrganization.OfficeID, 
                                enOrganization.Address,enOrganization.Province,
                                enParent.ZoneID as ParentZoneID,                             
                                enZone.Name as ZoneName, 
                                enCommitee.Name as CommitteeName, enOrganization.BranchEmail
                        from Organization as enOrganization
                        inner join vOrganization as enVOrg on enOrganization.OrganizationID = enVOrg.OrganizationID
                        left join Organization as enParent on enOrganization.ParentID = enParent.OrganizationID
                        left join adm_SystemParameter as enZone on enOrganization.ZoneID = enZone.SystemParameterID
                        left join Committee as enCommitee on enOrganization.CommitteeID = enCommitee.CommitteeID
                        where enOrganization.Deleted = 0
                            AND enOrganization.OrganizationID = @OrganizationID";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@OrganizationID", organizationID);

            using (DataContext dataContext = new DataContext())
            {
                var item = dataContext.ExecuteSelect<Organization>(command).FirstOrDefault();
                return item;
            }
        }

        public List<EmployeeInfo> GetEmployeeInOrganizationByOrgID(int organizationID)
        {
            string sql = @"select enEmployee.EmployeeID, enEmployee.Name, enEmployee.Username,
                                  enEmployee.Gender, enEmployee.Mobile, enEmployee.Email, enEmployee.Description
                            from adm_Employee as enEmployee
                                inner join OrganizationEmployee as enOrgEmp on enOrgEmp.EmployeeID = enEmployee.EmployeeID and enEmployee.Deleted = 0
                            where enOrgEmp.Deleted = 0 AND enOrgEmp.OrganizationID = @OrganizationID";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@OrganizationID", organizationID);

            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<EmployeeInfo>(command);
                return res;
            }
        }

        public List<EmployeeInfo> GetManagerInOrganizationByOrgID(int organizationID)
        {
            string sql = @"select enEmployee.EmployeeID, enEmployee.Name, enEmployee.Username,
                                  enEmployee.Gender, enEmployee.Mobile, enEmployee.Email, enEmployee.Description
                            from adm_Employee as enEmployee
                                inner join OrganizationManager as enOrgMng on enOrgMng.EmployeeID = enEmployee.EmployeeID and enEmployee.Deleted = 0
                            where enOrgMng.Deleted = 0 AND enOrgMng.OrganizationID = @OrganizationID";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@OrganizationID", organizationID);

            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<EmployeeInfo>(command);
                return res;
            }
        }

        public List<Committee> GetAllCommittees()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.SelectItemByColumnName<Committee>("1", "1");
            }
        }

        public void DeleteOrganizationEmployee(int organizationID)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.DeleteItemByColumn<OrganizationEmployee>(OrganizationEmployee.C_OrganizationID, organizationID);
            }
        }

        public void DeleteOrganizationManager(int organizationID)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.DeleteItemByColumn<OrganizationManager>(OrganizationManager.C_OrganizationID, organizationID);
            }
        }

        public bool ValidateEmployeeIsInOtherOrganization(int employeeID, int? organizationID)
        {
            string cmdText = @"Select count(*) from OrganizationEmployee
                               Where Deleted=0 and EmployeeID=@EmployeeID and 
                                     (@OrganizationID is null or OrganizationID <> @OrganizationID)";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            sqlCmd.Parameters.AddWithValue("@OrganizationID", organizationID);

            using (var dataContext = new DataContext())
            {
                int count = dataContext.ExecuteSelect<int>(sqlCmd).First();
                return count == 0;
            }
        }
    }
}