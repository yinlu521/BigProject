<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="iNethinkCMS.Web.admin.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="skin/js/command.js"></script>
</head>
<body style="background-color: rgb(244, 247, 249);">
    <form id="frm" runat="server">
    <div class="login">
        <p class="pl">
        </p>
        <div class="l">
        </div>
        <div class="c">
            <div class="to">
                <span class="tol"><a href="http://cms.inethink.com" target="_blank"></a></span><span
                    class="tor">轻松建站</span></div>
            <div class="in">
                <dl>
                    <dt>用户名</dt><dd><asp:TextBox ID="txtUserName" runat="server" Width="150"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>密&nbsp;&nbsp;码</dt><dd><asp:TextBox ID="txtUserPass" runat="server" Width="150" TextMode="Password"></asp:TextBox></dd></dl>
                <dl>
                    <dt>验证码</dt><dd><asp:TextBox ID="txtVerificationCode" runat="server" Width="80" MaxLength="4"></asp:TextBox></dd><dd
                        class="e"><img src="../inc/verificationcode.aspx" onclick="this.src='../inc/verificationcode.aspx?r=' + Math.random();"
                            alt="点击刷新验证码" style="cursor: pointer;" /></dd><dd class="e"></dd></dl>
            </div>
            <div class="su">
                <span>
                    <asp:Button ID="Button_Login" runat="server" Text="" class="go" OnClick="Button_Login_Click" />
                </span><a href="http://Cms.iNethink.com" target="_blank">http://Cms.iNethink.com</a></div>
        </div>
        <div class="r">
        </div>
        <p class="pr">
        </p>
    </div>
    </form>
</body>
</html>
