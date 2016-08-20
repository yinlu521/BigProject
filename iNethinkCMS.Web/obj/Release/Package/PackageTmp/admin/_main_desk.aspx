<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_main_desk.aspx.cs" Inherits="iNethinkCMS.Web.admin._main_desk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="skin/js/command.js"></script>
</head>
<body>
    <form id="form_main_desk" runat="server">
    <div class="maintop">
        当前位置：管理首页</div>
    <div class="main">
        <table width="100%" class="tablist">
            <tr class="trbg">
                <td colspan="2" class="tdleft" style="padding-left: 10px;">
                    欢迎进入 iNethinCMS 管理平台
                </td>
            </tr>
            <tr>
                <td width="80">
                    友情提示：
                </td>
                <td class="tdleft" id="sInfo_A" runat="server">
                </td>
            </tr>
            <tr>
                <td width="80">
                    系统版本：
                </td>
                <td class="tdleft" id="sInfo_B" runat="server">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="80">
                    官方网址：
                </td>
                <td class="tdleft" id="sInfo_C" runat="server">
                </td>
            </tr>
            <tr>
                <td width="80">
                    Email：
                </td>
                <td class="tdleft" id="sInfo_D" runat="server">
                </td>
            </tr>
            <tr>
                <td width="80">
                    联系QQ：</td>
                <td class="tdleft" id="sInfo_E" runat="server">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
