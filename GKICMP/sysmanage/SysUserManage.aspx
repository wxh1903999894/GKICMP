﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserManage.aspx.cs" Inherits="GKICMP.sysmanage.SysUserManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>智慧校园基础管理平台</title>
    <link href="../css/green_list.css" rel="stylesheet" />
    <link href="../css/green_asyncbox.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../js/AsyncBox.v1.4.js"></script>
    <script src="../js/AsyncBox.v1.4.5.js"></script>
    <script src="../js/choice.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btn_FP').click(function () {
                //box的ID，Url,data传值，宽，高,Type(-1为添加，0为修改,其他为查看)
                return openbox('A_id', 'DepartImportByUser.aspx', '', 600, 300, -1);
            });
            $('#btn_DeleteU').click(function () {
                var checkid = document.getElementById("hf_CheckIDS").value;
                var count = 0;
                var strs = new Array();
                strs = checkid.split(",");
                for (i = 0; i < strs.length; i++) {
                    if (strs[i] != "") {
                        count++;
                        break;
                    }
                }
                if (count == 0) {
                    alert("系统提示：请至少选择一条信息！");
                    return false;
                }
                else {
                    var res = confirm("删除该条信息的同时，也会删除对应的教师信息，您确认删除吗？");
                    if (res)
                        return confirm("您确认" + mess + "选中的" + info + "吗？");
                    else
                        return false;
                }
            });
        });
        function InputData(e) {
            var id = $(e).next().val();
            var tt= document.getElementById("hf_Flag").value;
            return openbox('A_id', 'SysUserImport.aspx', 'flag=' + tt, 1100, 500, 3);
        }
        function editinfo(e) {
            var id = $(e).next().next().val();
            return openbox('A_id', 'SysUserEdit.aspx', 'id=' + id, 1100, 500, 0);
        }

        function viewinfo(e) {
            var id = $(e).next().val();
            return openbox('A_id', 'SysUserDetail.aspx', 'id=' + id + '&flag=1', 860, 470, 4);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hf_CheckIDS" runat="server" />
        <asp:HiddenField ID="hf_Page" runat="server" />
        <asp:HiddenField ID="hf_Flag" runat="server" />
        <div class="positionc">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="18" valign="left" height="30">
                        <span class="zcbz"></span></td>
                    <td class="positiona"><a>首页</a><span>></span>系统管理<span>></span><asp:Label ID="lbl_Menuname" runat="server" Text="用户管理"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="dvTab">
            <ul class="menuall">
                <li class='tab <%=Flag!=2?"activeTab":"" %>'><a href="SysUserManage.aspx?flag=1">教师</a></li>
                <li class='tab <%=Flag==2?"activeTab":"" %>'><a href="SysUserManage.aspx?flag=2">学生</a></li>
                   <li class="tab"><a href="WSysUserManage.aspx">校外人士</a></li>
            </ul>
            <div class="dv"></div>
        </div>
        <div class="listcent searclass">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="10">
                <tbody>

                    <tr>
                        <td align="right" width="60">用户名：</td>
                        <td width="150">
                            <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
                        </td>
                        <td align="right" width="60">姓名：</td>
                        <td width="150">
                            <asp:TextBox ID="txt_RealName" runat="server"></asp:TextBox>
                        </td>
                        <td align="right" width="60" runat="server" id="th1">部门：</td>
                        <td width="100" runat="server" id="th2">
                            <asp:DropDownList ID="ddl_DepName" runat="server"></asp:DropDownList>
                        </td>
                        <td align="right" width="60" runat="server" id="th3">角色：</td>
                        <td width="100" runat="server" id="th4">
                            <asp:DropDownList ID="ddl_Role" runat="server"></asp:DropDownList>
                        </td>
                        <td align="right" width="60" runat="server" id="th5">班级：</td>
                        <td width="100" runat="server" id="th6">
                            <asp:DropDownList ID="ddl_Class" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btn_Search" runat="server" Text="查询" OnClick="btn_Search_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="listcent pad0">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="listbt">
                <tbody>
                    <tr>
                        <td align="left"></td>
                        <td align="right" valign="middle">
                            <asp:Button ID="btn_FP" runat="server" Text="部门分配"  CssClass="listbtncss listadd" />
                            <asp:Button ID="lbtn_Detain" runat="server" Text="禁用" CssClass="listbtncss listno" OnClick="btn_Detain_Click" />
                            <asp:Button ID="btn_Input" runat="server" Text="导入"   CssClass="listbtncss listinput" OnClientClick="return InputData(this)" />
                            <asp:Button ID="btn_Add" runat="server" Text="添加"  CssClass="listbtncss listadd" OnClick="btn_Add_Click" />
                            <asp:Button ID="btn_DeleteU" runat="server" Text="删除"  CssClass="listbtncss listdel"  OnClick="btn_Delete_Click"  />
                            <asp:Button ID="btn_PassWord" runat="server" Text="密码重置" title="重置后密码为：888888" CssClass="listbtncss listpassword" OnClick="btn_PassWord_Click" />
                             <asp:Button ID="btn_OutPut" runat="server" Text="导出"   CssClass="listbtncss listoutput" OnClick="btn_OutPut_Click" />
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
                        <th align="center">
                            <asp:Literal runat="server" ID="ltl_DepName"></asp:Literal></th>
                        <th align="center">用户名</th>
                        <th align="center">姓名</th>
                        <th align="center">性别</th>
                        <th align="center">角色</th>

                        <th align="center">民族</th>
                        <th align="center">身份证号码</th>
                        <th align="center">出生日期</th>
                        <th align="center">手机号码</th>
                       
                        <th align="center">最近登录时间</th>
                        <th align="center">是否采像</th>
                        <th align="center">状态</th>
                        <th align="center">是否关注</th>
                        <th width="70px" align="center">操作</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rp_List">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 5px">
                                    <label class="wxz" id='ck_<%#Eval("UID")%>l'>
                                        <input type="checkbox" name="checkbox" onclick="setid(this.id)" value='<%#Eval("UID") %>' id='ck_<%#Eval("UID") %>' <%#GetState(Eval("UserType")) %> /></label>
                                </td>
                                <td><%#Eval("DepName") %></td>
                                <td><%#Eval("UserName")%></td>
                                <td><%#Eval("RealName")%></td>
                                <td><%#GetName(Eval("UserSex"))%></td>
                                <td><%#Eval("RoleName")%></td>
                                <td><%#GK.GKICMP.Common.CommonFunction.CheckEnum<GK.GKICMP.Common.CommonEnum.MZ>(Eval("Nation"))%></td>
                                <td><%#Eval("IDCard").ToString()==""?"暂无身份证信息":Eval("IDCard").ToString().Length>14?Eval("IDCard").ToString().Substring(0,4)+"******"+Eval("IDCard").ToString().Substring(14):"身份证有误，请修改"%></td>
                                <td><%#Convert.ToDateTime( Eval("BirthDay")).ToString("yyyy-MM") %></td>
                                <td><%#Eval("CellPhone")%></td>
                              
                                <td><%#Eval("LastDate").ToString()==""?"":Eval("LastDate","{0:yyyy-MM-dd HH:mm:ss}")%></td>
                                <td><%#Eval("FaceNum").ToString()==""?"否":"是"%></td>
                                <td><%#GK.GKICMP.Common.CommonFunction.CheckEnum<GK.GKICMP.Common.CommonEnum.UState>(Eval("UState"))%></td>
                                <td><%#Eval("Followed").ToString()!="1"?"<span style='color:red'>未关注</span>":"已关注"%></td>
                                <td>
                                    <%--<asp:LinkButton ID="lbtn_Edit" runat="server" CssClass="listbtn btneditcolor" ToolTip="编辑" OnClientClick='return editinfo(this);'>编辑</asp:LinkButton>--%>
                                    <asp:LinkButton ID="lbtn_Edit" runat="server" CssClass="listbtn btneditcolor" ToolTip="编辑" CommandArgument='<%#Eval("UID")%>' OnClick="lbtn_Edit_Click">编辑</asp:LinkButton>
                                    <asp:LinkButton ID="lbtn_Detail" runat="server" CssClass="listbtn btndetialcolor" ToolTip="详细" OnClientClick='return viewinfo(this);'>详细</asp:LinkButton>
                                    <asp:HiddenField ID="hf_SelectID" Value='<%#Eval("UID") %>' runat="server" />

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr runat="server" id="tr_null">
                        <td colspan="14">暂无记录</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <wuc:Pager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged" />
    </form>
</body>
</html>
