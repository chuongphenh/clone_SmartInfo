using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.SmartInfo.DAO.Commons
{
    public class DataTrackingDao : BaseDao
    {
        #region Modification methods

        public void InsertDataTracking(DataTracking item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<DataTracking>(item);
            }
        }

        public void UpdateDataTracking(DataTracking item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<DataTracking>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(Messages.ItemNotExitOrChanged);
            }
        }

        #endregion

        #region Getting methods

        public DataTracking GetDataTrackingByID(int id)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<DataTracking>(id);
            }
        }

        #endregion
    }
}
