﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创 建 人:      gxl
** 创建日期:      2017年09月27日 15时43分02秒
** 描    述:      缴费登记的基本操作类
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
    public partial class AttendVacationDAL : DataEntity<AttendVacationEntity>
    {
        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        ///</summary>
        public int Edit(AttendVacationEntity model)
        {
            int result = 0;
            DbParameters.Clear();
            ProcedureName = "up_Tb_AttendVacation_Add";
            DataAccessChannelProtection = true;

            DbParameters.Add(new DatabaseParameter("result", result, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("Vid", model.Vid, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("VacName", model.VacName, DatabaseType.SQL_NVarChar, 200));
            DbParameters.Add(new DatabaseParameter("VBegin", model.VBegin, DatabaseType.SQL_DateTime, 8));
            DbParameters.Add(new DatabaseParameter("VEnd", model.VEnd, DatabaseType.SQL_DateTime, 8));


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




        #region 根据主键编号集合删除记录
        /// <summary>
        /// 根据主键编号集合删除记录
        ///</summary>
        public int DeleteBat(string ids)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_AttendVacation_DelBat";
            DataAccessChannelProtection = true;

            DbParameters.Add(new DatabaseParameter("Ids", ids, DatabaseType.SQL_NVarChar, 2000));
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


        #region 根据实体条件分页获取数据集，返回DataSet
        /// <summary>
        /// 根据实体条件分页获取数据集，返回DataSet
        /// </summary>
        /// <param name="pagesize">每页显示条数</param>
        /// <param name="pageindex">当前页码,从1开始</param>
        /// <param name="recordCount">为-1时统计满足条件的记录总数</param>
        /// <param name="model">条件实体</param>
        public DataTable GetPaged(int pagesize, int pageindex, ref int recordCount, AttendVacationEntity model, DateTime begin, DateTime end)
        {
            DbParameters.Clear();
            ProcedureName = "up_Tb_AttendVacation_Paged";
            DbParameters.Add(new DatabaseParameter("recordCount", recordCount, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("pagesize", pagesize, DatabaseType.SQL_Int, 4));
            DbParameters.Add(new DatabaseParameter("pageindex", pageindex, DatabaseType.SQL_Int, 4));

            DbParameters.Add(new DatabaseParameter("VacName", model.VacName, DatabaseType.SQL_NVarChar, 200));
            DbParameters.Add(new DatabaseParameter("Begin", begin, DatabaseType.SQL_DateTime, 20));
            DbParameters.Add(new DatabaseParameter("End", end, DatabaseType.SQL_DateTime, 20));

            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            recordCount = Convert.ToInt32(DbParameters[0].Value);
            return DataReflectionContainer;
        }
        #endregion


        public static object locker = new object();

        #region 判断是否节假日
        /// <summary>
        /// 判断是否节假日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int IsVacation(DateTime date)
        {
            int result = 0;
            lock (locker)
            {
                DbParameters.Clear();
                ProcedureName = "up_Tb_AttendVacation_IsVacation";
                DbParameters.Add(new DatabaseParameter("result", result, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
                DbParameters.Add(new DatabaseParameter("CardDate", date, DatabaseType.SQL_DateTime, 8));

                if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
                {
                    throw new Exception(DataReturn.SqlMessage);
                }

                result = Convert.ToInt32(DbParameters[0].Value.ToString());
            }

            return result;
        }
        #endregion



        #region 获取节假日天数
        public Decimal GetCount(DateTime begin, DateTime end, string ddlbegin, string ddlend)
        {
            int result = 0;
            DbParameters.Clear();
            ProcedureName = "up_Tb_AttendVacation_GetCount";
            DbParameters.Add(new DatabaseParameter("result", result, DatabaseType.SQL_Int, 4, ParameterDirection.Output));
            DbParameters.Add(new DatabaseParameter("begin", begin, DatabaseType.SQL_DateTime, 20));
            DbParameters.Add(new DatabaseParameter("end", end, DatabaseType.SQL_DateTime, 20));
            DbParameters.Add(new DatabaseParameter("ddlbegin", ddlbegin, DatabaseType.SQL_NVarChar, 40));
            DbParameters.Add(new DatabaseParameter("ddlend", ddlend, DatabaseType.SQL_NVarChar, 40));
            if (ExecuteStoredProcedure(DataOperationValue.SEL_OPERATION).DataReturn.SqlCode != 0)
            {
                throw new Exception(DataReturn.SqlMessage);
            }
            result = Convert.ToInt32(DbParameters[0].Value);
            return result;
        }
        #endregion
    }
}
