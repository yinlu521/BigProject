<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_user.aspx.cs" Inherits="iNethinkCMS.Web.admin.sys.sys_user" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_sys_user" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable">
            <table width="100%">
                <tr>
                    <td>
                        <div class="oper">
                            <a href="?">系统用户管理</a><a href="?act=guide">添加系统用户</a>
                        </div>
                    </td>
                    <td width="300" align="right">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="UserName">用户名</asp:ListItem>
                            <asp:ListItem Value="UserTrueName">真实姓名</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="sKey" runat="server"></asp:TextBox>
                        <asp:Button ID="Button_Search" runat="server" Text="搜索" CssClass="btnmini" OnClick="Button_Search_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="main" id="mainID" runat="server">
            <table width="100%" class="tablist">
                <tr class="trbg">
                    <td width="80">编号
                    </td>
                    <td width="140">用户类型
                    </td>
                    <td>用户名
                    </td>
                    <td width="140">真实姓名
                    </td>
                    <td width="180">电子邮箱
                    </td>
                    <td width="160">注册时间
                    </td>
                    <td width="160">操作
                    </td>
                </tr>
                <tr id="iNoInfo" runat="server">
                    <td colspan="7">暂无任何数据
                    </td>
                </tr>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"ID")%>
                            </td>
                            <td>
                                <%#Fun_UserType(DataBinder.Eval(Container.DataItem,"UserType"))%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"UserName")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "UserTrueName")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "UserEmail")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "UserRegTime")%>
                            </td>
                            <td>
                                <a href="?act=guide&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>">编辑</a>&nbsp;&nbsp;
                            <a href="javascript:if(confirm('您确定要删除这条记录吗?')){location.href='?act=delete&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>';}">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div id="pagelist" class="pages" style="text-align: center;" runat="server"></div>
        </div>
        <div class="main" id="guideID" runat="server">
            <ul class="switchs">
                <li id="s1">基本信息</li>
                <li id="s2" class="c">权限设定</li>
                <li id="s3" class="c">栏目权限</li>
            </ul>
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>用户名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="int" MaxLength="20"></asp:TextBox></dd>
                        <dd
                            class="t">最大长度20,支持中英文数字</dd>
                    </dl>
                    <dl>
                        <dt>用户密码：</dt>
                        <dd>
                            <asp:TextBox ID="txtUserPass" runat="server" CssClass="int" MaxLength="20"
                                TextMode="Password"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>真实姓名：</dt>
                        <dd>
                            <asp:TextBox ID="txtUserTrueName" runat="server" CssClass="int"
                                MaxLength="20"></asp:TextBox></dd>
                        <dd class="t">可留空</dd>
                    </dl>
                    <dl>
                        <dt>电子邮箱：</dt>
                        <dd>
                            <asp:TextBox ID="txtUserEmail" runat="server" CssClass="int" MaxLength="30"></asp:TextBox></dd>
                        <dd
                            class="t">可留空</dd>
                    </dl>
                    <dl>
                        <dt>注册时间：</dt>
                        <dd>
                            <asp:TextBox ID="txtUserRegTime" runat="server" CssClass="int"
                                ReadOnly="True"></asp:TextBox></dd>
                    </dl>
                </div>
                <div id="infos2" style="display: none">
                    <dl style="border: 0;">
                        <dt>选择权限：</dt>
                        <dd style="line-height: 22px;" id="pclasstype">
                            <asp:CheckBoxList ID="txtUserPower" runat="server" RepeatLayout="Flow">
                                <asp:ListItem Value="a">内容管理</asp:ListItem>
                                <asp:ListItem Value="b">栏目管理</asp:ListItem>
                                <asp:ListItem Value="c">扩展模块</asp:ListItem>
                                <asp:ListItem Value="d">标签&页面</asp:ListItem>
                                <asp:ListItem Value="e">系统管理</asp:ListItem>
                            </asp:CheckBoxList>
                        </dd>
                    </dl>
                </div>

                <div id="infos3" style="display: none">
                    <dl style="border: 0;">
                        <dt>全部权限：</dt>
                        <dd>
                            <asp:CheckBox ID="txtUserChannelPowerAll" runat="server" />
                        </dd>
                        <dd class="t">选择该项后,则该用户的栏目权限不受新增栏目的限制!</dd>
                    </dl>

                    <dl id="channelpowerselect" style="display: none;">
                        <dt>选择权限
                            <br />
                            <input id="allcheck" type="button" value="全选" class="btnmini" onclick="alls_channelpower('allcheck');" />
                        </dt>
                        <dd style="line-height: 22px;" id="channelpowerID">
                            <asp:CheckBoxList ID="txtUserChannelPower" runat="server" RepeatLayout="Flow">
                            </asp:CheckBoxList>
                        </dd>
                    </dl>

                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:HiddenField ID="txtUserType" runat="server" Value="1" />
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig" /></dd>
                </dl>
            </div>
        </div>
        <script type="text/javascript">change_channelpower();</script>
    </form>
</body>
</html>
