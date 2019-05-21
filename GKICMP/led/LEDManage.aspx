﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LEDManage.aspx.cs" Inherits="GKICMP.led.LEDManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>智慧校园行政办公平台</title>
    <link href="../css/green_list.css" rel="stylesheet" />
    <link href="../css/green_asyncbox.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../js/AsyncBox.v1.4.js"></script>
    <script src="../js/AsyncBox.v1.4.5.js"></script>
    <script src="../js/choice.js"></script>
    <script src="../js/My97/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btn_Add').click(function () {
                //box的ID，Url,data传值，宽，高,Type(-1为添加，0为修改,其他为查看)
                return openbox('A_id', 'LEDEdit.aspx', '', 940, 550, -1);
            });
        });

        function editinfo(e) {
            var id = $(e).next().next().val();
            return openbox('A_id', 'LEDEdit.aspx', 'id=' + id, 940, 550, 0);
        }

        function viewinfo(e) {
            var id = $(e).next().val();
            return openbox('A_id', 'LEDPublish.aspx', 'id=' + id, 860, 480, 32);
        }
    </script>
</head>
<body>
    <form runat="server">
        <asp:HiddenField ID="hf_CheckIDS" runat="server" />
        <asp:HiddenField ID="hf_Page" runat="server" />
        <div class="positionc">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="18" valign="left" height="30">
                        <span class="zcbz"></span></td>
                    <td class="positiona"><a>首页</a><span>></span><asp:Label ID="lbl_ParentMenu" runat="server" Text=""></asp:Label><span>></span><asp:Label ID="lbl_Menuname" runat="server" Text="教学活动"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
       
        <div class="listcent pad0">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listbt">
                <tbody>
                    <tr>
                        <td align="right" valign="middle">
                            <asp:Button ID="btn_Add" runat="server" Text="添加"  CssClass="listbtncss listadd" />
                            <asp:Button ID="btn_Delete" runat="server" Text="删除"  CssClass="listbtncss listdel" OnClick="btn_Delete_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listinfo">
                <tbody>
                    <tr>
                        <th width="5%" align="center">
                            <label class="wxz" id="checkalll">
                                <input type="checkbox" name="checkbox" value="复选框" id="checkall" onclick="qx(this.name, this.id)"></label></th>
                        <th align="center">品牌</th>
                        <th align="center">名称</th>
                        <th align="center">ip地址</th>
                        <th align="center">连接方式</th>
                        <th align="center">板卡类型</th>
                        <th align="center">屏色</th>
                        <th align="center">屏大小</th>
                        <th align="center">亮度(1-15)</th>
                        <th align="center" width="70px">操作</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rp_List">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 5px">
                                    <label class="wxz" id='ck_<%#Eval("LID")%>l'>
                                        <input type="checkbox" name="checkbox" onclick="setid(this.id)" value='<%#Eval("LID") %>' id='ck_<%#Eval("LID") %>' /></label>
                                </td>
                                <td><%#GK.GKICMP.Common.CommonFunction.CheckEnum<GK.GKICMP.Common.CommonEnum.LedBrand>(Eval("LBrand"))%></td>
                                <td><%#Eval("LName")%></td>
                                <td><%#Eval("LIP")%></td>
                                <td><%#GK.GKICMP.Common.CommonFunction.CheckEnum<GK.GKICMP.Common.CommonEnum.LedConn>(Eval("LCType"))%></td>
                                <td><%#GK.GKICMP.Common.CommonFunction.CheckEnum<GK.GKICMP.Common.CommonEnum.LedType>(Eval("LType"))%></td>
                                <td><%#GK.GKICMP.Common.CommonFunction.CheckEnum<GK.GKICMP.Common.CommonEnum.LedColor>(Eval("LColor"))%></td>
                                <td><%#Eval("LWidth")%>*<%#Eval("LHeight")%></td>
                                 <td><%#Eval("LBright")%></td>
                                <td>
                                    <div>
                                        <asp:LinkButton ID="lbtn_Edit" runat="server" CssClass="listbtn btneditcolor" OnClientClick="return editinfo(this);">编辑</asp:LinkButton>
                                        <asp:LinkButton ID="lbtn_Detail" runat="server" CssClass="listbtn btndetialcolor" OnClientClick='return viewinfo(this);'>发布</asp:LinkButton>
                                        <asp:HiddenField ID="HiddenField1" Value='<%#Eval("LID") %>' runat="server" />
                                        <asp:LinkButton ID="lbtn_OC" runat="server" CssClass="listbtn btndetialcolor" CommandArgument='<%#Eval("LID")%>' OnClick="lbtn_OC_Click"><%#Eval("IsOpen").ToString()=="1"?"关机":"开机"%></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate> 
                    </asp:Repeater>
                    <tr runat="server" id="tr_null">
                        <td colspan="9">暂无记录</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <wuc:Pager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged" />
    </form>
</body>
</html>

