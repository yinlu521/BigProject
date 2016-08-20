<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="custom_tags.aspx.cs" Inherits="iNethinkCMS.Web.admin.custom.custom_tags" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
    <script type="text/javascript" src="../../inc/xheditor/xheditor.js"></script>
    <script type="text/javascript" src="../../inc/xheditor/xheditor_lang/zh-cn.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#txtCode').xheditor({
                width: '100%',
                height: '400',
                html5Upload: false,
                upMultiple: 10,
                upLinkUrl: '/inc/upload.aspx?immediate=1',
                upLinkExt: '<%=siteConfig.UpFileType%>',
                upImgUrl: '/inc/upload.aspx?immediate=1&filetype=img',
                upImgExt: 'jpg,jpeg,gif,png,bmp',
                upFlashUrl: '/inc/upload.aspx?immediate=1',
                upFlashExt: 'swf',
                upMediaUrl: '/inc/upload.aspx?immediate=1',
                upMediaExt: 'wmv,avi,wma,mp3,mid'
            });
        });
    </script>
</head>
<body>
    <form id="form_custom_tags" runat="server">
        <div class="maintop" id="navInfoID" runat="server">
        </div>
        <div class="main_toptable">
            <table width="100%">
                <tr>
                    <td>
                        <div class="oper">
                            <a href="?">自定标签管理</a><a href="?act=guide">添加自定标签</a>
                        </div>
                    </td>
                    <td width="300" align="right">
                        <asp:DropDownList ID="sKeyType" runat="server">
                            <asp:ListItem Value="ID">编号</asp:ListItem>
                            <asp:ListItem Value="Name">标签名称</asp:ListItem>
                            <asp:ListItem Value="Remark">标签备注</asp:ListItem>
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
                    <td width="240">标签名称
                    </td>
                    <td>标签备注
                    </td>
                    <td width="160">操作
                    </td>
                </tr>
                <tr id="iNoInfo" runat="server">
                    <td colspan="4">暂无任何数据
                    </td>
                </tr>
                <asp:Repeater ID="Repeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem,"ID")%>
                            </td>
                            <td>{mytag:<%#DataBinder.Eval(Container.DataItem, "Name")%>}
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "Remark")%>
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
            <div class="info">
                <div id="infos1">
                    <dl style="border: 0;">
                        <dt>标签名称：</dt>
                        <dd>
                            <asp:TextBox ID="txtName" runat="server" CssClass="int" MaxLength="100" Width="250px"></asp:TextBox></dd>
                        <dd
                            class="t">只可为英文</dd>
                    </dl>
                    <dl>
                        <dt>标签备注：</dt>
                        <dd>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="int" MaxLength="100" Width="250px"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>标签内容：</dt>
                        <dd style="line-height: 0; width: 89%">
                            <asp:TextBox ID="txtCode" runat="server" TextMode="MultiLine"></asp:TextBox></dd>
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
