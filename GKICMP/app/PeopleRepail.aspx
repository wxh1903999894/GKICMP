﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PeopleRepail.aspx.cs" Inherits="GKICMP.app.PeopleRepail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <link href="../appcss/mui.min.css" rel="stylesheet" />
    <link href="../appcss/new_file.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../appjs/mui.min.js"></script>
    <title>我受理的</title>
    
    <style>
        .mui-bar-nav ~ .mui-content .mui-pull-top-pocket {
            top: 0px;
        }
         .atc1 {
            background: #4FA868;
        }
        .sl {
            margin-right: 10px;
            padding: 5px 10px;
            border-radius: 15px;
            display: inline-block;
            border: 1px solid #29a5e8;
            color: #222222;
            font-size: 12px;
            cursor:pointer;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <header class="mui-bar mui-bar-nav">
            <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
            <h1 class="mui-title">报修管理</h1>
        </header>
        <div class="mui-content">
            <div style="padding: 10px 10px;">
                <div id="segmentedControl" class="mui-segmented-control">
                    <a href="AppRepair.aspx" class="mui-control-item">报修登记</a>
                    <a href="RepairList.aspx" class="mui-control-item">我的报修</a>
                    <a href="PeopleRepail.aspx" class="mui-control-item mui-active">我受理的</a>
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
            <%--      <a class="mui-tab-item">
                <span class="mui-icon iconfont icon-bj"></span>
                <span class="mui-tab-label">班级</span>
            </a>--%>
            <a href="AppMain.aspx" class="mui-tab-item  mui-active">
                <span class="mui-icon iconfont icon-zhxy"></span>
                <span class="mui-tab-label">智慧校园</span>
            </a>
        </nav>
        <script type="text/javascript">
            $(document).ready(function () {
                mui(".list-btn").on('tap', '.btn-pz', function () {
                    $(".btn-pz").addClass("atc1");
                });
                mui(".list-btn").on('tap', '.btn-sh', function () {
                    $(".btn-sh").addClass("atc1");
                });

                //mui(".list-btn").on('tap', '.sl', function () {
                //    $(".sl").addClass("atc1");
                //});
            })
    </script>

        <script type="text/javascript" charset="utf-8">
            $(function () {
                document.body.addEventListener('touchstart', function () {
                });
            })

            mui('body').on('tap', 'a', function () {
                document.location.href = this.href;
            });


            //mui("body").on("tap", ".list-btn span", function () {
            //    $(this).addClass("atc1");
            //    var that = $(this).attr("id")
            //    setTimeout('show(that)', 2000);
                
            //    //show(this);
            //});

            mui("body").on('tap', '.list-btn span', function (event) {
                $(this).addClass('atc1');
                var that = $(this)
                setTimeout(function () { $(that).removeClass('atc1'); }, 200);
                setTimeout(function () { show(that); }, 200);
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
            /**
             * 下拉刷新具体业务实现
             */
            $.ajax({
                url: "../ashx/GetMainDate.ashx",
                cache: false,
                type: "GET",
                data: "method=RepairPeople&pageindex=1&pagesize=10",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.result == 'true') {
                        for (var j in data.data) {
                            var intem = "";
                            intem += "<li class='mui-table-view-cell mui-media'>";
                            intem += "<a href='RepairDetail.aspx?id=" + data.data[j].ARID + "'> ";
                            intem += "<img  class='mui-media-object mui-pull-left' src='" + (data.data[j].ARFile == "" ? "../appimages/nopic.png" : data.data[j].ARFile) + "'/>";
                            intem += "<div class='mui-media-body'>"
                            intem += "<h4>";
                            //intem += "<span style='margin-left: -6px;'>" + "【" + data.data[j].DutyDepName + "】" + data.data[j].RealName + "</span>";
                            intem += "<span >" + "报修人：" + "" + data.data[j].CreaterUserName + "" + "</span>";
                            intem += "</h4>";
                            intem += "<h5>";
                            //intem += "<span>" + "报修人：" + data.data[j].CreaterUserName + "</span>";
                            intem += "<span>" + "受理部门：" + "【" + data.data[j].DutyDepName + "】" + data.data[j].RealName + "</span>";
                            intem += "</h5>";
                            intem += "<h5>";
                            intem += "<span style='margin-left:0px;'>" + "报修日期：" + data.data[j].ARDate + "&nbsp;&nbsp;" + "<span style='font-weight:lighter;color:" + (data.data[j].ARState == "0" ? "#febe17" : data.data[j].ARState == "1" ? "#47ae6f" : data.data[j].ARState == "2" ? "red" : "gray") + "'>" + data.data[j].ARStateName + "</span>";
                            intem += "</h5>";

                            intem += "<p>";
                            intem += data.data[j].RepairObj;
                            intem += "</p>";
                            intem += "</div>";
                            intem += "</a>";
                            if (data.data[j].ARState == 0) {
                                intem += " <div class='list-btn' ><a href='RepairReject.aspx?id=" + data.data[j].ARID + "'  class='btn-pz'>驳回</a><span style='margin-right: 10px;padding: 5px 10px;border-radius: 15px;' id='"
                                            + data.data[j].ARID + "' state='" + data.data[j].ARState + "' class='sl'>受理</span></div>";
                            }
                            if (data.data[j].ARState == 1) {
                                intem += " <div class='list-btn' ><a href='RepairYJ.aspx?id=" + data.data[j].ARID + "'  class='btn-sh'>移交</a>";
                                intem += " <a href='PeopleRepairEdit.aspx?id=" + data.data[j].ARID + "'  class='btn-pz'>完成</a></div>";
                            }
                            //}
                            //else if (data.data[j].ARState == 2) {
                            //    intem += " <div class='list-btn' ><span style='margin-right: 10px;padding: 5px 10px;border-radius: 15px;' id='" + data.data[j].ARID + "' state='" + data.data[j].ARState + "'  class='btn-th' onclick='show(this)'>确认完成</span></div>";
                            //}
                            intem += "</li>";
                            $("#pullrefresh").append(intem);
                        }
                    }
                    else {
                        $("#pullrefresh").append("<div style='position: relative;padding: 8px 12px;text-align: center;'>暂无数据</div>");
                    }
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
                        data: "method=RepairPeople&pagesize=10&pageindex=" + i,
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.result == 'true') {
                                for (var j in data.data) {
                                    var intem = "";
                                    intem += "<li class='mui-table-view-cell mui-media'>";
                                    intem += "<a href='RepairDetail.aspx?id=" + data.data[j].ARID + "'> ";
                                    intem += "<img  class='mui-media-object mui-pull-left' src='" + (data.data[j].ARFile == "" ? "../appimages/nopic.png" : data.data[j].ARFile) + "'/>";
                                    intem += "<div class='mui-media-body'>"
                                    intem += "<h4>";
                                    //intem += "<span style='margin-left: -6px;'>" + "【" + data.data[j].DutyDepName + "】" + data.data[j].RealName + "</span>";
                                    intem += "<span >" + "报修人：" + "" + data.data[j].CreaterUserName + "" + "</span>";
                                    intem += "</h4>";
                                    intem += "<h5>";
                                    //intem += "<span>" + "报修人：" + data.data[j].CreaterUserName + "</span>";
                                      intem += "<span>" + "受理部门：" + "【" + data.data[j].DutyDepName + "】" + data.data[j].RealName + "</span>";
                                    intem += "</h5>";
                                    intem += "<h5>";
                                    intem += "<span style='margin-left:0px;'>" + "报修日期：" + data.data[j].ARDate + "&nbsp;&nbsp;" + "<span style='font-weight:lighter;color:" + (data.data[j].ARState == "0" ? "#febe17" : data.data[j].ARState == "1" ? "#47ae6f" : data.data[j].ARState == "2" ? "red" : "gray") + "'>" + data.data[j].ARStateName + "</span>";
                                    intem += "</h5>";
                                    intem += "<p>";
                                    intem += data.data[j].RepairObj;
                                    intem += "</p>";
                                    intem += "</div>";
                                    intem += "</a>";
                                    if (data.data[j].ARState == 0) {
                                        intem += " <div class='list-btn' ><a href='RepairReject.aspx?id=" + data.data[j].ARID + "'  class='btn-pz'>驳回</a><span style='margin-right: 10px;padding: 5px 10px;border-radius: 15px;' id='"
                                           + data.data[j].ARID + "' state='" + data.data[j].ARState + "' class='sl'>受理</span></div>";
                                    }
                                    if (data.data[j].ARState == 1) {
                                        intem += " <div class='list-btn' ><a href='RepairYJ.aspx?id=" + data.data[j].ARID + "'  class='btn-sh'>移交</a>";
                                        intem += " <a href='PeopleRepairEdit.aspx?id=" + data.data[j].ARID + "'  class='btn-pz'>完成</a></div>";
                                    }
                                    intem += "</li>";
                                    $("#pullrefresh").append(intem);
                                }
                            }
                            else {
                                // $("#pullrefresh").append("<div style='position: relative;padding: 8px 12px;text-align: center;'>暂无数据</div>");
                            }
                            i = i + 1;
                            jsondata = data.data;
                        }
                    });
                    if (jsondata.length == 0) {
                        mui('#offCanvasContentScroll').pullRefresh().endPullupToRefresh(true);
                    }
                    else {
                        mui('#offCanvasContentScroll').pullRefresh().endPullupToRefresh(false);
                    }
                    //mui('#offCanvasContentScroll').pullRefresh().endPullupToRefresh((++count > 100)); //参数为true代表没有更多数据了。
                }, 1500);
            }
        </script>
    </form>
</body>
</html>

<script>

    function show(e) {
        var arid = $(e).attr("id")
        var state = $(e).attr("state");
        if (arid != "" && state >= 0) {
            $.ajax({
                url: "../ashx/GetMainDate.ashx",
                cache: false,
                type: "GET",
                data: "method=LJWC&arid=" + arid + "&state=" + state,
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.result = 'true') {
                        if (state == 0) {
                            alert("受理成功");
                        }
                        else {
                            alert("完成成功");
                        }
                        window.location.reload();
                    }
                    else {
                        if (state == 0) {
                            alert("受理失败");
                        }
                        else {
                            alert("完成失败");
                        }
                    }
                }
            });
        }
    }
</script>

