﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创 建 人:      LFZ
** 创建日期:      2018年01月02日 14时06分44秒
** 描    述:      数据的基本操作类
** 修 改 人:      
** 修改日期:    
** 修改说明: 
**-----------------------------------------------------------------
*****************************************************************/
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using gk.rjb_Y.DataEntityProvider;
using gk.rjb_Y.Libraries;
using gk.rjb_Y.DBAccessConvertorProvider;
using GK.GKICMP.Entities;


namespace GK.GKICMP.DAL
{
    public partial class Purchase_BillDAL : DataEntity<Purchase_BillEntity>
    {
        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        ///</summary>
        public int Edit(Purchase_BillEntity model)
        {
            int result = 0;
            DbParameters.Clear();
            ProcedureName = "up_Tb_Purchase_Bill_Add";
            DataAccessChannelProtection = true;
            DbParameters.Add(new DatabaseParameter("result", result, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("BID", model.BID, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("PID", model.PID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("BName", model.BName, DatabaseType.SQL_NVarChar, 100));
            DbParameters.Add(new DatabaseParameter("BModel", model.BModel, DatabaseType.SQL_NVarChar, 100));
            DbParameters.Add(new DatabaseParameter("BCount", model.BCount, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("BPrice", model.BPrice, DatabaseType.SQL_Decimal, 9));
            DbParameters.Add(new DatabaseParameter("BReason", model.BReason, DatabaseType.SQL_NVarChar, 500));
            DbParameters.Add(new DatabaseParameter("CreateDate", model.CreateDate, DatabaseType.SQL_DateTime, 8));

            STMessage stmessage = ExecuteStoredProcedure(DataOperationValue.IDU_OPERATION).DataReturn;
            if (stmessage.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            DataAccessChannel.CommitRelease();
            DataAccessChannelProtection = false;
            return Convert.ToInt32( DbParameters[0].Value);
        }
        #endregion




        #region 根据主键编号集合删除记录
        /// <summary>
        /// 根据主键编号集合删除记录
        ///</summary>
        public int DeleteBat(string ids)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Purchase_Bill_DelBat";
            DataAccessChannelProtection = true;

            DbParameters.Add(new DatabaseParameter("Ids", ids, DatabaseType.SQL_NVarChar, 2000));
            DbParameters.Add(new DatabaseParameter("Isdel", ids, DatabaseType.SQL_Int, 4));
            STMessage stmessage = ExecuteStoredProcedure(DataOperationValue.IDU_OPERATION).DataReturn;
            if (stmessage.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            DataAccessChannel.CommitRelease();
            DataAccessChannelProtection = false;
            return stmessage.AffectRows;
        }
        #endregion


        #region 根据编号（主键）获取项:返回实体对象
        /// <summary>
        /// 根据编号（主键）获取项:返回实体对象
        /// </summary>
        /// <returns></returns>
        public Purchase_BillEntity GetObjByID(string id)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Purchase_Bill_Get";
            DbParameters.Add(new DatabaseParameter("BID", id, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("ExceptionCode", id, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("ExceptionMessage", id, DatabaseType.SQL_VarChar, 2048));
            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            return First();
        }
        #endregion


        #region 根据实体条件分页获取数据集，返回DataSet
        /// <summary>
        /// 根据实体条件分页获取数据集，返回DataSet
        /// </summary>
        /// <param name="pagesize">每页显示条数</param>
        /// <param name="pageindex">当前页码,从1开始</param>
        /// <param name="recordCount">为-1时统计满足条件的记录总数</param>
        /// <param name="model">条件实体</param>
        public DataTable GetByPID(string pid)
        {
            string sql = "select * from Tb_Purchase_Bill where pid='"+pid+"'";
            if (ExecuteStoredCommandtext(DataOperationValue.SEL_OPERATION,sql).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
           
            return DataReflectionContainer;
        }
        #endregion

        #region 根据实体条件分页获取数据集，返回DataSet
        /// <summary>
        /// 根据实体条件分页获取数据集，返回DataSet
        /// </summary>
        /// <param name="pagesize">每页显示条数</param>
        /// <param name="pageindex">当前页码,从1开始</param>
        /// <param name="recordCount">为-1时统计满足条件的记录总数</param>
        /// <param name="model">条件实体</param>
        public DataTable GetPaged(int pagesize, int pageindex, ref int recordCount, Purchase_BillEntity model)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Purchase_Bill_Paged";
            DbParameters.Add(new DatabaseParameter("pagesize", pagesize, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("pageindex", pageindex, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("recordCount", recordCount, DatabaseType.SQL_Int, 4, ParameterDirection.Output));

            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            recordCount = Convert.ToInt32(DbParameters[2].Value);
            return DataReflectionContainer;
        }
        #endregion


    }

}

