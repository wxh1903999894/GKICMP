﻿/*****************************************************************
** Copyright (c) 芜湖市高科电子有限公司
** 创建人:      lfz
** 创建日期:    2016年11月08日
** 描 述:       资源列表页面
** 修改人:      
** 修改日期:    
** 修改说明:
**-----------------------------------------------------------------
******************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using GK.GKICMP.Common;
using GK.GKICMP.DAL;
using GK.GKICMP.Entities;

namespace GKICMP.resource
{
    public partial class EduEesourseList : PageBase
    {
        protected int v = 0;
        public EduResourceDAL eduResourceDAL = new EduResourceDAL();
        public SysLogDAL sysLogDAL = new SysLogDAL();
        public GradeLevelDAL gradeLevelDAL = new GradeLevelDAL();

        #region 参数集合
        public int Flag
        {
            get { return GetQueryString<int>("flag", -1); }
        }

        public string Grade
        {
            get { return GetQueryString<string>("gid", ""); }
        }
        #endregion
        //public int Term
        //{ get { return GetQueryString<int>("term", -1); } }

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
               

                //DataTable dt = gradeLevelDAL.GetList();//年级绑定
                //CommonFunction.DDlTypeBind(this.ddl_MName, dt, "GLID", "ShortName", "-2");
                CommonFunction.BindEnum<CommonEnum.EType>(this.ddl_EType, "-2");//类别绑定
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
            ViewState["ResourseName"] = CommonFunction.GetCommoneString(this.txt_ResourseName.Text.ToString().Trim());
        }
        #endregion


        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBindList()
        {
            int recordCount = -1;
            EduResourceEntity model = new EduResourceEntity();
            model.ResourseName = (string)ViewState["ResourseName"];
            string[] grades = Grade.Split(',');
            model.GID = int.Parse(grades[0]);
            if (grades.Length > 1)
                model.TID = int.Parse(grades[1]);
            else
                model.TID = -2;
            model.EType = int.Parse(this.ddl_EType.SelectedValue);
            model.CreateUser = UserID;
            DataTable dt = eduResourceDAL.GetPaged(Pager.PageSize, Pager.CurrentPageIndex, ref recordCount, model, Flag);
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
            rp_List.DataBind();
     
        }
        #endregion


        #region 分页事件
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Pager_PageChanged(object sender, EventArgs e)
        {
            DataBindList();
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
            GetCondition();
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

                LinkButton btn = (LinkButton)sender;

                int result = eduResourceDAL.DeleteBat(btn.CommandArgument);
                if (result > 0)
                {
                    sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_删除, "删除资源", UserID));
                    ShowMessage("删除成功");
                }
                else
                {
                    ShowMessage("删除失败");
                    return;
                }
                DataBindList();
             
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_删除, ex.Message, UserID));
                return;
            }
        }
        #endregion


        //#region 发布
        //protected void btn_Release_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        LinkButton btn = (LinkButton)sender;



        //        int result = eduResourceDAL.Update(btn.CommandArgument, (int)CommonEnum.IsorNot.是);
        //        if (result > 0)
        //        {
        //            sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志, "发布资源信息", UserID));
        //            ShowMessage("发布成功");
        //        }
        //        else
        //        {
        //            ShowMessage("发布失败");
        //            return;
        //        }
        //        DataBindList();
          
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessage(ex.Message);
        //        sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志, ex.Message, UserID));
        //        return;
        //    }
        //}
        //#endregion


        public string GetPic(object lastname)
        {
            string pname = "";
            if (lastname.ToString() == "jpg" || lastname.ToString() == "gif" || lastname.ToString() == "png" || lastname.ToString() == "jpeg" || lastname.ToString() == "psd" || lastname.ToString() == "bmp")
            {
                pname = "../images/bmp_icon.png";
            }
            else if (lastname.ToString() == "xls" || lastname.ToString() == "xlsx")
            {
                pname = "../images/xlsx_icon.png";
            }
            else if (lastname.ToString() == "doc" || lastname.ToString() == "docx" || lastname.ToString() == "wps")
            {
                pname = "../images/docx_icon.png";
            }
            else if (lastname.ToString() == "ppt" || lastname.ToString() == "pps" || lastname.ToString() == "ppsx")
            {
                pname = "../images/pptx_icon.png";
            }
            else if (lastname.ToString() == "txt")
            {
                pname = "../images/text_icon.png";
            }
            else if (lastname.ToString() == "zip" || lastname.ToString() == "rar")
            {
                pname = "../images/rar_icon.png";
            }
            else if (lastname.ToString() == "pdf")
            {
                pname = "../images/pdf_icon.png";
            }
            else if (lastname.ToString() == "mp4")
            {
                pname = "../images/wmv_file_icon.png";
            }
            else if (lastname.ToString() == "mp4")
            {
                pname = "../images/wmv_file_icon.png";
            }
            else if (lastname.ToString() == "swf")
            {
                pname = "../images/flash_icon.png";
            }
            else
            {
                pname = "../images/unknown_ico.gif";
            }

            
            return pname;

        }

        public bool getState(object obj)
        {
            if (Flag == 1)
            {
                return false;
            }
            else
            {
                if ((int)obj == 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }

      

        protected void lbtn_Report_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btn = (LinkButton)sender;
                int result = eduResourceDAL.Update(btn.CommandArgument, (int)CommonEnum.IsorNot.是);
                if (result > 0)
                {
                    sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_其他, "发布资源信息", UserID));
                    ShowMessage("发布成功");
                }
                else
                {
                    ShowMessage("发布失败");
                    return;
                }
                DataBindList();

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_其他, ex.Message, UserID));
                return;
            }
        }

        #region 下载模板文件
        /// <summary>
        /// 下载模板文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtn_download_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
           
            string expath = btn.CommandArgument;
            string name = btn.CommandName;
            if (!CommonFunction.UpLoadFunciotn(expath, name))
            {
                ShowMessage("资源不存在，请联系系统管理员");
                return;
            }
            int result = eduResourceDAL.DownLoad("");
        }
        #endregion

        protected void lbtn_OnLineEdit_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btn = (LinkButton)sender;
                EduResourceEntity model = eduResourceDAL.GetObjByID(int.Parse(btn.CommandArgument));
                sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_其他,"在线编辑", UserID));
               // Response.Redirect("../test/OnlineEdit.aspx?id=" + Server.UrlEncode(model.ResourseUrl));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script> window.open('../test/OnlineEdit.aspx?id=" + Server.UrlEncode(model.ResourseUrl)+"','_blank')</script>");
               // System.Diagnostics.Process.Start(@"iexplore.exe","http://"+ Request.Url.Authority+ "/test/OnlineEdit.aspx?id="+Server.UrlEncode(model.ResourseUrl));
                //System.Diagnostics.Process p = new System.Diagnostics.Process();
                //p.StartInfo.UseShellExecute = true;
                //p.StartInfo.FileName = @"iexplore.exe";
                //p.StartInfo.Arguments = "http://" + Request.Url.Authority + "/test/OnlineEdit.aspx?id=" + Server.UrlEncode(model.ResourseUrl);
                //p.Start();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                sysLogDAL.Edit(new SysLogEntity((int)CommonEnum.LogType.操作日志_其他, ex.Message, UserID));
                return;
            }
        }

        //#region 添加
        ///// <summary>
        ///// 添加
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btn_Add_Click(object sender, EventArgs e)
        //{
        //    string aa = string.Format("<script language=javascript>window.open('NewsEdit.aspx?id={0}', '_self')</script>", "");
        //    Response.Write(aa);
        //}
        //#endregion


        //#region 编辑
        ///// <summary>
        ///// 编辑
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void lbtn_Edit_Click(object sender, EventArgs e)
        //{
        //    LinkButton lbtn = (LinkButton)sender;
        //    string id = lbtn.CommandArgument.ToString();
        //    string aa = string.Format("<script language=javascript>window.open('NewsEdit.aspx?id={0}', '_self')</script>", id);
        //    Response.Write(aa);
        //}
        //#endregion


        //#region 详情
        //protected void lbtn_Detail_Click(object sender, EventArgs e)
        //{
        //    LinkButton lbtn = (LinkButton)sender;
        //    string id = lbtn.CommandArgument.ToString();
        //    string aa = string.Format("<script language=javascript>window.open('NewsDetail.aspx?id={0}','_self')</script>", id);
        //    Response.Write(aa);
        //}
        //#endregion
        //#region 详情
        //protected void lbtn_Audit_Click(object sender, EventArgs e)
        //{
        //    LinkButton lbtn = (LinkButton)sender;
        //    string id = lbtn.CommandArgument.ToString();
        //    string aa = string.Format("<script language=javascript>window.open('NewsAudit.aspx?id={0}','_self')</script>", id);
        //    Response.Write(aa);
        //}
        //#endregion

        //#region 审核
        //protected void lbtn_Audit_Click(object sender, EventArgs e)
        //{
        //    LinkButton lbtn = (LinkButton)sender;
        //    string id = lbtn.CommandArgument.ToString();
        //    string aa = string.Format("<script language=javascript>window.open('NewsAudit.aspx?id={0}','_self')</script>", id);
        //    Response.Write(aa);
        //}
        //#endregion
    }
}