﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LectureStandard.aspx.cs" Inherits="GKICMP.lecturemanage.LectureStandard" %>

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
            line-height: 28px;
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
                return openbox('A_id', 'LectureStandardEdit.aspx', '&flag=0&type=1', 860, 420, -1);
            });
        });
        function showbox(id) {
            //var id = $(e).next().next().val();
            return openbox('A_id', 'LectureStandardEdit.aspx', 'pid=' + id + '&flag=0&type=1', 860, 420, -1);
        }

        function editinfo(id) {
            return openbox('A_id', 'LectureStandardEdit.aspx', 'id=' + id + '&flag=1&type=1', 860, 420, 0);
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
        <asp:Button runat="server" ID="btn_Search" Style="display: none;" OnClick="btn_Search_Click" />
        <asp:HiddenField runat="server" ID="hf_LSID" />
        <asp:Button runat="server" ID="btn_Delete" OnClick="btn_Delete_Click" Style="display: none;" />
        <div style="line-height: 26px; width: 98%; margin: auto; margin-top: 15px; font-size: 12px; font-family: 微软雅黑体; background: #f5f5f5;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="18" valign="left" height="30">
                        <span style="display: block; width: 100%; height: 90%; background: url(../images/green_yjqh_27.png) center center no-repeat;"></span></td>
                    <td class="positiona"><a>首页</a><span>></span><asp:Label ID="lbl_ParentMenu" runat="server" Text=""></asp:Label><span>></span><asp:Label ID="lbl_Menuname" runat="server" Text="评分标准"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin: 0px; margin: 0 10px;">
            <div id="content" style="width: 100%; height: 684px;">
                <div class="btable">
                    <div id="main">
                        <asp:Literal runat="server" ID="ltl_Content"></asp:Literal>
                        <%--<table border="0" cellpadding="0" cellspacing="0" class="layui-table layui-form" id="example-advanced">
                            <caption>
                                <a href="#" onclick="jQuery(&#39;#example-advanced&#39;).treetable(&#39;expandAll&#39;); return false;">全部展开</a> <a href="#" onclick="jQuery(&#39;#example-advanced&#39;).treetable(&#39;collapseAll&#39;); return false;">全部收起</a>
                            </caption>
                            <tr>
                                <th>&nbsp;</th>
                                <th>评分标准</th>
                                <th>分值</th>
                                <th></th>
                            </tr>
                            <tbody class="btable-content">
                                <tr data-tt-id="1" class="collapsed">
                                    <td>&nbsp;</td>
                                    <td><span class='treebg'>
                                        <b1></b1>
                                    </span><a href="#" onclick="return showbox(1)"><span class="folder">CHUD</span></a></td>
                                    <td>Folder</td>
                                    <td>
                                        <div>
                                            <asp:LinkButton ID="lbtn_Edit" runat="server" CssClass="listbtn btneditcolor" OnClientClick="return editinfo(this);">编辑</asp:LinkButton>
                                            <asp:LinkButton ID="lbtn_Delete" runat="server" CssClass="listbtn btndetialcolor" OnClick="lbtn_Delete_Click" CommandArgument='<%#Eval("LSID") %>'>详情</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr data-tt-id="1-1" data-tt-parent-id="1" class="branch" style="display: none;">
                                    <td>&nbsp;</td>
                                    <td><span class='treebg'>
                                        <b1></b1>
                                    </span><span class="folder">amber</span></td>
                                    <td>Folder</td>
                                    <td>
                                        <div>
                                            <a runat="server" class="listbtn btneditcolor" onclick="return editinfo(1)">编辑</a>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="listbtn btndetialcolor" CommandArgument='<%#Eval("LSID") %>'>详情</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>--%>
                    </div>
                    <div style="text-align: center; background-color: #f5f5f5;">
                        <asp:Button ID="btn_Add" runat="server" Text="添加一级标准" Style="width: 110px; height: 39px; color: #fff; border: none; background: url(../images/green_sb_12.png); font-size: 16px; margin: 10px; padding: 0px; text-indent: 0px; text-align: center" />
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
