<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="iNethinkCMS.Web.install.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>iNethinkCMS初始化安装</title>
    <link href="install.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form_install_index" runat="server">
        <div class="top">
            <span>iNethinkCMS安装</span><img src="/admin/skin/images/logo.gif" />
        </div>
        <div class="wp">
            <div class="main" runat="server" id="ErrorID"></div>
            <div class="main" runat="server" id="MainID">
                <div class="switchs">
                    &nbsp;请填写数据库信息
                </div>
                <div class="info">
                    <dl>
                        <dt>SQL服务器地址：</dt>
                        <dd>
                            <asp:TextBox ID="txtDataBaseServer" runat="server" CssClass="int"></asp:TextBox></dd>
                        <dd
                            class="t">一般无需修改</dd>
                    </dl>
                    <dl>
                        <dt>SQL服务器端口：</dt>
                        <dd>
                            <asp:TextBox ID="txtDataBasePort" runat="server" CssClass="int"></asp:TextBox></dd>
                        <dd
                            class="t">一般无需修改.如连接失败,请尝试将端口设置为:0</dd>
                    </dl>
                    <dl>
                        <dt>数据库名：</dt>
                        <dd>
                            <asp:TextBox ID="txtDataBaseName" runat="server" CssClass="int"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>数据库帐号：</dt>
                        <dd>
                            <asp:TextBox ID="txtDataBaseUser" runat="server" CssClass="int"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>数据库密码：</dt>
                        <dd>
                            <asp:TextBox ID="txtDataBasePass" runat="server" CssClass="int"></asp:TextBox></dd>
                    </dl>
                    <dl style="margin-bottom: 20px;">
                        <dt>&nbsp;</dt>
                        <dd>
                            <asp:Button
                                ID="Button_StartInstall" runat="server" Text="开始安装" CssClass="btnbig"
                                OnClick="Button_StartInstall_Click" /></dd>
                    </dl>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
