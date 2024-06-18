using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SoftMart.Core.Dao;
using SM.SmartInfo.BIZ.Commons;
using SM.SmartInfo.DAO.Commons;
using SM.SmartInfo.DAO.Common;

namespace SM.SmartInfo.BIZ.Administration
{
    class PlanBiz : BizBase, ISMFormCRUDBiz<PlanParam>
    {
        private PlanDao _dao = new PlanDao();

        public void SetupAddNewForm(PlanParam param)
        {

        }

        public void AddNewItem(PlanParam param)
        {
            ValidateItem(param);

            #region Prepare system data

            Plan item = param.Plan;
            item.Deleted = SMX.smx_IsNotDeleted;
            item.Version = SMX.smx_FirstVersion;
            item.CreatedBy = SM.SmartInfo.CacheManager.Profiles.MyProfile.UserName;
            item.CreatedDTG = DateTime.Now;

            List<PlanTarget> lstPlanTarget = CreatePlanTargetObject(param.TargetIDs);
            #endregion

            // Validate in database
            ValidatePlanInDatabase(param);

            // Save
            using (TransactionScope trans = new TransactionScope())
            {
                //Insert Plan
                param.PlanID  = _dao.InsertPlan(item);

                //Insert PlanTarget
                lstPlanTarget.ForEach(c => c.PlanID = param.PlanID);
                _dao.InsertPlanTarget(lstPlanTarget);

                trans.Complete();
            }
            //GlobalCache.UpdateOrganization(item);
        }
        private List<PlanTarget> CreatePlanTargetObject(List<int> lstTargetID)
        {
            string userName = Profiles.MyProfile.UserName;
            DateTime creationTime = DateTime.Now; 
            List<PlanTarget> lstPlanTarget = new List<PlanTarget>(lstTargetID.Count); 

            foreach (var targetID in lstTargetID)
            {
                lstPlanTarget.Add(new PlanTarget
                {
                    TargetID = targetID,
                    CreatedBy = userName,
                    CreatedDTG = creationTime
                });
            }
            return lstPlanTarget;
        }
        private void ValidatePlanInDatabase(PlanParam param, bool isEdit = false)
        {
            Plan item = param.Plan;
            List<string> lstMsg = new List<string>();

            // Validate Code
            bool isDuplicatedCode = _dao.IsPlanCodeDuplicate(item.PlanID, item.PlanCode);
            if (isDuplicatedCode)
                lstMsg.Add(Messages.Plan.DuplicateCode);

            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }
        public void ValidateItem(PlanParam param)
        {
            List<string> lsmMess = new List<string>();

            if (param.Plan == null)
                lsmMess.Add(Messages.Plan.NoItemPlan);

            if (param.Plan != null && string.IsNullOrWhiteSpace(param.Plan.PlanCode))
                lsmMess.Add(Messages.Plan.NoCode);

            if (param.Plan != null && string.IsNullOrWhiteSpace(param.Plan.Name))
                lsmMess.Add(Messages.Plan.NoName);

            //if (param.Plan != null && param.EmailTemplate.TemplateType == null)
            //    lsmMess.Add(Messages.Plan.NoTemplateType);

            //if (param.Plan != null && param.EmailTemplate.TransformType == null)
            //    lsmMess.Add(Messages.Emailtemplate.NoTransformType);

            if (lsmMess.Count > 0)
                throw new SMXException(lsmMess);
        }

        public void LoadDataDisplay(PlanParam param)
        {
            int planID = param.PlanID ?? default(int);
            // Get plan info
            Plan item = _dao.GetPlanInfoByID(planID);
            if (item == null)
                throw new SMXException(Messages.ItemNotExisted);

            param.Plan = item;

            param.TargetInfos = _dao.GetTargetInPlanByPlanID(planID);
        }

        public void SetupEditForm(PlanParam param)
        {
            //param.EmailTemplate = _dao.GetEmailTemplateByID(param.EmailTemplateID);
        }

        public void LoadDataEdit(PlanParam param)
        {
            LoadDataDisplay(param);
        }

        public void UpdateItem(PlanParam param)
        {
            // Validate
            ValidateItem(param);

            // Prepare system data
            var item = param.Plan;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;
            item.Version = item.Version + 1;

            List<PlanTarget> lstPlanTarget = CreatePlanTargetObject(param.TargetIDs);

            int itemPlanId = item.PlanID.Value;
            //Validate on database
            ValidatePlanInDatabase(param, true);

            // Save
            using (TransactionScope trans = new TransactionScope())
            {
                //Update Plan
                _dao.UpdatePlan(item);

                //Insert PlanTarget
                lstPlanTarget.ForEach(c => c.PlanID = itemPlanId);
                _dao.DeletePlanTarget(itemPlanId);
                _dao.InsertPlanTarget(lstPlanTarget);

                trans.Complete();
            }
        }

        public void SetupViewForm(PlanParam param)
        {
            //_dao.GetEmailTemplates(param);
        }
        
        public void DeleteItems(PlanParam param)
        {
            Plan item = param.Plan;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;
            item.Deleted = SMX.smx_IsDeleted;

            int targetPlanId = item.PlanID.Value;

            // Delete
            using (TransactionScope trans = new TransactionScope())
            {
                _dao.UpdatePlan(item);
                _dao.DeletePlanTarget(targetPlanId);

                trans.Complete();
            }
        }

        //public void SearchItemsForView(PlanParam param)
        //{
        //    _dao.GetEmailTemplates(param);
        //}

        public void ApproveRejectEmailTemplate(PlanParam param)
        {
            Plan item = param.Plan;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            _dao.UpdatePlan(item);
        }
        public void SearchItemsForView(PlanParam param)
        {
            // Implementation logic here
            // For example, you might want to retrieve a list of Plan items based on the criteria in param
            // This is a placeholder implementation. Adjust it according to your actual requirements.
            try
            {
                //var plans = _dao.GetPlansByCriteria(param); // Assuming there's a method in _dao to search plans by criteria
                //param.Plans = plans; // Assuming PlanParam has a property to hold the list of plans
            }
            catch (Exception ex)
            {
                // Handle any exceptions, possibly logging them and/or rethrowing as appropriate
                throw new SMXException("An error occurred while searching for plans.", ex);
            }
        }
    }
}
