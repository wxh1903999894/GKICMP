﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创 建 人:      樊紫红
** 创建日期:      2017年08月22日 09时07分01秒
** 描    述:      教师听课操作类
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
    public partial class LectureDAL : DataEntity<LectureEntity>
    {
        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        ///</summary>
        public int Edit(LectureEntity model)
        {
            int result = 0;
            DbParameters.Clear();
            ProcedureName = "up_Tb_Lecture_Add";
            DataAccessChannelProtection = true;

            DbParameters.Add(new DatabaseParameter("result", result, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("LID", model.LID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("ClaID", model.ClaID, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("CID", model.CID, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("BeginDate", model.BeginDate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("EndDate", model.EndDate, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("TID", model.TID, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("CreateUser", model.CreateUser, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("Isdel", model.Isdel, DatabaseType.SQL_Int, 4));

            STMessage stmessage = ExecuteStoredProcedure(DataOperationValue.IDU_OPERATION).DataReturn;
            if (stmessage.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            DataAccessChannel.CommitRelease();
            DataAccessChannelProtection = false;

            result = Convert.ToInt32(DbParameters[0].Value.ToString());
            return result;
        }
        #endregion


        #region 根据主键编号集合删除记录
        /// <summary>
        /// 根据主键编号集合删除记录
        ///</summary>
        public int DeleteBat(string ids, int isdel)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Lecture_DelBat";
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
        public LectureEntity GetObjByID(string id)
        {
            string sql = "select *,dbo.getDepName(ClaID) as ClassName,dbo.getCourseName(CID) as CourseName,dbo.getUserName(TID) as TeacherName from Tb_Lecture where LID='" + id + "'";
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
        public DataTable GetPaged(int pagesize, int pageindex, ref int recordCount, string classname, string teachername, DateTime begindate, DateTime enddate, int isdel, string userid, int flag)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_Lecture_Paged";
            DbParameters.Add(new DatabaseParameter("pagesize", pagesize, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("pageindex", pageindex, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("recordCount", recordCount, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("ClassName", classname, DatabaseType.SQL_NVarChar, 50));
            DbParameters.Add(new DatabaseParameter("TeacherName", teachername, DatabaseType.SQL_NVarChar, 50));
            DbParameters.Add(new DatabaseParameter("BeginDate", begindate, DatabaseType.SQL_DateTime, 18));
            DbParameters.Add(new DatabaseParameter("EndDate", enddate, DatabaseType.SQL_DateTime, 18));
            DbParameters.Add(new DatabaseParameter("Isdel", isdel, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("UserID", userid, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("Flag", flag, DatabaseType.SQL_Int, 4));

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
