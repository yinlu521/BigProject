<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNum.aspx.cs" Inherits="iNethinkCMS.Web.AddNum" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
		<meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
		<meta name="apple-touch-fullscreen" content="yes">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<meta name="format-detection" content="telephone=no">
		<link href="css/style.css" rel="stylesheet" type="text/css" />
		<link href="css/swiper3.08.min.css" rel="stylesheet" type="text/css" />
		<link rel="apple-touch-icon-precomposed" href="logo.png" />
        <script src="js/jquery-1.9.1.min.js"></script>
        <script src="js/layer/layer.js"></script>
        
    <style>
        .tb
        {
            width:100%;
           
            }
        .tb tr
        {
            height:60px;
            }
        .td1
        {
            width:45%;
            text-align:right;
            }
         .td2
         {
             width:60%
             }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <table class="tb">

        <tr>
            <td class="td1">类型：</td>
            <td  class="td2">
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="交通费" Text="交通费"></asp:ListItem>
                    <asp:ListItem Value="餐费" Text="餐费"></asp:ListItem>
                    <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                 </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td1">金额：</td>
            <td   class="td2">
               <input id="num" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td1">操作者：</td>
            <td  class="td2">
               <asp:CheckBox ID="CheckBox1" ValidationGroup="pl" runat="server" Text="陆文婷"  />
               <asp:CheckBox ID="CheckBox2" ValidationGroup="pl" runat="server" Text="尹露"  />
            </td>
        </tr>
        <tr>
            <td class="td1">收支：</td>
            <td  class="td2">
                <asp:RadioButton ID="RadioButton1" Text="收入"  GroupName="IO" runat="server" />
                <asp:RadioButton ID="RadioButton2" Text="支出"  GroupName="IO" runat="server" />

            </td>
        </tr>
        <tr>
            <td colspan="2" style=" text-align:center">
             <textarea id="Textarea1" style="width:200px;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td colspan="2" style=" text-align:center">
                <asp:Button ID="Button1" runat="server" Text="确认" onclick="Button1_Click" />
            </td>
        </tr>        
        </table>

    </form>
</body>
</html>
