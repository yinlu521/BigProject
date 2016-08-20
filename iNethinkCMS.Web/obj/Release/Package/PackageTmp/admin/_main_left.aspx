<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_main_left.aspx.cs" Inherits="iNethinkCMS.Web.admin._main_left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="skin/js/command.js"></script>
</head>
<body class="leftbody" style="overflow-x: hidden;">
    <form id="form_left" runat="server">
    <div class="lefttop">
        <%= vMenuName %>
    </div>
    <div class="leftmenu">
        <%= vMenuCon %>
    </div>
    </form>
</body>
</html>
