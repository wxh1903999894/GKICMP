﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubstituteEdit.aspx.cs" Inherits="GKICMP.educational.SubstituteEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>智慧校园考试管理</title>
    <link href="../css/green_formcss.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../js/Validform_v5.3.2.js"></script>
    <script src="../js/My97/WdatePicker.js"></script>
    <script src="../js/editinfor.js"></script>
    <script src="../js/common.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#form1").Validform();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hf_EID" />
        <asp:HiddenField runat="server" ID="hf_begin" />
        <asp:HiddenField runat="server" ID="hf_end" />
        <div class="listcent pad0">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listinfo">
                <tbody>
                    <tr>
                        <td align="right">调课人</td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddl_SubUser"></asp:DropDownList>
                        </td>
                        <td align="right" width="100px">调课课程</td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddl_SubCoruse" datatype="ddl" errormsg="请选择调课课程"></asp:DropDownList>
                            <span style="color: Red; float: none">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">调课时间</td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txt_BeginTime" Width="75px" datatype="*" nullmsg="请填写调课开始时间" onclick="WdatePicker({skin:'whyGreen'})"></asp:TextBox>
                            <span style="color: Red; float: none">*</span>
                        </td>
                        <td align="right">节次</td>
                        <td align="left">
                            <asp:TextBox ID="txt_SubName" runat="server" CssClass="searchbg"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">申请原因</td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txt_ApplyReason" runat="server" datatype="*" nullmsg="请填写申请原因" TextMode="MultiLine" CssClass="searchbg" Height="80px" Width="70%"></asp:TextBox>
                            <span style="color: Red; float: none">*</span></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btn_Sumbit" runat="server" Text="提交" CssClass="submit" OnClientClick="showokwin()" OnClick="btn_Sumbit_Click" />
                            <input type="button" name="button" id="cancell" value="取消" class="editor" onclick=' $.close("A_id");' />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
