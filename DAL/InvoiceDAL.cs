﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创 建 人:      樊紫红
** 创建日期:      2017年10月27日 14时49分57秒
** 描    述:      报销操作类
** 修 改 人:      
** 修改日期:    
** 修改说明: 
**-----------------------------------------------------------------
*****************************************************************/
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using GK.GKICMP.Entities;
using gk.rjb_Y.DataEntityProvider;
using gk.rjb_Y.Libraries;
using gk.rjb_Y.DBAccessConvertorProvider;


namespace GK.GKICMP.DAL
{
    public partial class InvoiceDAL : DataEntity<InvoiceEntity>
    {
        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        ///</summary>
        public int Edit(InvoiceEntity model,int isadd,AccessoryEntity amodel)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Invoice_Add";
            DataAccessChannelProtection = true;
            DbParameters.Add(new DatabaseParameter("Isadd", isadd, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("IID", model.IID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("AccountUnit", model.AccountUnit, DatabaseType.SQL_NVarChar, 200));
            DbParameters.Add(new DatabaseParameter("CreateUser", model.CreateUser, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("CreateDate", model.CreateDate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("AduitUser", model.AduitUser, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("TotelCash", model.TotelCash, DatabaseType.SQL_Decimal, 9));
            DbParameters.Add(new DatabaseParameter("InvoiceDesc", model.InvoiceDesc, DatabaseType.SQL_Text, 16));
            DbParameters.Add(new DatabaseParameter("PicUrl", model.PicUrl, DatabaseType.SQL_NVarChar, 500));
            DbParameters.Add(new DatabaseParameter("IsSign", model.IsSign, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("IState", model.IState, DatabaseType.SQL_Int, 4));
            //DbParameters.Add(new DatabaseParameter("InvType", model.InvType, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("InvModel", model.InvModel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("Isdel", model.Isdel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("AccessName",amodel.AccessName,DatabaseType.SQL_NVarChar,2000));
            DbParameters.Add(new DatabaseParameter("AccessUrl", amodel.AccessUrl, DatabaseType.SQL_NVarChar, 2000));
            DbParameters.Add(new DatabaseParameter("ObjID", amodel.ObjID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("AccessFlag", amodel.AccessFlag, DatabaseType.SQL_Int, 4));

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


        #region 更新报销归档信息
        public int Update(string iid, int type)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Invoice_Update";
            DataAccessChannelProtection = true;
            DbParameters.Add(new DatabaseParameter("IIDS", iid, DatabaseType.SQL_NVarChar, 2000));
            DbParameters.Add(new DatabaseParameter("InvType", type, DatabaseType.SQL_Int, 4));

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


        #region 添加审核记录
        /// <summary>
        /// 添加审核记录
        ///</summary>
        public int AuditEdit(Leave_AuditEntity model)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Invoice_Audit_Add";
            DataAccessChannelProtection = true;
            DbParameters.Add(new DatabaseParameter("IID", model.LID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("AuditUser", model.AuditUser, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("AuditMark", model.AuditMark, DatabaseType.SQL_NVarChar, 200));
            DbParameters.Add(new DatabaseParameter("AuditResult", model.AuditResult, DatabaseType.SQL_Int, 4));

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


        #region 根据主键编号集合删除记录
        /// <summary>
        /// 根据主键编号集合删除记录
        ///</summary>
        public int DeleteBat(string ids,int isdel)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Invoice_DelBat";
            DataAccessChannelProtection = true;

            DbParameters.Add(new DatabaseParameter("Ids", ids, DatabaseType.SQL_NVarChar, 2000));
            DbParameters.Add(new DatabaseParameter("Isdel", isdel, DatabaseType.SQL_Int, 4));
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
        public InvoiceEntity GetObjByID(string id)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Invoice_Get";
            DbParameters.Add(new DatabaseParameter("IID", id, DatabaseType.SQL_NVarChar, 40));
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
        public DataTable GetPaged(int pagesize, int pageindex, ref int recordCount, InvoiceEntity model,DateTime begindate,DateTime enddate,int flag,string username)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Invoice_Paged";
            DbParameters.Add(new DatabaseParameter("pagesize", pagesize, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("pageindex", pageindex, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("recordCount", recordCount, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("InvType",model.InvType,DatabaseType.SQL_Int,4));
            DbParameters.Add(new DatabaseParameter("InvModel", model.InvModel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("BeginDate", begindate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("EndDate", enddate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("Isdel", model.Isdel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("Flag", flag, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("UserName", username, DatabaseType.SQL_NVarChar, 50));

            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            recordCount = Convert.ToInt32(DbParameters[2].Value);
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
        public DataTable GetTotelPaged(int pagesize, int pageindex, ref int recordCount, InvoiceEntity model, DateTime begindate, DateTime enddate)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Invoice_TotelPaged";
            DbParameters.Add(new DatabaseParameter("pagesize", pagesize, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("pageindex", pageindex, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("recordCount", recordCount, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("InvType", model.InvType, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("InvModel", model.InvModel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("BeginDate", begindate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("EndDate", enddate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("Isdel", model.Isdel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("IState", model.IState, DatabaseType.SQL_Int, 4));

            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            recordCount = Convert.ToInt32(DbParameters[2].Value);
            return DataReflectionContainer;
        }
        #endregion


        #region 报销综合查询
        public DataTable GetTotel(InvoiceEntity model, DateTime begindate, DateTime enddate)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Invoice_Totel";
            DbParameters.Add(new DatabaseParameter("InvModel", model.InvModel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("BeginDate", begindate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("EndDate", enddate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("IState", model.IState, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("Isdel", model.Isdel, DatabaseType.SQL_Int, 4));

            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }

            return DataReflectionContainer;
        } 
        #endregion
    }
}

