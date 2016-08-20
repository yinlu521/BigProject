<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="extend_plugs.aspx.cs" Inherits="iNethinkCMS.Web.admin.extend.extend_plugs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_extend_plugs" runat="server">
    <div class="maintop" id="navInfoID" runat="server">
    </div>
    <div class="main" id="mainID" runat="server">
        <table width="100%" class="tablist">
            <tr class="trbg">
                <td width="60">
                    编号
                </td>
                <td width="160">
                    插件名称
                </td>
                <td>
                    插件说明
                </td>
                <td width="120">
                    插件版本
                </td>
                <td width="80">
                    启用状态
                </td>
                <td width="80">
                    操作
                </td>
            </tr>
            <tr id="iNoInfo" runat="server">
                <td colspan="6">
                    暂无任何数据
                </td>
            </tr>
            <asp:Repeater ID="Repeater" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem,"i")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "PlugsName")%>
                        </td>
                        <td>
                            <div style="text-align: left;">
                                <%#DataBinder.Eval(Container.DataItem, "PlugsDescription")%></div>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "PlugsVer")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "PlugsState")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem,"ManageUrl")%>
                                
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
