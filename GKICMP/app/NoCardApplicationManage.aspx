﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoCardApplicationManage.aspx.cs" Inherits="GKICMP.app.NoCardApplicationManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>补卡记录</title>
    <link href="../appcss/mui.min.css" rel="stylesheet" />
    <link href="../appcss/new_file.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../appjs/mui.min.js"></script>
    <style>
        .mui-bar-nav ~ .mui-content .mui-pull-top-pocket 
        {
            top: 0px;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <asp:HiddenField ID="hf_uid" runat="server" />
        <header class="mui-bar mui-bar-nav">
            <span class="mui-icon mui-icon-left-nav mui-pull-left goback" onclick="javascript :history.back(-1);"></span>
            <h1 class="mui-title">补卡记录</h1>
        </header>
        <div class="mui-content">
            <div style="padding: 10px 10px;">
                <div id="segmentedControl" class="mui-segmented-control">
                    <a href="NoCardApplicationEdit.aspx" class="mui-control-item ">补卡申请</a>
                    <a href="NoCardApplicationManage.aspx" class="mui-control-item mui-active">申请记录</a>
                    <a href="NoCardApplicationAudit.aspx" class="mui-control-item">我的审核</a>
                </div>
            </div>
            <div class=" mui-scroll-wrapper" id="offCanvasContentScroll" style="margin-top: 100px; margin-bottom: 50px">
                <div class="mui-scroll">
                    <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="pullrefresh">
                    </ul>
                </div>
            </div>
        </div>
        <nav class="mui-bar mui-bar-tab">
            <a href="/phone" class="mui-tab-item ">
                <span class="mui-icon mui-icon-home"></span>
                <span class="mui-tab-label">网站</span>
            </a>
            <a href="UserInfo.aspx" class="mui-tab-item">
                <span class="mui-icon iconfont icon-wd"></span>
                <span class="mui-tab-label">我的</span>
            </a>
            <a href="AppMain.aspx" class="mui-tab-item mui-active">
                <span class="mui-icon iconfont icon-zhxy"></span>
                <span class="mui-tab-label">智慧校园</span>
            </a>
        </nav>
        <script src="../js/mui.min.js"></script>
        <script type="text/javascript" charset="utf-8">
            mui('body').on('tap', 'a', function () { document.location.href = this.href; });
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
            mui("body").on("tap", ".list-btn span", function () {
                show(this);
            });
            $.ajax({
                url: "../ashx/GetMainDate.ashx",
                cache: false,
                type: "GET",
                data: "method=NoCardApplication&pageindex=1&pagesize=10",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.result == 'true') {
                        for (var j in data.data) {
                            var intem = "";
                            intem += "<li class='mui-table-view-cell'>";
                            intem += "<a href='NoCardApplicationDetail.aspx?id=" + data.data[j].ID + "'> ";
                            intem += "<div class='mui-table'>";
                            intem += "<div class='mui-col-xs-12'>";
                            intem += "<h4 class='mui-ellipsis'>";
                            intem += "<span style='margin-left: -6px;'>" +" &nbsp;&nbsp;"+ data.data[j].NoCardApplyUserName + "</span>";
                            intem += "&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-weight:lighter;color:" + (data.data[j].NoCardStateID == "0" ? "#febe17" : data.data[j].NoCardStateID == "1" ? "#47ae6f" : data.data[j].NoCardStateID == "2" ? "red" : "gray") + "'>" + data.data[j].NoCardStateName + "</span>";
                            intem += "</h4>";
                            intem += "</div>";
                            intem += "<div class='mui-col-xs-12'>";
                            intem += "<h5 class='mui-ellipsis'>";
                            intem += "补卡时间点：" + data.data[j].NoCardApplyDate + "&nbsp;&nbsp;&nbsp;&nbsp;审核人：" + data.data[j].NoCardAuditUserName;
                            intem += "</h5>";
                            intem += "</div>";
                            intem += "</div>";
                            intem += "</a>";
                            //if (data.data[j].NoCardStateID == 0)
                            //{
                               // intem += " <div class='list-btn' ><span style='margin-right: 10px;padding: 5px 10px;border-radius: 15px;' id='" + data.data[j].ID + "' class='btn-pz'>撤销</span></div>";
                            //}
                            intem += "</li>";
                            $("#pullrefresh").append(intem);
                        }
                    }
                    else {
                        $("#pullrefresh").append("<div style='position: relative;padding: 8px 12px;text-align: center;'>暂无数据</div>");
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
                    //alert(1);
                    var jsondata = "";
                    $.ajax({
                        url: "../ashx/GetMainDate.ashx",
                        cache: false,
                        type: "GET",
                        data: "method=NoCardApplication&pagesize=10&pageindex=" + i,
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.result == 'true') {
                                for (var j in data.data) {
                                    var intem = "";
                                    intem += "<li class='mui-table-view-cell'>";
                                    intem += "<a href='NoCardApplicationDetail.aspx?id=" + data.data[j].ID + "'> ";
                                    intem += "<div class='mui-table'>";
                                    intem += "<div class='mui-col-xs-12'>";
                                    intem += "<h4 class='mui-ellipsis'>";
                                    intem += "<span style='margin-left: -6px;'>" + " &nbsp;&nbsp;" + data.data[j].NoCardApplyUserName + "</span>";
                                    intem += "&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-weight:lighter;color:" + (data.data[j].NoCardStateID == "0" ? "#febe17" : data.data[j].NoCardStateID == "1" ? "#47ae6f" : data.data[j].NoCardStateID == "2" ? "red" : "gray") + "'>" + data.data[j].NoCardStateName + "</span>";
                                    intem += "</h4>";
                                    intem += "</div>";
                                    intem += "<div class='mui-col-xs-12'>";
                                    intem += "<h5 class='mui-ellipsis'>";
                                    intem += "补卡时间点：" + data.data[j].NoCardApplyDate + "&nbsp;&nbsp;审核人：" + data.data[j].NoCardAuditUserName;
                                    intem += "</h5>";
                                    intem += "</div>";
                                    intem += "</div>";
                                    intem += "</a>";
                                    //if (data.data[j].NoCardStateID == 0)
                                    //{
                                        //intem += " <div class='list-btn' ><span style='margin-right: 10px;padding: 5px 10px;border-radius: 15px;' id='" + data.data[j].ID + "' class='btn-pz'>撤销</span></div>";
                                    //}
                                    intem += "</li>";
                                    $("#pullrefresh").append(intem);
                                }
                            }
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


            function showNoCard(e) {
                var arid = $(e).attr("id")

                if (arid != "") {
                    $.ajax({
                        url: "../ashx/GetMainDate.ashx",
                        cache: false,
                        type: "GET",
                        data: "method=LeaveDel&id=" + arid,
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.result = 'true') {
                                alert("撤销成功");
                                window.location.reload();
                            }
                            else {
                                alert("撤销失败");
                            }
                        }
                    });
                }
            }


        </script>
    </form>
</body>
</html>
