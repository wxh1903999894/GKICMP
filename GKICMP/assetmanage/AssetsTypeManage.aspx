﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssetsTypeManage.aspx.cs" Inherits="GKICMP.assetmanage.AssetsTypeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>智慧校园行政办公平台</title>
    <link href="../css/green_list.css" rel="stylesheet" />
    <link href="../css/green_asyncbox.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../js/AsyncBox.v1.4.js"></script>
    <script src="../js/AsyncBox.v1.4.5.js"></script>
    <script src="../js/choice.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btn_Add').click(function () {
                //box的ID，Url,data传值，宽，高,Type(-1为添加，0为修改,其他为查看)
                return openbox('A_id', 'AssetsTypeEdit.aspx', '&flag=' + document.getElementById("hf_CID").value, 640, 400, -1);
            });
        });
        function editinfo(e) {
            var id = $(e).next().val();
            var flag = document.getElementById("hf_CID").value;
            return openbox('A_id', 'AssetsTypeEdit.aspx', 'id=' + id + '&flag=' + flag, 640, 400, 0);
        }
    </script>
</head>
<body>
    <form runat="server">
        <asp:HiddenField ID="hf_CheckIDS" runat="server" />
        <asp:HiddenField ID="hf_Page" runat="server" />
        <asp:HiddenField ID="hf_CID" runat="server" />
        <div class="positionc">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="18" valign="left" height="30">
                        <span class="zcbz"></span></td>
                    <td class="positiona"><a>首页</a><span>></span><asp:Label ID="lbl_ParentMenu" runat="server" Text=""></asp:Label><span>></span><asp:Label ID="lbl_Menuname" runat="server" Text="资产类别管理"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="listcent searclass">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="10">
                <tbody>
                    <tr>
                        <td align="right" width="60">分类名称：</td>
                        <td width="299">
                            <asp:TextBox ID="txt_AssetName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_Search" runat="server" Text="查询" OnClick="btn_Search_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="listcent pad0">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listbt" id="btntab">
                <tbody>
                    <tr>
                        <td align="right" valign="middle">
                            <asp:Button ID="btn_Add" runat="server" Text="添加"  CssClass="listbtncss listadd" />
                            <asp:Button ID="btn_Delete" runat="server" Text="删除"  CssClass="listbtncss listdel" OnClick="btn_Delete_Click" />
                            <asp:Button ID="btn_Update" runat="server" Text="更新"  CssClass="listbtncss listdel" OnClick="btn_Update_Click" />
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
                        <th align="center">代码</th>
                        <th align="center">上级分类名称</th>
                        <th align="center">分类名称</th>
                        <%--<th width="70px" align="center">操作</th>--%>
                    </tr>
                    <asp:Repeater runat="server" ID="rp_List">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 5px">
                                    <label class="wxz" id='ck_<%#Eval("SDID")%>l'>
                                        <input type="checkbox" name="checkbox" onclick="setid(this.id)" value='<%#Eval("SDID") %>' id='ck_<%#Eval("SDID") %>' /></label>
                                </td>
                                <td><%#Eval("DataDesc")%></td>
                                <%-- <td><%#GK.GKICMP.Common.CommonFunction.CheckEnum<GK.GKICMP.Common.CommonEnum.DataType>( Eval("DataType"))%></td>--%>
                                <td><%#Eval("ParName")%></td>
                                <td><%#Eval("DataName")%></td>
                                <%--<td>
                                        <asp:LinkButton ID="lbtn_Edit"  runat="server" CssClass="listbtn btneditcolor" OnClientClick='return editinfo(this);'>编辑</asp:LinkButton>
                                        <asp:HiddenField ID="hf_SelectID" Value='<%#Eval("SDID") %>' runat="server" />
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr runat="server" id="tr_null">
                        <td colspan="4">暂无记录</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <wuc:Pager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged" />
    </form>
</body>
</html>
