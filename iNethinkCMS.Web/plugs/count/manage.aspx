<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="iNethinkCMS.Web.plugs.count.manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../admin/skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../admin/skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../admin/skin/js/command.js"></script>
</head>
<body>
    <form id="form_count_manage" runat="server">
        <div class="maintop" runat="server">
            当前位置：浏览统计插件功能设置
        </div>
        <div class="main" runat="server">
            <div class="info">
                <dl style="border: 0;">
                    <dt>开启浏览统计：</dt>
                    <dd>
                        <asp:DropDownList ID="txtState" runat="server">
                            <asp:ListItem Value="0">停用</asp:ListItem>
                            <asp:ListItem Value="1">启用</asp:ListItem>
                        </asp:DropDownList>
                    </dd>
                </dl>
                <dl>
                    <dt>显示浏览数据：</dt>
                    <dd>
                        <asp:DropDownList ID="txtShow" runat="server">
                            <asp:ListItem Value="0">不显示</asp:ListItem>
                            <asp:ListItem Value="1">显示</asp:ListItem>
                        </asp:DropDownList></dd>
                </dl>
                <dl>
                    <dt>使用说明：</dt>
                    <dd>在内容模板页面的相应位置添加如下代码：
                        <br />
                        <asp:TextBox ID="txtCode" runat="server" Height="35px" ReadOnly="True" TextMode="MultiLine" Width="650px" CssClass="int"><script language="JavaScript" type="text/javascript" src="/plugs/count/count.aspx?id={field:id}"></script></asp:TextBox>
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
