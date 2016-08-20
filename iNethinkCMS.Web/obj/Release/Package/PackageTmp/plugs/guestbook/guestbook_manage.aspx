<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="guestbook_manage.aspx.cs" Inherits="iNethinkCMS.Web.plugs.guestbook.guestbook_manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>留言列表</title>
    <link href="../../admin/skin/css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
            当前位置：客户报名列表
        </div>
        <div class="main_toptable">
            姓名：<input type="text" id="txtName" runat="server" />
            手机号：<input type="text" id="txtmobile" runat="server" />
            性别：<asp:DropDownList ID="dropSex" runat="server">
                <asp:ListItem Value="">--请选择--</asp:ListItem>
                <asp:ListItem Value="先生">先生</asp:ListItem>
                <asp:ListItem Value="女士">女士</asp:ListItem>
            </asp:DropDownList>
            归属地：<input type="text" id="txtCallAddr" runat="server" />
        </div>
         <div class="main" id="mainID" runat="server">
            <table width="100%" class="tablist">
                <tr class="trbg">
                    <td>编号</td>
                    <td>姓名</td>
                    <td>性别</td>
                    <td>手机号</td>
                    <td>归属地</td>
                    <td>IP</td>
                    <td>注册时间</td>
                    <td>操作</td>
                </tr>
                 <tr id="iNoInfo" runat="server">
                    <td colspan="100">暂无任何数据
                    </td>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                           <td>
                                <%#DataBinder.Eval(Container.DataItem,"ID")%>
                            </td>
                            <td><%#Eval("username") %></td>
                            <td><%#Eval("sex") %></td>
                            <td><%#Eval("mobile") %></td>
                            <td><%#Eval("calladdr") %></td>
                            <td><%#Eval("IP") %></td>
                            <td><%#Eval("addtime") %></td>
                            <td><asp:LinkButton   ID="linkDelete" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName='del'  OnClientClick="return confirm('您确定要删除这条记录吗?')">删除</asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
             <div id="pagelist" class="pages" style="text-align:center;" runat="server"></div>
         </div>
    </form>
</body>
</html>

