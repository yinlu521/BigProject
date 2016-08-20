<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_cache.aspx.cs" Inherits="iNethinkCMS.Web.admin.sys.sys_cache" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../skin/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../skin/js/jquery.min.js"></script>
    <script type="text/javascript" src="../skin/js/command.js"></script>
</head>
<body>
    <form id="form_sys_cache" runat="server">
    <div class="maintop" id="navInfoID" runat="server">
    </div>
    <div class="main_toptable">
        <table width="100%">
            <tr>
                <td>
                    <div class="oper">
                        <a href="?">刷新</a><a href="?act=clearcache">清空缓存</a></div>
                </td>

            </tr>
        </table>
    </div>
    <div class="main" id="mainID" runat="server">
        <table width="100%" class="tablist">
            <tr class="trbg">
                <td width="80">
                    序号
                </td>
                <td>
                    缓存键值
                </td>
                <td width="200">
                    缓存大小(字节)
                </td>
                
            </tr>
            <tr id="iNoInfo" runat="server">
                <td colspan="7">
                    暂无任何数据
                </td>
            </tr>
            <asp:Repeater ID="Repeater" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                           <%#DataBinder.Eval(Container.DataItem,"ID")%>
                        </td>
                        <td>
                           <%#DataBinder.Eval(Container.DataItem,"CacheName")%>
                        </td>
                        <td>
                        <%#DataBinder.Eval(Container.DataItem,"CacheInfo")%>
                        </td>
                        
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
