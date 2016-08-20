<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="iNethinkCMS.Web.plugs.search.manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../admin/skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../admin/skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../admin/skin/js/command.js"></script>
</head>
<body>
    <form id="form_search_manage" runat="server">
        <div class="maintop" runat="server">
            当前位置：搜索功能设置
        </div>
        <div class="main" runat="server">
            <div class="info">
                <dl style="border: 0;">
                    <dt>开启搜索功能：</dt>
                    <dd>
                        <asp:DropDownList ID="txtState" runat="server">
                            <asp:ListItem Value="0">停用</asp:ListItem>
                            <asp:ListItem Value="1">启用</asp:ListItem>
                        </asp:DropDownList>
                    </dd>
                </dl>
                <dl>
                    <dt>模板路径：</dt>
                    <dd>
                        <asp:TextBox ID="txtTemplatepath" runat="server" CssClass="int" Width="250"></asp:TextBox>
                    </dd>
                    <dd class="t">相对于模板文件夹</dd>
                </dl>
                <dl>
                    <dt>关键字长度：</dt>
                    <dd>
                        <asp:TextBox ID="txtKeywordlengthMin" runat="server" CssClass="int" Width="40"></asp:TextBox>
                        至
                        <asp:TextBox ID="txtKeywordlengthMax" runat="server" CssClass="int" Width="40"></asp:TextBox>
                    </dd>
                    
                </dl>
                 <dl>
                    <dt>搜索模式：</dt>
                    <dd>
                        <asp:DropDownList ID="txtSearchMode" runat="server">
                            <asp:ListItem Value="1">标题</asp:ListItem>
                            <asp:ListItem Value="2">标题/子标题</asp:ListItem>
                            <asp:ListItem Value="3">标题/子标题/关键字</asp:ListItem>
                            <asp:ListItem Value="4">标题/子标题/描述简介</asp:ListItem>
                            <asp:ListItem Value="5">标题/子标题/描述简介/内容</asp:ListItem>
                        </asp:DropDownList>
                    </dd>
                </dl>
                <dl>
                    <dt>&nbsp;</dt>
                    <dd>
                        <asp:Button ID="Button_Submit" runat="server" Text="提 交" CssClass="btnbig"
                            OnClick="Button_Submit_Click" /></dd>
                </dl>
            </div>
        </div>
    </form>
</body>
</html>
