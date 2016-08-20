<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extend_dict.aspx.cs" Inherits="iNethinkCMS.Web.admin.extend.extend_dict" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_extend_dict" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable">
            <table width="100%">
                <tr>
                    <td>
                        <div class="oper">
                            <a href="?">数据字典管理</a><a href="?act=guide">添加数据字典</a>
                        </div>
                    </td>
                    <td width="300" align="right">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="DictName">字典名称</asp:ListItem>
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
                    <td width="160">字典类型
                    </td>
                    <td>字典名称
                    </td>
                    <td width="80">是否显示
                    </td>
                    <td width="80">排序权重
                    </td>
                    <td width="160">操作
                    </td>
                </tr>
                <tr id="iNoInfo" runat="server">
                    <td colspan="6">暂无任何数据
                    </td>
                </tr>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"ID")%>
                            </td>
                            <td><%#iNethinkCMS.Web.UI.WebUI_Function.Fun_GetDictTypeName_FromDictType(DataBinder.Eval(Container.DataItem, "DictType"))%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "DictName")%>
                            </td>
                            <td>
                                <%#Fun_DisplayInfo(DataBinder.Eval(Container.DataItem, "Display"))%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "OrderNum")%>
                            </td>
                            <td>
                                <a href="?act=guide&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>">编辑</a>&nbsp;&nbsp;
                            <a href="javascript:if(confirm('您确定要删除这条记录吗?')){location.href='?act=delete&id=<%#DataBinder.Eval(Container.DataItem,"ID")%>';}">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div id="pagelist" class="pages" style="text-align:center;" runat="server"></div>
        </div>
        <div class="main" id="guideID" runat="server">
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>字典类型：</dt>
                        <dd>
                            <asp:RadioButtonList ID="txtDictType" runat="server" RepeatColumns="6">
                                <asp:ListItem Value="1">友情链接分类</asp:ListItem>
                            </asp:RadioButtonList>
                        </dd>
                    </dl>
                    <dl>
                        <dt>字典名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtDictName" runat="server" CssClass="int" MaxLength="100" Width="250px"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>是否使用：</dt>
                        <dd>
                            <asp:CheckBox ID="txtDisplay" runat="server" Text="勾选则设定为使用" /></dd>
                    </dl>
                    <dl>
                        <dt>排序权重：</dt>
                        <dd>
                            <asp:TextBox ID="txtOrderNum" runat="server" CssClass="int" MaxLength="6" Width="80"></asp:TextBox></dd>
                    </dl>
                </div>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig" /></dd>
                </dl>
            </div>
        </div>
    </form>
</body>
</html>
