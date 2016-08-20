<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="iNethinkCMS.Web.admin.main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="skin/css/css.css" rel="stylesheet" type="text/css" />
</head>
<frameset rows="75,*,30" cols="*" frameborder="no" border="0" framespacing="0">
  <frame src="_main_top.aspx" name="top" scrolling="No" noresize="noresize" id="top" />
  <frameset cols="150,*" frameborder="no" border="0" framespacing="0">
    <frame src="_main_left.aspx" name="left" scrolling="auto" noresize="noresize" id="left" />
    <frame src="_main_desk.aspx" name="main" id="main" />
  </frameset>
  <frame src="_main_bottom.aspx" name="bottom" scrolling="No" noresize="noresize" id="bottom" />
</frameset>
<noframes>
    <body>
        很抱歉，您使用的浏览器不支援框架功能，请升级您的浏览器。
    </body>
</noframes>
</html>
