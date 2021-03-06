﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创建人:      yzr
** 创建日期:     2017年03月03日
** 描 述:       指导学生获奖编辑页面
** 修改人:      
** 修改日期:    
** 修改说明:
**-----------------------------------------------------------------
******************************************************************/
using System;

using GK.GKICMP.Common;
using GK.GKICMP.DAL;
using GK.GKICMP.Entities;
using System.Configuration;

namespace GKICMP.app
{
    public partial class TeacherGuidanceEdit : PageBaseApp
    {
        public SysLogDAL sysLogDAL = new SysLogDAL();
        public Teacher_GuidanceDAL teacher_GuidanceDAL = new Teacher_GuidanceDAL();


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 提交
        protected void btn_Sumbit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt_RewardName.Text == "")
                {
                    ShowMessage("请填写奖励名称");
                    return;
                }
                if (this.txt_Lunit.Text == "")
                {
                    ShowMessage("请填写奖励单位");
                    return;
                }
                if (this.hf_GRoles.Value == "")
                {
                    ShowMessage("请选择本人角色");
                    return;
                }
                if (this.hf_begin.Value == "")
                {
                    ShowMessage("请选择获奖年月");
                    return;
                }
                if (this.hf_GuiDesc.Value == "")
                {
                    ShowMessage("请填写本人承担工作描述");
                    return;
                }
                if (Convert.ToDateTime(this.hf_begin.Value) > DateTime.Now)
                {
                    ShowMessage("获奖年月不能超过当前年月");
                    return;
                }
                Teacher_GuidanceEntity model = new Teacher_GuidanceEntity();

                model.TGID = "";
                model.TID = UserID;
                model.RGrade = this.txt_RGrade.Text.Trim();
                model.RewardName = this.txt_RewardName.Text.ToString();
                model.Lunit = this.txt_Lunit.Text.ToString();
                model.PubDate = Convert.ToDateTime(this.hf_begin.Value);
                model.Isdel = (int)CommonEnum.Deleted.未删除;
                model.GRole = Convert.ToInt32(this.hf_GRoles.Value);
                model.GuiDesc = this.hf_GuiDesc.Value;
                //附件上传      
                int upsize = 4000000;
                try
                {
                    upsize = Convert.ToInt32(ConfigurationManager.AppSettings["upsize"].ToString());
                }
                catch (Exception) { }
                AccessoryEntity accessinfo = CommonFunction.upfile(0, 1, hf_file, "ImageUrl");

                model.RFile = accessinfo.AccessUrl;
                int result = teacher_GuidanceDAL.Edit(model);
                if (result> 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存成功');window.location.href='TeacherGuidanceManage.aspx'</script>");
                    sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_其他, "", UserID));
                }
                else
                {
                    ShowMessage("提交失败");
                    return;
                }
            }
            catch (Exception ex)
            {
                sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.系统日志, ex.Message, UserID));
                ShowMessage(ex.Message);
            }
        }
        #endregion
    }
}