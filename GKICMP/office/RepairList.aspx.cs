﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创建人:      gxl
** 创建日期:    2016年12月24日
** 描 述:       受理报修管理页面
** 修改人:      
** 修改日期:    
** 修改说明:
**-----------------------------------------------------------------
******************************************************************/
using GK.GKICMP.Common;
using GK.GKICMP.DAL;
using GK.GKICMP.Entities;

using System;
using System.Data;
using System.Web.UI.WebControls;

namespace GKICMP.office
{
    public partial class RepairList : PageBase
    {
        public SysLogDAL sysLogDAL = new SysLogDAL();
        public Asset_RepairDAL repairDAL = new Asset_RepairDAL();


        #region 页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CommonFunction.BindEnum<CommonEnum.ARState>(this.ddl_ARState, "-2");
                GetCondition();
                DataBindList();
            }
        }
        #endregion


        #region 获取查询条件
        public void GetCondition()
        {
            ViewState["RepairObj"] = CommonFunction.GetCommoneString(this.txt_RepairObj.Text.Trim());
            ViewState["begin"] = this.txt_Begin.Text == "" ? "1900-01-01" : this.txt_Begin.Text;
            ViewState["end"] = this.txt_End.Text == "" ? "9999-12-31" : this.txt_End.Text;
            ViewState["DutyUser"] = CommonFunction.GetCommoneString(this.txt_DutyUser.Text.Trim());
            ViewState["ARState"] = this.ddl_ARState.SelectedValue;
        }
        #endregion


        #region 数据绑定
        public void DataBindList()
        {
            int recordCount = 0;
            Asset_RepairEntity model = new Asset_RepairEntity();
            model.RepairObj = ViewState["RepairObj"].ToString();
            DateTime begin = Convert.ToDateTime(ViewState["begin"].ToString());
            DateTime end = Convert.ToDateTime(ViewState["end"].ToString());
            model.DutyUser = ViewState["DutyUser"].ToString();
            model.ARState = Convert.ToInt32(ViewState["ARState"].ToString());
            model.Isdel = Convert.ToInt32(CommonEnum.Deleted.未删除);
            model.CreaterUser = UserID;
            DataTable dt = repairDAL.GetPaged(Pager.PageSize, Pager.CurrentPageIndex, ref recordCount, model, begin, end);
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


        #region 分页
        public void Pager_PageChanged(object sender, EventArgs e)
        {
            DataBindList();
        }
        #endregion


        #region 查询
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            Pager.CurrentPageIndex = 1;
            GetCondition();
            DataBindList();
        }
        #endregion


        #region 删除
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = this.hf_CheckIDS.Value.TrimEnd(',');
                int result = repairDAL.DeleteByID(ids, Convert.ToInt32(CommonEnum.Deleted.删除));
                if (result > 0)
                {
                    sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_删除, "删除我的报修数据", UserID));
                    ShowMessage();
                }
                else
                {
                    ShowMessage("删除失败");
                }
                DataBindList();
                this.hf_CheckIDS.Value = "";
            }
            catch (Exception ex)
            {
                sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.系统日志, ex.Message, UserID));
                ShowMessage(ex.Message);
            }
        }
        #endregion


        #region 复选框是否可用
        public string istrue(object arstate)
        {
            if (arstate.ToString() == Convert.ToInt32(CommonEnum.ARState.未受理).ToString())
            {
                return "";
            }
            else
            {
                return "disabled";
            }
        }
        #endregion

        protected void lbtn_IsConfirm_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            string id = lbtn.CommandArgument.ToString();
            int result = repairDAL.IsConfirm(id, (int)CommonEnum.IsorNot.是, (int)CommonEnum.ARState.确认完成);
            if (result > 0)
            {
                ShowMessage("确认成功");
                GetCondition();
                DataBindList();
            }
            else
            {
                ShowMessage("提交失败");
                return;
            }
        }


        public string GetState(object obj)
        {
            string name = "";
            name = obj.ToString() == "0" ? "<span style='color:#febe17'>" +
                CommonFunction.CheckEnum<CommonEnum.ARState>(obj.ToString()) + "</span>" : obj.ToString() == "1" ? "<span style='color:#47ae6f'>" +
                CommonFunction.CheckEnum<CommonEnum.ARState>(obj.ToString()) + "</span>" : obj.ToString() == "2" ? "<span style='color:red'>" + 
                CommonFunction.CheckEnum<CommonEnum.ARState>(obj.ToString()) + "</span>" : obj.ToString() == "-1" ?"<span style='color:gray'>" +
                CommonFunction.CheckEnum<CommonEnum.ARState>(obj.ToString()) + "</span>" : "<span style='color:gray'>" + CommonFunction.CheckEnum<CommonEnum.ARState>(obj.ToString());
            return name;
        }
    }
}