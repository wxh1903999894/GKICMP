﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LectureScoreDetail.aspx.cs" Inherits="GKICMP.lecturemanage.LectureScoreDetail" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <title>btable</title>
    <meta name="renderer" content="webkit">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <link href="../css/green_asyncbox.css" rel="stylesheet" />
    <link rel="stylesheet" href="../css/layui.css" media="all" />
    <link rel="stylesheet" href="../css/jquery.treetable.theme.default.css">
    <style>
        #myMenu {
            border: 1px solid #D8D8D8;
            background: #fff;
            padding: 10px 0px;
            box-shadow: #939393 3px 3px 3px;
        }

            #myMenu li {
                border-bottom: 1px dashed #DDDDDD;
                width: 150px;
                text-indent: 20px;
                line-height: 30px;
            }

                #myMenu li:hover {
                    background: #5FB878;
                }

                #myMenu li a {
                    display: block;
                }

                #myMenu li:hover a {
                    color: #fff;
                }

        .layui-table tr:nth-child(2n) {
            background-color: #f8f8f8;
        }

        .layui-table td {
            padding: 0 0.5em;
            line-height: 40px;
        }

        #A_id_content {
            height: 410px;
        }
    </style>
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/jquery.treetable.js"></script>
    <script src="../js/AsyncBox.v1.4.js"></script>
    <script src="../js/AsyncBox.v1.4.5.js"></script>
    <script src="../js/choice.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btn_Add').click(function () {
                //box的ID，Url,data传值，宽，高,Type(-1为添加，0为修改,其他为查看)
                return openbox('A_id', 'LectureStandardEdit.aspx', '&flag=0', 860, 420, -1);
            });
        });
        function showbox(id) {
            //var id = $(e).next().next().val();
            return openbox('A_id', 'LectureStandardEdit.aspx', 'pid=' + id + '&flag=0', 860, 420, -1);
        }

        function editinfo(id) {
            return openbox('A_id', 'LectureStandardEdit.aspx', 'id=' + id + '&flag=1', 860, 420, 0);
        }

        function deleteinfo(id) {
            document.getElementById("hf_LSID").value = id;
            document.getElementById("btn_Delete").click();
        }
    </script>
</head>

<body style="background-color: #f5f5f5;">
<%--<body>--%>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hf_LSID" />
        <div style="line-height: 26px; width: 98%; margin: auto; margin-top: 15px; font-size: 12px; font-family: 微软雅黑体; background: #f5f5f5;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="18" valign="left" height="30">
                        <span style="display: block; width: 100%; height: 90%; background: url(../images/green_yjqh_27.png) center center no-repeat;"></span></td>
                    <td class="positiona"><a>首页</a><span>></span>公开课管理<span>></span><asp:Label ID="lbl_Menuname" runat="server" Text="听课打分"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin: 0px; background-color: white; margin: 0 10px;">
            <div id="content" style="width: 100%; height: 684px;">
                <div class="btable">
                    <div id="main">
                        <asp:Literal runat="server" ID="ltl_Content"></asp:Literal>
                    </div>
                    <script>
                        $("#example-advanced").treetable({ expandable: true });
                        window.onload = function () {
                            jQuery('#example-advanced').treetable('expandAll'); return false;
                        }
                    </script>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
