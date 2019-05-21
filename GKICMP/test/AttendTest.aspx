﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendTest.aspx.cs" Inherits="GKICMP.test.AttendTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>考勤统计</title>
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/jquery.easyui.min.js"></script>
  
     <link href="../css/green_formcss.css" rel="stylesheet" />
    <script src="../js/My97/WdatePicker.js"></script>
    <script src="../js/Validform_v5.3.2.js"></script>
    <script src="../js/editinfor.js"></script>

    <script>
        $(function () {
            jQuery("#form1").Validform();
        });
    </script>

    <style type="text/css">
        .auto-style1 {
            height: 40px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

         <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listinfo1">
            <tbody>
                <tr>
                    <th colspan="7" align="left" style="padding-left: 15px">
                        <div class="xxsm">
                            <ul>
                                <li ><a href="AttendTDay.aspx">按日统计</a></li>
                                <li class="selected"><a href="AttendTest.aspx">按月统计</a></li>
                            </ul>
                        </div>
                    </th>
                </tr>
              </tbody>
            </table>

         <div class="listcent pad0">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listinfo">
                <tbody>
                    <tr>
                        <th colspan="4" align="left">考勤月报表导出</th>
                    </tr>
                    <tr>
                       <td align="right" width="100" class="auto-style1">考勤月份</td>
                        <td align="left" class="auto-style1">
                            <asp:TextBox ID="txt_InDate" runat="server" datatype="*" nullmsg="请选择考勤月份" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM'})"></asp:TextBox>
                             <span style="color: Red; float: none">*</span>
                        </td>
                    </tr>

                     <tr>
                        <td align="right">导出说明：</td>
                        <td>
                            <span style="color: red;">
                                1.考勤月份不能为空<br />
                                2.由于系统分析考勤数据量比较大，导出统计表可能在三分钟左右,请勿关闭页面<br />
                            </span>
                        </td>
                    </tr>

                     <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btn_Sumbit" runat="server" Text="提交" CssClass="submit" OnClick="btn_Sumbit_Click" />
                            <input type="button" name="button" id="cancell" value="取消" class="editor" onclick='$.close("A_id");' />
                        </td>
                    </tr>

                    </tbody>
            </table>
        </div>


    </form>
</body>
</html>