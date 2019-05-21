﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysNoticeManage.aspx.cs" Inherits="GKICMP.appstu.SysNoticeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>智慧校园</title>
    <link href="../appcss/mui.min.css" rel="stylesheet" />
    <link href="../appcss/new_file.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../appjs/mui.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <header class="mui-bar mui-bar-nav">
            <span class="mui-icon mui-icon-left-nav mui-pull-left goback" onclick="javascript :history.back(-1);"></span>
            <h1 class="mui-title">系统通知</h1>
        </header>
        <div class="mui-content  mui-scroll-wrapper" id="offCanvasContentScroll">
            <div class="mui-scroll">
                <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="pullrefresh">
                </ul>
            </div>
        </div>
        <nav class="mui-bar mui-bar-tab">
             <a href="/phone" class="mui-tab-item ">
                <span class="mui-icon mui-icon-home"></span>
                <span class="mui-tab-label">网站</span>
            </a>
            <a href="../app/UserInfo.aspx" class="mui-tab-item">
                <span class="mui-icon iconfont icon-wd"></span>
                <span class="mui-tab-label">我的</span>
            </a>
            <a href="AppMain.aspx" class="mui-tab-item mui-active">
                <span class="mui-icon iconfont icon-zhxy"></span>
                <span class="mui-tab-label">智慧校园</span>
            </a>
        </nav>
        <script src="/js/mui.min.js"></script>
        <script type="text/javascript" charset="utf-8">
            mui('body').on('tap', 'a', function () {
                document.location.href = this.href;
            });
        </script>
        <script>
            function getUrlParam(name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
                var r = window.location.search.substr(1).match(reg);  //匹配目标参数
                if (r != null) return unescape(r[2]); return null; //返回参数值
            }
            mui.init({
                pullRefresh: {
                    container: '#offCanvasContentScroll',
                    down: {
                        contentrefresh: '正在加载...',
                        callback: pulldownRefresh
                    },
                    up: {
                        contentrefresh: '正在加载...',
                        callback: pullupRefresh
                    }
                }
            });
            $.ajax({
                url: "../ashx/GetMainDate.ashx",
                cache: false,
                type: "GET",
                data: "method=NoticeData&pageindex=1&pagesize=10",
                dataType: "json",
                async: false,
                success: function (data) {
                    for (var j in data.data) {
                        console.log(data.data[j]);
                        var intem = "";
                        intem += " <li class='mui-table-view-cell'><a href='SysNoticeDetail.aspx?id=" + data.data[j].SNID + "'> <div class='mui-table'><div class='mui-col-xs-12'><h4 class='mui-ellipsis'>"
                            + (data.data[j].IsRead == "0" ? "<span style='color:red;'>[未读]</span>" : "<span style='color:#48bd81'>[已读]</span>")
                            + data.data[j].NContent + "</h4><div class='mui-col-xs-12'><span class='mui-h5'> "
                            + data.data[j].SendDate + "</span></div>"
                            + "<h5>发件人：" + data.data[j].SendUserName+ "</h5></div></div></a></li> ";
                        $("#pullrefresh").append(intem);
                    }
                },
                error: function () {
                    alert("暂无查询到数据！");
                }
            });

            var i = 2;
            function pulldownRefresh() {
                setTimeout(function () {
                    window.location.reload();
                }, 1500);
            };
            /**
             * 上拉加载具体业务实现
             */
            function pullupRefresh() {
                setTimeout(function () {
                    var jsondata = "";
                    $.ajax({
                        url: "../ashx/GetMainDate.ashx",
                        cache: false,
                        type: "GET",
                        data: "method=NoticeData&pagesize=10&pageindex=" + i,
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            for (var j in data.data) {
                                intem += " <li class='mui-table-view-cell'><a href='SysNoticeDetail.aspx?id=" + data.data[j].SNID + "'> <div class='mui-table'><div class='mui-col-xs-12'><h4 class='mui-ellipsis'>"
                            + (data.data[j].IsRead == "0" ? "<span style='color:red;'>[未读]</span>" : "<span style='color:#48bd81'>[已读]</span>")
                            + data.data[j].NContent + "</h4><div class='mui-col-xs-12'><span class='mui-h5'> "
                            + data.data[j].SendDate + "</span></div>"
                            + "<h5>发件人：" + data.data[j].SendUserName + "</h5></div></div></a></li> ";
                                $("#pullrefresh").append(intem);
                            }

                            i = i + 1;
                            jsondata = data.data;
                        },
                        error: function () {
                            alert("暂无查询到数据");
                        }
                    });

                    if (jsondata.length == 0) {
                        mui('#offCanvasContentScroll').pullRefresh().endPullupToRefresh(true);
                    }
                    else {
                        mui('#offCanvasContentScroll').pullRefresh().endPullupToRefresh(false);
                    }
                }, 1500);
            }
        </script>
    </form>
</body>
</html>