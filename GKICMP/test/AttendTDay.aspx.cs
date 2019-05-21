﻿using GK.GKICMP.Common;
using GK.GKICMP.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace GKICMP.test
{
    public partial class AttendTDay : System.Web.UI.Page
    {
        public AttendRecordDAL A = new AttendRecordDAL();
        public AttendSetDAL AS = new AttendSetDAL();
        public SysUserDAL u = new SysUserDAL();
        public LeaveDAL l = new LeaveDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



            }

        }


        #region 提交事件
        /// <summary>
        /// 提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Sumbit_Click(object sender, EventArgs e)
        {

            DateTime InDate = this.txt_InDate.Text == "" ? Convert.ToDateTime("1900/1/1 0:00:00") : Convert.ToDateTime(this.txt_InDate.Text.Trim());

            #region 提交事件
            //DataTable dt =A.GetList("test");//一个月的考勤记录
            DataTable user = u.List();
            DataTable dt1 = AS.List();
            DataTable port = new DataTable();
            //port.Columns.Add("id", typeof(string));
            port.Columns.Add("姓名", typeof(string));
            port.Columns.Add("考勤日期", typeof(string));
            port.Columns.Add("对应时段", typeof(string));
            port.Columns.Add("上班时间", typeof(string));
            port.Columns.Add("下班时间", typeof(string));
            port.Columns.Add("签到时间", typeof(string));
            port.Columns.Add("签退时间", typeof(string));
            port.Columns.Add("请假类型", typeof(string));
            port.Columns.Add("备注", typeof(string));

            //DateTime dt =Convert.ToDateTime("2018-06-01");
            DateTime dt = Convert.ToDateTime(InDate);
            int yy = dt.Year;
            int mm = dt.Month;
            int days = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(yy, mm);
            //int days = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(DateTime.Now.Year, 4);


            Stopwatch sss = new Stopwatch();
            sss.Start();
           // for (int i = 0; i < days; i++)
            //{
                Stopwatch ss = new Stopwatch();
                ss.Start();
                #region 一天中所有老师记录
                foreach (DataRow dr in user.Rows)
                {
                    Stopwatch sone = new Stopwatch();
                    sone.Start();
                    //foreach (DataRow dr1 in dt1.Rows)
                    //{

                    //string begin = dt.AddDays(i).ToString("yyyy-MM-dd");
                    //string end = dt.AddDays(i).ToString("yyyy-MM-dd");

                    string begin = this.txt_InDate.Text.Trim();
                    string end = this.txt_InDate.Text.Trim();

                    Stopwatch s = new Stopwatch();
                    s.Start();
                    string aa = dr["UID"].ToString();
                    DataTable record = A.GetList(begin, end, dr["UID"].ToString());//教师一天打卡记录
                    s.Stop();
                    TimeSpan ts = s.Elapsed;
                    if (ts.TotalMilliseconds > 1000)
                        FileHelper.CreateFileWithContent(System.Web.HttpContext.Current.Server.MapPath("/test/"), "record.txt", "查询一天记录运行时间：" + ts.TotalMilliseconds.ToString() + @"\r\n", false);
                    // DataRow[] drecord = record.Select("UserNum='" + dr["UID"] + "'");//教师一天打卡记录
                    if (record != null && record.Rows.Count > 0)
                    {
                        #region 有考勤记录
                        Stopwatch oneday = new Stopwatch();
                        oneday.Start();
                        string m = "";
                        string mx = "";
                        string a = "";
                        string ax = "";
                        string mdesc = "";
                        string adesc = "";

                        string md = "";
                        string mxd = "";
                        string ad = "";
                        string axd = "";

                        string lmdesc = "";
                        string ladesc = "";

                        #region 一天考勤记录
                        foreach (DataRow jl in record.Rows)
                        {

                            if (Convert.ToDateTime(jl["RecordDate"].ToString()) <= Convert.ToDateTime(begin + " " + dt1.Rows[1]["MEnd"].ToString()) && Convert.ToDateTime(jl["RecordDate"].ToString()) >= Convert.ToDateTime(begin + " " + dt1.Rows[0]["MBegin"].ToString()))//上午上班
                            {
                                m = jl["RecordDate"].ToString();
                                md = jl["IsAnalysis"].ToString() == "372" ? "正常" : jl["IsAnalysis"].ToString() == "373" ? "迟到" : jl["IsAnalysis"].ToString() == "374" ? "早退" : "其他";
                            }
                            else if (Convert.ToDateTime(jl["RecordDate"].ToString()) <= Convert.ToDateTime(begin + " " + dt1.Rows[3]["MEnd"].ToString()) && Convert.ToDateTime(jl["RecordDate"].ToString()) >= Convert.ToDateTime(begin + " " + dt1.Rows[2]["MBegin"].ToString()))//上午下班
                            {
                                mx = jl["RecordDate"].ToString();
                                mxd = jl["IsAnalysis"].ToString() == "372" ? "正常" : jl["IsAnalysis"].ToString() == "373" ? "迟到" : jl["IsAnalysis"].ToString() == "374" ? "早退" : "其他";
                            }
                            else if (Convert.ToDateTime(jl["RecordDate"].ToString()) <= Convert.ToDateTime(begin + " " + dt1.Rows[5]["MEnd"].ToString()) && Convert.ToDateTime(jl["RecordDate"].ToString()) >= Convert.ToDateTime(begin + " " + dt1.Rows[4]["MBegin"].ToString()))//下午上班
                            {
                                a = jl["RecordDate"].ToString();
                                ad = jl["IsAnalysis"].ToString() == "372" ? "正常" : jl["IsAnalysis"].ToString() == "373" ? "迟到" : jl["IsAnalysis"].ToString() == "374" ? "早退" : "其他";
                            }
                            else if (Convert.ToDateTime(jl["RecordDate"].ToString()) <= Convert.ToDateTime(begin + " " + dt1.Rows[7]["MEnd"].ToString()) && Convert.ToDateTime(jl["RecordDate"].ToString()) >= Convert.ToDateTime(begin + " " + dt1.Rows[6]["MBegin"].ToString()))//下午下班
                            {
                                ax = jl["RecordDate"].ToString();
                                axd = jl["IsAnalysis"].ToString() == "372" ? "正常" : jl["IsAnalysis"].ToString() == "373" ? "迟到" : jl["IsAnalysis"].ToString() == "374" ? "早退" : "其他";
                            }
                        }

                        #region 上午打卡时间为空
                        if (m == "" || mx == "")//上午打卡时间为空
                        {
                            //查询请假记录
                            DataTable leave = l.GetList(begin + " 00:01:01", begin + " 13:01:01", dr["UID"].ToString());
                            if (leave != null && leave.Rows.Count > 0)
                            {
                                mdesc = leave.Rows[0]["TypeName"].ToString();
                                lmdesc = leave.Rows[0]["TypeName"].ToString();
                            }
                            else
                            {
                                lmdesc = "";
                                if (m == "" && mx == "")
                                    mdesc = "未打卡";
                                else if (m == "")
                                    mdesc = "上班未打卡";
                                else
                                    mdesc = "下班未打卡";
                            }
                        }
                        else
                        {
                            lmdesc = "";
                            if (md == "正常" && mxd == "正常")
                                mdesc = "正常" + ";" + "正常";
                            else if (md == "正常")
                                mdesc = mxd;
                            else if (mxd == "正常")
                                mdesc = md;
                            else
                                mdesc = md + ";" + mxd;
                        }
                        #endregion

                        #region 下午打卡时间为空
                        if (a == "" || ax == "")//下午打卡时间为空
                        {
                            DataTable leave = l.GetList(begin + " 12:59:00", begin + " 23:59:59", dr["UID"].ToString());
                            if (leave != null && leave.Rows.Count > 0)
                            {
                                ladesc = leave.Rows[0]["TypeName"].ToString();
                                adesc = leave.Rows[0]["TypeName"].ToString();
                            }
                            else
                            {
                                ladesc = "";
                                if (a == "" && ax == "")
                                    adesc = "未打卡";
                                else if (a == "")
                                    adesc = "上班未打卡";
                                else
                                    adesc = "下班未打卡";
                            }
                        }
                        else
                        {
                            ladesc = "";
                            if (ad == "正常" && axd == "正常")
                                adesc = "正常";
                            else if (ad == "正常")
                                adesc = mxd;
                            else if (axd == "正常")
                                adesc = ad;
                            else
                                adesc = ad + ";" + axd;
                        }

                        #endregion

                        port.Rows.Add(new object[] { dr["RealName"], begin, "上午", dt1.Rows[0]["MEnd"], dt1.Rows[3]["MBegin"], m, mx, lmdesc, mdesc });
                        port.Rows.Add(new object[] { dr["RealName"], begin, "下午", dt1.Rows[4]["MEnd"], dt1.Rows[7]["MBegin"], a, ax, ladesc, adesc });

                        #endregion

                        oneday.Stop();
                        TimeSpan ts2 = oneday.Elapsed;
                        if (ts2.TotalMilliseconds > 1000)
                            FileHelper.CreateFileWithContent(System.Web.HttpContext.Current.Server.MapPath("/test/"), "record.txt", "单个运行时间：" + ts2.TotalMilliseconds.ToString() + "\\r\\n", false);
                        #endregion
                    }
                    else//没有考勤记录
                    {
                        #region 没有考勤记录
                        string desc = "";
                        string asc = "";
                        string mdesc = "";
                        string adesc = "";
                        DataTable leave = l.GetList(begin + "  00:00:01", end + " 23:59:59", dr["UID"].ToString());
                        if (leave != null && leave.Rows.Count > 0)
                        {
                            if ((decimal)leave.Rows[0]["LeaveDays"] < 1 && leave.Rows.Count < 2)
                            {
                                if (Convert.ToDateTime(leave.Rows[0]["EndDate"].ToString()) < Convert.ToDateTime(begin + " 13:00:01"))
                                {
                                    mdesc = leave.Rows[0]["TypeName"].ToString();
                                    asc = "未打卡";
                                }
                                else
                                {
                                    adesc = leave.Rows[0]["TypeName"].ToString();
                                    desc = "未打卡";
                                }
                            }
                            else
                            {
                                mdesc = leave.Rows[0]["TypeName"].ToString();
                                adesc = leave.Rows[0]["TypeName"].ToString();
                            }

                        }
                        else
                        {
                            desc = "未打卡";
                            asc = "未打卡";
                        }
                        port.Rows.Add(new object[] { dr["RealName"], begin, "上午", dt1.Rows[0]["MEnd"], dt1.Rows[3]["MBegin"], "", "", mdesc, desc });
                        port.Rows.Add(new object[] { dr["RealName"], begin, "下午", dt1.Rows[4]["MEnd"], dt1.Rows[7]["MBegin"], "", "", adesc, asc });
                        #endregion
                    }

                    sone.Stop();
                    TimeSpan tsone = sone.Elapsed;
                    if (ts.TotalMilliseconds > 1000)
                        FileHelper.CreateFileWithContent(System.Web.HttpContext.Current.Server.MapPath("/test/"), "record.txt", "单个运行总时间【】：" + tsone.TotalMilliseconds.ToString() + @"\r\n", false);

                }
                #endregion
                ss.Stop();
                TimeSpan tss = ss.Elapsed;
                if (tss.TotalMilliseconds > 1000)
                    FileHelper.CreateFileWithContent(System.Web.HttpContext.Current.Server.MapPath("/test/"), "record.txt", "查询一天记录运行时间：" + tss.TotalMilliseconds.ToString() + "\\r\\n", false);
                //}

            //}
            sss.Stop();
            TimeSpan tsss = sss.Elapsed;
            if (tsss.TotalMilliseconds > 1000)
                FileHelper.CreateFileWithContent(System.Web.HttpContext.Current.Server.MapPath("/test/"), "record.txt", "总运行时间：" + tsss.TotalMilliseconds.ToString() + "\\r\\n", false);
                FileHelper.CreateFileWithContent(System.Web.HttpContext.Current.Server.MapPath("/test/"), "record.txt", "共有：" + port.Rows.Count.ToString() + "条数据\n", false);
            CommonFunction.ExportByWeb(port, "", DateTime.Now.Month.ToString() + "月份考勤统计" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            #endregion



        }
        #endregion

    }
}