﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AfficheResearchAccept.aspx.cs" Inherits="GKICMP.app.AfficheResearchAccept" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>已收教研</title>
    <link href="../appcss/mui.min.css" rel="stylesheet" />
    <link href="../appcss/new_file.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../appjs/mui.min.js"></script>
    <style>
        .mui-bar .mui-title {
            left: 100px;
            right: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header class="mui-bar mui-bar-nav">
            <span class="mui-icon mui-icon-left-nav mui-pull-left goback" onclick="javascript :history.back(-1);"></span>
            <h1 class="mui-title">已收教研</h1>
        </header>
       <div class="mui-content">
            <div style="padding: 10px 10px;">
                <div id="segmentedControl" class="mui-segmented-control">
                    <a href="AfficheResearch.aspx" class="mui-control-item">教研活动</a>
                    <a href="AfficheResearchSend.aspx" class="mui-control-item">已发教研</a>
                    <a href="AfficheResearchAccept.aspx" class="mui-control-item mui-active">已收教研</a>

                </div>
            </div>

            <div class="mui-input-row mui-search" style="margin-top: 0px">
                <input type="search" class="mui-input-clear" placeholder="请输入教研标题" id="keyword" />
            </div>

            <div class=" mui-scroll-wrapper" id="offCanvasContentScroll" style="margin-top: 150px; margin-bottom: 50px">
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
        <script type="text/javascript" charset="utf-8">
            //mui.init({
            //    swipeBack: true //启用右滑关闭功能
            //});
            //var slider = mui("#slider");
            //slider.slider({
            //    interval: 3000
            //});
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
                data: "method=AfficheResearchAccept&pageindex=1&pagesize=10",
                dataType: "json",
                async: false,
                success: function (data) {
                    for (var j in data.data) {

                        var intem = "";
                        intem += " <li class='mui-table-view-cell'><a href='AfficheResearchDetail.aspx?id=" + data.data[j].AID + "&&users=" + data.data[j].AcceptUser + "'> <div class='mui-table'><div class='mui-col-xs-12'><h4 class='mui-ellipsis'><span style='font-weight:lighter;color:#47AE6F'>[" + data.data[j].ATypeName + "]</span>";
                        intem += data.data[j].AfficheTitle + "</h4><div class='mui-col-xs-12'><span class='mui-h5'> ";
                        intem += data.data[j].SendDate + "</span></div>";
                        intem += "<h5>发件人：" + data.data[j].SenduserName;
                        intem += "&nbsp&nbsp&nbsp&nbsp状态：" + (data.data[j].IsRead == "0" ? "<span style='color:red'>不参与</span>" : "<span style='color:#48bd81'>参与</span>") + "</h5> ";
                        intem += "</div>";
                        intem += "</div>";
                        intem += "</a>";

                        if (data.data[j].IsRead == 0 && data.data[j].AuditMark == "") {
                            intem += " <div class='list-btn' ><a href='AfficheResearchEdit.aspx?&id=" + data.data[j].AID + "&&users=" + data.data[j].AcceptUser + "'  class='btn-pz'>是否参与</a></div>";
                        }

                        intem += "</li>";
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
                        data: "method=AfficheResearchAccept&pagesize=10&pageindex=" + i,
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            for (var j in data.data) {

                                var intem = "";
                                intem += " <li class='mui-table-view-cell'><a href='AfficheResearchDetail.aspx?id=" + data.data[j].AID + "&&users=" + data.data[j].AcceptUser + "'> <div class='mui-table'><div class='mui-col-xs-12'><h4 class='mui-ellipsis'><span style='font-weight:lighter;color:#47AE6F'>[" + data.data[j].ATypeName + "]</span>";
                                intem += data.data[j].AfficheTitle + "</h4><div class='mui-col-xs-12'><span class='mui-h5'> ";
                                intem += data.data[j].SendDate + "</span></div>";
                                intem += "<h5>发件人：" + data.data[j].SenduserName;
                                intem += "&nbsp&nbsp&nbsp&nbsp状态：" + (data.data[j].IsRead == "0" ? "<span style='color:red'>不参与</span>" : "<span style='color:#48bd81'>参与</span>") + "</h5> ";
                                intem += "</div>";
                                intem += "</div>";
                                intem += "</a>";
                                if (data.data[j].IsRead == 0 && data.data[j].AuditMark == "") {
                                    intem += " <div class='list-btn' ><a href='AfficheResearchEdit.aspx?&id=" + data.data[j].AID + "&&users=" + data.data[j].AcceptUser + "'  class='btn-pz'>是否参与</a> </div>";
                                }
                                intem += "</li>";
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
