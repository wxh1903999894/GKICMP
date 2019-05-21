﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendVacationEdit.aspx.cs" Inherits="GKICMP.teachermanage.AttendVacationEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>智慧校园基础管理平台</title>
     
     <link href="../css/green_formcss.css" rel="stylesheet" />
   <script src="../js/jquery-1.8.2.min.js"></script>
     <script src="../js/jquery.min.js"></script>
     <script src="../js/jquery.easyui.min.js"></script>
     <link href="../css/easyui.css" rel="stylesheet" />
     <link href="../css/demo.css" rel="stylesheet" />
    <script src="../js/Validform_v5.3.2.js"></script>
    <script src="../js/My97/WdatePicker.js"></script>
    <script src="../js/editinfor.js"></script>
    <script src="../js/common.js"></script> 
    <script src="../js/ImgPreview.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#TeacherName').combotree({
                onSelect: function (node) {
                    var val = node.id;
                    document.getElementById("hf_SelectedValue").value = val;
                    //alert(val);
                }
            });

            jQuery("#form1").Validform();
        });
    </script>
</head>
<body>
     <form id="form1" runat="server">
        <asp:Literal ID="ltl_JQ" runat="server"></asp:Literal>
        <asp:Literal ID="ltl_xz" runat="server"></asp:Literal>
        <asp:HiddenField runat="server" ID="hf_SelectedValue" />
        <asp:HiddenField runat="server" ID="hf_UsersPwd" Value="" />
        <asp:HiddenField runat="server" ID="hf_UState" />

        <div class="listcent pad0">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listinfo">
                <tbody>
                    <tr>
                        <th colspan="4" align="left">假期信息</th>
                    </tr>
                    <tr>
                        <td align="right">名称</td>
                        <td colspan="3">
                           <asp:TextBox ID="txt_VacName" runat="server" datatype="*1-50" nullmsg="请填写假期名称名称" MaxLength="50" CssClass="searchbg"></asp:TextBox>
                            <span style="color: Red; float: none">*</span>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="right">开始时间：</td>
                        <td>
                            <asp:TextBox ID="txt_VBegin" runat="server" datatype="*" nullmsg="请选择假期开始年月" onfocus="WdatePicker({skin:'whyGreen'})"></asp:TextBox>
                            <span style="color: Red; float: none">*</span>
                        </td>
                        <td align="right">结束时间：</td>
                        <td align="left">
                            <asp:TextBox ID="txt_VEnd" runat="server" datatype="*" nullmsg="请选择假期结束年月" onfocus="WdatePicker({skin:'whyGreen'})" ckdate="txt_VBegin"></asp:TextBox>
                            <span style="color: Red; float: none">*</span>
                        </td>
                    </tr>
                  
                  
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btn_Sumbit" runat="server" Text="提交" CssClass="submit"  OnClick="btn_Sumbit_Click" />
                            <input type="button" name="button" id="cancell" value="取消" class="editor" onclick='$.close("A_id");' />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>