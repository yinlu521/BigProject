<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extend_blogroll.aspx.cs" Inherits="iNethinkCMS.Web.admin.extend.extend_blogroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script src="../../inc/artDialog/artDialog.js?skin=default" type="text/javascript"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_extend_blogroll" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable">
            <table width="100%">
                <tr>
                    <td>
                        <div class="oper">
                            <a href="?">友情链接管理</a><a href="?act=guide">添加友情链接</a>
                        </div>
                    </td>
                    <td width="300" align="right">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="BlogrollName">链接名称</asp:ListItem>
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
                    <td width="160">友情链接分类
                    </td>
                    <td>友情链接名称
                    </td>
                    <td width="65">LOGO</td>
                    <td width="80">是否显示
                    </td>
                    <td width="80">排序权重
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
                            <td><%#Fun_GetDictName(int.Parse(DataBinder.Eval(Container.DataItem, "BlogrollClass").ToString()))%>
                            </td>
                            <td>
                              <a href="<%#DataBinder.Eval(Container.DataItem, "BlogrollUrl")%>" title="<%#DataBinder.Eval(Container.DataItem, "BlogrollUrl")%>" target="_blank"><%#DataBinder.Eval(Container.DataItem, "BlogrollName")%></a>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "BlogrollImg").ToString().Length > 0 ? "有" : "无"%>
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
                        <dt>友情链接分类：</dt>
                        <dd>
                            <asp:DropDownList ID="txtBlogrollClass" runat="server">
                                <asp:ListItem Value="0">请选择友情链接分类</asp:ListItem>
                            </asp:DropDownList>
                        </dd>

                    </dl>
                    <dl>
                        <dt>友情链接名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtBlogrollName" runat="server" CssClass="int" MaxLength="100" Width="250px"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>LOGO地址：</dt>
                        <dd>
                            <asp:TextBox ID="txtBlogrollImg" runat="server" CssClass="int" MaxLength="400" Width="250px"></asp:TextBox></dd>
                        <dd>
                            <iframe src="/inc/upload_base.aspx?rname=txtBlogrollImg" scrolling="no" frameborder="0" height="25px"></iframe>
                        </dd>
                    </dl>
                    <dl style="border: none; display: none;" id="iUpInfo_txtBlogrollImg">
                        <dt>&nbsp;</dt>
                        <dd id="iUpInfo_msg_txtBlogrollImg"></dd>
                    </dl>
                    <dl>
                        <dt>URL地址：</dt>
                        <dd>
                            <asp:TextBox ID="txtBlogrollUrl" runat="server" CssClass="int" MaxLength="400" Width="250px"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>是否显示：</dt>
                        <dd>
                            <asp:CheckBox ID="txtDisplay" runat="server" Text="勾选则设定为显示" /></dd>
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
            <script type="text/javascript">
                js_showImg(<%=siteConfig.ImageSeconds%>,$('#txtBlogrollImg').val());
        </script>
        </div>
    </form>
</body>
</html>
