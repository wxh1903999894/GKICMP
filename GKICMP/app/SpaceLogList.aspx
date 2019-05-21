﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpaceLogList.aspx.cs" Inherits="GKICMP.app.SpaceLogList" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>校园活动</title>
    <link href="../appcss/mui.min.css" rel="stylesheet" />
    <link href="../appcss/new_file.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../appjs/mui.min.js"></script>

</head>

<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hf_User" runat="server" />

        <header class="mui-bar mui-bar-nav">
             <span class="mui-icon mui-icon-left-nav mui-pull-left goback" onclick="javascript :history.back(-1);"></span>
            <h1 class="mui-title">校园活动</h1>
           <%-- <a class="mui-icon mui-icon-plusempty mui-pull-right" href="WorkPlan.aspx"></a>--%>
           <%-- <a class="mui-icon  mui-pull-right" href="WorkPlan.aspx">新增</a>--%>
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
                data: "method=SpaceLog&pageindex=1&pagesize=10",
                dataType: "json",
                async: false,
                success: function (data) {
                    //alert(321);
                    for (var j in data.data) {
                        var intem = "";
                        intem += " <li class='mui-table-view-cell'><a href='SpaceLogEdit.aspx?id=" + data.data[j].SAID + "'> <div class='mui-table'><div class='mui-col-xs-12'><h4 class='mui-ellipsis'> "
                            + "<span style='font-weight:lighter;color:#47AE6F'>" + "[" + data.data[j].ActTypeName + "]</span>"
                            + (data.data[j].ActName.Length > 10 ? data.data[j].ActName.substr(0, 10) + "..." : data.data[j].ActName) + "</h4><div class='mui-col-xs-12'><span class='mui-h5'> "
                            + "</span></div>"
                            + "<h5>活动日期：" + data.data[j].ABegin + "<span>至"
                            + (data.data[j].AEnd)
                            + "</span></h5></div></div></a></li> ";

                        //intem += " <li class='mui-table-view-cell margin10'>"
                        // +" <span> "
                        // +" <div class='mui-table'>"
                        // +" <div class='mui-table-cell mui-col-xs-10'>"
                        // +" <h5 class='mui-ellipsis color-2 mui-ellipsis'>"
                        // + "<span class='color-0' style='color: #47AE6F; font-size: 14px'>" + "[" + data.data[j].ActTypeName + "]</span>"
                        // + "<a href='SpaceLogEdit.aspx?id=" + data.data[j].SAID +"'>"+data.data[j].ActName+"</a>"
                        // +"</h5>"
                        // +" <p class='mui-h5 mui-ellipsis' style='float: right'>"
                        // + " <span>活动日期:" + data.data[j].ABegin + "至" + data.data[j].AEnd + "</span>"
                        // +" <span></span>"
                        // + "</p></div></div></span></li>"


                        //alert(intem);
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
                    //alert(i);
                    var jsondata = "";
                    $.ajax({
                        url: "../ashx/GetMainDate.ashx",
                        cache: false,
                        type: "GET",
                        data: "method=SpaceLog&pagesize=10&pageindex=" + i,
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            for (var j in data.data) {
                                var intem = "";
                                   intem += " <li class='mui-table-view-cell'><a href='SpaceLogEdit.aspx?id=" + data.data[j].SAID + "'> <div class='mui-table'><div class='mui-col-xs-12'><h4 class='mui-ellipsis'> "
                                         + "<span style='font-weight:lighter;color:#47AE6F'>" + "[" + data.data[j].ActTypeName + "]</span>"
                                         + (data.data[j].ActName.Length > 10 ? data.data[j].ActName.substr(0, 10) + "..." : data.data[j].ActName) + "</h4><div class='mui-col-xs-12'><span class='mui-h5'> "
                                         + "</span></div>"
                                         + "<h5>活动日期：" + data.data[j].ABegin + "<span>至"
                                         + (data.data[j].AEnd)
                                         + "</span></h5></div></div></a></li> ";

                                $("#pullrefresh").append(intem);
                            }

                            i = i + 1;
                            jsondata = data.data;
                            // alert(data.data.length );
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
                    //mui('#offCanvasContentScroll').pullRefresh().endPullupToRefresh((++count > 100)); //参数为true代表没有更多数据了。
                }, 1500);
            }

        </script>

    </form>
</body>
</html>


