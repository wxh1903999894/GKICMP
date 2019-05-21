﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创 建 人:      殷志瑞
** 创建日期:      2017年08月22日 10时20分40秒
** 描    述:      车辆申请基本操作类
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
    public partial class Vehicle_ApplyDAL : DataEntity<Vehicle_ApplyEntity>
    {
        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        ///</summary>
        public int Edit(Vehicle_ApplyEntity model)
        {
            int result = 0;
            DbParameters.Clear();
            ProcedureName = "up_Tb_Vehicle_Apply_Add";
            DataAccessChannelProtection = true;
            DbParameters.Add(new DatabaseParameter("result", result, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("ApplyID", model.ApplyID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("VHID", model.VHID, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("ApplyUser", model.ApplyUser, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("BeginAddress", model.BeginAddress, DatabaseType.SQL_NVarChar, 400));
            DbParameters.Add(new DatabaseParameter("EndAddress", model.EndAddress, DatabaseType.SQL_NVarChar, 400));
            DbParameters.Add(new DatabaseParameter("ApplyDesc", model.ApplyDesc, DatabaseType.SQL_Text, 16));
            DbParameters.Add(new DatabaseParameter("BeginDate", model.BeginDate, DatabaseType.SQL_DateTime, 12));
            DbParameters.Add(new DatabaseParameter("EndDate", model.EndDate, DatabaseType.SQL_DateTime, 12));
            DbParameters.Add(new DatabaseParameter("PeerCount", model.PeerCount, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("VState", model.VState, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("SysUid", model.SysUid, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("Isdel", model.Isdel, DatabaseType.SQL_Int, 4));

            STMessage stmessage = ExecuteStoredProcedure(DataOperationValue.IDU_OPERATION).DataReturn;
            if (stmessage.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            result = Convert.ToInt32(DbParameters[0].Value);
            DataAccessChannel.CommitRelease();
            DataAccessChannelProtection = false;
            return result;
        }
        #endregion



        #region 根据主键编号删除记录
        /// <summary>
        /// 根据主键编号删除记录
        ///</summary>
        public int DeleteByID(string id, int isdel)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Vehicle_Apply_DelBat";
            DataAccessChannelProtection = true;

            DbParameters.Add(new DatabaseParameter("Ids", id, DatabaseType.SQL_NVarChar, 2000));
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
        public Vehicle_ApplyEntity GetObjByID(string id)
        {
            string sql = "SELECT a.*,dbo.getUserName(ApplyUser) ApplyUserName,dbo.getUserName(a.SysUid) SysUidName,b.DID FROM [Tb_Vehicle_Apply] a left join Tb_Driver b on a.SysUid=b.SysUid WHERE [ApplyID] = '" + id + "'";
            if (ExecuteStoredCommandtext(DataOperationValue.SEL_OPERATION, sql).DataReturn.SqlCode != 0)
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
        public DataTable GetPaged(int pagesize, int pageindex, ref int recordCount, Vehicle_ApplyEntity model, DateTime begin, DateTime end)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Vehicle_Apply_Paged";
            DbParameters.Add(new DatabaseParameter("recordCount", recordCount, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("pagesize", pagesize, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("pageindex", pageindex, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("Isdel", model.Isdel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("VState", model.VState, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("begin", begin, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("end", end, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("ApplyUserName", model.ApplyUserName, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("SysUid", model.SysUid, DatabaseType.SQL_NVarChar, 40));
            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            recordCount = Convert.ToInt32(DbParameters[0].Value);
            return DataReflectionContainer;
        }
        #endregion


        #region 根据主键编号删除记录
        /// <summary>
        /// 根据主键编号删除记录
        ///</summary>
        public int Update(string ApplyID, string SysUid)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Vehicle_Apply_Update";
            DataAccessChannelProtection = true;

            DbParameters.Add(new DatabaseParameter("ApplyID", ApplyID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("SysUid", SysUid, DatabaseType.SQL_NVarChar, 40));

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
    }

}

