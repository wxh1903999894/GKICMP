﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创 建 人:      LFZ
** 创建日期:      2017年06月16日 14时35分10秒
** 描    述:      数据的基本操作类
** 修 改 人:      
** 修改日期:    
** 修改说明: 
**-----------------------------------------------------------------
*****************************************************************/
using System;
using System.Data;

using gk.rjb_Y.DataEntityProvider;
using gk.rjb_Y.Libraries;
using gk.rjb_Y.DBAccessConvertorProvider;
using GK.GKICMP.Entities;


namespace GK.GKICMP.DAL
{
    public partial class Stu_RewardDAL : DataEntity<Stu_RewardEntity>
    {
        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        ///</summary>
        public int Edit(Stu_RewardEntity model)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Stu_Reward_Add";
            DataAccessChannelProtection = true;

            DbParameters.Add(new DatabaseParameter("SRID", model.SRID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("StuID", model.StuID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("EYear", model.EYear, DatabaseType.SQL_NVarChar, 30));
            DbParameters.Add(new DatabaseParameter("Term", model.Term, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("RewardName", model.RewardName, DatabaseType.SQL_Text, 16));
            DbParameters.Add(new DatabaseParameter("RewardGrade", model.RewardGrade, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("RewardRand", model.RewardRand, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("RewardType", model.RewardType, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("RewardReason", model.RewardReason, DatabaseType.SQL_NVarChar, 1000));
            DbParameters.Add(new DatabaseParameter("RewardCash", model.RewardCash, DatabaseType.SQL_Decimal, 9));
            DbParameters.Add(new DatabaseParameter("RewardDep", model.RewardDep, DatabaseType.SQL_NVarChar, 200));
            DbParameters.Add(new DatabaseParameter("RStyle", model.RStyle, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("RMode", model.RMode, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("RDate", model.RDate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("IsAchievement", model.IsAchievement, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("TeaID", model.TeaID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("CreateUser", model.CreateUser, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("CreateDate", model.CreateDate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("RFile", model.RFile, DatabaseType.SQL_NVarChar, 1000));
            DbParameters.Add(new DatabaseParameter("Isdel", model.Isdel, DatabaseType.SQL_Int, 4));

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
        public int DeleteBat(string ids, int isdel)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Stu_Reward_DelBat";
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
        public Stu_RewardEntity GetObjByID(string id)
        {
            string sql = "select * from Tb_Stu_Reward where SRID='" + id + "'";
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
        public DataTable GetPaged(int pagesize, int pageindex, ref int recordCount, Stu_RewardEntity model,int type)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Stu_Reward_Paged";
            DbParameters.Add(new DatabaseParameter("pagesize", pagesize, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("pageindex", pageindex, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("recordCount", recordCount, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("StuID", model.StuID, DatabaseType.SQL_NVarChar, 100));
            DbParameters.Add(new DatabaseParameter("EYear", model.EYear, DatabaseType.SQL_NVarChar, 100));
            DbParameters.Add(new DatabaseParameter("Term", model.Term, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("RewardName", model.RewardName, DatabaseType.SQL_NVarChar, 100));
            DbParameters.Add(new DatabaseParameter("type", type, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("RewardGrade", model.RewardGrade, DatabaseType.SQL_Int, 4));

            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            recordCount = Convert.ToInt32(DbParameters[2].Value);
            return DataReflectionContainer;
        }
        #endregion

        #region 根据学年学期导出学生奖惩信息到报告单
        /// <summary>
        /// 根据实体条件分页获取数据集，返回DataSet
        /// </summary>
        /// <param name="pagesize">每页显示条数</param>
        /// <param name="pageindex">当前页码,从1开始</param>
        /// <param name="recordCount">为-1时统计满足条件的记录总数</param>
        /// <param name="model">条件实体</param>
        public DataTable GetStu(string year, int term, int did)
        {
            string sql = "select * from Tb_Stu_Reward where isdel=0 and  EYear='" + year + "' and Term=" + term + " and dbo.getDepByTID(stuid)=" + did + " order by stuid desc";
            if (ExecuteStoredCommandtext(DataOperationValue.SEL_OPERATION, sql).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            return DataReflectionContainer;
        }
        #endregion
    }
}

