﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创 建 人:      樊紫红
** 创建日期:      2017年08月22日 09时37分01秒
** 描    述:      教师听课管理页面
** 修 改 人:      
** 修改日期:    
** 修改说明: 
**-----------------------------------------------------------------
*****************************************************************/
using System;
using System.Data;
using System.Web.UI.WebControls;

using GK.GKICMP.DAL;
using GK.GKICMP.Common;
using GK.GKICMP.Entities;


namespace GKICMP.lecturemanage
{
    public partial class LectureList : PageBase
    {
        public LectureDAL lecDAL = new LectureDAL();
        public SysLogDAL sysLogDAL = new SysLogDAL();

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCondition();
                DataBindList();
            }
        }
        #endregion


        #region 获取查询条件
        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetCondition()
        {
            ViewState["ClassName"] = CommonFunction.GetCommoneString(this.txt_ClassName.Text.ToString().Trim());
            ViewState["TeacherName"] = CommonFunction.GetCommoneString(this.txt_TeacherName.Text.ToString().Trim());
            ViewState["BeginDate"] = this.txt_Begin.Text.ToString() == "" ? "1900-01-01" : this.txt_Begin.Text.ToString();
            ViewState["EndDate"] = this.txt_End.Text.ToString() == "" ? "9999-12-31" : this.txt_End.Text.ToString();
        }
        #endregion


        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBindList()
        {
            int recordCount = -1;
            DataTable dt = lecDAL.GetPaged(Pager.PageSize, Pager.CurrentPageIndex, ref recordCount, ViewState["ClassName"].ToString(), ViewState["TeacherName"].ToString(), Convert.ToDateTime(ViewState["BeginDate"].ToString()), Convert.ToDateTime(ViewState["EndDate"].ToString()), (int)CommonEnum.Deleted.未删除, UserID, 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.tr_null.Visible = false;
            }
            else
            {
                this.tr_null.Visible = true;
            }
            this.rp_List.DataSource = dt;
            Pager.RecordCount = recordCount;
            this.rp_List.DataBind();
            this.hf_CheckIDS.Value = "";
        }
        #endregion


        #region 查询事件
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            Pager.CurrentPageIndex = 1;
            GetCondition();
            DataBindList();
        }
        #endregion


        #region 分页事件
        /// <summary>
        /// 分页事件
        /// </summary>
        protected void Pager_PageChanged(object sender, EventArgs e)
        {
            DataBindList();
        }
        #endregion


        #region 删除事件
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = this.hf_CheckIDS.Value.ToString();
                ids = ids.TrimEnd(',').TrimStart(',');

                int result = lecDAL.DeleteBat(ids, (int)CommonEnum.Deleted.删除);
                if (result > 0)
                {
                    ShowMessage("删除成功");
                    sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_删除, "删除教师听课信息", UserID));
                }
                else
                {
                    ShowMessage("删除失败");
                    return;
                }
                this.hf_CheckIDS.Value = "";
                DataBindList();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.系统日志, ex.Message, UserID));
                return;
            }
        }
        #endregion


        #region 添加跳转
        /// <summary>
        /// 添加跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.open('LectureEdit.aspx', '_self')</script>");
        }
        #endregion


        #region 修改跳转
        /// <summary>
        /// 修改跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtn_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            string id = lbtn.CommandArgument;
           
            Response.Write("<script language=javascript>window.open('LectureEdit.aspx?id=" + id + "', '_self')</script>");
        }
        #endregion
    }
}