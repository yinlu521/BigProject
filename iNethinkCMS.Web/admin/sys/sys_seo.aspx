<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_seo.aspx.cs" Inherits="iNethinkCMS.Web.admin.sys.seo_settings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_seo_setting" runat="server">
    <div class="maintop">
        当前位置：SEO优化</div>
    <div class="main">
        <div class="info">
            <dl style="border: 0px">
                <dt>获取文章描述：</dt><dd><asp:DropDownList ID="txtAutoDescription" runat="server">
                    <asp:ListItem Value="1" Text="自动"></asp:ListItem>
                    <asp:ListItem Value="0" Text="手动"></asp:ListItem>
                </asp:DropDownList>
                </dd>
            </dl>
            <dl>
                <dt>标题附加字：</dt><dd><asp:TextBox ID="txtSeoTitle" runat="server" CssClass="int" Width="300px"></asp:TextBox></dd><dd
                    class="t">{sys:seotitle}</dd></dl>
            <dl>
                <dt>首页关键字：</dt><dd><asp:TextBox ID="txtIndexKeywords" runat="server" CssClass="int"
                    Height="50px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                </dd>
                <dd class="t">
                    {sys:keywords}</dd>
            </dl>
            <dl>
                <dt>首页描述：</dt><dd><asp:TextBox ID="txtIndexDescription" runat="server" CssClass="int"
                    Height="50px" TextMode="MultiLine" Width="500px"></asp:TextBox></dd><dd
                            class="t">{sys:description}</dd></dl>
            <dl>
                <dt>&nbsp;</dt><dd>
                    <asp:Button ID="Submit_Seo_Setting" runat="server" Text="提 交" CssClass="btnbig" OnClick="Submit_Seo_Setting_Click" />
                </dd>
            </dl>
        </div>
    </div>
    </form>
</body>
</html>
