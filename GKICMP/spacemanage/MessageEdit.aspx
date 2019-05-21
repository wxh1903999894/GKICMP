﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageEdit.aspx.cs" Inherits="GKICMP.spacemanage.MessageEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>智慧校园管理平台</title>
    <link href="../css/green_formcss.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../js/choice.js"></script>
    <script src="../js/Validform_v5.3.2.js"></script>
    <script src="../js/editinfor.js"></script>
    <script src="../js/common.js"></script>
    <script src="../utf8-net/ueditor.config.js"></script>
    <script src="../utf8-net/ueditor.all.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#form1").Validform();
        });
    </script>
    <style>
        .edilab label {
            float: none;
        }

        .edilab input {
            height: 13px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="listcent pad0">
            <table width="100%" cellspacing="0" cellpadding="0" class="listinfo">
                <tr>
                    <th colspan="4" align="left"><asp:Literal runat="server" ID="ltl_Top"></asp:Literal>信息
                    </th>
                </tr>
                <tr>
                    <td align="right" width="100px"><asp:Literal runat="server" ID="ltl_Name"></asp:Literal>内容：</td>
                    <td colspan="3">
                        <asp:TextBox ID="txt_Content" runat="server" Width="60%" Height="100px" TextMode="MultiLine" datatype="*" nullmsg="请填写留言内容"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0">
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_Sumbit" runat="server" Text="提交" CssClass="submit" OnClick="btn_Sumbit_Click" />
                        <input type="button" name="button" id="cancell" value="取消" class="editor" onclick=' $.close("A_id");' />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>